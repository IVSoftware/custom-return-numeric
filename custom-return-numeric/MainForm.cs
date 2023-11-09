using System.ComponentModel;

namespace custom_return_numeric
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
    }
    class NumericUpDownEx : NumericUpDown
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.KeyData)
            {
                case Keys.Enter:
                    e.Handled = e.SuppressKeyPress = true;
                    BeginInvoke((MethodInvoker)delegate
                    {
                        if (Value < 0 || Value > 10)
                        {
                            MessageBox.Show($"Error: '{Value}' is not legal.");
                            Value = 1;
                        }
                        var length = Value.ToString().Length;
                        if (length > 0)
                        {
                            Select(0, length);
                        }
                    });
                    break;
            }
        }
        protected override void OnLostFocus(EventArgs e) => 
            OnKeyDown(new KeyEventArgs(Keys.Enter));
    }
}