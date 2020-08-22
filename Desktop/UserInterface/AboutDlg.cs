using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Blurts
{
  public partial class AboutDlg : Form
  {
    static public String version = "2.0.0.11";
    public AboutDlg()
    {
      InitializeComponent();
      versionLabel.Text = version;
    }

    private void webLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      try
      {
        System.Diagnostics.Process.Start("http://www.mlhsoftware.com/");
      }
      catch (Exception )
      {
      }
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      try
      {
        System.Diagnostics.Process.Start("mailto:feedback@mlhsoftware.com?subject=Feedback:%20Blurts%20" + version);
      }
      catch (Exception )
      {
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      System.Drawing.Drawing2D.LinearGradientBrush baseBackground;
      baseBackground = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, ClientSize.Width, ClientSize.Height), Color.White, Color.FromArgb(113, 138, 244), 90, false);
      e.Graphics.FillRectangle(baseBackground, ClientRectangle);
    }
  }
}