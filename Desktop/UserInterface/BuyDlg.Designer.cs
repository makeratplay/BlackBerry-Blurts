namespace Blurts
{
  partial class BuyDlg
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuyDlg));
      this.linkLabel1 = new System.Windows.Forms.LinkLabel();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.OkBtn = new System.Windows.Forms.Button();
      this.BuyBtn = new System.Windows.Forms.Button();
      this.linkLabel2 = new System.Windows.Forms.LinkLabel();
      this.SuspendLayout();
      // 
      // linkLabel1
      // 
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Location = new System.Drawing.Point(83, 61);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new System.Drawing.Size(113, 13);
      this.linkLabel1.TabIndex = 1;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "www.mlhsoftware.com";
      this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      // 
      // textBox1
      // 
      this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBox1.Location = new System.Drawing.Point(12, 13);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new System.Drawing.Size(255, 45);
      this.textBox1.TabIndex = 2;
      this.textBox1.Text = "This feature requires Blurts Pro. Click the link below for more information.";
      this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // OkBtn
      // 
      this.OkBtn.Location = new System.Drawing.Point(163, 93);
      this.OkBtn.Name = "OkBtn";
      this.OkBtn.Size = new System.Drawing.Size(75, 23);
      this.OkBtn.TabIndex = 3;
      this.OkBtn.Text = "Cancel";
      this.OkBtn.UseVisualStyleBackColor = true;
      this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
      // 
      // BuyBtn
      // 
      this.BuyBtn.Location = new System.Drawing.Point(40, 93);
      this.BuyBtn.Name = "BuyBtn";
      this.BuyBtn.Size = new System.Drawing.Size(75, 23);
      this.BuyBtn.TabIndex = 4;
      this.BuyBtn.Text = "Buy";
      this.BuyBtn.UseVisualStyleBackColor = true;
      this.BuyBtn.Click += new System.EventHandler(this.BuyBtn_Click);
      // 
      // linkLabel2
      // 
      this.linkLabel2.AutoSize = true;
      this.linkLabel2.Location = new System.Drawing.Point(64, 125);
      this.linkLabel2.Name = "linkLabel2";
      this.linkLabel2.Size = new System.Drawing.Size(203, 13);
      this.linkLabel2.TabIndex = 5;
      this.linkLabel2.TabStop = true;
      this.linkLabel2.Text = "Help: I have already purchased Blurts Pro";
      this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
      // 
      // BuyDlg
      // 
      this.AcceptButton = this.OkBtn;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(279, 147);
      this.ControlBox = false;
      this.Controls.Add(this.linkLabel2);
      this.Controls.Add(this.BuyBtn);
      this.Controls.Add(this.OkBtn);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.linkLabel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "BuyDlg";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Blurts";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Button OkBtn;
    private System.Windows.Forms.Button BuyBtn;
    private System.Windows.Forms.LinkLabel linkLabel2;
  }
}