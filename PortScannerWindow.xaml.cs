using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProjectAnatolia
{
    public partial class PortScannerWindow : Window
    {
        public PortScannerWindow()
        {
            InitializeComponent();
            txtPorts.PreviewTextInput += TxtPorts_PreviewTextInput;
        }

        // Event handler for the "Scan Ports" button
        private async void ScanPorts_Click(object sender, RoutedEventArgs e)
        {
            string ipAddress = txtScanIP.Text;

            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                MessageBox.Show("Please enter an IP address.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            List<int> portList = chkAllPorts.IsChecked == true ? Enumerable.Range(1, 65535).ToList() : GetPortList();
            if (portList == null)
            {
                return;
            }

            progressBar.Visibility = Visibility.Visible;
            progressBar.Maximum = portList.Count;

            List<int> openPorts = await ScanPortsAsync(ipAddress, portList);

            DisplayScanResults(openPorts);
            progressBar.Visibility = Visibility.Collapsed;
        }

        // Get the list of ports to scan from the user input
        private List<int> GetPortList()
        {
            string ports = txtPorts.Text;

            if (string.IsNullOrWhiteSpace(ports))
            {
                MessageBox.Show("Please enter a list of ports.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }

            string[] portArray = ports.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<int> portList = new List<int>();

            foreach (var port in portArray)
            {
                if (int.TryParse(port.Trim(), out int portNumber))
                {
                    if (portNumber < 1 || portNumber > 65535)
                    {
                        MessageBox.Show($"Port number {portNumber} is out of range (1-65535).", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return null;
                    }
                    portList.Add(portNumber);
                }
                else
                {
                    MessageBox.Show($"Invalid port number: {port}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }

            return portList;
        }

        // Asynchronous method to scan a list of ports
        private async Task<List<int>> ScanPortsAsync(string ipAddress, List<int> ports)
        {
            List<int> openPorts = new List<int>();

            await Task.Run(() => Parallel.ForEach(ports, new ParallelOptions { MaxDegreeOfParallelism = 100 }, port =>
            {
                if (IsPortOpen(ipAddress, port))
                {
                    openPorts.Add(port);
                }
                Dispatcher.Invoke(() => progressBar.Value++);
            }));

            return openPorts;
        }

        // Check if a specific port is open
        private bool IsPortOpen(string ipAddress, int port)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    var connectTask = client.ConnectAsync(ipAddress, port);
                    if (connectTask.Wait(1000)) // 1 second timeout
                    {
                        return client.Connected;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        // Display the scan results
        private void DisplayScanResults(List<int> openPorts)
        {
            string resultMessage = openPorts.Count > 0
                ? "Open ports found:\n" + string.Join(", ", openPorts)
                : "No open ports found.";

            txtScanResult.Text = resultMessage;
        }

        // Input validation to restrict letters in the ports text box
        private void TxtPorts_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            return text.All(c => char.IsDigit(c) || c == ',');
        }

        // Event handlers to handle checking/unchecking the "Scan all ports" checkbox
        private void chkAllPorts_Checked(object sender, RoutedEventArgs e)
        {
            txtPorts.IsEnabled = false;
        }

        private void chkAllPorts_Unchecked(object sender, RoutedEventArgs e)
        {
            txtPorts.IsEnabled = true;
        }
        private void txtScanIP_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtScanIP.Text == "Enter IP Address")
            {
                txtScanIP.Text = string.Empty;
                txtScanIP.Foreground = Brushes.Black;
            }
        }

        private void txtScanIP_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtScanIP.Text))
            {
                txtScanIP.Text = "Enter IP Address";
                txtScanIP.Foreground = Brushes.Gray;
            }
        }

        private void txtPorts_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtPorts.Text == "Enter ports separated by commas")
            {
                txtPorts.Text = string.Empty;
                txtPorts.Foreground = Brushes.Black;
            }
        }

        private void txtPorts_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPorts.Text))
            {
                txtPorts.Text = "Enter ports separated by commas";
                txtPorts.Foreground = Brushes.Gray;
            }
        }

    }
}
