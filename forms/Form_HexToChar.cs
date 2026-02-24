namespace PortOSC
{
    public partial class Form_HexToChar : Form
    {
        public Form_HexToChar()
        {
            InitializeComponent();
        }

        private void Hex_1_Leave(object sender, EventArgs e)
        {
            try
            {
                Hex_1.BackColor = System.Drawing.Color.White;
                char a = (char)Convert.ToInt32(Hex_1.Text, 16);
                Char_1.Text = a.ToString();
                Char_1.BackColor = System.Drawing.Color.White;
            }
            catch
            {
                Char_1.Text = "";
                Char_1.BackColor = System.Drawing.Color.Yellow;
            }
        }

        private void Char_1_Leave(object sender, EventArgs e)
        {
            try
            {
                Char_1.BackColor = System.Drawing.Color.White;
                Hex_1.Text = string.Format("{0:X2}", (int)Char_1.Text[0]);
                Hex_1.BackColor = System.Drawing.Color.White;
            }
            catch
            {
                Hex_1.Text = "";
                Hex_1.BackColor = System.Drawing.Color.Yellow;
            }
        }

        private void Hex_2_Leave(object sender, EventArgs e)
        {
            try
            {
                Hex_2.BackColor = System.Drawing.Color.White;
                char a = (char)Convert.ToInt32(Hex_2.Text, 16);
                Char_2.Text = a.ToString();
                Char_2.BackColor = System.Drawing.Color.White;
            }
            catch
            {
                Char_2.Text = "";
                Char_2.BackColor = System.Drawing.Color.Yellow;
            }
        }

        private void Char_2_Leave(object sender, EventArgs e)
        {
            try
            {
                Char_2.BackColor = System.Drawing.Color.White;
                Hex_2.Text = string.Format("{0:X2}", (int)Char_2.Text[0]);
                Hex_2.BackColor = System.Drawing.Color.White;
            }
            catch
            {
                Hex_2.Text = "";
                Hex_2.BackColor = System.Drawing.Color.Yellow;
            }
        }

        private void Hex_3_Leave(object sender, EventArgs e)
        {
            try
            {
                Hex_3.BackColor = System.Drawing.Color.White;
                char a = (char)Convert.ToInt32(Hex_3.Text, 16);
                Char_3.Text = a.ToString();
                Char_3.BackColor = System.Drawing.Color.White;
            }
            catch
            {
                Char_3.Text = "";
                Char_3.BackColor = System.Drawing.Color.Yellow;
            }
        }

        private void Char_3_Leave(object sender, EventArgs e)
        {
            try
            {
                Char_3.BackColor = System.Drawing.Color.White;
                Hex_3.Text = string.Format("{0:X2}", (int)Char_3.Text[0]);
                Hex_3.BackColor = System.Drawing.Color.White;
            }
            catch
            {
                Hex_3.Text = "";
                Hex_3.BackColor = System.Drawing.Color.Yellow;
            }
        }

        private void Hex_4_Leave(object sender, EventArgs e)
        {
            try
            {
                Hex_4.BackColor = System.Drawing.Color.White;
                char a = (char)Convert.ToInt32(Hex_4.Text, 16);
                Char_4.Text = a.ToString();
                Char_4.BackColor = System.Drawing.Color.White;
            }
            catch
            {
                Char_4.Text = "";
                Char_4.BackColor = System.Drawing.Color.Yellow;
            }
        }

        private void Char_4_Leave(object sender, EventArgs e)
        {
            try
            {
                Char_4.BackColor = System.Drawing.Color.White;
                Hex_4.Text = string.Format("{0:X2}", (int)Char_4.Text[0]);
                Hex_4.BackColor = System.Drawing.Color.White;
            }
            catch
            {
                Hex_4.Text = "";
                Hex_4.BackColor = System.Drawing.Color.Yellow;
            }
        }

        private void Hex_5_Leave(object sender, EventArgs e)
        {
            try
            {
                Hex_5.BackColor = System.Drawing.Color.White;
                char a = (char)Convert.ToInt32(Hex_5.Text, 16);
                Char_5.Text = a.ToString();
                Char_5.BackColor = System.Drawing.Color.White;
            }
            catch
            {
                Char_5.Text = "";
                Char_5.BackColor = System.Drawing.Color.Yellow;
            }
        }
        private void Char_5_Leave(object sender, EventArgs e)
        {
            try
            {
                Char_5.BackColor = System.Drawing.Color.White;
                Hex_5.Text = string.Format("{0:X2}", (int)Char_5.Text[0]);
                Hex_5.BackColor = System.Drawing.Color.White;
            }
            catch
            {
                Hex_5.Text = "";
                Hex_5.BackColor = System.Drawing.Color.Yellow;
            }
        }

        private void Form_HexToChar_Load(object sender, EventArgs e)
        {

        }
    }
}
