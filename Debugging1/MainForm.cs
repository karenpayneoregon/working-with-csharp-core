using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DebuggingFiles.Classes;

namespace DebuggingFiles
{
    public partial class MainForm : Form
    {
        private readonly BindingSource _bsValidData = new BindingSource();
        public MainForm()
        {
            InitializeComponent();
        }

        private void cmdProcess_Click(object sender, EventArgs e)
        {
            var ops = new FileOperations();

            ops.BadNcisHandler += OpsOnBadNcisHandler;

            var (success, exception, validRows, invalidRows, _) = 
                ops.LoadCsvFileTextFieldParser();
            
            if (!success)
            {
                MessageBox.Show(exception.Message);
                return;
            }

           
            _bsValidData.DataSource = validRows;
            dataGridView1.DataSource = _bsValidData;
            bindingNavigator1.BindingSource = _bsValidData;

            dataGridView1.Columns["id"].HeaderText = "Row index";
            dataGridView1.Columns["inspect"].DisplayIndex = 0;
            dataGridView1.Columns["Address"].Width = 300;
            dataGridView1.Columns["Description"].Width = 215;
            dataGridView1.Columns["line"].Visible = false;

            dataGridView1.Columns[1].Frozen = true;

            cboInspectRowIndices.DataSource = validRows.Where(item => item.Inspect).Select(item => item.Id).ToList();

            dataGridView2.DataSource = invalidRows;

            dataGridView2.ExpandColumns();
        }

        private void OpsOnBadNcisHandler(int sender)
        {
            Console.WriteLine(sender);
        }

        private void cmdInspectRows_Click(object sender, EventArgs e)
        {
            if (cboInspectRowIndices.DataSource == null) return;

            var item = _bsValidData.List.OfType<DataItem>()
                .ToList()
                .Find(dataItem => dataItem.Id == Convert.ToInt32(cboInspectRowIndices.Text));

            var position = _bsValidData.IndexOf(item);
            
            if (position > -1)
            {
                _bsValidData.Position = position;
            }
        }

        private void cmdReview_Click(object sender, EventArgs e)
        {
            if (_bsValidData.DataSource == null) return;

            List<DataItem> results = ((List<DataItem>)_bsValidData.DataSource).Where(item => item.Inspect).ToList();

            var f = new ReviewForm(results);
            
            f.UpdateRecord += OnUpdateRecord;

            try
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    // get changed rows
                    var changedData = f.Data.Where(item => item.Inspect == false).ToList();
                    // bail out if no changed rows
                    if (changedData.Count <= 0) return;

                    // update rows in DataGridView
                    foreach (var dataItem in changedData)
                    {
                        var Item = _bsValidData.List.OfType<DataItem>().ToList().Find(item => item.Id == dataItem.Id);
                        Item.Inspect = false;
                        Item.Beat = dataItem.Beat;
                    }

                    // update ComboBox to excluded updated rows from review form.
                    results = ((List<DataItem>)_bsValidData.DataSource).Where(item => item.Inspect).ToList();
                    cboInspectRowIndices.DataSource = results;

                }
            }
            finally
            {
                f.Dispose();
            }
        }
        /// <summary>
        /// List<DataItem> results = ((List<DataItem>)_bsValidData.DataSource).Where(item => item.Inspect).ToList();
        /// </summary>
        /// <param name="sender"></param>
        private void OnUpdateRecord(DataItem sender)
        {
            List<DataItem> items = (List<DataItem>)_bsValidData.DataSource;

            var item = items.FirstOrDefault(x => x.Id == sender.Id);
            if (item  != null)
            {
                item.Beat = sender.Beat;
            }
        }
    }
}
