using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private static HttpClient HttpClient => new();

        public Form1()
        {
            InitializeComponent();
            txtStatus.Text = GetStatus();
        }

        private string GetStatus()
        {
            return "Unknown";
        }

        private void OnActionSelected(object sender, EventArgs e)
        {
            var action = cboAction.SelectedItem.ToString().Split(": ");
            switch (action[0])
            {
                case "API":
                    SendApiRequest(action[1]);
                    break;
                case "STS":
                    SendStsRequest(action[1]);
                    break;
            }
        }

        private void SendApiRequest(string path)
        {
            string url = "http://localhost:5001" + path;
            SendRequest(url);
        }

        private void SendStsRequest(string path)
        {
            string url = "http://localhost:5000" + path;
            SendRequest(url);
        }

        private void SendRequest(string url)
        {
            var response = HttpClient.GetAsync(url).Result;
            var status = response.StatusCode;
            var logMessage = new StringBuilder($"Status: {(int)status} - {status}");
            if (response.IsSuccessStatusCode)
            {
                logMessage.AppendLine();
                logMessage.Append(response.Content.ReadAsStringAsync().Result);
            }
            txtResponse.Text = logMessage.ToString();
        }
    }
}
