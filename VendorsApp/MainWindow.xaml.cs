using System;
using System.Windows;
using System.Windows.Interop;
using VendorsApp.Classes;
using VendorsApp.Models;

namespace VendorsApp
{

    public partial class MainWindow : Window
    {
        private IntPtr _intPtr;
        private bool _shown;

        public MainWindow()
        {
            InitializeComponent();

            VendorModel vendorModel = new();
            DataContext = vendorModel;
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            if (_shown)
            {
                return;
            }

            _shown = true;
            Window window = GetWindow(this);
            var windowInterop = new WindowInteropHelper(window ?? throw new InvalidOperationException());
            _intPtr = windowInterop.Handle;
        }

        /// <summary>
        /// Setup to show change notification and/or show current.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedVendorButton_Click(object sender, RoutedEventArgs e)
        {
            Vendor current = (Vendor)VendorsCombobox.SelectedItem;
            //MessageBox.Show(current.Id == 0 ? 
            //    "Please make a selection" : 
            //    $"Current identifier {current.Id}\n{current.DisplayName}\n{current.AccountNumber}\n{current.CreditRating}");

            var text = current.Id == 0 ? "Please make a selection" : $"Id {current.Id}\n{current.DisplayName}\n{current.AccountNumber}\n{current.CreditRating}";
            var heading = current.Id == 0 ? "" : "Current selection";
            Dialogs.Information(_intPtr, heading, text,"Got it!!!");
        }
    }
}
