using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using System.Diagnostics;

namespace OneTabBrowser
{
    public partial class frmBrowser : Form
    {
        private List<string> history = new();
        private bool isDarkMode = false;

        // Use full name to avoid ambiguity
        private readonly System.Windows.Forms.Timer titleTimer = new System.Windows.Forms.Timer();
        private Stopwatch stopwatch = new Stopwatch();

        public frmBrowser()
        {
            InitializeComponent();

            this.Text = "OneTab Browser - Clean & Simple";
            this.Font = new Font("Segoe UI", 10F);
            this.BackColor = SystemColors.Window;
            webView.Source = new Uri("https://www.google.com");

            titleTimer.Interval = 1000; // 1 second
            titleTimer.Tick += TitleTimer_Tick;
        }

        private async void FrmBrowser_Load(object sender, EventArgs e)
        {
            try
            {
                LoadHistoryFromFile();

                await webView.EnsureCoreWebView2Async();
                webView.CoreWebView2InitializationCompleted += WebView_Initialized;
                webView.NavigationStarting += WebView_Navigating;
                webView.NavigationCompleted += WebView_Navigated;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"WebView2 init failed:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadHistoryFromFile()
        {
            string path = Path.Combine(Application.StartupPath, "ht.txt");
            if (File.Exists(path))
            {
                var lines = File.ReadAllLines(path);
                foreach (var line in lines)
                {
                    int dashIndex = line.IndexOf(" - ");
                    if (dashIndex >= 0)
                    {
                        string url = line[(dashIndex + 3)..].Trim();
                        if (!string.IsNullOrWhiteSpace(url) && !history.Contains(url))
                        {
                            history.Add(url);
                        }
                    }
                }
            }
        }

        private void WebView_Initialized(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            if (!e.IsSuccess)
            {
                MessageBox.Show($"WebView2 failed:\n{e.InitializationException.Message}", "Error");
                return;
            }

            webView.CoreWebView2.Navigate("https://www.google.com");
        }

        private void BtnGo_Click(object sender, EventArgs e)
        {
            lstSuggestions.Visible = false;
            string input = txtUrl.Text.Trim();

            if (!input.StartsWith("http", StringComparison.OrdinalIgnoreCase) &&
                !input.Contains(".") &&
                !Uri.IsWellFormedUriString(input, UriKind.Absolute))
            {
                input = "https://www.google.com/search?q=" + Uri.EscapeDataString(input);
            }
            else if (!input.StartsWith("http"))
            {
                input = "https://" + input;
            }

            webView.CoreWebView2.Navigate(input);

            // Check duplicates before saving
            if (!history.Contains(input))
            {
                history.Add(input);
                SaveHistoryToFile(input);
            }
        }

        private void SaveHistoryToFile(string url)
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "ht.txt");
                // Avoid duplicate lines in file (optional)
                if (!File.Exists(path) || !File.ReadLines(path).Any(line => line.Contains(url)))
                {
                    File.AppendAllText(path, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {url}{Environment.NewLine}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to write history: " + ex.Message);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (webView.CanGoBack)
                webView.GoBack();
        }

        private void BtnForward_Click(object sender, EventArgs e)
        {
            if (webView.CanGoForward)
                webView.GoForward();
        }

        private void WebView_Navigating(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            progressBar.Value = 10;
        }

        private void WebView_Navigated(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            txtUrl.Text = webView.Source?.ToString() ?? "";
            progressBar.Value = 100;

            Task.Delay(400).ContinueWith(_ =>
            {
                if (!this.IsDisposed)
                    this.Invoke(() => progressBar.Value = 0);
            });

            lstSuggestions.Visible = false;

            stopwatch.Restart();
            titleTimer.Start();
        }

        private void TitleTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = stopwatch.Elapsed;
            this.Text = $"OneTab Browser - Viewing for {elapsed.Hours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}";
        }

        private void txtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnGo_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
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

            var suggestions = history
                .Where(h => h.Contains(text, StringComparison.OrdinalIgnoreCase))
                .Take(5)
                .ToList();

            lstSuggestions.DataSource = suggestions;
            lstSuggestions.Visible = suggestions.Count > 0;
            if (lstSuggestions.Visible)
                lstSuggestions.BringToFront();
        }

        private void LstSuggestions_Click(object sender, EventArgs e)
        {
            if (lstSuggestions.SelectedItem is not string selected) return;

            txtUrl.Text = selected;
            lstSuggestions.Visible = false;
            txtUrl.Focus();
            txtUrl.SelectionStart = selected.Length;

            BtnGo_Click(sender, e);
        }

        private void BtnClearHistory_Click(object sender, EventArgs e)
        {
            // Clear in-memory history
            history.Clear();

            // Clear history file
            try
            {
                string path = Path.Combine(Application.StartupPath, "ht.txt");
                if (File.Exists(path))
                    File.WriteAllText(path, string.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to clear history: " + ex.Message);
                return;
            }

            // Clear suggestions and URL textbox
            lstSuggestions.Visible = false;
            //txtUrl.Clear();

            MessageBox.Show("History cleared successfully.", "Clear History", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                txtUrl.Clear();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
