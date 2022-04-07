using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DebuggingFiles.Classes;

namespace DebuggingFiles
{
    public partial class ReviewForm : Form
    {
        private BindingSource _bs = new BindingSource();
        private List<DataItem> _data;

        public delegate void OnUpdateRecord(DataItem sender);
        public event OnUpdateRecord UpdateRecord;

        /// <summary>
        /// Provide access by the calling form to the data presented
        /// </summary>
        public List<DataItem> Data => _data;

        /// <summary>
        /// Acceptable values for beat field. In part 2 these will be read from a database reference table.
        /// </summary>
        private List<string> _beatList = new List<string>()
            {
                "1A", "1B", "1C", "2A", "2B", "2C", "3A", "3B", "3C", "3M", "4A",
                "4B", "4C", "5A", "5B", "5C", "6A", "6B", "6C"
            } ;

        public ReviewForm()
        {
            InitializeComponent();
        }

        public ReviewForm(List<DataItem> pData)
        {
            InitializeComponent();

            _data = pData.OrderBy(x => x.Id).ToList();
            Shown += ReviewForm_Shown;
        }

        private void ReviewForm_Shown(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;

            // ReSharper disable once PossibleNullReferenceException
            ((DataGridViewComboBoxColumn) dataGridView1.Columns["beatColumn"]).DataSource = _beatList;

            _bs.DataSource = _data;
            dataGridView1.DataSource = _bs;
            dataGridView1.ExpandColumns();

            dataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;

        }

        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.IsComboBoxCell())
            {
                if (dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name == "beatColumn")
                {
                    if (e.Control is ComboBox cb)
                    {
                        cb.SelectionChangeCommitted -= _SelectionChangeCommitted;
                        cb.SelectionChangeCommitted += _SelectionChangeCommitted;
                    }
                }
            }

        }
        /// <summary>
        /// Update current row beat field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _SelectionChangeCommitted(object sender, EventArgs e)
        {
            
            if (_bs.Current !=null)
            {
                if (!string.IsNullOrWhiteSpace(((DataGridViewComboBoxEditingControl)sender).Text))
                {
                    var currentRow = (DataItem) _bs.Current;
                    currentRow.Beat = ((DataGridViewComboBoxEditingControl) sender).Text;
                    currentRow.Inspect = false;

                    UpdateRecord?.Invoke((DataItem) _bs.Current);
                }
            }
        }

    }
}
