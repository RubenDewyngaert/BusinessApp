using FestivalApp.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FestivalApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = await client.GetAsync("http://localhost:26102/api/band");
            if (response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();
                DataContractSerializer dxml = new DataContractSerializer(typeof(List<Band>));
                List<Band> list = dxml.ReadObject(stream) as List<Band>;
                bandslist.ItemsSource = list;
            }
        }

        private void bandslist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Band band = (Band)(bandslist.SelectedItem);
            txtBand.Text = band.Name;
            txtBeschrijving.Text = "Beschrijving: " + band.Description;
            txtFacebook.Text = "Facebook: " + band.Facebook;
            txtTwitter.Text = "Twitter: " + band.Twitter;

            BitmapImage img = new BitmapImage(); 
            img.UriSource = new Uri(this.BaseUri, "Images/" + band.Name +".jpg");
            imgBand.Source = img;
        }


    }
}
