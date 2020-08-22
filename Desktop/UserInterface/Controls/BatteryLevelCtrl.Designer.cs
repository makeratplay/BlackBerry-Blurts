namespace Blurts
{
  partial class BatteryLevelCtrl
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.battery = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.battery)).BeginInit();
      this.SuspendLayout();
      // 
      // battery
      // 
      this.battery.BackColor = System.Drawing.Color.Transparent;
      this.battery.Image = global::Blurts.Properties.Resources.battery0;
      this.battery.Location = new System.Drawing.Point(0, 0);
      this.battery.Margin = new System.Windows.Forms.Padding(0);
      this.battery.Name = "battery";
      this.battery.Size = new System.Drawing.Size(43, 14);
      this.battery.TabIndex = 7;
      this.battery.TabStop = false;
      // 
      // BatteryLevelCtrl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Transparent;
      this.Controls.Add(this.battery);
      this.DoubleBuffered = true;
      this.Name = "BatteryLevelCtrl";
      this.Size = new System.Drawing.Size(43, 14);
      this.DoubleClick += new System.EventHandler(this.BatteryLevelCtrl_DoubleClick);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BatteryLevelCtrl_MouseDown);
      ((System.ComponentModel.ISupportInitialize)(this.battery)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox battery;
  }
}
