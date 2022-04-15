using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TryCatchExamples.Classes;
using WindowsFormsLibrary.Classes;

namespace TryCatchExamples
{
    public partial class Form1 : Form
    {
        private const int _connectionTimeout = 4;
        
        private CancellationTokenSource _cancellationTokenSourceWithTimeOut = new(TimeSpan.FromSeconds(_connectionTimeout));
        private CancellationTokenSource _cancellationTokenSourceNoTimeOut = new();

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// demonstrates how to avoid a long timeout for a bad connection to a database
        /// </summary>
        /// <remarks>
        /// For details
        /// https://social.technet.microsoft.com/wiki/contents/articles/54260.sql-server-freezes-when-connecting-c.aspx
        /// </remarks>
        private async void ConnectionTimeOutButton_Click(object sender, EventArgs e)
        {

            if (_cancellationTokenSourceWithTimeOut.IsCancellationRequested || _cancellationTokenSourceNoTimeOut.IsCancellationRequested)
            {
                _cancellationTokenSourceWithTimeOut.Dispose();
                _cancellationTokenSourceNoTimeOut.Dispose();

                _cancellationTokenSourceWithTimeOut = new(TimeSpan.FromSeconds(_connectionTimeout));
                _cancellationTokenSourceNoTimeOut = new CancellationTokenSource();

            }

            Operations.RunWithoutIssues = false;

            if (FailConnectionCheckBox.Checked)
            {
                Operations.RunWithoutIssues = FailConnectionCheckBox.Checked;
                var dataResults = await Operations.ReadProductsTask(_cancellationTokenSourceWithTimeOut.Token);
                Dialogs.Information(this, dataResults.ExceptionMessage, "Dang");
            }
            else
            {
                var dataResults = await Operations.ReadProductsTask(_cancellationTokenSourceNoTimeOut.Token);
                Dialogs.Information(this, $"Record count {dataResults.DataTable.Rows.Count}", "Cool");
            }
        }

        private void TryFinallyButton_Click(object sender, EventArgs e)
        {
            try
            {
                // TryCast produces an unhandled exception.
                Operations.TryCast();
            }
            catch (Exception ex)
            {
                // Catch the exception that is unhandled in TryCast.
                Dialogs.Information(this, $"Catching the {ex.GetType()} exception triggers the finally block.");

                // Restore the original unhandled exception. You might not
                // know what exception to expect, or how to handle it, so pass
                // it on.
                //throw;
            }
        }
    }
}
