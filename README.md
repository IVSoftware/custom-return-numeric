# Custom Return Numeric

One way to do this is to handle the `KeyDown` event. The approach I would personally use is to inherit `NumericUpDown` and replace instances of it in the MainForm.Designer.cs file with my custom class.


[![screenshot][1]][1]
___

```
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
    .
    .
    .
}
```

You'll also want to do the same kind of action when the control loses focus.

```
    .
    .
    .
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
```

  [1]: https://i.stack.imgur.com/gTqcc.png