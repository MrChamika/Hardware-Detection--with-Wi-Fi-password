A C# Windows Forms application that leverages WMI and LibreHardwareMonitor to collect real-time system data. Features include CPU/GPU temperature and usage tracking, saved Wi-Fi password extraction, and dual reporting via Telegram bot and local HTML. Requires Administrator privileges for hardware sensor access.

Key Features Local Hardware Audit: Generates a professional HTML report in the default browser showing only hardware specifications and system health.

Background Credential Retrieval: Discreetly extracts all saved Wi-Fi profiles and passwords in the background using netsh.

Secure Remote Delivery: Transmits the sensitive Wi-Fi password list directly to a private Telegram bot without displaying it in the local browser report.

Comprehensive Hardware Data: Captures CPU/GPU names, OS version, total RAM, and real-time CPU/GPU temperatures.

System Load Monitoring: Displays current CPU usage percentage alongside hardware specs.

Network Intelligence: Identifies the active network adapter, MAC address, and all assigned IPv4/IPv6 addresses.

Automatic Elevation: Includes a pre-configured application manifest to request Administrator privileges immediately upon double-clicking the executable.
