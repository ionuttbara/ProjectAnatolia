using System.Linq;
using System.Net.NetworkInformation;
using System.Management;
using System.Windows;

namespace ProjectAnatolia
{
    public partial class MacLookupWindow : Window
    {
        public MacLookupWindow()
        {
            InitializeComponent();
            LoadNetworkAdapters();
        }

        private void LoadNetworkAdapters()
        {
            var adapters = NetworkInterface.GetAllNetworkInterfaces();
            cmbNetworkAdapters.ItemsSource = adapters.Select(a => a.Name).ToList();
        }

        private void GetMac_Click(object sender, RoutedEventArgs e)
        {
            string selectedAdapter = cmbNetworkAdapters.SelectedItem?.ToString();
            if (selectedAdapter == null)
            {
                txtMacResult.Text = "Please select a network adapter.";
                return;
            }

            var adapter = NetworkInterface.GetAllNetworkInterfaces()
                .FirstOrDefault(a => a.Name == selectedAdapter);

            if (adapter != null)
            {
                string formattedMac = string.Join("-", adapter.GetPhysicalAddress().GetAddressBytes().Select(b => b.ToString("X2")));
                txtMacResult.Text = $"MAC Address: {formattedMac}";
            }
            else
            {
                txtMacResult.Text = "Adapter not found.";
            }
        }


    }
}
