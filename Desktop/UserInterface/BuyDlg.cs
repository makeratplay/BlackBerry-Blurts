using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Blurts
{
  public partial class BuyDlg : Form
  {
    public BuyDlg()
    {
      InitializeComponent();
    }

    private void BuyBtn_Click(object sender, EventArgs e)
    {
      try
      {
        if (ApplicationSettings.Instance.Channel == 2) // BlackBerry App World
        {
          System.Diagnostics.Process.Start("http://appworld.blackberry.com/webstore/content/4299?lang=en");
        }
        else // MLH Shopping Cart
        {
          System.Diagnostics.Process.Start("https://www.mobihand.com/cart1.asp?posid=234&pid=40695&tracking1=dt");
        }
      }
      catch (Exception)
      {
        MessageBox.Show("Failed to open shopping cart");
      }
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      try
      {
        System.Diagnostics.Process.Start("http://www.mlhsoftware.com/blurts");
      }
      catch (Exception)
      {
        MessageBox.Show("Failed to open http://www.mlhsoftware.com/blurts");
      }
    }

    private void OkBtn_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      try
      {
        System.Diagnostics.Process.Start("http://www.mlhsoftware.com/Blurts/faq.html#activation");
      }
      catch (Exception)
      {
        MessageBox.Show("Failed to open http://www.mlhsoftware.com/Blurts/faq.html#activation");
      }
      
    }
  }
}