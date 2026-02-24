using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace _NumberTextBox
{
    /// <summary>
    /// 数值输入框
    /// </summary>
    public class NumberTextBox : TextBox
    {
        /// <summary>
        /// 输入数值类型
        /// </summary>
        private NumberType numberType;

        /// <summary>
        /// 输入小数点，默认2位
        /// </summary>
        private int decimalPointNumber;

        /// <summary>
        /// 允许输入的最小值，空值即代表当前类型的最小值
        /// </summary>
        private string? minValue;

        /// <summary>
        /// 允许输入的最大值，空值即代表当前类型的最大值
        /// </summary>
        private string? maxValue;

        [Browsable(true)]
        [Category("NumberTextBox"), Description("指定控件数值输入的最小值"), DisplayName("最小值")]
        public string? MinValue
        {
            get
            {
                return minValue;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    minValue = "";
                }
                else if (numberType == NumberType.Integer)
                {
                    if (int.TryParse(value, out int result))
                    {
                        minValue = result.ToString();
                    }
                    else
                    {
                        minValue = "";
                    }

                }
                else if (numberType == NumberType.Long)
                {
                    if (long.TryParse(value, out long result))
                    {
                        minValue = result.ToString();
                    }
                    else
                    {
                        minValue = "";
                    }

                }
                else if (numberType == NumberType.Float)
                {
                    if (float.TryParse(value, out float result))
                    {
                        minValue = result.ToString("F" + decimalPointNumber);
                    }
                    else
                    {
                        minValue = "";
                    }

                }
                else if (numberType == NumberType.Double)
                {
                    if (double.TryParse(value, out double result))
                    {
                        minValue = result.ToString("F" + decimalPointNumber);
                    }
                    else
                    {
                        minValue = "";
                    }
                }
            }
        }

        [Browsable(true)]
        [Category("NumberTextBox"), Description("指定控件数值输入的最大值"), DisplayName("最大值")]
        public string? MaxValue
        {
            get
            {
                return maxValue;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    maxValue = "";
                }
                else if (numberType == NumberType.Integer)
                {
                    if (int.TryParse(value, out int result))
                    {
                        maxValue = result.ToString();
                    }
                    else
                    {
                        maxValue = "";
                    }

                }
                else if (numberType == NumberType.Long)
                {
                    if (long.TryParse(value, out long result))
                    {
                        maxValue = result.ToString();
                    }
                    else
                    {
                        maxValue = "";
                    }

                }
                else if (numberType == NumberType.Float)
                {
                    if (float.TryParse(value, out float result))
                    {
                        maxValue = result.ToString("F" + decimalPointNumber);
                    }
                    else
                    {
                        maxValue = "";
                    }

                }
                else if (numberType == NumberType.Double)
                {
                    if (double.TryParse(value, out double result))
                    {
                        maxValue = result.ToString("F" + decimalPointNumber);
                    }
                    else
                    {
                        maxValue = "";
                    }
                }
            }
        }

        [Browsable(true)]
        [Category("NumberTextBox"), Description("指定控件数值输入类型"), DisplayName("数值类型")]
        public NumberType NumberType { get => numberType; set => numberType = value; }

        [Browsable(true)]
        [Category("NumberTextBox"), Description("当指定输入数值类型为小数时保留小数点位数"), DisplayName("小数点位数")]
        //[DefaultValue(2)]
        public int DecimalPointNumber { get => decimalPointNumber; set => decimalPointNumber = value; }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (string.IsNullOrWhiteSpace(Text) || Text == "-")//输入负值
            {
                return;
            }
            switch (numberType)
            {
                case NumberType.Integer:
                    if (!int.TryParse(Text, out _))
                    {
                        Text = Text[..^1];
                        SelectionStart = Text.Length;
                    }
                    break;
                case NumberType.Long:
                    if (!long.TryParse(Text, out _))
                    {
                        Text = Text[..^1];
                        SelectionStart = Text.Length;
                    }
                    break;
                case NumberType.Float:
                    if (!float.TryParse(Text, out _))
                    {
                        Text = Text[..^1];
                        SelectionStart = Text.Length;
                    }
                    break;
                case NumberType.Double:
                    if (!double.TryParse(Text, out _))
                    {
                        Text = Text[..^1];
                        SelectionStart = Text.Length;
                    }
                    break;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Enter)
            {
                VerifyThenFormatValue();
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            VerifyThenFormatValue();

        }

        private void VerifyThenFormatValue()
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                return;
            }
            if (numberType == NumberType.Float)
            {
                Text = float.Parse(Text).ToString("F" + decimalPointNumber).ToString();
            }
            else if (numberType == NumberType.Double)
            {
                Text = double.Parse(Text).ToString("F" + decimalPointNumber).ToString();
            }
            if (!string.IsNullOrWhiteSpace(minValue))
            {
                if (numberType == NumberType.Integer)
                {
                    if (int.Parse(Text) < int.Parse(minValue))
                    {
                        Text = minValue;
                    }
                }
                else if (numberType == NumberType.Long)
                {
                    if (long.Parse(Text) < long.Parse(minValue))
                    {
                        Text = minValue;
                    }
                }
                else if (numberType == NumberType.Float)
                {
                    if (float.Parse(Text) < float.Parse(minValue))
                    {
                        Text = minValue;
                    }
                }
                else if (numberType == NumberType.Double)
                {
                    if (double.Parse(Text) < double.Parse(minValue))
                    {
                        Text = minValue;
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(maxValue))
            {
                if (numberType == NumberType.Integer)
                {
                    if (int.Parse(Text) > int.Parse(maxValue))
                    {
                        Text = maxValue;
                    }
                }
                else if (numberType == NumberType.Long)
                {
                    if (long.Parse(Text) > long.Parse(maxValue))
                    {
                        Text = maxValue;
                    }
                }
                else if (numberType == NumberType.Float)
                {
                    if (float.Parse(Text) > float.Parse(maxValue))
                    {
                        Text = maxValue;
                    }
                }
                else if (numberType == NumberType.Double)
                {
                    if (double.Parse(Text) > double.Parse(maxValue))
                    {
                        Text = maxValue;
                    }
                }
            }
            Text = (Text == "0") ? "0" : Text.TrimStart('0');
            SelectionStart = Text.Length;
        }

        /// <summary>
        /// 复写Text，确保值是符合我们预设的类型
        /// </summary>
        [AllowNull]
        public override string Text
        {
            get => base.Text ?? "";
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    base.Text = value;
                }
                else
                {
                    if (numberType == NumberType.Integer)
                    {
                        if (int.TryParse(value, out var result))
                        {
                            base.Text = result.ToString();
                        }
                        else
                        {
                            base.Text = "";
                        }
                    }
                    else if (numberType == NumberType.Long)
                    {
                        if (long.TryParse(value, out var result))
                        {
                            base.Text = result.ToString();
                        }
                        else
                        {
                            base.Text = "";
                        }
                    }
                    else if (numberType == NumberType.Float)
                    {
                        if (float.TryParse(value, out var result))
                        {
                            base.Text = result.ToString("F" + decimalPointNumber);
                        }
                        else
                        {
                            base.Text = "";
                        }
                    }
                    else if (numberType == NumberType.Double)
                    {
                        if (double.TryParse(value, out var result))
                        {
                            base.Text = result.ToString("F" + decimalPointNumber);
                        }
                        else
                        {
                            base.Text = "";
                        }
                    }
                }
            }
        }

        [Browsable(false)]
        [DefaultValue(0)]
        public int IntValue
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Text) || numberType != NumberType.Integer)
                {
                    return 0;
                }
                VerifyThenFormatValue();
                return int.Parse(Text);
            }
            set
            {
                Text = value.ToString();
                VerifyThenFormatValue();
            }
        }

        [Browsable(false)]
        [DefaultValue(0)]
        public long LongValue
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Text) || numberType != NumberType.Long)
                {
                    return 0;
                }
                VerifyThenFormatValue();
                return long.Parse(Text);
            }
            set
            {
                Text = value.ToString();
                VerifyThenFormatValue();
            }
        }

        [Browsable(false)]
        [DefaultValue(0)]
        public float FloatValue
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Text) || numberType != NumberType.Float)
                {
                    return 0;
                }
                VerifyThenFormatValue();
                return float.Parse(Text);
            }
            set
            {
                Text = value.ToString();
                VerifyThenFormatValue();
            }
        }
        [Browsable(false)]
        [DefaultValue(0)]
        public double DoubleValue
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Text) || numberType != NumberType.Double)
                {
                    return 0;
                }
                VerifyThenFormatValue();
                return double.Parse(Text);
            }
            set
            {
                Text = value.ToString();
                VerifyThenFormatValue();
            }
        }

    }

    public enum NumberType
    {
        Integer, Long, Float, Double
    }
}
