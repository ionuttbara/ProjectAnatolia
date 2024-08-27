using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectAnatolia
{
    public partial class PingWindow : Window
    {
        private CancellationTokenSource _cancellationTokenSource;

        public PingWindow()
        {
            InitializeComponent();
        }

        private async void Ping_Click(object sender, RoutedEventArgs e)
        {
            string address = txtPingAddress.Text;

            if (string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Please enter a valid IP address or hostname.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            btnPing.IsEnabled = false;
            btnStop.IsEnabled = true;
            txtPingResult.Text = "Pinging...";

            _cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = _cancellationTokenSource.Token;

            try
            {
                await Task.Run(() => ContinuousPing(address, cancellationToken));
            }
            catch (OperationCanceledException)
            {
                txtPingResult.Text += "\nPing stopped.";
            }
            finally
            {
                btnPing.IsEnabled = true;
                btnStop.IsEnabled = false;
            }
        }

        private void ContinuousPing(string address, CancellationToken cancellationToken)
        {
            Ping ping = new Ping();

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    PingReply reply = ping.Send(address);
                    string result = reply.Status == IPStatus.Success
                        ? $"Reply from {reply.Address}: time={reply.RoundtripTime}ms"
                        : "Ping failed.";

                    Dispatcher.Invoke(() => txtPingResult.Text += result + Environment.NewLine);
                }
                catch (PingException ex)
                {
                    Dispatcher.Invoke(() => txtPingResult.Text += $"Ping failed: {ex.Message}" + Environment.NewLine);
                }

                Thread.Sleep(1000); // Wait for 1 second before next ping
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            _cancellationTokenSource?.Cancel();
        }
    }
}
