namespace Blurts
{
  partial class AboutDlg
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDlg));
      this.webLink = new System.Windows.Forms.LinkLabel();
      this.label1 = new System.Windows.Forms.Label();
      this.versionLabel = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.OkBtn = new System.Windows.Forms.Button();
      this.linkLabel1 = new System.Windows.Forms.LinkLabel();
      this.SuspendLayout();
      // 
      // webLink
      // 
      this.webLink.AutoSize = true;
      this.webLink.BackColor = System.Drawing.Color.Transparent;
      this.webLink.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
      this.webLink.LinkColor = System.Drawing.Color.Black;
      this.webLink.Location = new System.Drawing.Point(98, 134);
      this.webLink.Name = "webLink";
      this.webLink.Size = new System.Drawing.Size(122, 13);
      this.webLink.TabIndex = 0;
      this.webLink.TabStop = true;
      this.webLink.Text = "www.MLHSoftware.com";
      this.webLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.webLink_LinkClicked);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.BackColor = System.Drawing.Color.Transparent;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.ForeColor = System.Drawing.Color.Black;
      this.label1.Location = new System.Drawing.Point(110, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(99, 37);
      this.label1.TabIndex = 1;
      this.label1.Text = "Blurts";
      // 
      // versionLabel
      // 
      this.versionLabel.AutoSize = true;
      this.versionLabel.BackColor = System.Drawing.Color.Transparent;
      this.versionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.versionLabel.ForeColor = System.Drawing.Color.Black;
      this.versionLabel.Location = new System.Drawing.Point(127, 52);
      this.versionLabel.Name = "versionLabel";
      this.versionLabel.Size = new System.Drawing.Size(64, 20);
      this.versionLabel.TabIndex = 2;
      this.versionLabel.Text = "0.0.0.1";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.BackColor = System.Drawing.Color.Transparent;
      this.label3.ForeColor = System.Drawing.Color.Black;
      this.label3.Location = new System.Drawing.Point(150, 80);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(18, 13);
      this.label3.TabIndex = 3;
      this.label3.Text = "by";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.BackColor = System.Drawing.Color.Transparent;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.ForeColor = System.Drawing.Color.Black;
      this.label4.Location = new System.Drawing.Point(96, 99);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(127, 24);
      this.label4.TabIndex = 4;
      this.label4.Text = "MLH Software";
      // 
      // OkBtn
      // 
      this.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.OkBtn.ForeColor = System.Drawing.Color.Black;
      this.OkBtn.Location = new System.Drawing.Point(122, 191);
      this.OkBtn.Name = "OkBtn";
      this.OkBtn.Size = new System.Drawing.Size(75, 23);
      this.OkBtn.TabIndex = 5;
      this.OkBtn.Text = "Ok";
      this.OkBtn.UseVisualStyleBackColor = true;
      // 
      // linkLabel1
      // 
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
      this.linkLabel1.LinkColor = System.Drawing.Color.Black;
      this.linkLabel1.Location = new System.Drawing.Point(41, 163);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new System.Drawing.Size(236, 13);
      this.linkLabel1.TabIndex = 6;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "Send Feedback to: feedback@mlhsoftware.com";
      this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      // 
      // AboutDlg
      // 
      this.AcceptButton = this.OkBtn;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.ClientSize = new System.Drawing.Size(318, 242);
      this.ControlBox = false;
      this.Controls.Add(this.linkLabel1);
      this.Controls.Add(this.OkBtn);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.versionLabel);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.webLink);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "AboutDlg";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "About - Blurts by MLH Software";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.LinkLabel webLink;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label versionLabel;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button OkBtn;
    private System.Windows.Forms.LinkLabel linkLabel1;
  }
}