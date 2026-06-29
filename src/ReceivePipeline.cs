using System.Text;
using System.Globalization;

namespace PortOSC.Services;

public readonly record struct ReceivePipelineOptions(
    bool StopReceive,
    bool EnableOsc,
    bool ShowHex,
    bool AppendNewLine,
    int ChannelLength,
    int FrameCapacity,
    string HeadToken,
    string EndToken);

public sealed class ReceivePipelineResult
{
    public required byte[] ForwardData { get; init; }
    public string DisplayText { get; set; } = string.Empty;
    public bool AppendNewLine { get; init; }
    public List<double[]> OscFrames { get; } = [];
}

public sealed class ReceivePipeline
{
    private readonly List<string> _oscReceiveBuffer = [];
    private readonly object _oscBufferLock = new();

    public ReceivePipelineResult Process(byte[] sourceData, ReceivePipelineOptions options)
    {
        ArgumentNullException.ThrowIfNull(sourceData);

        var forwardData = sourceData.ToArray();
        if (forwardData.Length == 0)
        {
            return new ReceivePipelineResult
            {
                ForwardData = forwardData,
                AppendNewLine = false
            };
        }

        if (options.StopReceive)
        {
            return new ReceivePipelineResult
            {
                ForwardData = forwardData,
                AppendNewLine = false
            };
        }

        var result = new ReceivePipelineResult
        {
            ForwardData = forwardData,
            AppendNewLine = options.AppendNewLine
        };

        if (options.EnableOsc)
        {
            ParseOscFrames(forwardData, options, result.OscFrames);
        }

        result.DisplayText = BuildDisplayText(forwardData, options.ShowHex);

        return result;
    }

    private static string BuildDisplayText(byte[] forwardData, bool showHex)
        => showHex ? string.Join(" ", forwardData.Select(b => b.ToString("X2"))) + " " : Encoding.ASCII.GetString(forwardData);

    private void ParseOscFrames(byte[] receivedBytes, ReceivePipelineOptions options, List<double[]> outputFrames)
    {
        if (string.IsNullOrEmpty(options.HeadToken) || string.IsNullOrEmpty(options.EndToken))
        {
            return;
        }

        // Use Latin-1 encoding to preserve all byte values (0-255) for frame token matching.
        // ASCII encoding would map bytes > 0x7F to '?', causing frame header/footer match failures.
        var receiveText = Encoding.GetEncoding("iso-8859-1").GetString(receivedBytes);
        var parts = receiveText.Split([' '], StringSplitOptions.RemoveEmptyEntries);

        lock (_oscBufferLock)
        {
            _oscReceiveBuffer.AddRange(parts);

            // FrameCapacity: maximum number of complete frames to buffer before truncation.
            // This prevents unbounded growth while preserving frame integrity.
            var frameTokenCount = options.ChannelLength + 2; // head + N data + end
            var maxCount = frameTokenCount * options.FrameCapacity;
            if (_oscReceiveBuffer.Count > maxCount)
            {
                var excess = _oscReceiveBuffer.Count - maxCount;
                // Align truncation to the closest frame header to avoid cutting a frame in half.
                var headPos = _oscReceiveBuffer.LastIndexOf(options.HeadToken, excess - 1);
                _oscReceiveBuffer.RemoveRange(0, headPos >= 0 ? headPos : excess);
            }

            while (TryExtractFrame(options.HeadToken, options.EndToken, options.ChannelLength, out var frameTokens))
            {
                if (TryConvertFrameTokens(frameTokens, out var frame))
                {
                    outputFrames.Add(frame);
                }
            }
        }
    }

    private static bool TryConvertFrameTokens(List<string> frameTokens, out double[] frame)
    {
        frame = [];

        var values = new double[frameTokens.Count];
        for (var i = 0; i < frameTokens.Count; i++)
        {
            if (!double.TryParse(frameTokens[i], NumberStyles.Float, CultureInfo.InvariantCulture, out values[i]))
            {
                return false;
            }
        }

        frame = values;
        return true;
    }

    private bool TryExtractFrame(string headToken, string endToken, int channelLength, out List<string> frameTokens)
    {
        frameTokens = [];

        var headIndex = _oscReceiveBuffer.IndexOf(headToken);
        if (headIndex < 0)
        {
            return false;
        }

        var endIndex = _oscReceiveBuffer.IndexOf(endToken, headIndex + 1);
        if (endIndex < 0)
        {
            return false;
        }

        var start = headIndex + 1;
        var count = endIndex - start;
        if (count <= 0)
        {
            return false;
        }

        // Token count must match exactly the number of data channels.
        // A mismatch indicates a corrupted/truncated/interleaved frame — discard it.
        if (count != channelLength)
        {
            _oscReceiveBuffer.RemoveRange(headIndex, endIndex - headIndex + 1);
            return false;
        }

        frameTokens = _oscReceiveBuffer.GetRange(start, count);
        _oscReceiveBuffer.RemoveRange(headIndex, endIndex - headIndex + 1);

        return true;
    }
}