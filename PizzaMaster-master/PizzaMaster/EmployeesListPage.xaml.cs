using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PizzaMaster
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EmployeesListPage : Page
    {
        ObservableCollection<Employee> employees;
        public EmployeesListPage()
        {
            this.InitializeComponent();

            this.Loaded += PhonesListPage_Loaded;
        }
        private void PhonesListPage_Loaded(object sender, RoutedEventArgs e)
        {
            titleText.Text = MainPage.selectedLocation.ToString();
            using (var db = new LocationsContext())
            {
                if (MainPage.selectedLocation != null)
                {
                    employees = new ObservableCollection<Employee>(db.Employees.Include(l => l.Location).Where<Employee>(emp => emp.Location == MainPage.selectedLocation).ToList());
                }               
            }
            employeesList.ItemsSource = employees;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (MainPage.selectedLocation != null)
            {
                Employee employee = new Employee()
                {
                    FullName = nameBox.Text,
                    Ages = Int32.Parse(agesBox.Text),
                    Salary = Int32.Parse(salaryBox.Text),
                    Position = "Employee",
                    Location = MainPage.selectedLocation,
                };

                using (var db = new LocationsContext())
                {
                    db.Locations.Attach(MainPage.selectedLocation);
                    db.Employees.Add(employee);
                    if (db.SaveChanges() > 0)
                    {
                        employees.Add(employee);
                    }
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack) Frame.GoBack();
        }
    }
}
