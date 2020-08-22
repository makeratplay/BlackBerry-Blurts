using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Blurts
{
  public partial class NoBluetoothDlg : Form
  {
    public NoBluetoothDlg()
    {
      InitializeComponent();
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      try
      {
        System.Diagnostics.Process.Start("http://www.mlhsoftware.com/Blurts/help.html");
      }
      catch (Exception )
      {
      }
    }
  }
}