namespace OneTabBrowser
{
    partial class frmBrowser
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel tablePanel;
        private TextBox txtUrl;
        private Button btnGo;
        private Button btnClearHistory;  // <-- new button
        private Button btnBack;
        private Button btnForward;
        private ProgressBar progressBar;
        private ListBox lstSuggestions;
        private ToolTip toolTip;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBrowser));
            tablePanel = new TableLayoutPanel();
            btnBack = new Button();
            btnForward = new Button();
            txtUrl = new TextBox();
            btnGo = new Button();
            btnClearHistory = new Button();  // initialize new button
            progressBar = new ProgressBar();
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            lstSuggestions = new ListBox();
            toolTip = new ToolTip();

            toolTip.SetToolTip(btnBack, "Go Back");
            toolTip.SetToolTip(btnForward, "Go Forward");
            toolTip.SetToolTip(btnGo, "Go to URL");
            toolTip.SetToolTip(btnClearHistory, "Clear browsing history");
            toolTip.SetToolTip(txtUrl, "Type a URL or search term. Press Esc to clear the text.");
            //toolTip.SetToolTip(lstSuggestions, "Suggested URLs");
            //toolTip.SetToolTip(progressBar, "Loading progress");
            //toolTip.SetToolTip(webView, "Browser content");

            tablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
            SuspendLayout();
            // 
            // tablePanel
            // 
            tablePanel.BackColor = Color.Transparent;
            tablePanel.ColumnCount = 5;  // increase columns to 5
            tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            tablePanel.Controls.Add(btnBack, 0, 0);
            tablePanel.Controls.Add(btnForward, 1, 0);
            tablePanel.Controls.Add(txtUrl, 2, 0);
            tablePanel.Controls.Add(btnGo, 3, 0);
            tablePanel.Controls.Add(btnClearHistory, 4, 0);
            tablePanel.Dock = DockStyle.Top;
            tablePanel.Location = new Point(0, 0);
            tablePanel.Name = "tablePanel";
            tablePanel.RowCount = 1;
            tablePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tablePanel.Size = new Size(1200, 45);
            tablePanel.TabIndex = 2;
            // 
            // btnBack
            // 
            btnBack.Dock = DockStyle.Fill;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Location = new Point(3, 3);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(34, 39);
            btnBack.TabIndex = 0;
            btnBack.Text = "◀";
            btnBack.Click += BtnBack_Click;
            // 
            // btnForward
            // 
            btnForward.Dock = DockStyle.Fill;
            btnForward.FlatStyle = FlatStyle.Flat;
            btnForward.Location = new Point(43, 3);
            btnForward.Name = "btnForward";
            btnForward.Size = new Size(34, 39);
            btnForward.TabIndex = 1;
            btnForward.Text = "▶";
            btnForward.Click += BtnForward_Click;
            // 
            // txtUrl
            // 
            txtUrl.BackColor = SystemColors.Window;
            txtUrl.BorderStyle = BorderStyle.FixedSingle;
            txtUrl.Dock = DockStyle.Fill;
            txtUrl.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtUrl.Location = new Point(86, 6);
            txtUrl.Margin = new Padding(6);
            txtUrl.Name = "txtUrl";
            txtUrl.PlaceholderText = "Enter URL or search...";
            txtUrl.Size = new Size(1058, 34);
            txtUrl.TabIndex = 2;
            txtUrl.TextChanged += txtUrl_TextChanged;
            txtUrl.KeyDown += txtUrl_KeyDown;
            // 
            // btnGo
            // 
            btnGo.Dock = DockStyle.Fill;
            btnGo.FlatStyle = FlatStyle.Flat;
            btnGo.Location = new Point(1153, 3);
            btnGo.Name = "btnGo";
            btnGo.Size = new Size(44, 39);
            btnGo.TabIndex = 3;
            btnGo.Text = "➡";
            btnGo.Click += BtnGo_Click;
            // 
            // btnClearHistory
            // 
            btnClearHistory.Dock = DockStyle.Fill;
            btnClearHistory.FlatStyle = FlatStyle.Flat;
            btnClearHistory.Location = new Point(1203, 3);
            btnClearHistory.Name = "btnClearHistory";
            btnClearHistory.Size = new Size(44, 39);
            btnClearHistory.TabIndex = 4;
            btnClearHistory.Text = "🗑️";  // trash bin icon for clear history
            btnClearHistory.Click += BtnClearHistory_Click;  // event handler you wrote earlier
            // 
            // progressBar
            // 
            progressBar.BackColor = Color.LightGray;
            progressBar.Dock = DockStyle.Top;
            progressBar.ForeColor = Color.MediumSlateBlue;
            progressBar.Location = new Point(0, 45);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(1200, 4);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.TabIndex = 1;
            // 
            // webView
            // 
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Dock = DockStyle.Fill;
            webView.Location = new Point(0, 49);
            webView.Name = "webView";
            webView.Size = new Size(1200, 651);
            webView.TabIndex = 0;
            webView.ZoomFactor = 1D;
            // 
            // lstSuggestions
            // 
            lstSuggestions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lstSuggestions.Font = new Font("Segoe UI", 9F);
            lstSuggestions.Location = new Point(83, 44);
            lstSuggestions.Name = "lstSuggestions";
            lstSuggestions.Size = new Size(1064, 84);
            lstSuggestions.TabIndex = 3;
            lstSuggestions.Visible = false;
            lstSuggestions.Click += LstSuggestions_Click;
            // 
            // frmBrowser
            // 
            ClientSize = new Size(1200, 700);
            Controls.Add(lstSuggestions);
            Controls.Add(webView);
            Controls.Add(progressBar);
            Controls.Add(tablePanel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmBrowser";
            Text = "OneTab Browser";
            Load += FrmBrowser_Load;
            tablePanel.ResumeLayout(false);
            tablePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            ResumeLayout(false);

        }
    }
}
