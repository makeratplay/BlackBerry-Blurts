namespace Blurts
{
  partial class DeviceListDlg
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceListDlg));
      this.devicelistBox = new System.Windows.Forms.ListBox();
      this.okBtn = new System.Windows.Forms.Button();
      this.cancelBtn = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // devicelistBox
      // 
      this.devicelistBox.FormattingEnabled = true;
      this.devicelistBox.Location = new System.Drawing.Point(12, 12);
      this.devicelistBox.Name = "devicelistBox";
      this.devicelistBox.Size = new System.Drawing.Size(260, 95);
      this.devicelistBox.TabIndex = 0;
      this.devicelistBox.SelectedValueChanged += new System.EventHandler(this.devicelistBox_SelectedValueChanged);
      // 
      // okBtn
      // 
      this.okBtn.Location = new System.Drawing.Point(37, 118);
      this.okBtn.Name = "okBtn";
      this.okBtn.Size = new System.Drawing.Size(75, 23);
      this.okBtn.TabIndex = 1;
      this.okBtn.Text = "OK";
      this.okBtn.UseVisualStyleBackColor = true;
      this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
      // 
      // cancelBtn
      // 
      this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelBtn.Location = new System.Drawing.Point(173, 118);
      this.cancelBtn.Name = "cancelBtn";
      this.cancelBtn.Size = new System.Drawing.Size(75, 23);
      this.cancelBtn.TabIndex = 2;
      this.cancelBtn.Text = "Cancel";
      this.cancelBtn.UseVisualStyleBackColor = true;
      // 
      // DeviceListDlg
      // 
      this.AcceptButton = this.okBtn;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cancelBtn;
      this.ClientSize = new System.Drawing.Size(284, 153);
      this.ControlBox = false;
      this.Controls.Add(this.cancelBtn);
      this.Controls.Add(this.okBtn);
      this.Controls.Add(this.devicelistBox);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "DeviceListDlg";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Select Your BlackBerry";
      this.Load += new System.EventHandler(this.DeviceListDlg_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListBox devicelistBox;
    private System.Windows.Forms.Button okBtn;
    private System.Windows.Forms.Button cancelBtn;
  }
}