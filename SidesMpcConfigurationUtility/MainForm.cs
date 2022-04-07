using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SidesMpcConfigurationUtility.Classes;
using SidesMpcConfigurationUtility.Extensions;

namespace SidesMpcConfigurationUtility
{
    public partial class MainForm : Form
    {
        private readonly BindingSource _bindingSource = new BindingSource();
        private SidesEnvironment _environment;
        private List<Type> _types;
        public MainForm()
        {
            InitializeComponent();
            Shown += OnShown;
        }

        private void OnShown(object sender, EventArgs e)
        {
            var (success, exception) = LoadConfiguration(SidesEnvironment.Development);

            if (success)
            {
                EnvironmentComboBox.DataSource = Enum.GetValues(typeof(SidesEnvironment));
                EnvironmentComboBox.SelectedIndexChanged += EnvironmentComboBoxOnSelectedIndexChanged;
                SettingsDataGridView.MouseDoubleClick += DataGridView1OnMouseDoubleClick;
                SettingsDataGridView.KeyDown += DataGridView1OnKeyDown;
                ActiveControl = SettingsDataGridView;
            }
            else
            {
                MessageBox.Show($"Failed loading file\n{exception.Message}");
            }

        }


        private void EnvironmentComboBoxOnSelectedIndexChanged(object sender, EventArgs e)
        {
            _environment = (SidesEnvironment)EnvironmentComboBox.SelectedItem;

            var (success, exception) = LoadConfiguration(_environment);

            if (success)
            {
                Text = $@"SIDE MPC Configuration - ({_environment})";
            }
            else
            {
                MessageBox.Show($"Failed loading file\n{exception.Message}");
            }

        }

        private void EditCurrentButton_Click(object sender, EventArgs e)
        {
            EditCurrentRow();
        }

        private void EditCurrentRow()
        {
            
            if (_bindingSource.Current == null)
            {
                return;
            }

            var current = (SettingItem)_bindingSource.Current;
            var f = new EditForm(_environment, current, _types);

            try
            {
                f.ShowDialog(this);
            }
            finally
            {
                f.Dispose();
            }
        }

        private void DataGridView1OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            e.Handled = true;

            EditCurrentRow();

        }

        private void DataGridView1OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditCurrentRow();
        }

        private (bool success, Exception exception) LoadConfiguration(SidesEnvironment sender)
        {

            var (success, exception, items, types) = SettingsOperations.ReadSettings(sender);

            if (success)
            {
                _bindingSource.DataSource = items;

                SettingsDataGridView.DataSource = _bindingSource;
                SettingsDataGridView.Columns["Name"].ReadOnly = true;
                SettingsDataGridView.Columns["Name"].HeaderText = "Setting";
                SettingsDataGridView.Columns["IsPath"].Visible = false;
                SettingsDataGridView.Columns["Type"].Visible = false;
                SettingsDataGridView.ExpandColumns();

                _types = types;

                return (true, null);

            }
            else // Resharper got it wrong here
            {
                return (false, exception);
            }

        }
    }
}
