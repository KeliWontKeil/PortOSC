using System.Text;
using System.Text.Json;
using PortOSC.Services;

namespace PortOSC;

public partial class Form_SendStringLibrary : Form
{
    private const string DefaultPresetFileName = "SendStringLibrary.json";
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    private readonly Action<string> _fillHexText;
    private readonly Action<string> _fillStringText;
    private readonly List<SendStringPresetItem> _items = [];
    private bool _defaultPresetLoaded;

    public Form_SendStringLibrary(Action<string> fillHexText, Action<string> fillStringText)
    {
        ArgumentNullException.ThrowIfNull(fillHexText);
        ArgumentNullException.ThrowIfNull(fillStringText);

        _fillHexText = fillHexText;
        _fillStringText = fillStringText;

        InitializeComponent();
        RowsFlowLayoutPanel.SizeChanged += RowsFlowLayoutPanel_SizeChanged;
        Shown += Form_SendStringLibrary_Shown;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        try
        {
            SaveToDefaultFile();
        }
        catch (Exception ex) when (ex is IOException or JsonException or NotSupportedException or UnauthorizedAccessException)
        {
            MessageBox.Show($"自动保存发送字符串库失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        base.OnFormClosing(e);
    }

    private void Form_SendStringLibrary_Shown(object? sender, EventArgs e)
    {
        if (_defaultPresetLoaded)
        {
            return;
        }

        _defaultPresetLoaded = true;
        LoadDefaultFile();
    }

    private void AddButton_Click(object? sender, EventArgs e)
    {
        var content = ContentTextBox.Text.Trim();
        if (string.IsNullOrWhiteSpace(content))
        {
            MessageBox.Show("待发送字符串不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var tag = TagTextBox.Text.Trim();
        AddItem(new SendStringPresetItem
        {
            Content = content,
            Tag = tag
        });

        ContentTextBox.Clear();
        TagTextBox.Clear();
        ContentTextBox.Focus();
    }

    private void LoadButton_Click(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "发送字符串文件 (*.json)|*.json",
            DefaultExt = ".json",
            Multiselect = false
        };

        if (dialog.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.FileName))
        {
            return;
        }

        try
        {
            LoadFromFile(dialog.FileName);
        }
        catch (Exception ex) when (ex is IOException or JsonException or NotSupportedException or UnauthorizedAccessException)
        {
            MessageBox.Show($"读取发送字符串文件失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void SaveButton_Click(object? sender, EventArgs e)
    {
        using var dialog = new SaveFileDialog
        {
            Filter = "发送字符串文件 (*.json)|*.json",
            DefaultExt = ".json",
            AddExtension = true
        };

        if (dialog.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.FileName))
        {
            return;
        }

        try
        {
            SaveToFile(dialog.FileName);
        }
        catch (Exception ex) when (ex is IOException or JsonException or NotSupportedException or UnauthorizedAccessException)
        {
            MessageBox.Show($"保存发送字符串文件失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void FillHexButton_Click(object? sender, EventArgs e)
    {
        if (sender is not Button button || button.Tag is not SendStringPresetItem item)
        {
            return;
        }

        _fillHexText(BuildHexPayload(item.Content));
    }

    private void FillStringButton_Click(object? sender, EventArgs e)
    {
        if (sender is not Button button || button.Tag is not SendStringPresetItem item)
        {
            return;
        }

        _fillStringText(item.Content);
    }

    private void DeleteButton_Click(object? sender, EventArgs e)
    {
        if (sender is not Button button || button.Tag is not TableLayoutPanel rowPanel || rowPanel.Tag is not SendStringPresetItem item)
        {
            return;
        }

        _items.Remove(item);
        RowsFlowLayoutPanel.Controls.Remove(rowPanel);
        rowPanel.Dispose();
        ResizeRowPanels();
    }

    private void ContentBox_TextChanged(object? sender, EventArgs e)
    {
        if (sender is not TextBox textBox || textBox.Tag is not SendStringPresetItem item)
        {
            return;
        }

        item.Content = textBox.Text;
    }

    private void TagBox_TextChanged(object? sender, EventArgs e)
    {
        if (sender is not TextBox textBox || textBox.Tag is not SendStringPresetItem item)
        {
            return;
        }

        item.Tag = textBox.Text;
    }

    private void AddItem(SendStringPresetItem item)
    {
        _items.Add(item);
        RowsFlowLayoutPanel.Controls.Add(CreateRowPanel(item));
        ResizeRowPanels();
    }

    private void RowsFlowLayoutPanel_SizeChanged(object? sender, EventArgs e)
    {
        ResizeRowPanels();
    }

    private void ResizeRowPanels()
    {
        var width = RowsFlowLayoutPanel.ClientSize.Width - RowsFlowLayoutPanel.Padding.Horizontal - SystemInformation.VerticalScrollBarWidth - 6;
        if (width < 0)
        {
            width = 0;
        }

        foreach (Control control in RowsFlowLayoutPanel.Controls)
        {
            control.Width = width;
        }
    }

    private TableLayoutPanel CreateRowPanel(SendStringPresetItem item)
    {
        var rowPanel = new TableLayoutPanel
        {
            AutoSize = false,
            ColumnCount = 5,
            Height = 36,
            Margin = new Padding(0, 0, 0, 6),
            Padding = new Padding(0),
            RowCount = 1,
            Tag = item
        };
        rowPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rowPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
        rowPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        rowPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        rowPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

        var contentBox = new TextBox
        {
            Text = item.Content,
            Dock = DockStyle.Fill,
            Margin = new Padding(3, 4, 6, 4),
            Tag = item,
            Anchor = AnchorStyles.Left | AnchorStyles.Right
        };
        contentBox.TextChanged += ContentBox_TextChanged;

        var tagBox = new TextBox
        {
            Text = item.Tag,
            Dock = DockStyle.Fill,
            Margin = new Padding(0, 4, 6, 4),
            Tag = item,
            Anchor = AnchorStyles.Left | AnchorStyles.Right
        };
        tagBox.TextChanged += TagBox_TextChanged;

        var fillHexButton = new Button
        {
            Text = "16进制发送",
            AutoSize = true,
            Tag = item,
            Margin = new Padding(0, 3, 6, 3)
        };
        fillHexButton.Click += FillHexButton_Click;

        var fillStringButton = new Button
        {
            Text = "字符串发送",
            AutoSize = true,
            Tag = item,
            Margin = new Padding(0, 3, 6, 3)
        };
        fillStringButton.Click += FillStringButton_Click;

        Button deleteButton = new()
        {
            Text = "删除",
            AutoSize = true,
            Margin = new Padding(0, 3, 0, 3),
            Tag = rowPanel
        };
        deleteButton.Click += DeleteButton_Click;

        rowPanel.Controls.Add(contentBox, 0, 0);
        rowPanel.Controls.Add(tagBox, 1, 0);
        rowPanel.Controls.Add(fillHexButton, 2, 0);
        rowPanel.Controls.Add(fillStringButton, 3, 0);
        rowPanel.Controls.Add(deleteButton, 4, 0);

        return rowPanel;
    }

    private void LoadFromFile(string fileName)
    {
        var document = ReadDocument(fileName);

        RowsFlowLayoutPanel.SuspendLayout();

        try
        {
            foreach (Control control in RowsFlowLayoutPanel.Controls)
            {
                control.Dispose();
            }

            RowsFlowLayoutPanel.Controls.Clear();
            _items.Clear();

            foreach (var item in document.Items)
            {
                AddItem(new SendStringPresetItem
                {
                    Content = item.Content ?? string.Empty,
                    Tag = item.Tag ?? string.Empty
                });
            }
        }
        finally
        {
            RowsFlowLayoutPanel.ResumeLayout(true);
        }
    }

    private void LoadDefaultFile()
    {
        var fileName = GetDefaultFilePath();
        if (!File.Exists(fileName))
        {
            return;
        }

        try
        {
            LoadFromFile(fileName);
        }
        catch (Exception ex) when (ex is IOException or JsonException or NotSupportedException or UnauthorizedAccessException)
        {
            MessageBox.Show($"加载字符串库失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static SendStringPresetDocument ReadDocument(string fileName)
    {
        var json = File.ReadAllText(fileName, Encoding.UTF8);
        return JsonSerializer.Deserialize<SendStringPresetDocument>(json, JsonOptions)
            ?? throw new JsonException("文件内容无效");
    }

    private static string GetDefaultFilePath()
        => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DefaultPresetFileName);

    private void SaveToDefaultFile()
    {
        SaveToFile(GetDefaultFilePath());
    }

    private void SaveToFile(string fileName)
    {
        var document = new SendStringPresetDocument
        {
            Items = [.. _items.Select(item => new SendStringPresetItem
            {
                Content = item.Content,
                Tag = item.Tag
            })]
        };

        var json = JsonSerializer.Serialize(document, JsonOptions);
        File.WriteAllText(fileName, json, Encoding.UTF8);
    }

    private static string BuildHexPayload(string text)
        => string.Join(' ', Encoding.UTF8.GetBytes(text).Select(b => b.ToString("X2")));
}
