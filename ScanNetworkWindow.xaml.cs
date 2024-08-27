using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProjectAnatolia
{
    public partial class ScanNetworkWindow : Window
    {
        public ScanNetworkWindow()
        {
            InitializeComponent();
            SetAutomaticRange();
        }

        private void SetAutomaticRange()
        {
            var localIP = GetLocalIPAddress();
            if (localIP != null)
            {
                string baseIP = localIP.Substring(0, localIP.LastIndexOf('.') + 1);  // e.g., "192.168.1."
                txtAutoRange.Text = $"{baseIP}1 - {baseIP}254";
            }
        }

        private async void AutoScan_Click(object sender, RoutedEventArgs e)
        {
            listBoxResults.Items.Clear();
            ToggleButtons(false);
            progressBar.Visibility = Visibility.Visible;

            var localIP = GetLocalIPAddress();
            if (localIP == null)
            {
                MessageBox.Show("Unable to determine the local IP address.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ToggleButtons(true);
                return;
            }

            string baseIP = localIP.Substring(0, localIP.LastIndexOf('.') + 1);  // e.g., "192.168.1."
            await ScanIPRangeAsync(baseIP + "1", baseIP + "254");

            ToggleButtons(true);
        }

        private async void ManualScan_Click(object sender, RoutedEventArgs e)
        {
            listBoxResults.Items.Clear();
            ToggleButtons(false);
            progressBar.Visibility = Visibility.Visible;

            string startIP = $"{txtStartIP1.Text}.{txtStartIP2.Text}.{txtStartIP3.Text}.{txtStartIP4.Text}";
            string endIP = $"{txtEndIP1.Text}.{txtEndIP2.Text}.{txtEndIP3.Text}.{txtEndIP4.Text}";

            if (!IsValidIPAddress(startIP) || !IsValidIPAddress(endIP))
            {
                MessageBox.Show("Please enter valid IP addresses.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                ToggleButtons(true);
                return;
            }

            await ScanIPRangeAsync(startIP, endIP);
            ToggleButtons(true);
        }

        private async Task ScanIPRangeAsync(string startIP, string endIP)
        {
            string[] startOctets = startIP.Split('.');
            string[] endOctets = endIP.Split('.');
            int start = int.Parse(startOctets[3]);
            int end = int.Parse(endOctets[3]);
            string baseIP = string.Join(".", startOctets[0], startOctets[1], startOctets[2]) + ".";

            progressBar.Maximum = end - start + 1;
            progressBar.Value = 0;

            for (int i = start; i <= end; i++)
            {
                string currentIP = baseIP + i;
                if (await PingIPAddressAsync(currentIP))
                {
                    listBoxResults.Items.Add(currentIP);
                }
                progressBar.Value++;
            }

            if (listBoxResults.Items.Count == 0)
            {
                listBoxResults.Items.Add("No active IPs found.");
            }
        }

        private async Task<bool> PingIPAddressAsync(string ipAddress)
        {
            Ping ping = new Ping();
            try
            {
                PingReply reply = await ping.SendPingAsync(ipAddress, 100);
                return reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                return false;
            }
        }

        private bool IsValidIPAddress(string ipAddress)
        {
            return IPAddress.TryParse(ipAddress, out _);
        }

        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return null;
        }

        private void ToggleButtons(bool isEnabled)
        {
            btnAutoScan.IsEnabled = isEnabled;
            btnManualScan.IsEnabled = isEnabled;
            progressBar.Visibility = isEnabled ? Visibility.Hidden : Visibility.Visible;
        }
    }
}
