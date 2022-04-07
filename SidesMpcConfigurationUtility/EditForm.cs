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

namespace SidesMpcConfigurationUtility
{
    public partial class EditForm : Form
    {
        private readonly SettingItem _settingItem;
        private readonly SidesEnvironment _environment;


        public EditForm()
        {
            InitializeComponent();
        }

        public EditForm(SidesEnvironment environment, SettingItem item, List<Type> types)
        {
            InitializeComponent();

            _settingItem = item;
            _environment = environment;
      
            comboBox1.DataSource = types.Select(type => type.Name).ToList();

            NameLabel.Text = _settingItem.Name;
            ValueTextBox.Text = _settingItem.Value;
            IsPathCheckBox.Checked = _settingItem.IsPath;
            
            var index = comboBox1.FindStringExact(_settingItem.Type.Name);
            comboBox1.SelectedIndex = index;
            

        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {

            var (success, exception) = SettingsOperations.SetValue(_environment, _settingItem.Name, ValueTextBox.Text);

            if (success)
            {
                _settingItem.Value = ValueTextBox.Text;
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show($@"Failed to update{Environment.NewLine}{exception.Message}");
            }

        }


    }
}
