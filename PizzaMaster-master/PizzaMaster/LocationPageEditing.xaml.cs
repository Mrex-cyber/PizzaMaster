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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PizzaMaster
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LocationPageEditing : Page
    {
        Location location;
        public LocationPageEditing()
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
                    location = db.Locations.FirstOrDefault(l => l.Id == id);
                }
            }

            if (location != null)
            {
                headerBlock.Text = "Editing location";
                nameBox.Text = location.City;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new LocationsContext())
            {
                if (location != null)
                {
                    location.City = nameBox.Text;
                    db.Locations.Update(location);
                }
                else
                {
                    db.Locations.Add(new Location { City = nameBox.Text });
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
