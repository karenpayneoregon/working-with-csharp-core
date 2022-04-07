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
using WindowsFormsLibrary.Classes;
using static System.Threading.Thread;

namespace AsyncSimple
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource _cts = new ();
        public Form1()
        {
            InitializeComponent();
        }

        private async void StartButton_Click(object sender, EventArgs e)
        {
            var cancelled = false;
            if (_cts.IsCancellationRequested == true)
            {
                _cts.Dispose();
                _cts = new CancellationTokenSource();
            }


            var progressIndicator = new Progress<int>(ReportProgress);

            try
            {
                await AsyncMethod(progressIndicator, _cts.Token);
            }
            catch (OperationCanceledException)
            {
                StatusLabel.Text = "Cancelled";
                cancelled = true;
            }

            if (!cancelled) return;

            await Task.Delay(1000);
            StatusLabel.Text = "Go again!";
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            _cts.Cancel();
        }
        private static async Task AsyncMethod(IProgress<int> progress, CancellationToken ct)
        {

            for (int index = 100; index <= 120; index++)
            {
                //Simulate an async call that takes some time to complete
                await Task.Delay(500, ct);

                if (ct.IsCancellationRequested)
                {
                    ct.ThrowIfCancellationRequested();
                }

                progress?.Report(index);

            }

        }
        private void ReportProgress(int value)
        {
            StatusLabel.Text = value.ToString();
            TextBox1.Text = value.ToString();
        }

        private void NoviceButton_Click(object sender, EventArgs e)
        {
            static void BusyWait(int milliseconds)
            {
                var sw = Stopwatch.StartNew();

                while (sw.ElapsedMilliseconds < milliseconds)
                {
                    SpinWait(1000);
                }
            }

            if (Dialogs.Question(this,"Question", "Do you really, really want to wait?","Yep","Nope", DialogResult.No))
            {
                for (int index = 0; index < 10; index++)
                {
                    BusyWait(1000);
                }
            }


        }
    }
}
