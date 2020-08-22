namespace Blurts
{
  partial class NoBluetoothDlg
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoBluetoothDlg));
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.linkLabel1 = new System.Windows.Forms.LinkLabel();
      this.OKBtn = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(36, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(256, 20);
      this.label1.TabIndex = 0;
      this.label1.Text = "Thanks for downloading Blurts.";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.ForeColor = System.Drawing.Color.Red;
      this.label2.Location = new System.Drawing.Point(43, 37);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(242, 20);
      this.label2.TabIndex = 1;
      this.label2.Text = " Bluetooth failed to initialize. ";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(21, 65);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(287, 20);
      this.label3.TabIndex = 2;
      this.label3.Text = "This application requires Bluetooth";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(38, 93);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(253, 20);
      this.label4.TabIndex = 3;
      this.label4.Text = "with Microsoft driver software. ";
      // 
      // linkLabel1
      // 
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Location = new System.Drawing.Point(53, 121);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new System.Drawing.Size(222, 13);
      this.linkLabel1.TabIndex = 4;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "http://www.mlhsoftware.com/Blurts/help.html";
      this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      // 
      // OKBtn
      // 
      this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.OKBtn.Location = new System.Drawing.Point(127, 145);
      this.OKBtn.Name = "OKBtn";
      this.OKBtn.Size = new System.Drawing.Size(75, 23);
      this.OKBtn.TabIndex = 5;
      this.OKBtn.Text = "OK";
      this.OKBtn.UseVisualStyleBackColor = true;
      // 
      // NoBluetoothDlg
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(329, 179);
      this.ControlBox = false;
      this.Controls.Add(this.OKBtn);
      this.Controls.Add(this.linkLabel1);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "NoBluetoothDlg";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Blurts - by MLH Software";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.Button OKBtn;
  }
}