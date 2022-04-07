using System.Drawing;

namespace WpfApplicationListViewImage.Classes
{
    public class ViewData
    {
        public ViewData(Bitmap icon, string name)
        {
            _icon = icon;
            _name = name;
        }

        private readonly Bitmap _icon;
        /// <summary>
        /// Icon to display
        /// </summary>
        public Bitmap Icon => _icon;

        private readonly string _name;
        /// <summary>
        /// Icon name
        /// </summary>
        public string Name => _name;
    }
}