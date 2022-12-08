using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PizzaMaster
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductsListPage : Page
    {
        ObservableCollection<Product> products;
        List<Location> locations;
        public ProductsListPage()
        {
            this.InitializeComponent();

            this.Loaded += ProductsListPage_Loaded;
        }

        private void ProductsListPage_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new LocationsContext())
            {
                products = new ObservableCollection<Product>(db.Products.Include(pr => pr.Location).ToList());
                locations = db.Locations.ToList();
            }

            locationsList.ItemsSource = locations;
            productsList.ItemsSource = products;

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Location location = locationsList.SelectedItem as Location;
            if (location != null)
            {
                Product product = new Product()
                {
                    Name = nameBox.Text,
                    SellingPrice = Double.Parse(sellingPrice.Text),
                    BuyingPrice = Double.Parse(buyingPrice.Text),
                    Amount = Int32.Parse(amountBox.Text),
                    Location = location,
                };

                using (var db = new LocationsContext())
                {
                    db.Locations.Attach(location);
                    db.Products.Add(product);
                    if (db.SaveChanges() > 0)
                    {
                        products.Add(product);
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
