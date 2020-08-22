namespace Blurts
{
  partial class SMSDlg
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SMSDlg));
      this.label1 = new System.Windows.Forms.Label();
      this.addressTxtBox = new System.Windows.Forms.TextBox();
      this.msgTxtBox = new System.Windows.Forms.TextBox();
      this.textCnt = new System.Windows.Forms.Label();
      this.sendBtn = new System.Windows.Forms.Button();
      this.cancelBtn = new System.Windows.Forms.Button();
      this.contactBtn = new System.Windows.Forms.Button();
      this.webBrowser = new System.Windows.Forms.WebBrowser();
      this.clearHxBtn = new System.Windows.Forms.LinkLabel();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 327);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(23, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "To:";
      this.label1.Visible = false;
      // 
      // addressTxtBox
      // 
      this.addressTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.addressTxtBox.Location = new System.Drawing.Point(55, 375);
      this.addressTxtBox.MaxLength = 15;
      this.addressTxtBox.Name = "addressTxtBox";
      this.addressTxtBox.Size = new System.Drawing.Size(330, 20);
      this.addressTxtBox.TabIndex = 1;
      this.addressTxtBox.Visible = false;
      this.addressTxtBox.TextChanged += new System.EventHandler(this.addressTxtBox_TextChanged);
      this.addressTxtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.addressTxtBox_KeyPress);
      // 
      // msgTxtBox
      // 
      this.msgTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.msgTxtBox.Location = new System.Drawing.Point(5, 361);
      this.msgTxtBox.MaxLength = 1400;
      this.msgTxtBox.Multiline = true;
      this.msgTxtBox.Name = "msgTxtBox";
      this.msgTxtBox.Size = new System.Drawing.Size(421, 61);
      this.msgTxtBox.TabIndex = 2;
      this.msgTxtBox.TextChanged += new System.EventHandler(this.msgTxtBox_TextChanged);
      this.msgTxtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.msgTxtBox_KeyPress);
      // 
      // textCnt
      // 
      this.textCnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.textCnt.AutoSize = true;
      this.textCnt.Location = new System.Drawing.Point(5, 428);
      this.textCnt.Name = "textCnt";
      this.textCnt.Size = new System.Drawing.Size(25, 13);
      this.textCnt.TabIndex = 3;
      this.textCnt.Text = "160";
      // 
      // sendBtn
      // 
      this.sendBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.sendBtn.Location = new System.Drawing.Point(432, 361);
      this.sendBtn.Name = "sendBtn";
      this.sendBtn.Size = new System.Drawing.Size(75, 61);
      this.sendBtn.TabIndex = 4;
      this.sendBtn.Text = "Send";
      this.sendBtn.UseVisualStyleBackColor = true;
      this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
      // 
      // cancelBtn
      // 
      this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelBtn.Location = new System.Drawing.Point(430, 370);
      this.cancelBtn.Name = "cancelBtn";
      this.cancelBtn.Size = new System.Drawing.Size(75, 23);
      this.cancelBtn.TabIndex = 5;
      this.cancelBtn.Text = "Close";
      this.cancelBtn.UseVisualStyleBackColor = true;
      this.cancelBtn.Visible = false;
      // 
      // contactBtn
      // 
      this.contactBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.contactBtn.Location = new System.Drawing.Point(16, 370);
      this.contactBtn.Name = "contactBtn";
      this.contactBtn.Size = new System.Drawing.Size(37, 23);
      this.contactBtn.TabIndex = 6;
      this.contactBtn.Text = "To:";
      this.contactBtn.UseVisualStyleBackColor = true;
      this.contactBtn.Visible = false;
      this.contactBtn.Click += new System.EventHandler(this.contactBtn_Click);
      // 
      // webBrowser
      // 
      this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.webBrowser.Location = new System.Drawing.Point(-1, -2);
      this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
      this.webBrowser.Name = "webBrowser";
      this.webBrowser.Size = new System.Drawing.Size(510, 357);
      this.webBrowser.TabIndex = 7;
      this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
      // 
      // clearHxBtn
      // 
      this.clearHxBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.clearHxBtn.AutoSize = true;
      this.clearHxBtn.Location = new System.Drawing.Point(440, 428);
      this.clearHxBtn.Name = "clearHxBtn";
      this.clearHxBtn.Size = new System.Drawing.Size(66, 13);
      this.clearHxBtn.TabIndex = 8;
      this.clearHxBtn.TabStop = true;
      this.clearHxBtn.Text = "Clear History";
      this.clearHxBtn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.clearHxBtn_LinkClicked);
      // 
      // SMSDlg
      // 
      this.AcceptButton = this.sendBtn;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(138)))), ((int)(((byte)(244)))));
      this.CancelButton = this.cancelBtn;
      this.ClientSize = new System.Drawing.Size(509, 445);
      this.Controls.Add(this.clearHxBtn);
      this.Controls.Add(this.webBrowser);
      this.Controls.Add(this.sendBtn);
      this.Controls.Add(this.textCnt);
      this.Controls.Add(this.msgTxtBox);
      this.Controls.Add(this.addressTxtBox);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.contactBtn);
      this.Controls.Add(this.cancelBtn);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "SMSDlg";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Text Message (SMS)";
      this.Load += new System.EventHandler(this.SMSDlg_Load);
      this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SMSDlg_KeyPress);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SMSDlg_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox addressTxtBox;
    private System.Windows.Forms.TextBox msgTxtBox;
    private System.Windows.Forms.Label textCnt;
    private System.Windows.Forms.Button sendBtn;
    private System.Windows.Forms.Button cancelBtn;
    private System.Windows.Forms.Button contactBtn;
    private System.Windows.Forms.WebBrowser webBrowser;
    private System.Windows.Forms.LinkLabel clearHxBtn;
  }
}