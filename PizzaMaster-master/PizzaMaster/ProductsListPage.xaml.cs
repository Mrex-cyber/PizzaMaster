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
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PizzaMaster
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductsListPage : Page
    {
        ObservableCollection<Product> products;

        private double sumOfEarning;
        public ProductsListPage()
        {
            this.InitializeComponent();

            this.Loaded += ProductsListPage_Loaded;

        }

        private void ProductsListPage_Loaded(object sender, RoutedEventArgs e)
        {
            titleText.Text = MainPage.selectedLocation.ToString();
            using (var db = new LocationsContext())
            {
                if (MainPage.selectedLocation != null)
                {
                    products = new ObservableCollection<Product>(db.Products
                        .Include(pr => pr.Location)
                        .Where<Product>(pr => pr.Location == MainPage.selectedLocation)
                        .ToList());

                    sumOfEarning = 0;
                    foreach (var item in products)
                    {
                        sumOfEarning += item.Earnings;
                    }
                    earnings.Text = "All earnings: " + sumOfEarning.ToString();
                }
            }
            productsList.ItemsSource = products;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (MainPage.selectedLocation != null)
            {
                Product product = new Product()
                {
                    Name = nameBox.Text,
                    SellingPrice = Int32.Parse(sellingPrice.Text),
                    BuyingPrice = Int32.Parse(buyingPrice.Text),
                    Amount = Int32.Parse(amountBox.Text),
                    Location = MainPage.selectedLocation,
                };
                
                using (var db = new LocationsContext())
                {
                    EditAndShowEarnings(product);

                    db.Locations.Attach(MainPage.selectedLocation); 
                    db.Products.Add(product);
                    MainPage.selectedLocation.Products.Add(product);


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

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (productsList.SelectedItem != null)
            {
                Product product = productsList.SelectedItem as Product;
                if (product != null)
                {
                    Frame.Navigate(typeof(ProductsListPage), product.Id);
                }
            }            
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (productsList.SelectedItem != null)
            {
                Product product = productsList.SelectedItem as Product;
                if (product != null)
                {
                    using (LocationsContext db = new LocationsContext())
                    {
                        EditAndShowEarnings(product);

                        db.Products.Remove(product);
                        db.SaveChanges();
                        productsList.ItemsSource = db.Products.ToList();
                    }
                }
            }
        }

        private void EditAndShowEarnings(Product editProduct)
        {
            sumOfEarning -= editProduct.Earnings;
            earnings.Text = "All earnings: " + sumOfEarning.ToString();
        }

    }
}
