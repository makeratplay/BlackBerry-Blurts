namespace Blurts
{
  partial class DialDlg
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialDlg));
      this.cancelBtn = new System.Windows.Forms.Button();
      this.OkBtn = new System.Windows.Forms.Button();
      this.webBrowser1 = new System.Windows.Forms.WebBrowser();
      this.searchTxtBx = new System.Windows.Forms.TextBox();
      this.closeBtn = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // cancelBtn
      // 
      this.cancelBtn.BackColor = System.Drawing.Color.Black;
      this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelBtn.FlatAppearance.BorderSize = 0;
      this.cancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.cancelBtn.Image = global::Blurts.Properties.Resources.callEndBtn;
      this.cancelBtn.Location = new System.Drawing.Point(250, 321);
      this.cancelBtn.Name = "cancelBtn";
      this.cancelBtn.Size = new System.Drawing.Size(61, 23);
      this.cancelBtn.TabIndex = 3;
      this.cancelBtn.UseVisualStyleBackColor = false;
      // 
      // OkBtn
      // 
      this.OkBtn.BackColor = System.Drawing.Color.Black;
      this.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.OkBtn.FlatAppearance.BorderSize = 0;
      this.OkBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.OkBtn.Image = global::Blurts.Properties.Resources.sendBtn;
      this.OkBtn.Location = new System.Drawing.Point(12, 321);
      this.OkBtn.Name = "OkBtn";
      this.OkBtn.Size = new System.Drawing.Size(61, 22);
      this.OkBtn.TabIndex = 2;
      this.OkBtn.UseVisualStyleBackColor = false;
      // 
      // webBrowser1
      // 
      this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.webBrowser1.Location = new System.Drawing.Point(10, 59);
      this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
      this.webBrowser1.Name = "webBrowser1";
      this.webBrowser1.ScrollBarsEnabled = false;
      this.webBrowser1.Size = new System.Drawing.Size(300, 255);
      this.webBrowser1.TabIndex = 1;
      // 
      // searchTxtBx
      // 
      this.searchTxtBx.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.searchTxtBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.searchTxtBx.Location = new System.Drawing.Point(10, 22);
      this.searchTxtBx.Name = "searchTxtBx";
      this.searchTxtBx.Size = new System.Drawing.Size(300, 37);
      this.searchTxtBx.TabIndex = 0;
      this.searchTxtBx.TextChanged += new System.EventHandler(this.searchTxtBx_TextChanged);
      // 
      // closeBtn
      // 
      this.closeBtn.BackColor = System.Drawing.Color.Black;
      this.closeBtn.FlatAppearance.BorderSize = 0;
      this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.closeBtn.Image = global::Blurts.Properties.Resources.closeBtn;
      this.closeBtn.Location = new System.Drawing.Point(297, 3);
      this.closeBtn.Margin = new System.Windows.Forms.Padding(0);
      this.closeBtn.Name = "closeBtn";
      this.closeBtn.Size = new System.Drawing.Size(14, 14);
      this.closeBtn.TabIndex = 4;
      this.closeBtn.UseVisualStyleBackColor = false;
      this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
      // 
      // DialDlg
      // 
      this.AcceptButton = this.OkBtn;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Fuchsia;
      this.BackgroundImage = global::Blurts.Properties.Resources.dailBackground;
      this.ClientSize = new System.Drawing.Size(320, 350);
      this.ControlBox = false;
      this.Controls.Add(this.closeBtn);
      this.Controls.Add(this.searchTxtBx);
      this.Controls.Add(this.webBrowser1);
      this.Controls.Add(this.cancelBtn);
      this.Controls.Add(this.OkBtn);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "DialDlg";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Dial Phone - Blurts by MLH Software";
      this.TransparencyKey = System.Drawing.Color.Fuchsia;
      this.Load += new System.EventHandler(this.DialDlg_Load);
      this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DialDlg_MouseUp);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DialDlg_MouseDown);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DialDlg_FormClosing);
      this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DialDlg_MouseMove);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button OkBtn;
    private System.Windows.Forms.Button cancelBtn;
    private System.Windows.Forms.WebBrowser webBrowser1;
    private System.Windows.Forms.TextBox searchTxtBx;
    private System.Windows.Forms.Button closeBtn;
  }
}