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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PizzaMaster
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
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
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LocationPage));
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (locationsList.SelectedItem != null)
            {
                Location location = locationsList.SelectedItem as Location;
                if (location != null)
                {
                    Frame.Navigate(typeof(LocationPage), location.Id);
                }
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (locationsList.SelectedItem != null)
            {
                Location location = locationsList.SelectedItem as Location;
                if (location != null)
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
            Frame.Navigate(typeof(EmployeesListPage));
        }

        private void ProductsList_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ProductsListPage));
        }
    }
}
