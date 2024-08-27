using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectAnatolia
{
    public partial class SpeedTestWindow : Window
    {
        public SpeedTestWindow()
        {
            InitializeComponent();
        }

        private async void RunSpeedTest_Click(object sender, RoutedEventArgs e)
        {
            string speedTestCLIPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "speedtest.exe");

            if (!File.Exists(speedTestCLIPath))
            {
                txtSpeedTestResult.Text = "Speedtest CLI executable not found.";
                return;
            }

            txtSpeedTestResult.Text = "Running speed test...";

            var speedTestResult = await RunSpeedTestAsync(speedTestCLIPath);

            if (speedTestResult != null)
            {
                DisplaySpeedTestResult(speedTestResult);
            }
            else
            {
                txtSpeedTestResult.Text = "Failed to retrieve speed test results.";
            }
        }

        private async Task<SpeedTestResult> RunSpeedTestAsync(string speedTestCLIPath)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = speedTestCLIPath,
                Arguments = "--format=json",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = psi })
            {
                process.Start();
                string result = await process.StandardOutput.ReadToEndAsync();
                process.WaitForExit();

                try
                {
                    return JsonConvert.DeserializeObject<SpeedTestResult>(result);
                }
                catch (JsonException)
                {
                    return null;
                }
            }
        }

        private void DisplaySpeedTestResult(SpeedTestResult result)
        {
            if (result == null)
            {
                txtSpeedTestResult.Text = "No results found.";
                return;
            }

            double downloadMbps = result.Download.Bandwidth / 125000.0; // Convert from bits/sec to MB/s
            double uploadMbps = result.Upload.Bandwidth / 125000.0; // Convert from bits/sec to MB/s

            string formattedResult = $"ISP: {result.ISP}\n" +
                                     $"Timestamp: {result.Timestamp}\n" +
                                     $"Ping: {result.Ping.Latency} ms (Low: {result.Ping.Low} ms, High: {result.Ping.High} ms)\n" +
                                     $"Download: {downloadMbps:F2} MB/s\n" +
                                     $"Upload: {uploadMbps:F2} MB/s\n";

            txtSpeedTestResult.Text = formattedResult;
        }
    }

    public class SpeedTestResult
    {
        [JsonProperty("isp")]
        public string ISP { get; set; }

        [JsonProperty("ping")]
        public PingResult Ping { get; set; }

        [JsonProperty("download")]
        public SpeedResult Download { get; set; }

        [JsonProperty("upload")]
        public SpeedResult Upload { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }

    public class PingResult
    {
        [JsonProperty("latency")]
        public double Latency { get; set; }

        [JsonProperty("low")]
        public double Low { get; set; }

        [JsonProperty("high")]
        public double High { get; set; }
    }

    public class SpeedResult
    {
        [JsonProperty("bandwidth")]
        public double Bandwidth { get; set; } // in bits per second
    }
}
