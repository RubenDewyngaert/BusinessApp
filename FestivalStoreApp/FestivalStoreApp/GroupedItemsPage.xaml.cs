using FestivalApp.model;
using FestivalStoreApp.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Grouped Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234231

namespace FestivalStoreApp
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class GroupedItemsPage : FestivalStoreApp.Common.LayoutAwarePage
    {
        public GroupedItemsPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var sampleDataGroups = SampleDataSource.GetGroups((String)navigationParameter);
            this.DefaultViewModel["Groups"] = sampleDataGroups;
        }

        /// <summary>
        /// Invoked when a group header is clicked.
        /// </summary>
        /// <param name="sender">The Button used as a group header for the selected group.</param>
        /// <param name="e">Event data that describes how the click was initiated.</param>
        void Header_Click(object sender, RoutedEventArgs e)
        {
            // Determine what group the Button instance represents
            var group = (sender as FrameworkElement).DataContext;

            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            this.Frame.Navigate(typeof(GroupDetailPage), ((SampleDataGroup)group).UniqueId);
        }

        /// <summary>
        /// Invoked when an item within a group is clicked.
        /// </summary>
        /// <param name="sender">The GridView (or ListView when the application is snapped)
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
            this.Frame.Navigate(typeof(ItemDetailPage), itemId);
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Data ophalen met JSON
            //HttpClient client = new HttpClient();
            //HttpResponseMessage response = await client.GetAsync("http://api.irail.be/stations/?format=json");
            //string result = await response.Content.ReadAsStringAsync();

            //Station.Stations = new ObservableCollection<Station>();

            //JsonObject obj = JsonObject.Parse(result);
            //JsonArray stations = obj["station"].GetArray() as JsonArray;

            //foreach (JsonValue val in stations)
            //{
            //    Station s = new Station() { 
            //        ID =  val.GetObject().GetNamedString("id"),
            //        Name =  val.GetObject().GetNamedString("name") 
            //    };
            //    Station.Stations.Add(s);
            //}
            //stationslist.ItemsSource = Station.Stations;

            //Data ophalen met XML
            XmlDocument xmlDoc = await XmlDocument.LoadFromUriAsync(new Uri("http://api.irail.be/stations/"));
            XmlNodeList stations = xmlDoc.GetElementsByTagName("station");
            Band.Bands = new ObservableCollection<Band>();

            for (int i = 0; i < stations.Count; i++)
            {
                Band s = new Band() { ID = stations[i].Attributes[0].InnerText, Name = stations[i].InnerText };
                Band.Bands.Add(s);
            }
            bandslist.ItemsSource = Band.Bands;
        }
    }
}
