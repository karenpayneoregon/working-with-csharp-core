using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqlServerVeryBasic.Classes;
using SqlServerVeryBasic.Models;

namespace SqlServerVeryBasicFormApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            categCB.DataSource = SqlServerOperations.Categories();
        }

        private void GetCurrentCategoryButton_Click(object sender, EventArgs e)
        {
            var current = (Category)categCB.SelectedItem;
            MessageBox.Show($"{current.CategoryID,-5}{current.CategoryName}");
        }
    }
}
