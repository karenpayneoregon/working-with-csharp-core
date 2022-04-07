using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
//using Dotnet5Standard.LanguageExtensions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StealingAndConflicts.Classes;

namespace StealingAndConflicts
{
    public partial class Form1 : Form
    {
        private readonly string _input = "This,is,an,example,for,the,C#,community";

        public Form1()
        {
            InitializeComponent();
        }


        private void StealButton_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new();
            textBox1.Text = "";
            List<string> result = Stealing.Chunking(_input);
            
            result.ForEach(x => builder.AppendLine(x));

            textBox1.Text = builder.ToString();
        }

        private void NotStealingButton_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new();
            textBox2.Text = "";
            List<string> result = NotStealing.Chunking(_input);

            result.ForEach(x => builder.AppendLine(x));

            textBox2.Text = builder.ToString();
        }
    }
}
