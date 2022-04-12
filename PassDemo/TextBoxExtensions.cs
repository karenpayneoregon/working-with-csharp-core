using System.Windows.Forms;

namespace PassDemo
{
    public static class TextBoxExtensions
    {
        public static void ToggleShow(this TextBox sender, bool show = true, char hideWith = '\u25CF')
        {
            sender.PasswordChar = show ? '\0' : hideWith;
        }
    }
}