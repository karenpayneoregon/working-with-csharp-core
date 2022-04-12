using System;
using System.Windows.Forms;

namespace PassDemo
{
    public partial class Form1 : Form
    {
        private const char _hideChar = '\u25CF';

        public Form1()
        {
            InitializeComponent();
            PasswordTextBox.PasswordChar = _hideChar;
        }

        private void ToggleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            PasswordTextBox.ToggleShow(ToggleCheckBox.Checked, _hideChar);
        }
    }
}
