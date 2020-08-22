namespace Blurts
{
  partial class ContactDlg
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContactDlg));
      this.cancelBtn = new System.Windows.Forms.Button();
      this.minimizeBtn = new System.Windows.Forms.Button();
      this.closeBtn = new System.Windows.Forms.Button();
      this.webBrowser1 = new System.Windows.Forms.WebBrowser();
      this.downloadBtn = new System.Windows.Forms.Button();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.deleteAllContactsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // cancelBtn
      // 
      this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelBtn.Location = new System.Drawing.Point(187, 231);
      this.cancelBtn.Name = "cancelBtn";
      this.cancelBtn.Size = new System.Drawing.Size(75, 23);
      this.cancelBtn.TabIndex = 1;
      this.cancelBtn.Text = "Cancel";
      this.cancelBtn.UseVisualStyleBackColor = true;
      // 
      // minimizeBtn
      // 
      this.minimizeBtn.BackColor = System.Drawing.Color.Transparent;
      this.minimizeBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
      this.minimizeBtn.FlatAppearance.BorderSize = 0;
      this.minimizeBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
      this.minimizeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
      this.minimizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.minimizeBtn.ForeColor = System.Drawing.Color.Transparent;
      this.minimizeBtn.Image = global::Blurts.Properties.Resources.minimizeBtn;
      this.minimizeBtn.Location = new System.Drawing.Point(272, 6);
      this.minimizeBtn.Margin = new System.Windows.Forms.Padding(0);
      this.minimizeBtn.Name = "minimizeBtn";
      this.minimizeBtn.Size = new System.Drawing.Size(18, 20);
      this.minimizeBtn.TabIndex = 9;
      this.minimizeBtn.UseVisualStyleBackColor = false;
      this.minimizeBtn.MouseLeave += new System.EventHandler(this.minimizeBtn_MouseLeave);
      this.minimizeBtn.Click += new System.EventHandler(this.minimizeBtn_Click);
      this.minimizeBtn.MouseEnter += new System.EventHandler(this.minimizeBtn_MouseEnter);
      // 
      // closeBtn
      // 
      this.closeBtn.BackColor = System.Drawing.Color.Transparent;
      this.closeBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
      this.closeBtn.FlatAppearance.BorderSize = 0;
      this.closeBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
      this.closeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
      this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.closeBtn.ForeColor = System.Drawing.Color.Transparent;
      this.closeBtn.Image = global::Blurts.Properties.Resources.close_Btn;
      this.closeBtn.Location = new System.Drawing.Point(293, 6);
      this.closeBtn.Margin = new System.Windows.Forms.Padding(0);
      this.closeBtn.Name = "closeBtn";
      this.closeBtn.Size = new System.Drawing.Size(18, 20);
      this.closeBtn.TabIndex = 8;
      this.closeBtn.UseVisualStyleBackColor = false;
      this.closeBtn.MouseLeave += new System.EventHandler(this.closeBtn_MouseLeave);
      this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
      this.closeBtn.MouseEnter += new System.EventHandler(this.closeBtn_MouseEnter);
      // 
      // webBrowser1
      // 
      this.webBrowser1.Location = new System.Drawing.Point(11, 41);
      this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
      this.webBrowser1.Name = "webBrowser1";
      this.webBrowser1.ScrollBarsEnabled = false;
      this.webBrowser1.Size = new System.Drawing.Size(298, 271);
      this.webBrowser1.TabIndex = 10;
      this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
      // 
      // downloadBtn
      // 
      this.downloadBtn.BackColor = System.Drawing.Color.Black;
      this.downloadBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.downloadBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.downloadBtn.Location = new System.Drawing.Point(11, 318);
      this.downloadBtn.Name = "downloadBtn";
      this.downloadBtn.Size = new System.Drawing.Size(138, 23);
      this.downloadBtn.TabIndex = 11;
      this.downloadBtn.Text = "Download";
      this.downloadBtn.UseVisualStyleBackColor = false;
      this.downloadBtn.Click += new System.EventHandler(this.downloadBtn_Click);
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteAllContactsToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(177, 26);
      // 
      // deleteAllContactsToolStripMenuItem
      // 
      this.deleteAllContactsToolStripMenuItem.Name = "deleteAllContactsToolStripMenuItem";
      this.deleteAllContactsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
      this.deleteAllContactsToolStripMenuItem.Text = "Delete All Contacts";
      this.deleteAllContactsToolStripMenuItem.Click += new System.EventHandler(this.deleteAllContactsToolStripMenuItem_Click);
      // 
      // ContactDlg
      // 
      this.BackColor = System.Drawing.Color.Fuchsia;
      this.BackgroundImage = global::Blurts.Properties.Resources.smsBackground;
      this.ClientSize = new System.Drawing.Size(320, 372);
      this.ContextMenuStrip = this.contextMenuStrip1;
      this.ControlBox = false;
      this.Controls.Add(this.downloadBtn);
      this.Controls.Add(this.webBrowser1);
      this.Controls.Add(this.minimizeBtn);
      this.Controls.Add(this.closeBtn);
      this.DoubleBuffered = true;
      this.ForeColor = System.Drawing.Color.White;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximumSize = new System.Drawing.Size(320, 372);
      this.MinimumSize = new System.Drawing.Size(320, 372);
      this.Name = "ContactDlg";
      this.Text = "Blurts - Contacts";
      this.TransparencyKey = System.Drawing.Color.Fuchsia;
      this.Load += new System.EventHandler(this.ContactDlg_Load);
      this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ContactDlg_MouseUp);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContactDlg_MouseDown);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ContactDlg_FormClosing);
      this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ContactDlg_MouseMove);
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button cancelBtn;
    private System.Windows.Forms.Button minimizeBtn;
    private System.Windows.Forms.Button closeBtn;
    private System.Windows.Forms.WebBrowser webBrowser1;
    private System.Windows.Forms.Button downloadBtn;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem deleteAllContactsToolStripMenuItem;
  }
}