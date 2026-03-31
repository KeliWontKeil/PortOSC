namespace PortOSC.Services;

public sealed class SendStringPresetItem
{
    public string Content { get; set; } = string.Empty;
    public string Tag { get; set; } = string.Empty;
}

public sealed class SendStringPresetDocument
{
    public List<SendStringPresetItem> Items { get; set; } = [];
}
