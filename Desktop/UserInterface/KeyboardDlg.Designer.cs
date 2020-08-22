namespace Blurts
{
  partial class KeyboardDlg
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyboardDlg));
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      // 
      // timer1
      // 
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // KeyboardDlg
      // 
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(352, 199);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "KeyboardDlg";
      this.Text = "Blurts";
      this.Deactivate += new System.EventHandler(this.KeyboardDlg_Deactivate);
      this.Load += new System.EventHandler(this.KeyboardDlg_Load);
      this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.KeyboardDlg_MouseUp);
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.KeyboardDlg_Paint);
      this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KeyboardDlg_MouseClick);
      this.Activated += new System.EventHandler(this.KeyboardDlg_Activated);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.KeyboardDlg_MouseDown);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KeyboardDlg_FormClosing);
      this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.KeyboardDlg_MouseMove);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Timer timer1;
  }
}