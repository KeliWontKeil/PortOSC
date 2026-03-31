namespace PortOSC;

partial class Form_SendStringLibrary
{
    private System.ComponentModel.IContainer components = null;
    private FlowLayoutPanel RowsFlowLayoutPanel;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        RowsFlowLayoutPanel = new FlowLayoutPanel();
        SaveButton = new Button();
        LoadButton = new Button();
        AddButton = new Button();
        TagTextBox = new TextBox();
        TagLabel = new Label();
        ContentTextBox = new TextBox();
        ContentLabel = new Label();
        SuspendLayout();
        // 
        // RowsFlowLayoutPanel
        // 
        RowsFlowLayoutPanel.AutoScroll = true;
        RowsFlowLayoutPanel.BorderStyle = BorderStyle.Fixed3D;
        RowsFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
        RowsFlowLayoutPanel.Location = new Point(8, 48);
        RowsFlowLayoutPanel.Margin = new Padding(0);
        RowsFlowLayoutPanel.Name = "RowsFlowLayoutPanel";
        RowsFlowLayoutPanel.Padding = new Padding(8);
        RowsFlowLayoutPanel.Size = new Size(908, 556);
        RowsFlowLayoutPanel.TabIndex = 1;
        RowsFlowLayoutPanel.WrapContents = false;
        // 
        // SaveButton
        // 
        SaveButton.AutoSize = true;
        SaveButton.Location = new Point(827, 11);
        SaveButton.Margin = new Padding(3, 3, 12, 3);
        SaveButton.Name = "SaveButton";
        SaveButton.Size = new Size(83, 30);
        SaveButton.TabIndex = 6;
        SaveButton.Text = "保存配置";
        SaveButton.UseVisualStyleBackColor = true;
        SaveButton.Click += SaveButton_Click;
        // 
        // LoadButton
        // 
        LoadButton.AutoSize = true;
        LoadButton.Location = new Point(738, 11);
        LoadButton.Margin = new Padding(3, 3, 12, 3);
        LoadButton.Name = "LoadButton";
        LoadButton.Size = new Size(83, 30);
        LoadButton.TabIndex = 5;
        LoadButton.Text = "加载配置";
        LoadButton.UseVisualStyleBackColor = true;
        LoadButton.Click += LoadButton_Click;
        // 
        // AddButton
        // 
        AddButton.AutoSize = true;
        AddButton.Location = new Point(627, 11);
        AddButton.Margin = new Padding(3, 3, 12, 3);
        AddButton.Name = "AddButton";
        AddButton.Size = new Size(73, 30);
        AddButton.TabIndex = 4;
        AddButton.Text = "添加";
        AddButton.UseVisualStyleBackColor = true;
        AddButton.Click += AddButton_Click;
        // 
        // TagTextBox
        // 
        TagTextBox.Location = new Point(469, 12);
        TagTextBox.Margin = new Padding(3, 3, 12, 3);
        TagTextBox.Name = "TagTextBox";
        TagTextBox.Size = new Size(150, 27);
        TagTextBox.TabIndex = 3;
        // 
        // TagLabel
        // 
        TagLabel.AutoSize = true;
        TagLabel.Location = new Point(422, 16);
        TagLabel.Margin = new Padding(3, 6, 3, 3);
        TagLabel.Name = "TagLabel";
        TagLabel.Size = new Size(41, 20);
        TagLabel.TabIndex = 2;
        TagLabel.Text = "标签";
        // 
        // ContentTextBox
        // 
        ContentTextBox.Location = new Point(123, 12);
        ContentTextBox.Margin = new Padding(3, 3, 12, 3);
        ContentTextBox.Name = "ContentTextBox";
        ContentTextBox.Size = new Size(293, 27);
        ContentTextBox.TabIndex = 1;
        // 
        // ContentLabel
        // 
        ContentLabel.AutoSize = true;
        ContentLabel.Location = new Point(12, 15);
        ContentLabel.Margin = new Padding(3, 6, 3, 3);
        ContentLabel.Name = "ContentLabel";
        ContentLabel.Size = new Size(105, 20);
        ContentLabel.TabIndex = 0;
        ContentLabel.Text = "待发送字符串";
        // 
        // Form_SendStringLibrary
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(925, 613);
        Controls.Add(SaveButton);
        Controls.Add(LoadButton);
        Controls.Add(AddButton);
        Controls.Add(TagTextBox);
        Controls.Add(TagLabel);
        Controls.Add(ContentTextBox);
        Controls.Add(ContentLabel);
        Controls.Add(RowsFlowLayoutPanel);
        Font = new Font("Segoe UI", 9F);
        Name = "Form_SendStringLibrary";
        Text = "发送字符串库";
        ResumeLayout(false);
        PerformLayout();
    }

    private Button SaveButton;
    private Button LoadButton;
    private Button AddButton;
    private TextBox TagTextBox;
    private Label TagLabel;
    private TextBox ContentTextBox;
    private Label ContentLabel;
}
