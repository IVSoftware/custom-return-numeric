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
        // Disable range checking in the base class.
        public NumericUpDownEx()
        {
            base.Maximum = decimal.MaxValue;
            base.Minimum = decimal.MinValue;
        }
        public new decimal Maximum { get; set; } = 100; // Keep default value the same
        public new decimal Minimum { get; set; } = 0;   // Keep default value the same

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.KeyData)
            {
                case Keys.Enter:
                    e.Handled = e.SuppressKeyPress = true;
                    BeginInvoke((MethodInvoker)delegate
                    {
                        // In this case, Designer has set Min = 1, Max = 10
                        if (Value < Minimum || Value > Maximum)
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
        ///  The Validating event doesn't always fire when we want it to,
        ///  especially if this is the only control. Do this instead;
        /// </summary>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            OnKeyDown(new KeyEventArgs(Keys.Enter));
        }
    }
}