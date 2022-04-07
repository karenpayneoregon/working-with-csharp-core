using System.Linq;
using System.Windows.Forms;

namespace DebuggingFiles.Classes
{
    public static class DataGridViewExtensions
    {
        /// <summary>
        /// Expand all columns excluding in this case Orders column
        /// </summary>
        /// <param name="sender"></param>
        public static void ExpandColumns(this DataGridView sender)
        {
            sender.Columns.Cast<DataGridViewColumn>().ToList().ForEach(col => col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells);
        }

        /// <summary>
        /// Used to determine if the current cell type is a ComboBoxCell
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static bool IsComboBoxCell(this DataGridViewCell sender)
        {
            var result = false;
            
            if (sender.EditType != null)
            {
                if (sender.EditType == typeof(DataGridViewComboBoxEditingControl))
                {
                    result = true;
                }
            }
            
            return result;
        }
    }
}
