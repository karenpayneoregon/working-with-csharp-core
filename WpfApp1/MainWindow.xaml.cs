﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using DataValidatorLibrary.Helpers;
using DataValidatorLibrary.LanguageExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WpfApp1.Contexts;
using WpfApp1.Models;
using static WpfApp1.Classes.Dialogs;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HRContext _context;
        public static RoutedCommand IterateRoutedCommand = new ();

        private void IterateCommandOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (DataContext == null) return;

            var employees = (ObservableCollection<Employees>)DataContext;

            foreach (var employee in employees)
            {
                Debug.WriteLine($"{employee.FirstName} {employee.LastName}");
            }

        }

        private void IterateCanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;

        public MainWindow()
        {
            InitializeComponent();

            _context = new HRContext();

            CommandBindings.Add(new CommandBinding(
                IterateRoutedCommand,
                IterateCommandOnExecute,
                IterateCanExecute));

        }

        bool _hasShown;

        protected override async void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            if (_hasShown)
            {
                return;
            }

            _hasShown = true;

            ObservableCollection<Employees> employeeCollection = new ObservableCollection<Employees>();

            await Task.Run(async () =>
            {
                List<Employees> list = await _context.Employees.ToListAsync();
                employeeCollection = new ObservableCollection<Employees>(list);
            });


            EmployeeGrid.ItemsSource = employeeCollection;
            employeeCollection.CollectionChanged += EmployeeCollection_CollectionChanged;

            DataContext = employeeCollection;

            /*
             * Find employee by last name, if found
             * select and scroll into view in the DataGrid
             */
            var employee = (employeeCollection)
                .FirstOrDefault(emp => emp.LastName == "Russell");

            if (employee == null) return;

            EmployeeGrid.SelectedItem = employee;
            EmployeeGrid.ScrollIntoView(employee);

            SaveButton.IsEnabled = true;

        }
        /// <summary>
        /// Informational
        /// Example of gaining access to a deleted employee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmployeeCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems == null) return;
            
            Debug.WriteLine(e.Action);

            if (e.Action != NotifyCollectionChangedAction.Remove) return;

            var employee = (Employees)e.OldItems[0];
            InformationDialog($"Index: {e.OldStartingIndex} - {employee.FirstName} {employee.LastName}", "Just removed");

        }
        /// <summary>
        /// Show some details on the currently selected employee when clicking
        /// the view button in the DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewCurrentEmployee(object sender, RoutedEventArgs e)
        {
            var employee = (Employees)(sender as Button)?.DataContext;
            
            var window = new DetailsWindow(employee) { Owner = this };
            
            try
            {
                window.ShowDialog();
            }
            finally
            {
                window.Close();
            }

        }

        /// <summary>
        /// Perform iterative case insensitive search on employee last name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastNameSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            var lastNameFilter = textBox.Text;
            var cvs = CollectionViewSource.GetDefaultView(EmployeeGrid.ItemsSource);

            if (cvs is null)
            {
                return;
            }

            // nothing entered to search so remove an existing filter
            if (string.IsNullOrWhiteSpace(lastNameFilter))
            {
                cvs.Filter = null;
            }
            else
            {
                // do the filter
                cvs.Filter = item =>
                    item is Employees employees && (employees.LastName.StartsWith(lastNameFilter,
                       StringComparison.InvariantCultureIgnoreCase));
            }
        }
        /// <summary>
        /// Prompt to remove the current row in the DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var grid = (DataGrid)sender;

            if (e.Command != DataGrid.DeleteCommand) return;

            if (grid.SelectedItem is Employees employee)
            {
                string employeeName = $"{employee.FirstName} {employee.LastName}";
                if (!Question($"Delete {employeeName}", "Confirm Delete"))
                {
                    e.Handled = true;
                }
            }
        }
        /// <summary>
        /// Validate any changes via DataAnnotations, display errors if any.
        /// No validation issues
        ///    Has changes, prompt to save
        ///    No changes, inform user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // detect changes for delete, updated and added
                IEnumerable<EntityEntry> modified = _context.ChangeTracker.Entries().Where(entry =>
                    entry.State == EntityState.Deleted ||
                    entry.State == EntityState.Modified ||
                    entry.State == EntityState.Added);


                // If there are changes run each employee through validation
                if (modified.Any())
                {
                    StringBuilder builderMessages = new ();

                    foreach (var entityEntry in modified)
                    {
                        var employee = (Employees)entityEntry.Entity;

                        EntityValidationResult validationResult = ValidationHelper.ValidateEntity(employee);
                        if (validationResult.HasError)
                        {
                            InspectEntities(entityEntry);
                            builderMessages.AppendLine($"{employee.EmployeeId} - {validationResult.ErrorMessageList()}");
                        }
                    }

                    // if there validation errors display them
                    if (builderMessages.Length > 0)
                    {
                        MessageBox(builderMessages.ToString());
                        return;
                    }

                    // has changes, no validation issues, prompt to save
                    if (Question("Save changes?"))
                    {
                        // save changes, count may or may not be needed
                        await Task.Run(async () =>
                        {
                            var count = await _context.SaveChangesAsync();
                        });
                    }
                }
                else
                {
                    InformationDialog("Nothing to save.");
                }
            }
            catch (Exception ex)
            {
                ExceptionDialog("Something went wrong", "Ooops", ex);
            }
        }
        /// <summary>
        /// Shows how to get original and current values while validating entries
        /// in <see cref="SaveChangesButton_Click"/>
        /// </summary>
        /// <param name="entityEntry">EntityEntry</param>
        private void InspectEntities(EntityEntry entityEntry)
        {
            foreach (var property in entityEntry.Metadata.GetProperties())
            {
                
                var originalValue = entityEntry.Property(property.Name).OriginalValue;
                var currentValue = entityEntry.Property(property.Name).CurrentValue;

                if (originalValue != null || currentValue != null)
                {
                    if (!currentValue.Equals(originalValue))
                    {
                        Debug.WriteLine($"{property.Name}: Original: '{originalValue}', Current: '{currentValue}'");
                    }
                }
                else
                {
                    // TODO handle nulls
                }

            }
        }
        /// <summary>
        /// Example for adding a new employee without going thru the hassle of
        /// rigging up user interface as there are plenty needed to collect
        /// required fields.
        ///
        /// Note no manager assigned, this means in the view button click we
        /// need to do a null check.
        /// </summary>
        /// <remarks>
        /// This is a great way (outside unit test) to test validation
        /// </remarks>
        private void AddHardCodedEmployee()
        {
            // create new employee
            var employee = new Employees()
            {
                FirstName = "Jim",
                LastName = "Lewis",
                Email = "jlewis@comcast.net",
                HireDate = new DateTime(2012, 3, 14),
                JobId = 4,
                Salary = 100000,
                DepartmentId = 9
            };

            EntityValidationResult validationResult = ValidationHelper.ValidateEntity(employee);
            if (validationResult.HasError)
            {
                var errors = validationResult.ErrorMessageList();
                MessageBox(errors, "Validation errors");
                return;
            }
            else
            {
                // add and set state for change tracker
                _context.Entry(employee).State = EntityState.Added;
                // add employee to the grid
                var test = EmployeeGrid.ItemsSource;
                ((ObservableCollection<Employees>)EmployeeGrid.ItemsSource).Add(employee);
                MessageBox("Added");
            }

        }
        /// <summary>
        /// Close this application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void EmployeeGrid_OnRowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            /*
             * If you want access to the employee 
             */
            Employees employee = (Employees)e.Row.Item;

            if (e.EditAction == DataGridEditAction.Commit)
            {
                _context.SaveChanges();
            }

        }
    }
}
