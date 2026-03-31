using System.Text.Json;

namespace Tools
{
    public static class SafeOperateTools
    {
        public static void SafeInvoke<T>(T control, Action<T> action) where T : Control
        {
            if (control == null || control.IsDisposed) return;

            if (control.InvokeRequired && control.IsHandleCreated)
            {
                control.BeginInvoke(new Action(() => action(control)));
            }
            else if (control.InvokeRequired)
            {
                return;
            }
            else
            {
                action(control);
            }
        }
    }

    public static class SaveConfigTools
    {
        private static readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

        public static void SaveConfig<T>(T cfg, string FileName)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName);
            string json = JsonSerializer.Serialize(cfg, _jsonOptions);
            File.WriteAllText(filePath, json);
        }

        public static T? LoadConfig<T>(string FileName)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName);
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    return JsonSerializer.Deserialize<T>(json);
                }
                return default;
            }
            catch
            {
                return default;
            }
        }
    }

    public static class ShowMassageTools
    {
        public enum LogType
        {
            Info,
            Success,
            Warning,
            Error,
        }

        public static void ReportMassage(RichTextBox box, string message, LogType type = LogType.Info)
        {
            if (box == null || box.IsDisposed) return;

            SafeOperateTools.SafeInvoke(box, rtb =>
            {
                Color color;
                string prefix;

                switch (type)
                {
                    case LogType.Success:
                        color = Color.Green;
                        prefix = "成功";
                        break;
                    case LogType.Warning:
                        color = Color.Orange;
                        prefix = "警告";
                        break;
                    case LogType.Error:
                        color = Color.Red;
                        prefix = "错误";
                        break;
                    default:
                        color = Color.Black;
                        prefix = "信息";
                        break;
                }

                string time = DateTime.Now.ToString("HH:mm:ss");
                string line = string.Format("{0} {1}：{2}{3}",
                                            time, prefix, message, Environment.NewLine);

                rtb.SelectionStart = rtb.TextLength;
                rtb.SelectionLength = 0;
                rtb.SelectionColor = color;
                rtb.AppendText(line);
                rtb.ScrollToCaret();

            });
        }
    }

    public class ChangeEventValue<T>(T initialValue)
    {
        private T _value = initialValue;

        public event EventHandler<T>? ValueChangedOccured;

        public T Value
        {
            get => _value;
            set
            {
                if (!Equals(_value, value))
                {
                    _value = value;
                    ValueChangedOccured?.Invoke(this, _value);
                }
            }
        }
    }

}
