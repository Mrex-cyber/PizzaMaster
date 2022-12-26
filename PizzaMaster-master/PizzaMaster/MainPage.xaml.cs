using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PizzaMaster
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public static Location selectedLocation;
        public double sumOfLocationEarnings;

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;

        }
        
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            using (LocationsContext db = new LocationsContext())
            {
                locationsList.ItemsSource = db.Locations.ToList();
                var locations = db.Locations
                    .Include(l => l.Products)
                    .Include(l => l.Employees)
                    .ToList();

                foreach (var location in locations)
                {
                    foreach (var product in location.Products)
                    {
                        sumOfLocationEarnings += product.Earnings;
                    }
                    location.Earnings = sumOfLocationEarnings;
                }

                foreach (var location in locations)
                {                    
                    location.Employees = location.Employees;
                }

            }                   
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LocationPageEditing));
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (locationsList.SelectedItem != null)
            {
                Location location = locationsList.SelectedItem as Location;
                if (location != null)
                {
                    Frame.Navigate(typeof(EmployeesListPage), location.Id);
                }
            }
        }

        private async void deleteButton_Click(object sender, RoutedEventArgs e)
        {
           
            if (locationsList.SelectedItem != null)
            {
                Location location = locationsList.SelectedItem as Location;
                ContentDialog deleteLocationDialog = new ContentDialog()
                {
                    Title = "Delete the " + location + " location",
                    Content = "Are you sure of deleting?",
                    PrimaryButtonText = "Yes, of course",
                    SecondaryButtonText = "No, cancel it"
                };

                ContentDialogResult resultOfDialog = await deleteLocationDialog.ShowAsync();

                if (location != null && resultOfDialog == ContentDialogResult.Primary)
                {
                    using (LocationsContext db = new LocationsContext())
                    {
                        db.Locations.Remove(location);
                        db.SaveChanges();
                        locationsList.ItemsSource = db.Locations.ToList();
                    }
                }
            }
        }

        private void EmployeesList_Click(object sender, RoutedEventArgs e)
        {
            if (locationsList.SelectedItem != null)
            {
                selectedLocation = locationsList.SelectedItem as Location;
                if (selectedLocation != null)
                {
                    Frame.Navigate(typeof(EmployeesListPage));
                }
            }
        }

        private void ProductsList_Click(object sender, RoutedEventArgs e)
        {
            if (locationsList.SelectedItem != null)
            {
                selectedLocation = locationsList.SelectedItem as Location;
                if (selectedLocation != null)
                {
                    Frame.Navigate(typeof(ProductsListPage));
                }
            }
        }
    }
}
