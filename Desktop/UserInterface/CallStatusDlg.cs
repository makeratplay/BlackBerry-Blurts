using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Blurts
{
  public delegate void CallStatusDlgClosedEventHandler();

  public partial class CallStatusDlg : Form
  {
    public event CallStatusDlgClosedEventHandler CallStatusDlgClosedEvent;

    private bool draggingToolbar;
    private Point draggedFrom;
    private ToolTip m_toolTip;

    public CallStatusDlg()
    {
      InitializeComponent();
    }

    private void closeBtn_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      Close();
    }

    private void CallStatusDlg_Load(object sender, EventArgs e)
    {
      // Create the ToolTip and associate with the Form container.
      m_toolTip = new ToolTip();

      // Set up the delays for the ToolTip.
      m_toolTip.AutoPopDelay = 5000;
      m_toolTip.InitialDelay = 1000;
      m_toolTip.ReshowDelay = 500;
      // Force the ToolTip text to be displayed whether or not the form is active.
      m_toolTip.ShowAlways = true;

      // Set up the ToolTip text for the Button and Checkbox.
      m_toolTip.SetToolTip(this.showKeypadBtn, "Show/Hide Keypad");
      m_toolTip.SetToolTip(this.speakerPhoneBtn, "Speaker Phone");
      m_toolTip.SetToolTip(this.muteBtn, "Mute Phone");
      m_toolTip.SetToolTip(this.volumeUpBtn, "Volume Up");
      m_toolTip.SetToolTip(this.volumeDownBtn, "Volume Down");
      m_toolTip.SetToolTip(this.cancelBtn, "End Call");
      m_toolTip.SetToolTip(this.OkBtn, "Answer Call");


      this.TopMost = true;
      webBrowser.AllowWebBrowserDrop = false;
      webBrowser.IsWebBrowserContextMenuEnabled = false;
      webBrowser.WebBrowserShortcutsEnabled = true;
      webBrowser.ScriptErrorsSuppressed = true;

      showKeypadBtn_Click(null, null); // hide keypad when dialog is first shown

      //webBrowser.ObjectForScripting = this;
    }

    public void displayAlert(CallAlert alert)
    {
      try
      {
        if (alert != null)
        {
          alert.ImageFile = Contacts.Instance.getImageFile(alert.PhoneNumber, null);
          alert.XSLFile = "callStatus.xslt";
          webBrowser.DocumentText = alert.HTML;

          this.Visible = true;
          this.TopMost = true;
          this.BringToFront();
          //this.TopMost = false;
        }
      }
      catch (Exception)
      {
      }
    }

    private void CallStatusDlg_MouseDown(object sender, MouseEventArgs e)
    {
      draggingToolbar = true;
      draggedFrom = new Point(e.X, e.Y);
      this.Capture = true;
    }

    private void CallStatusDlg_MouseMove(object sender, MouseEventArgs e)
    {
      if (draggingToolbar)
      {
        this.Left = e.X + this.Left - draggedFrom.X;
        this.Top = e.Y + this.Top - draggedFrom.Y;

        /*
        Screen desktop = Screen.PrimaryScreen;
        if ( (this.Left + this.Width) > (desktop.WorkingArea.Width - 10) )
        {
          this.Left = desktop.WorkingArea.Width - this.Width;
        }
        else
        {

        }
         * */
      }
    }

    private void CallStatusDlg_MouseUp(object sender, MouseEventArgs e)
    {
      draggingToolbar = false;
      this.Capture = false;
    }

    private void key1_Click(object sender, EventArgs e)
    {
      Device.Instance.SendDTMF("1");
    }

    private void key2_Click(object sender, EventArgs e)
    {
      Device.Instance.SendDTMF("2");
    }

    private void key3_Click(object sender, EventArgs e)
    {
      Device.Instance.SendDTMF("3");
    }

    private void key4_Click(object sender, EventArgs e)
    {
      Device.Instance.SendDTMF("4");
    }

    private void key5_Click(object sender, EventArgs e)
    {
      Device.Instance.SendDTMF("5");
    }

    private void key6_Click(object sender, EventArgs e)
    {
      Device.Instance.SendDTMF("6");
    }

    private void key7_Click(object sender, EventArgs e)
    {
      Device.Instance.SendDTMF("7");
    }

    private void key8_Click(object sender, EventArgs e)
    {
      Device.Instance.SendDTMF("8");
    }

    private void key9_Click(object sender, EventArgs e)
    {
      Device.Instance.SendDTMF("9");
    }

    private void keyStar_Click(object sender, EventArgs e)
    {
      Device.Instance.SendDTMF("*");
    }

    private void key0_Click(object sender, EventArgs e)
    {
      Device.Instance.SendDTMF("0");
    }

    private void keyPound_Click(object sender, EventArgs e)
    {
      Device.Instance.SendDTMF("#");
    }

    private void CallStatusDlg_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (CallStatusDlgClosedEvent != null)
      {
        CallStatusDlgClosedEvent();
      }
    }

    private void cancelBtn_Click(object sender, EventArgs e)
    {
      Device.Instance.PressEndKey();
    }

    private void OkBtn_Click(object sender, EventArgs e)
    {
      Device.Instance.PressSendKey();
    }

    private void key1_MouseDown(object sender, MouseEventArgs e)
    {
      this.key1.Image = global::Blurts.Properties.Resources._1key_sel;
    }

    private void key1_MouseUp(object sender, MouseEventArgs e)
    {
      this.key1.Image = global::Blurts.Properties.Resources._1key;
    }

    private void key2_MouseDown(object sender, MouseEventArgs e)
    {
      this.key2.Image = global::Blurts.Properties.Resources._2key_sel;
    }

    private void key2_MouseUp(object sender, MouseEventArgs e)
    {
      this.key2.Image = global::Blurts.Properties.Resources._2key;
    }

    private void key3_MouseDown(object sender, MouseEventArgs e)
    {
      this.key3.Image = global::Blurts.Properties.Resources._3key_sel;
    }

    private void key3_MouseUp(object sender, MouseEventArgs e)
    {
      this.key3.Image = global::Blurts.Properties.Resources._3key;
    }

    private void key4_MouseDown(object sender, MouseEventArgs e)
    {
      this.key4.Image = global::Blurts.Properties.Resources._4key_sel;
    }

    private void key4_MouseUp(object sender, MouseEventArgs e)
    {
      this.key4.Image = global::Blurts.Properties.Resources._4key;
    }

    private void key5_MouseDown(object sender, MouseEventArgs e)
    {
      this.key5.Image = global::Blurts.Properties.Resources._5key_sel;
    }

    private void key5_MouseUp(object sender, MouseEventArgs e)
    {
      this.key5.Image = global::Blurts.Properties.Resources._5key;
    }

    private void key6_MouseDown(object sender, MouseEventArgs e)
    {
      this.key6.Image = global::Blurts.Properties.Resources._6key_sel;
    }

    private void key6_MouseUp(object sender, MouseEventArgs e)
    {
      this.key6.Image = global::Blurts.Properties.Resources._6key;
    }

    private void key7_MouseDown(object sender, MouseEventArgs e)
    {
      this.key7.Image = global::Blurts.Properties.Resources._7key_sel;
    }

    private void key7_MouseUp(object sender, MouseEventArgs e)
    {
      this.key7.Image = global::Blurts.Properties.Resources._7key;
    }

    private void key8_MouseDown(object sender, MouseEventArgs e)
    {
      this.key8.Image = global::Blurts.Properties.Resources._8key_sel;
    }

    private void key8_MouseUp(object sender, MouseEventArgs e)
    {
      this.key8.Image = global::Blurts.Properties.Resources._8key;
    }

    private void key9_MouseDown(object sender, MouseEventArgs e)
    {
      this.key9.Image = global::Blurts.Properties.Resources._9key_sel;
    }

    private void key9_MouseUp(object sender, MouseEventArgs e)
    {
      this.key9.Image = global::Blurts.Properties.Resources._9key;
    }

    private void keyStar_MouseDown(object sender, MouseEventArgs e)
    {
      this.keyStar.Image = global::Blurts.Properties.Resources.Starkey_sel;
    }

    private void keyStar_MouseUp(object sender, MouseEventArgs e)
    {
      this.keyStar.Image = global::Blurts.Properties.Resources.Starkey;
    }

    private void key0_MouseDown(object sender, MouseEventArgs e)
    {
      this.key0.Image = global::Blurts.Properties.Resources._0key_sel;
    }

    private void key0_MouseUp(object sender, MouseEventArgs e)
    {
      this.key0.Image = global::Blurts.Properties.Resources._0key;
    }

    private void keyPound_MouseDown(object sender, MouseEventArgs e)
    {
      this.keyPound.Image = global::Blurts.Properties.Resources.poundkey_sel;
    }

    private void keyPound_MouseUp(object sender, MouseEventArgs e)
    {
      this.keyPound.Image = global::Blurts.Properties.Resources.poundkey;
    }

    private void minimizeBtn_Click(object sender, EventArgs e)
    {
      this.WindowState = FormWindowState.Minimized;
    }

    private void closeBtn_MouseEnter(object sender, EventArgs e)
    {
      this.closeBtn.Image = global::Blurts.Properties.Resources.close_Btn_focus;
    }

    private void closeBtn_MouseLeave(object sender, EventArgs e)
    {
      this.closeBtn.Image = global::Blurts.Properties.Resources.close_Btn;
    }

    private void minimizeBtn_MouseEnter(object sender, EventArgs e)
    {
      this.minimizeBtn.Image = global::Blurts.Properties.Resources.minimizeBtn_focus;
    }

    private void minimizeBtn_MouseLeave(object sender, EventArgs e)
    {
      this.minimizeBtn.Image = global::Blurts.Properties.Resources.minimizeBtn;
    }

    private void speakerPhoneBtn_Click(object sender, EventArgs e)
    {
      Device.Instance.PressSpeakerKey();
    }

    private void muteBtn_Click(object sender, EventArgs e)
    {
      Device.Instance.PressMuteKey();
    }

    private void volumeUpBtn_Click(object sender, EventArgs e)
    {
      Device.Instance.PressVolumeUpKey();
    }

    private void volumeDownBtn_Click(object sender, EventArgs e)
    {
      Device.Instance.PressVolumeDownKey();
    }

    private void showKeypadBtn_Click(object sender, EventArgs e)
    {
      if (ClientSize.Height > 250)
      {
        moveToolbar(105);
        this.ClientSize = new System.Drawing.Size(308, 195);
        this.BackgroundImage = global::Blurts.Properties.Resources.callStatusSmBackground;
        showKeypad(false);
      }
      else
      {
        moveToolbar(365);
        this.ClientSize = new System.Drawing.Size(308, 455);
        this.BackgroundImage = global::Blurts.Properties.Resources.callStatusBackground;
        showKeypad(true);
      }
    }

    private void moveToolbar( int yPos )
    {
      this.showKeypadBtn.Location = new System.Drawing.Point(0, yPos);
      this.speakerPhoneBtn.Location = new System.Drawing.Point(61, yPos);
      this.muteBtn.Location = new System.Drawing.Point(124, yPos);
      this.volumeUpBtn.Location = new System.Drawing.Point(185, yPos);
      this.volumeDownBtn.Location = new System.Drawing.Point(246, yPos);

      this.cancelBtn.Location = new System.Drawing.Point(235, yPos + 58);
      this.OkBtn.Location = new System.Drawing.Point(12, yPos + 58);
    }

    private void showKeypad(Boolean bShow)
    {
      key1.Visible = bShow;
      key2.Visible = bShow;
      key3.Visible = bShow;
      key4.Visible = bShow;
      key5.Visible = bShow;
      key6.Visible = bShow;
      key7.Visible = bShow;
      key8.Visible = bShow;
      key9.Visible = bShow;
      key0.Visible = bShow;
      keyPound.Visible = bShow;
      keyStar.Visible = bShow;
    }

    private void showKeypadBtn_MouseDown(object sender, MouseEventArgs e)
    {
      this.showKeypadBtn.Image = global::Blurts.Properties.Resources.showKeypadBtn_sel;
    }

    private void showKeypadBtn_MouseUp(object sender, MouseEventArgs e)
    {
      this.showKeypadBtn.Image = global::Blurts.Properties.Resources.showKeypadBtn;
    }

    private void speakerPhoneBtn_MouseDown(object sender, MouseEventArgs e)
    {
      this.speakerPhoneBtn.Image = global::Blurts.Properties.Resources.speakerPhoneBtn_sel;
    }

    private void speakerPhoneBtn_MouseUp(object sender, MouseEventArgs e)
    {
      this.speakerPhoneBtn.Image = global::Blurts.Properties.Resources.speakerPhoneBtn;
    }

    private void muteBtn_MouseDown(object sender, MouseEventArgs e)
    {
      this.muteBtn.Image = global::Blurts.Properties.Resources.muteBtn_sel;
    }

    private void muteBtn_MouseUp(object sender, MouseEventArgs e)
    {
      this.muteBtn.Image = global::Blurts.Properties.Resources.muteBtn;
    }

    private void volumeUpBtn_MouseDown(object sender, MouseEventArgs e)
    {
      this.volumeUpBtn.Image = global::Blurts.Properties.Resources.volumeUpBtn_sel;
    }

    private void volumeUpBtn_MouseUp(object sender, MouseEventArgs e)
    {
      this.volumeUpBtn.Image = global::Blurts.Properties.Resources.volumeUpBtn;
    }

    private void volumeDownBtn_MouseDown(object sender, MouseEventArgs e)
    {
      this.volumeDownBtn.Image = global::Blurts.Properties.Resources.volumeDownBtn_sel;
    }

    private void volumeDownBtn_MouseUp(object sender, MouseEventArgs e)
    {
      this.volumeDownBtn.Image = global::Blurts.Properties.Resources.volumeDownBtn;
    }

    private void OkBtn_MouseDown(object sender, MouseEventArgs e)
    {
      this.OkBtn.Image = global::Blurts.Properties.Resources.sendBtn_sel;
    }

    private void OkBtn_MouseUp(object sender, MouseEventArgs e)
    {
      this.OkBtn.Image = global::Blurts.Properties.Resources.sendBtn;
    }

    private void cancelBtn_MouseDown(object sender, MouseEventArgs e)
    {
      this.cancelBtn.Image = global::Blurts.Properties.Resources.callEndBtn_sel;
    }

    private void cancelBtn_MouseUp(object sender, MouseEventArgs e)
    {
      this.cancelBtn.Image = global::Blurts.Properties.Resources.callEndBtn;
    }

    private void CallStatusDlg_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      showKeypadBtn_Click(null, null); // toggle show keypad
    }
  }
}