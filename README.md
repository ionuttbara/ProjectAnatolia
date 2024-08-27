# NetworkToolkit

IBBE NetworkToolkit or Internally ("Project Anatolia") is a simple Windows App used by IBBE Software Service for checking network configurations and troubleshoting. It provides various network-related utilities, such as network scanning, pinging, port scanning, public IP lookup, MAC address lookup, and speed testing. The application is designed with a user-friendly interface, allowing users to perform network operations easily.

## Features

- **Network Scanning**
  - **Automatic IP Range Scan**: Scans devices on the local network within an automatically determined IP range.
  - **Manual IP Range Scan**: Allows users to manually input an IP range to scan for active devices.
  
- **Ping Utility**
  - Ping specific IP addresses or websites to check connectivity and response time.

- **Port Scanner**
  - Scan open ports on the current device or another device in the network.

- **Public IP Lookup**
  - Displays information about the public IP address, including ISP details and location.

- **MAC Address Lookup**
  - Shows the MAC address of the selected network adapter, formatted as `AA-BB-CC-DD-EE-FF`.
  - Displays additional information about the network adapter, such as driver version and driver date.

- **Speed Test**
  - Measures download and upload speeds by connecting to a nearby server using the Speedtest CLI provided by Ookla.

## Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/yourusername/NetworkToolkit.git
   cd NetworkToolkit
   ```
2. **Open the project**:

  - Open the solution file (NetworkToolkit.sln) in Visual Studio.

3. **Build the project**:

  - Build the project using Visual Studio to restore NuGet packages and compile the code.

4. **Run the application**.

- **Or Download from <a href="https://github.com/ionuttbara/ProjectAnatolia/releases">Releases </a> Section.**

## Usage

- **Network Scanning**

- **Automatic IP Range**: The automatic range is determined based on the local IP address of the machine and is displayed in the format 192.168.1.1 - 192.168.1.254. Click "Scan Automatic Range" to start scanning.

- **Manual IP Range**: Enter the start and end IP addresses using the provided text boxes, and click "Scan Manual Range" to start scanning.

- **Ping Utility**
Enter the IP address or website URL you wish to ping, and click "Ping". The result will display the latency.

- **Port Scanner**
Enter the target IP address and port range, and click "Scan Ports". The application will list the open ports.

- **Public IP Lookup**
Click "Lookup Public IP" to display your public IP address, along with ISP details and location. Ensure proper text encoding is installed to display special characters correctly.

- **MAC Address Lookup**
Select a network adapter from the list, and click "Lookup MAC Address". The MAC address will be displayed in AA-BB-CC-DD-EE-FF format along with driver details.

- **Speed Test**
The speed test uses Ookla’s Speedtest CLI to measure download and upload speeds. Results include bandwidth in Mbps, ping (latency), and the timestamp of the test.

## Troubleshooting

**Known Issues**

- **Driver Information Not Found:** If the driver version or date is not displayed for a network adapter, it may be due to the adapter's WMI properties not being available. Working for an API to solve this.


## Contributing
Contributions are welcome! Please fork this repository, make your changes, and submit a pull request.

## Acknowledgments
Thanks to Ookla for providing the Speedtest CLI.
Inspired by the need for a simple and efficient network toolkit for everyday use.