using System.Collections.Generic;

namespace WpfApplicationListViewImage.Classes
{
    public class SampleModel
    {
        public IEnumerable<ViewData> Items
        {
            get
            {
                yield return new ViewData(Properties.Resources.critical, "Critical");
                yield return new ViewData(Properties.Resources.Database, "Database");
                yield return new ViewData(Properties.Resources.Excel, "Excel");
                yield return new ViewData(Properties.Resources.csv, "CSV");
            }
        }
    }
}
