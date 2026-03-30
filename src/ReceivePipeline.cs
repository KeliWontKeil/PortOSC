using System.Text;

namespace PortOSC.Services;

public readonly record struct ReceivePipelineOptions(
    bool StopReceive,
    bool EnableOsc,
    bool ShowHex,
    bool AppendNewLine,
    int ChannelLength,
    int AdditionalBufferLength,
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

    public ReceivePipelineResult Process(byte[] sourceData, ReceivePipelineOptions options)
    {
        ArgumentNullException.ThrowIfNull(sourceData);

        var forwardData = sourceData.ToArray();
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

        result.DisplayText = options.ShowHex
            ? string.Join(" ", forwardData.Select(b => b.ToString("X2"))) + " "
            : Encoding.ASCII.GetString(forwardData);

        return result;
    }

    private void ParseOscFrames(byte[] receivedBytes, ReceivePipelineOptions options, List<double[]> outputFrames)
    {
        if (string.IsNullOrEmpty(options.HeadToken) || string.IsNullOrEmpty(options.EndToken))
        {
            return;
        }

        var receiveText = Encoding.ASCII.GetString(receivedBytes);
        var parts = receiveText.Split([' '], StringSplitOptions.RemoveEmptyEntries);
        _oscReceiveBuffer.AddRange(parts);

        var maxCount = (options.ChannelLength + 2) * 2 + options.AdditionalBufferLength;
        if (_oscReceiveBuffer.Count > maxCount)
        {
            _oscReceiveBuffer.RemoveRange(0, _oscReceiveBuffer.Count - maxCount);
        }

        while (TryExtractFrame(options.HeadToken, options.EndToken, out var frameTokens))
        {
            try
            {
                outputFrames.Add([.. frameTokens.Select(Convert.ToDouble)]);
            }
            catch (FormatException)
            {
                continue;
            }
            catch (OverflowException)
            {
                continue;
            }
        }
    }

    private bool TryExtractFrame(string headToken, string endToken, out List<string> frameTokens)
    {
        frameTokens = [];

        var headIndex = _oscReceiveBuffer.LastIndexOf(headToken);
        var endIndex = _oscReceiveBuffer.LastIndexOf(endToken);

        if (headIndex < 0 || endIndex < 0 || headIndex >= endIndex)
        {
            return false;
        }

        var start = headIndex + 1;
        var count = endIndex - start;
        if (count <= 0)
        {
            return false;
        }

        frameTokens = _oscReceiveBuffer.GetRange(start, count);
        _oscReceiveBuffer.RemoveRange(0, endIndex + 1);

        return true;
    }
}
