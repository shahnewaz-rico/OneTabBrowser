namespace OneTabBrowser
{
    partial class frmBrowser
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel tablePanel;
        private TextBox txtUrl;
        private Button btnGo;
        private Button btnBack;
        private Button btnForward;
        private ProgressBar progressBar;
        private ListBox lstSuggestions;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBrowser));
            tablePanel = new TableLayoutPanel();
            btnBack = new Button();
            btnForward = new Button();
            txtUrl = new TextBox();
            btnGo = new Button();
            progressBar = new ProgressBar();
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            lstSuggestions = new ListBox();
            tablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
            SuspendLayout();
            // 
            // tablePanel
            // 
            tablePanel.BackColor = Color.LightGray;
            tablePanel.ColumnCount = 4;
            tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            tablePanel.Controls.Add(btnBack, 0, 0);
            tablePanel.Controls.Add(btnForward, 1, 0);
            tablePanel.Controls.Add(txtUrl, 2, 0);
            tablePanel.Controls.Add(btnGo, 3, 0);
            tablePanel.Dock = DockStyle.Top;
            tablePanel.Location = new Point(0, 0);
            tablePanel.Name = "tablePanel";
            tablePanel.RowCount = 1;
            tablePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tablePanel.Size = new Size(1200, 40);
            tablePanel.TabIndex = 2;
            // 
            // btnBack
            // 
            btnBack.Dock = DockStyle.Fill;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Location = new Point(3, 3);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(34, 34);
            btnBack.TabIndex = 0;
            btnBack.Text = "◀";
            // 
            // btnForward
            // 
            btnForward.Dock = DockStyle.Fill;
            btnForward.FlatStyle = FlatStyle.Flat;
            btnForward.Location = new Point(43, 3);
            btnForward.Name = "btnForward";
            btnForward.Size = new Size(34, 34);
            btnForward.TabIndex = 1;
            btnForward.Text = "▶";
            // 
            // txtUrl
            // 
            txtUrl.Dock = DockStyle.Fill;
            txtUrl.Font = new Font("Segoe UI", 10F);
            txtUrl.Location = new Point(83, 3);
            txtUrl.Name = "txtUrl";
            txtUrl.PlaceholderText = "Enter URL or search...";
            txtUrl.Size = new Size(1064, 30);
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
            btnGo.Size = new Size(44, 34);
            btnGo.TabIndex = 3;
            btnGo.Text = "➡";
            // 
            // progressBar
            // 
            progressBar.BackColor = Color.LightGray;
            progressBar.Dock = DockStyle.Top;
            progressBar.ForeColor = Color.MediumSlateBlue;
            progressBar.Location = new Point(0, 40);
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
            webView.Location = new Point(0, 44);
            webView.Name = "webView";
            webView.Size = new Size(1200, 656);
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
            tablePanel.ResumeLayout(false);
            tablePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            ResumeLayout(false);

        }
    }
}
