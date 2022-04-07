using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using SwitchExpressions_efcore.Classes;
using SwitchExpressions_efcore.LanguageExtensions;

namespace SwitchExpressions_efcore
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();

            /*
             * Subscribe to OnIteratePersonGradesEvent which adds a StudentEntity in a for-next
             *
             * Alternate would be using yield as in
             *      Relational-Pattern-Matching Helpers.GetEmployeesWhereManagerHasThreeYearsAsManager
             */
            SchoolOperations.OnIteratePersonGradesEvent += SchoolOperationsOnOnIteratePersonGradesEvent;

        }

        private void SchoolOperationsOnOnIteratePersonGradesEvent(PersonGrades personGrades)
        {

            if (personGrades.Grade is null) return;
            
            var item = new ListViewItem(new[]
            {
                personGrades.PersonID.ToString(),
                personGrades.FullName, 
                personGrades.Grade.Value.ToString(CultureInfo.CurrentCulture), 
                personGrades.GradeLetter
            });

            /*
             * Prevent cross thread violations
             */
            listView1.InvokeIfRequired(lv => lv.Items.Add(item));

        }


        private async void StudentGradesButton_Click(object sender, EventArgs e)
        {

            listView1.Items.Clear();

            /*
             * Nothing need to be passed as GetGradesForPeople has a default value
             * Be careful when using defaults when code is intended for production 
             */
            await Task.Run(async () => await SchoolOperations.GetGradesForPeople());

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
    }
}
