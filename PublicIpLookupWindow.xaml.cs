using System.Net;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace ProjectAnatolia
{
    public partial class PublicIpLookupWindow : Window
    {
        public PublicIpLookupWindow()
        {
            InitializeComponent();
        }

        private async void LookupPublicIP_Click(object sender, RoutedEventArgs e)
        {
            txtPublicIPResult.Text = "Fetching public IP...";
            using (WebClient client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                string json = await client.DownloadStringTaskAsync("http://ip-api.com/json");
                JObject data = JObject.Parse(json);
                string ip = data["query"].ToString();
                string isp = data["isp"].ToString();
                string city = data["city"].ToString();
                string region = data["regionName"].ToString();
                string country = data["country"].ToString();

                txtPublicIPResult.Text = $"IP: {ip}\nISP: {isp}\nLocation: {city}, {region}, {country}";
            }
        }
    }
}
