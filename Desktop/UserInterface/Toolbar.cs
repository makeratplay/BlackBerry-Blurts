using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Blurts.Domain;

namespace Blurts
{
  public partial class Toolbar : Form
  {
    private bool draggingToolbar;
    private Point draggedFrom;
    
    private ToolTip m_toolTip;
    CallStatusDlg m_callStatusDlg;
    private bool m_connected;
    private Point m_lastPos;

    private SignalLevelCtrl signal;
    private BatteryLevelCtrl battery;

    public Toolbar()
    {
      m_connected = true;
      m_callStatusDlg = null;
      InitializeComponent();


      // 
      // signal
      // 
      this.signal = new SignalLevelCtrl();
      this.signal.BackColor = System.Drawing.Color.Transparent;
      this.signal.Location = new System.Drawing.Point(24, 8);
      this.signal.Name = "signal";
      this.signal.Size = new System.Drawing.Size(35, 14);
      this.signal.TabIndex = 7;
      this.signal.TabStop = false;
      this.signal.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Toolbar_MouseDoubleClick);
      this.signal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Toolbar_MouseDown);
      this.Controls.Add(this.signal);


      // 
      // battery
      // 
      this.battery = new BatteryLevelCtrl();
      this.battery.BackColor = System.Drawing.Color.Transparent;
      this.battery.Location = new System.Drawing.Point(20, 26);
      this.battery.Name = "battery";
      this.battery.Size = new System.Drawing.Size(43, 14);
      this.battery.TabIndex = 6;
      this.battery.TabStop = false;
      this.battery.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Toolbar_MouseDoubleClick);
      this.battery.MouseDownEvent += new System.Windows.Forms.MouseEventHandler(this.Toolbar_MouseDown);
      this.Controls.Add(this.battery);

      Opacity = ApplicationSettings.Instance.ToolbarOpacity / 100d;

      Device.Instance.BluetoothConnectEvent += new BluetoothConnectEventHandler(BlackBerry_BluetoothConnectEvent);
      Device.Instance.BluetoothDisconnectEvent += new BluetoothDisconnectEventHandler(BlackBerry_BluetoothDisconnectEvent);
      Device.Instance.MsgArrivedEvent += new MsgArrivedEventHandler(BlackBerry_MsgArrivedEvent);
    }

    private void Toolbar_Load(object sender, EventArgs e)
    {
      if (ApplicationSettings.Instance.ToolbarTopMost)
      {
        this.TopMost = true;
      }
      // Create the ToolTip and associate with the Form container.
      m_toolTip = new ToolTip();

      // Set up the delays for the ToolTip.
      m_toolTip.AutoPopDelay = 5000;
      m_toolTip.InitialDelay = 1000;
      m_toolTip.ReshowDelay = 500;
      // Force the ToolTip text to be displayed whether or not the form is active.
      m_toolTip.ShowAlways = true;

      // Set up the ToolTip text for the Button and Checkbox.
      m_toolTip.SetToolTip(this.phoneBtn, "Place Call");
      m_toolTip.SetToolTip(this.smsBtn, "Send SMS");
      m_toolTip.SetToolTip(this.screenCapture, "Capture BlackBerry Screen");
      m_toolTip.SetToolTip(this.clipboardBtn, "Read BlackBerry Clipboard");
      m_toolTip.SetToolTip(this.clipboardGetBtn, "Send Windows Clipboard");


      this.ClientSize = new System.Drawing.Size(82, 432);

      if (ApplicationSettings.Instance.ToolBar.xPos == -1)
      {
        Screen desktop = Screen.PrimaryScreen;
        ApplicationSettings.Instance.ToolBar.xPos = desktop.WorkingArea.Right - Width - 10;
        ApplicationSettings.Instance.ToolBar.yPos = desktop.WorkingArea.Bottom - Height - 10;
        ApplicationSettings.Instance.SetDirty();
        ApplicationSettings.Instance.SaveAppSettings();
      }

      Location = new Point(ApplicationSettings.Instance.ToolBar.xPos, ApplicationSettings.Instance.ToolBar.yPos);
      m_lastPos = Location;

      if (Device.Instance.IsConnected())
      {
        Device.Instance.GetLevelStatus();
      }
      else
      {
        onDisconnect(null, null);
      }

      if (ApplicationSettings.Instance.ToolBar.State == 1)
      {
        toggleSize();
      }

    }

    void BlackBerry_MsgArrivedEvent(object msg)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new EventHandler(OnMsgArrivedEvent), new Object[] { msg, null });
      }
      else
      {
        OnMsgArrivedEvent(msg, null);
      }
    }

    void OnMsgArrivedEvent(object msg, EventArgs e)
    {
      try
      {
        AlertBase alert = (AlertBase)msg;
        DisplayItemType type = alert.ItemType;
        string data = msg.ToString();
        switch (type)
        {
          case DisplayItemType.UNKNOWN:
          {
            break;
          }
          case DisplayItemType.STATUS:
          {
            //processStatusAlert(new StatusAlert(data));
            break;
          }
          case DisplayItemType.CONNECT:
          {
            //processConnectAlert(new ConnectAlert(data));
            break;
          }
          case DisplayItemType.EMAIL:
          {
            //processEmailAlert(new EmailAlert(data));
            break;
          }
          case DisplayItemType.CALL:
          {
            processCallAlert(new CallAlert(data));
            break;
          }
          case DisplayItemType.LOCK:
          {
            //processLockAlert(new LockAlert(data));
            break;
          }
          case DisplayItemType.SMS:
          {
            //processSMSAlert(new SMSAlert(data));
            break;
          }
          case DisplayItemType.SCREEN:
          {
            //processScreenCaptureAlert(new ScreenCaptureAlert(data));
            break;
          }
          case DisplayItemType.CONTACTS:
          {
            //processContactAlert(new ContactAlert(data));
            break;
          }
          case DisplayItemType.CLIPBOARD:
          {
            //processClipboardAlert(new ClipboardAlert(data));
            break;
          }
          case DisplayItemType.DISCONNECT:
          {
            //processDisconnectAlert(new DisconnectAlert(data));
            break;
          }
          case DisplayItemType.LEVEL:
          {
            //processLevelAlertAlert(new LevelAlert(data));
            break;
          }
          case DisplayItemType.MACRO:
          {
            //processMacroAlertAlert(new MacroAlert(data));
            break;
          }
          case DisplayItemType.PIN_MSG:
          {
            //processPINMsgAlert(new PINMsgAlert(data));
            break;
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("BlackBerry_MsgArrivedEvent:" + Environment.NewLine + ex.ToString());
      }
    }

    private void processCallAlert(CallAlert alert)
    {
      if (alert != null)
      {
        //if (alert.Action == alert.Incoming )
        if ( alert.Action == alert.Initiated ||
                  alert.Action == alert.Connected ||
                  alert.Action == alert.Waiting ||
                  alert.Action == alert.Answered )
        {
          if (m_callStatusDlg == null)
          {
            m_callStatusDlg = new CallStatusDlg();
            m_callStatusDlg.Show();
            m_callStatusDlg.CallStatusDlgClosedEvent += new CallStatusDlgClosedEventHandler(m_callStatusDlg_CallStatusDlgClosedEvent);
          }
          m_callStatusDlg.displayAlert(alert);
        }
        else if ( alert.Action == alert.Disconnected )
        {
          if (m_callStatusDlg != null)
          {
            m_callStatusDlg.Close();
            m_callStatusDlg = null;
          }
        }
      }
    }

    void m_callStatusDlg_CallStatusDlgClosedEvent()
    {
      m_callStatusDlg = null;
    }

    private void onConnect(object o, EventArgs e)
    {
      if (ApplicationSettings.Instance.ShowToolbarOnConnect)
      {
        Visible = true;
      }
      m_connected = true;
      setBackGround();
      Invalidate();
    }

    private void onDisconnect(object o, EventArgs e)
    {
      if (ApplicationSettings.Instance.ShowToolbarOnConnect)
      {
        Visible = false;
      }
      m_connected = false;
      setBackGround();
      Invalidate();
    }

    private void setBackGround()
    {
      if (ClientSize.Height > 100)
      {
        if (m_connected)
        {
          BackgroundImage = global::Blurts.Properties.Resources.toolbarBackground;
        }
        else
        {
          BackgroundImage = global::Blurts.Properties.Resources.toolbarBackground2;
        }
      }
      else
      {
        BackgroundImage = global::Blurts.Properties.Resources.toolbarBackground_sm;
      }
    }

    private void BlackBerry_BluetoothDisconnectEvent()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new EventHandler(onDisconnect));
      }
      else
      {
        onDisconnect(null, null);
      }
    }

    private void BlackBerry_BluetoothConnectEvent()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new EventHandler(onConnect));
      }
      else
      {
        onConnect(null, null);
      }
    }

    private void Toolbar_MouseDown(object sender, MouseEventArgs e)
    {
      draggingToolbar = true;
      draggedFrom = new Point(e.X, e.Y);
      this.Capture = true;
    }

    private void Toolbar_MouseUp(object sender, MouseEventArgs e)
    {
      if (draggingToolbar)
      {
        ApplicationSettings.Instance.ToolBar.xPos = this.Left;
        ApplicationSettings.Instance.ToolBar.yPos = this.Top;
        ApplicationSettings.Instance.SetDirty();
        ApplicationSettings.Instance.SaveAppSettings();
        
        draggingToolbar = false;
        this.Capture = false;
      }
    }

    private void Toolbar_MouseMove(object sender, MouseEventArgs e)
    {
      if (draggingToolbar)
      {
        this.Left = e.X + this.Left - draggedFrom.X;
        this.Top = e.Y + this.Top - draggedFrom.Y;
        m_lastPos = Location;
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

    private void closeBtn_Click(object sender, EventArgs e)
    {
      hideToolbar();
      //Close();
    }

    public void showToolbar()
    {
      if (!Visible)
      {
        Visible = true;
        TopMost = true;
        Focus();
        BringToFront();
        if (!ApplicationSettings.Instance.ToolbarTopMost)
        {
          TopMost = false;
        }
        ApplicationSettings.Instance.ToolBar.Visable = Visible;
        ApplicationSettings.Instance.SetDirty();
        ApplicationSettings.Instance.SaveAppSettings();
      }
    }

    public void hideToolbar()
    {
      Visible = false;
      ApplicationSettings.Instance.ToolBar.Visable = Visible;
      ApplicationSettings.Instance.SetDirty();
      ApplicationSettings.Instance.SaveAppSettings();
    }

    private void Toolbar_SizeChanged(object sender, EventArgs e)
    {
    }

    private void Toolbar_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (e.Y < 30)
      {
        toggleSize();
      }
    }

    private void phoneBtn_Click(object sender, EventArgs e)
    {
      //processCallAlert(null);

      if (Device.Instance.IsConnected())
      {
        Device.Instance.DialPhone("");
      }
      else
      {
        MessageBox.Show("BlackBerry is disconnected");
      }
    }


    private Boolean IsConnected()
    {
      Boolean retVal = true;
      if (!Device.Instance.IsConnected())
      {
        retVal = false;
        MessageBox.Show("BlackBerry is disconnected");
      }
      return retVal;
    }

    private void smsBtn_Click(object sender, EventArgs e)
    {
      if (IsConnected())
      {
        Device.Instance.SendSMS("", "");
      }
    }

    private void screenCapture_Click(object sender, EventArgs e)
    {
      if (IsConnected())
      {
        Device.Instance.TakeScreenCapture();
      }
    }

    private void clipboardBtn_Click(object sender, EventArgs e)
    {
      if (IsConnected())
      {
        Device.Instance.ReadClipboard();
      }
    }

    private void clipboardGetBtn_Click(object sender, EventArgs e)
    {
      if (IsConnected())
      {
        Device.Instance.WriteClipboard();
      }
    }


    

    private void signal_MouseDown(object sender, MouseEventArgs e)
    {

    }

    private void placeCallToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (IsConnected())
      {
        Device.Instance.DialPhone("");
      }
    }

    private void endCallToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (IsConnected())
      {
        Device.Instance.PressEndKey();
      }
    }

    private void speakerphoneToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (IsConnected())
      {
        Device.Instance.PressSpeakerKey();
      }
    }

    private void volumeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (IsConnected())
      {
        Device.Instance.PressVolumeUpKey();
      }
    }

    private void volumeToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      if (IsConnected())
      {
        Device.Instance.PressVolumeDownKey();
      }
    }

    private void sendSMSToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (IsConnected())
      {
        Device.Instance.SendSMS("", "");
      }
    }

    private void keyboardToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (IsConnected())
      {
        //m_form.showCommandDlg();
      }
    }

    private void screenShotToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (IsConnected())
      {
        Device.Instance.TakeScreenCapture();
      }
    }

    private void sendToBlackBerryToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (IsConnected())
      {
        Device.Instance.ReadClipboard();
      }
    }

    private void sendToPCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (IsConnected())
      {
        Device.Instance.WriteClipboard();
      }
    }

    private void runMacroToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (IsConnected())
      {
        Device.Instance.RunMacro("OnMacro");
      }
    }

    private void buzzToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (IsConnected())
      {
        Device.Instance.Buzz();
      }
    }

    private void keepOnTopToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (ApplicationSettings.Instance.ToolbarTopMost)
      {
        ApplicationSettings.Instance.ToolbarTopMost = false;
        ApplicationSettings.Instance.SaveAppSettings();
        this.TopMost = false;
      }
      else
      {
        ApplicationSettings.Instance.ToolbarTopMost = true;
        ApplicationSettings.Instance.SaveAppSettings();
        this.TopMost = true;
      }
    }

    private void minimizeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      toggleSize();
    }

    private void toggleSize()
    {
      if (ClientSize.Height > 100)
      {
        ApplicationSettings.Instance.ToolBar.State = 1; 
        ApplicationSettings.Instance.SetDirty();
        ApplicationSettings.Instance.SaveAppSettings();
        this.ClientSize = new System.Drawing.Size(82, 50);
        Location = m_lastPos;
      }
      else
      {
        ApplicationSettings.Instance.ToolBar.State = 0;
        ApplicationSettings.Instance.SetDirty();
        ApplicationSettings.Instance.SaveAppSettings();
        this.ClientSize = new System.Drawing.Size(82, 432);
        Screen desktop = Screen.PrimaryScreen;
        if (this.Top + ClientSize.Height > desktop.WorkingArea.Bottom)
        {
          this.Top = desktop.WorkingArea.Bottom - ClientSize.Height;
        }
      }
      setBackGround();
    }

    private void contextMenu_Opening(object sender, CancelEventArgs e)
    {
      if (Device.Instance.IsConnected())
      {
        phoneToolStripMenuItem.Enabled = true;
        sendSMSToolStripMenuItem.Enabled = true;
        buzzToolStripMenuItem.Enabled = true;
        keyboardToolStripMenuItem.Enabled = true;
        screenShotToolStripMenuItem.Enabled = true;
        clipboardToolStripMenuItem.Enabled = true;
        runMacroToolStripMenuItem.Enabled = true;
      }
      else
      {
        phoneToolStripMenuItem.Enabled = false;
        sendSMSToolStripMenuItem.Enabled = false;
        buzzToolStripMenuItem.Enabled = false;
        keyboardToolStripMenuItem.Enabled = false;
        screenShotToolStripMenuItem.Enabled = false;
        clipboardToolStripMenuItem.Enabled = false;
        runMacroToolStripMenuItem.Enabled = false;
      }

      if (ApplicationSettings.Instance.ToolbarTopMost)
      {
        keepOnTopToolStripMenuItem.Checked = true;
      }
      else
      {
        keepOnTopToolStripMenuItem.Checked = false;
      }

      if (ClientSize.Height > 100)
      {
        minimizeToolStripMenuItem.Text = "Minimize";
      }
      else
      {
        minimizeToolStripMenuItem.Text = "Maximize";
      }

    }

    private void closeBtn_MouseEnter(object sender, EventArgs e)
    {
      this.closeBtn.Image = global::Blurts.Properties.Resources.close_Btn_focus;
    }

    private void closeBtn_MouseLeave(object sender, EventArgs e)
    {
      this.closeBtn.Image = global::Blurts.Properties.Resources.close_Btn;
    }

    private void hideToolStripMenuItem_Click(object sender, EventArgs e)
    {
      hideToolbar();
    }

    private void Toolbar_MouseEnter(object sender, EventArgs e)
    {
      FadeUpTo(100);
    }

    private void Toolbar_MouseLeave(object sender, EventArgs e)
    {
      if (!Bounds.Contains(Cursor.Position))
      {
        FadeDownTo(ApplicationSettings.Instance.ToolbarOpacity);
      }
    }

    private void FadeUpTo(int max)
    {
      for (int i = Int32Opacity; i <= max; i += 5)
      {
        SetOpacityAndWait(i);
      }
    }

    private int Int32Opacity
    {
      get { return (int)(Opacity * 100); }
    }

    private void FadeDownTo(int min)
    {
      for (int i = Int32Opacity; i >= min; i -= 5)
      {
        SetOpacityAndWait(i);
      }
    }

    private void SetOpacityAndWait(int opacity)
    {
      Opacity = opacity / 100d;
      Thread.Sleep(20);
    }
  }
}