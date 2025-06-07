using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace OneTabBrowser
{
    public partial class frmBrowser : Form
    {
        private List<string> history = new List<string>();
        private bool isDarkMode = false;

        public frmBrowser()
        {
            InitializeComponent();

            this.Text = "OneTab Browser - Clean & Simple";
            this.Font = new Font("Segoe UI", 20F);
            //this.Icon = new Icon(SystemIcons.Application, 40, 40);

            //this.FormBorderStyle = FormBorderStyle.None;
            this.Padding = new Padding(0, 10, 0, 0); // Add top space for your title bar
            this.BackColor = SystemColors.Window;

            // Event bindings
            this.Load += FrmBrowser_Load;
            btnGo.Click += BtnGo_Click;
            btnBack.Click += BtnBack_Click;
            btnForward.Click += BtnForward_Click;

            foreach (Control ctrl in tablePanel.Controls)
            {
                ctrl.Font = new Font("Segoe UI", 10F);
                ctrl.BackColor = Color.WhiteSmoke;
                ctrl.ForeColor = Color.Black;
                ctrl.Padding = new Padding(2);
                ctrl.Margin = new Padding(2);
            }

            webView.Source = new Uri("https://www.google.com");
        }

        private async void FrmBrowser_Load(object sender, EventArgs e)
        {
            try
            {
                await webView.EnsureCoreWebView2Async();
                webView.CoreWebView2InitializationCompleted += WebView_CoreWebView2InitializationCompleted;
                webView.NavigationStarting += WebView_NavigationStarting;
                webView.NavigationCompleted += WebView_NavigationCompleted;
            }
            catch (Exception ex)
            {
                MessageBox.Show("WebView2 initialization failed: " + ex.Message);
            }
        }

        private void WebView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                webView.CoreWebView2.Navigate("https://www.google.com");
            }
            else
            {
                MessageBox.Show("Failed to initialize WebView2: " + e.InitializationException.Message);
            }
        }

        private void BtnGo_Click(object sender, EventArgs e)
        {
            string input = txtUrl.Text.Trim();

            // Check if input is a URL
            if (!input.StartsWith("http", StringComparison.OrdinalIgnoreCase) &&
                !input.Contains(".") && !Uri.IsWellFormedUriString(input, UriKind.Absolute))
            {
                // Treat as search query
                input = "https://www.google.com/search?q=" + Uri.EscapeDataString(input);
            }
            else if (!input.StartsWith("http"))
            {
                // Add https if it's a URL without scheme
                input = "https://" + input;
            }

            webView.CoreWebView2.Navigate(input);
            history.Add(input);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (webView.CanGoBack)
            {
                webView.GoBack();
            }
        }

        private void BtnForward_Click(object sender, EventArgs e)
        {
            if (webView.CanGoForward)
            {
                webView.GoForward();
            }
        }

        private void BtnDarkMode_Click(object sender, EventArgs e)
        {
            isDarkMode = !isDarkMode;

            // Change form and control colors
            this.BackColor = isDarkMode ? Color.Black : Color.White;
            tablePanel.BackColor = isDarkMode ? Color.FromArgb(30, 30, 30) : Color.LightGray;

            foreach (Control ctrl in tablePanel.Controls)
            {
                ctrl.ForeColor = isDarkMode ? Color.White : Color.Black;
                ctrl.BackColor = isDarkMode ? Color.FromArgb(45, 45, 45) : Color.White;
            }

            // Apply page background via JavaScript
            if (webView?.CoreWebView2 != null)
            {
                string bgColor = isDarkMode ? "#1e1e1e" : "#ffffff";
                webView.CoreWebView2.ExecuteScriptAsync($"document.body.style.background = '{bgColor}';");
            }
        }

        private void WebView_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            progressBar.Value = 10;
        }

        private void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            txtUrl.Text = webView.Source?.ToString() ?? "";
            progressBar.Value = 100;

            Task.Delay(400).ContinueWith(_ =>
            {
                if (!this.IsDisposed)
                {
                    this.Invoke(() => progressBar.Value = 0);
                }
            });
        }

        private void txtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnGo_Click(sender, e); // Simulate Go button click
                e.Handled = true;
                e.SuppressKeyPress = true; // Prevent "ding" sound
            }
        }

        private void txtUrl_TextChanged(object sender, EventArgs e)
        {
            string text = txtUrl.Text.Trim();

            if (string.IsNullOrEmpty(text))
            {
                lstSuggestions.Visible = false;
                return;
            }

            var historyMatches = history
                .Where(h => h.Contains(text, StringComparison.OrdinalIgnoreCase))
                .Take(5);

            // Get Google suggestions using Web API (free, no key needed)
            // But since no internet call here, I provide a simple example for history only.

            // Combine results - history first, then Google suggestions
            var suggestions = historyMatches.ToList();

            // TODO: You can call Google suggest API if you want (advanced)

            if (suggestions.Count > 0)
            {
                lstSuggestions.DataSource = suggestions;
                lstSuggestions.Visible = true;
                lstSuggestions.BringToFront();
            }
            else
            {
                lstSuggestions.Visible = false;
            }
        }

        private void LstSuggestions_Click(object sender, EventArgs e)
        {
            if (lstSuggestions.SelectedItem != null)
            {
                txtUrl.Text = lstSuggestions.SelectedItem.ToString();
                lstSuggestions.Visible = false;
                txtUrl.Focus();
                txtUrl.SelectionStart = txtUrl.Text.Length;

                BtnGo_Click(sender, e); // Simulate Go button click

            }
        }
    }
}
