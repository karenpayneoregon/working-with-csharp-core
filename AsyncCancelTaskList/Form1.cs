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
using AsyncCancelTaskList.Classes;
using WindowsFormsLibrary.Classes;

namespace AsyncCancelTaskList
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource _cancellationTokenSource = new();
        public Form1()
        {
            InitializeComponent();

            Operations.OnProcess += OnOnProcess;

        }

        private void OnOnProcess(string sender)
        {
            ResultsListBox.Items.Add(sender);
            ResultsListBox.SelectedIndex = ResultsListBox.Items.Count - 1;
        }

        private async void StartButton_Click(object sender, EventArgs e)
        {

            if (ResultsListBox.Items.Count >0)
            {
                ResultsListBox.Items.Clear();
            }

            if (_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Dispose();

                _cancellationTokenSource = new CancellationTokenSource();
            }

            Task task = Operations.SumPageSizesAsync(_cancellationTokenSource);

            //await Task.WhenAny(task);

            //await Task.Run((() => task));


            //try
            //{
            //    await task;
            //}
            //catch (Exception exception)
            //{
            //    // TaskCanceledException
            //    Debug.WriteLine(exception.GetType());
            //}

            try
            {
                await task;
            }
            catch (OperationCanceledException)
            {
                Dialogs.Information(this,"Cancelled task");
            }
            catch (Exception exception)
            {
                Dialogs.Information($"Something went wrong: {exception.Message}");
            }

        }

        private void CancellationButton_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
