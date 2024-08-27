using ProjectAnatolia;
using System.Windows;

namespace ProjectAnatolia
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ScanNetwork_Click(object sender, RoutedEventArgs e)
        {
            ScanNetworkWindow scanNetworkWindow = new ScanNetworkWindow();
            scanNetworkWindow.ShowDialog();
        }

        private void Ping_Click(object sender, RoutedEventArgs e)
        {
            PingWindow pingWindow = new PingWindow();
            pingWindow.ShowDialog();
        }

        private void PortScanner_Click(object sender, RoutedEventArgs e)
        {
            PortScannerWindow portScannerWindow = new PortScannerWindow();
            portScannerWindow.ShowDialog();
        }

        private void PublicIpLookup_Click(object sender, RoutedEventArgs e)
        {
            PublicIpLookupWindow publicIpLookupWindow = new PublicIpLookupWindow();
            publicIpLookupWindow.ShowDialog();
        }

        private void MacLookup_Click(object sender, RoutedEventArgs e)
        {
            MacLookupWindow macLookupWindow = new MacLookupWindow();
            macLookupWindow.ShowDialog();
        }

        private void SpeedTest_Click(object sender, RoutedEventArgs e)
        {
            SpeedTestWindow speedTestWindow = new SpeedTestWindow();
            speedTestWindow.ShowDialog();
        }
    }
}
