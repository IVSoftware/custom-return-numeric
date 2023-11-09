using System.ComponentModel;

namespace custom_return_numeric
{
    public partial class MainForm : Form
    {
        public MainForm() =>InitializeComponent();
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
                            var message = $"Error: '{Value}' is not legal.";
                            Value = 1;
                            MessageBox.Show(message);
                            Focus();
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
        /// <summary>
        ///  The Validating event doesn't always fire when we 
        ///  want it to, especially if this is the only control.
        ///  Do this instead;
        /// </summary>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e); 
            OnKeyDown(new KeyEventArgs(Keys.Enter));
        }
    }
}