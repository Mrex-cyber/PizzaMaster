using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PizzaMaster
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EmployeePageEditing : Page
    {
        Employee employee;
        public EmployeePageEditing()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                int id = (int)e.Parameter;
                using (var db = new LocationsContext())
                {
                    employee = db.Employees
                        .Include(emp => emp.Location)
                        .FirstOrDefault(emp => emp.Id == id);
                }
            }

            if (employee != null)
            {
                headerBlock.Text = "Editing employee";
                nameBox.Text = employee.FullName;
                agesBox.Text = employee.Ages.ToString();
                positionsList.Text = employee.Position;
                salaryBox.Text = employee.Salary.ToString();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new LocationsContext())
            {
                if (employee != null)
                {
                    employee.FullName = nameBox.Text;
                    employee.Ages = Int32.Parse(agesBox.Text);
                    employee.Salary = Int32.Parse(salaryBox.Text);
                    employee.Position = "Employee";
                    employee.Location = employee.Location;

                    db.Employees.Update(employee);
                }
                else
                {
                    db.Employees.Add(new Employee
                    {
                        FullName = nameBox.Text,
                        Ages = Int32.Parse(agesBox.Text),
                        Salary = Int32.Parse(salaryBox.Text),
                        Position = "Employee",
                        Location = employee.Location,
                    });
                }
                db.SaveChanges();
            }
            GoToMainPage();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            GoToMainPage();
        }

        private void GoToMainPage()
        {
            if (Frame.CanGoBack) Frame.GoBack();
            else Frame.Navigate(typeof(MainPage));
        }
    }
}
