namespace Blurts
{
  partial class SignalLevelCtrl
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
      this.signalImg = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.signalImg)).BeginInit();
      this.SuspendLayout();
      // 
      // signalImg
      // 
      this.signalImg.BackColor = System.Drawing.Color.Transparent;
      this.signalImg.Image = global::Blurts.Properties.Resources.signal0;
      this.signalImg.Location = new System.Drawing.Point(0, 0);
      this.signalImg.Name = "signalImg";
      this.signalImg.Size = new System.Drawing.Size(35, 14);
      this.signalImg.TabIndex = 8;
      this.signalImg.TabStop = false;
      // 
      // SignalLevelCtrl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Transparent;
      this.Controls.Add(this.signalImg);
      this.Name = "SignalLevelCtrl";
      this.Size = new System.Drawing.Size(35, 14);
      ((System.ComponentModel.ISupportInitialize)(this.signalImg)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox signalImg;
  }
}
