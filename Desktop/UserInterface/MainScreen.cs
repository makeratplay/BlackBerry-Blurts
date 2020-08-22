using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;
using System.Runtime.InteropServices;
using MSScriptControl;
using System.IO;
using System.Text.RegularExpressions;
using Blurts.Domain;
namespace Blurts
{

  public delegate void SMSMessageEventHandler();
  public delegate void DisconnectEventHandler();


  [ComVisible(true)]
  public partial class MainScreen : Form
  {
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]

    #region PInvoke declarations

    [DllImport("User32.dll")]
    public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

    [DllImport("User32.dll")]
    public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hwnd, int Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool LockWorkStation();

    #endregion


    public event SMSMessageEventHandler SMSMessageEvent;
    public event DisconnectEventHandler DisconnectEvent;

    private SignalLevelCtrl signal;
    private BatteryLevelCtrl battery;
    private ToolTip m_toolTip;
    private int rightMargin = 10;
    
    
    private Toolbar m_toolbar;
    private Boolean m_autoConnecting;
    private eTabs activeTab;
    private IntPtr _nextClipboardViewer;		// The next clipboard in the Windows clipboard chain

    

    public Boolean m_pauseMsgs;
    public string m_clipBoardData;
    public string m_screenCaptureImage;
    private List<IDisplayItem> displayQue;
    public ScriptControlClass m_scriptControl;

    enum eTabs
    {
      eTab_Home,
      eTab_ScreenShot,
      eTab_All,
      eTab_Keypad
    }


    public MainScreen()
    {
      /*
      string[] args = Environment.GetCommandLineArgs();
      if ( args.Length > 1 )
      {
        MessageBox.Show(args[1]);
        this.Close();
        return;
      }
       * */



      this.displayQue = new List<IDisplayItem>();

      m_autoConnecting = false;
      m_pauseMsgs = false;
      m_clipBoardData = "";
      
      Device.Instance.BluetoothConnectEvent += new BluetoothConnectEventHandler(OnBlackBerryConnectEvent);
      Device.Instance.BluetoothDisconnectEvent += new BluetoothDisconnectEventHandler(OnBlackBerryDisconnectEvent);
      Device.Instance.MsgArrivedEvent += new MsgArrivedEventHandler(Instance_MsgArrivedEvent);

      m_toolbar = new Toolbar();
      Boolean toolBarVisable = ApplicationSettings.Instance.ToolBar.Visable;
      m_toolbar.showToolbar();
      if (!toolBarVisable)
      {
        m_toolbar.hideToolbar();
      }
      m_scriptControl = null;

      if (ApplicationSettings.Instance.EnableScript)
      {
        try
        {
          m_scriptControl = new ScriptControlClass();
          if (m_scriptControl != null)
          {
            m_scriptControl.Language = ApplicationSettings.Instance.ScriptLanguage;
            m_scriptControl.AddObject("AlertWindow", this, true);
            m_scriptControl.AddObject("BlackBerry", Device.Instance, true);
          }
          else
          {
            MessageBox.Show("Microsoft Scripting control is not installed.", "Blurts", MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
        }
        catch (Exception)
        {
          MessageBox.Show("Microsoft Scripting control is not installed.", "Blurts", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }

      // setup file extension support
      /*
      Registry.ClassesRoot.CreateSubKey(".blurts").SetValue("", "Blurts", Microsoft.Win32.RegistryValueKind.String);
      Registry.ClassesRoot.CreateSubKey(@"Blurts\shell\open\command").SetValue("", Application.ExecutablePath + " \"%l\" ", Microsoft.Win32.RegistryValueKind.String);
      */

      
      

      if (!Directory.Exists(ApplicationSettings.Instance.LocalDataPath + @"\SMS"))
      {
        Directory.CreateDirectory(ApplicationSettings.Instance.LocalDataPath + @"\SMS");
      }

      HotKeysManager.Instance.SetHotKeys();



      InitializeComponent();

      
      // 
      // signal
      // 
      this.signal = new SignalLevelCtrl();
      this.signal.BackColor = System.Drawing.Color.Transparent;
      this.signal.Location = new System.Drawing.Point(440, 14);
      this.signal.Name = "signal";
      this.signal.Size = new System.Drawing.Size(35, 14);
      this.signal.TabIndex = 9;
      this.signal.TabStop = false;
      this.Controls.Add(this.signal);

      // 
      // battery
      // 
      this.battery = new BatteryLevelCtrl();
      this.battery.BackColor = System.Drawing.Color.Transparent;
      this.battery.Location = new System.Drawing.Point(436, 34);
      this.battery.Name = "battery";
      this.battery.Size = new System.Drawing.Size(43, 14);
      this.battery.TabIndex = 8;
      this.battery.TabStop = false;
      this.Controls.Add(this.battery);

      MainScreen_Resize(null, null);


      notifyIcon.Text = Application.ProductName + " - MLH Software";

    }

    void Instance_MsgArrivedEvent(object msg)
    {
      IDisplayItem alert = (IDisplayItem)msg;
      this.displayQue.Add(alert);
      Invoke(new EventHandler(DisplayAlert));
    }


    public void DisplayAlert(object sender, EventArgs e)
    {
      IDisplayItem msg = null;
      lock (this.displayQue)
      {
        int count = this.displayQue.Count;
        if (count > 0)
        {
          msg = this.displayQue[0];
          this.displayQue.RemoveAt(0);
        }
      }

      if (msg != null)
      {
        if (msg.ItemType == DisplayItemType.CLIPBOARD)
        {
          try
          {
            System.Windows.Forms.Clipboard.SetDataObject(((ClipboardAlert)msg).Text, true);
          }
          catch (Exception ex)
          {
            Console.WriteLine("Error Writting to Clipboard:" + Environment.NewLine + ex.ToString());
          }
        }

        if (msg.ItemType == DisplayItemType.STATUS_SCREEN_SHOT ||
             msg.ItemType == DisplayItemType.SCREEN ||
             msg.ItemType == DisplayItemType.CLIPBOARD)
        {
          DisplayScreenShot(msg);
        }
        else if (msg.DisplayAlert && !m_pauseMsgs)
        {
          AlertScreen.DisplayAlert(msg);
        }

        if (this.activeTab == eTabs.eTab_Home)
        {
          Display();
        }
      }
    }

    private void showCommandDlg()
    {
      CommandDlg dlg = new CommandDlg(this);
      dlg.ShowDialog();
    }

    private void MainScreen_Load(object sender, EventArgs e)
    {
      connectTimer.Start();
      disconnectMenuItem.Visible = false;

      m_autoConnecting = true;
      OnBlackBerryDisconnectEvent();


      string address = ApplicationSettings.Instance.DeviceAddress;
      if (address == null || address.Length == 0)
      {
        try
        {
          System.Diagnostics.Process.Start("http://www.mlhsoftware.com/Blurts/setup.html");
        }
        catch (Exception)
        {
          //MessageBox.Show("Failed to open http://www.mlhsoftware.com/blurts/helpSetupDlg.html");
        }

        OnSetupMenu();
      }


      m_toolTip = new ToolTip();
      // Set up the delays for the ToolTip.
      m_toolTip.AutoPopDelay = 5000;
      m_toolTip.InitialDelay = 1000;
      m_toolTip.ReshowDelay = 500;
      // Force the ToolTip text to be displayed whether or not the form is active.
      m_toolTip.ShowAlways = true;

      // Set up the ToolTip text for the Button and Checkbox.
      m_toolTip.SetToolTip(this.blurtsIcon, "Blurts connected");
      m_toolTip.SetToolTip(this.homeBtn, "Home");
      m_toolTip.SetToolTip(this.phoneBtn, "Phone");
      m_toolTip.SetToolTip(this.smsBtn, "SMS");
      m_toolTip.SetToolTip(this.emailBtn, "Email");
      m_toolTip.SetToolTip(this.winClipboardBtn, "Send Windows Clipboard");
      m_toolTip.SetToolTip(this.bbClipboardBtn, "Read BlackBerry Clipboard");
      m_toolTip.SetToolTip(this.keybaordBtn, "Keybaord");
      m_toolTip.SetToolTip(this.screenCaptureBtn, "Capture BlackBerry Screen");
      m_toolTip.SetToolTip(this.contactsBtn, "Contacts");

      webBrowser.AllowWebBrowserDrop = false;
      webBrowser.IsWebBrowserContextMenuEnabled = false;
      webBrowser.WebBrowserShortcutsEnabled = true;
      webBrowser.ScriptErrorsSuppressed = true;
      webBrowser.ObjectForScripting = this;
      Display();

      // Sign up for clipboard change notifications from the operating system.
      _nextClipboardViewer = SetClipboardViewer(Handle);
    }

    private void DisplayScreenShot(IDisplayItem msg)
    {
      //this.Cursor = Cursors.WaitCursor;
      XslCompiledTransform xsl = new XslCompiledTransform();
      xsl.Load(ApplicationSettings.Instance.AppPath + @"\screenShotTab.xslt");


      XsltArgumentList argList = new XsltArgumentList();
      argList.AddParam("systemPath", "", ApplicationSettings.Instance.AppPath);
      argList.AddParam("dataPath", "", ApplicationSettings.Instance.LocalDataPath);
      //argList.AddParam("contactFilter", "", m_contactFilter);
      //argList.AddParam("scrollTop", "", m_scrollTop.ToString());


      XmlDocument xmlDoc = new XmlDocument();
      xmlDoc.LoadXml(msg.XML);

      if (Directory.Exists(ApplicationSettings.Instance.LocalDataPath + @"\debug"))
      {
        StreamWriter SW;
        SW = File.CreateText(ApplicationSettings.Instance.LocalDataPath + @"\debug\DisplayScreenShot.xml");
        SW.Write(msg.XML);
        SW.Close();
      }

      XmlDocument outputDocument = new XmlDocument();
      System.Xml.XPath.XPathNavigator outputNavigator = outputDocument.CreateNavigator();
      using (XmlWriter writer = outputNavigator.AppendChild())
      {
        xsl.Transform(xmlDoc, argList, writer);
      }
      webBrowser.DocumentText = outputDocument.OuterXml;

      this.activeTab = eTabs.eTab_ScreenShot;
    }

    // Invoke the Changed event; called whenever list changes
    public virtual void OnSMSMessageEvent(EventArgs e)
    {
      if (SMSMessageEvent != null)
      {
        SMSMessageEvent();
      }
    }

    public void FireSMSEvent(object sender, EventArgs e)
    {
      OnSMSMessageEvent(EventArgs.Empty);
    }

    public void OnAnswerCallHotKey(object o, EventArgs e)
    {
      Device.Instance.PressSendKey();
    }

    public void OnEndCallHotKey(object o, EventArgs e)
    {
      Device.Instance.PressEndKey();
    }

    public void OnConnectTimer()
    {
      if (!Device.Instance.IsConnected())
      {
        if (ApplicationSettings.Instance.AutoConnect)
        {
          m_autoConnecting = true;
          OnConnectMenu();
        }
      }
    }

    public void OnBlackBerryConnectEvent()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new EventHandler(OnBlackBerryConnect));
      }
      else
      {
        OnBlackBerryConnect(null, null);
      }
    }

    public void OnBlackBerryConnect(object o, EventArgs e)
    {
      m_autoConnecting = false;
      setConnectedIcon();
      connectMenuItem.Visible = false;
      disconnectMenuItem.Visible = true;
    }

    private void connectTimer_Tick(object sender, EventArgs e)
    {
      OnConnectTimer();
    }

    public void setMsgIcon()
    {
      if (!m_pauseMsgs)
      {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlertScreen));
        notifyIcon.Icon = (System.Drawing.Icon)global::Blurts.Properties.Resources.icon_msg;
      }
    }

    public void setConnectedIcon()
    {
      if (Device.Instance.IsConnected())
      {
        this.blurtsIcon.Image = global::Blurts.Properties.Resources.Blurts_64x64;

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlertScreen));

        if (!m_pauseMsgs)
        {
          notifyIcon.Icon = (System.Drawing.Icon)global::Blurts.Properties.Resources.connected;
        }
        else
        {
          notifyIcon.Icon = (System.Drawing.Icon)global::Blurts.Properties.Resources.paused;
        }
      }
    }

    public void OnBlackBerryDisconnectEvent()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new EventHandler(OnBlackBerryDisconnect));
      }
      else
      {
        OnBlackBerryDisconnect(null, null);
      }
    }

    public void OnBlackBerryDisconnect(object o, EventArgs e)
    {
      //Device.Instance.Disconnect();
      this.blurtsIcon.Image = global::Blurts.Properties.Resources.Blurts_red_64x64;

      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlertScreen));
      notifyIcon.Icon = (System.Drawing.Icon)global::Blurts.Properties.Resources.disconnected;
      connectMenuItem.Visible = true;

      if (DisconnectEvent != null)
      {
        DisconnectEvent();
      }

      if (!m_autoConnecting)
      {
        if (ApplicationSettings.Instance.AutoLock)
        {
          if (ApplicationSettings.Instance.Active)
          {
            LockWorkStation();
          }
        }
      }

      disconnectMenuItem.Visible = false;
      ApplicationSettings.Instance.Active = false;
    }

    public void setClipBoard(object sender, EventArgs e)
    {
      Clipboard = m_clipBoardData;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      int x = 0; //menuStrip.Right
      int width = ClientSize.Width; //ClientSize.Width - (menuStrip.Width + blurtsIcon.Width)
      Rectangle rect = new Rectangle(x, 0, width, blurtsIcon.Height);
      Rectangle rect2 = new Rectangle(x - 1, 0, rect.Width + 2, rect.Height);

      System.Drawing.Drawing2D.LinearGradientBrush baseBackground;
      baseBackground = new System.Drawing.Drawing2D.LinearGradientBrush(rect2, Color.White, Color.FromArgb(3, 100, 255), 45, true);
      e.Graphics.FillRectangle(baseBackground, rect);
      e.Graphics.DrawLine(new Pen(Color.Black), new Point(0, blurtsIcon.Height), new Point(ClientSize.Width, blurtsIcon.Height));
    }

    private void MainScreen_Resize(object sender, EventArgs e)
    {
      webBrowser.Top = blurtsIcon.Bottom + 1;
      webBrowser.Height = ClientSize.Height - (blurtsIcon.Bottom + 1);
      webBrowser.Width = ClientSize.Width;
      signal.Left = (ClientSize.Width - battery.Width) - rightMargin; // use batter image width for both
      battery.Left = (ClientSize.Width - battery.Width) - rightMargin;

      int toolbarWidth = homeBtn.Width * 9;
      int toolbarLeft = (ClientSize.Width - toolbarWidth) / 2;
      homeBtn.Left = toolbarLeft;
      phoneBtn.Left = toolbarLeft += homeBtn.Width;
      smsBtn.Left = toolbarLeft += homeBtn.Width;
      emailBtn.Left = toolbarLeft += homeBtn.Width;
      winClipboardBtn.Left = toolbarLeft += homeBtn.Width;
      bbClipboardBtn.Left = toolbarLeft += homeBtn.Width;
      keybaordBtn.Left = toolbarLeft += homeBtn.Width;
      screenCaptureBtn.Left = toolbarLeft += homeBtn.Width;
      contactsBtn.Left = toolbarLeft += homeBtn.Width;
      
      Invalidate(true);
    }


    private void Display()
    {
      //this.Cursor = Cursors.WaitCursor;
      XslCompiledTransform xsl = new XslCompiledTransform();
      xsl.Load(ApplicationSettings.Instance.AppPath + @"\homeTab.xslt");
      // AppPath is startup and true app path

    //  if (webBrowser.Document != null)
    //  {
    //    m_scrollTop = webBrowser.Document.Body.ScrollTop;
    //    m_scrollLeft = webBrowser.Document.Body.ScrollLeft;
    //  }

      XsltArgumentList argList = new XsltArgumentList();
      argList.AddParam("systemPath", "", ApplicationSettings.Instance.AppPath);
      argList.AddParam("dataPath", "", ApplicationSettings.Instance.LocalDataPath);
      //argList.AddParam("contactFilter", "", m_contactFilter);
      //argList.AddParam("scrollTop", "", m_scrollTop.ToString());


      XmlDocument outputDocument = new XmlDocument();
      System.Xml.XPath.XPathNavigator outputNavigator = outputDocument.CreateNavigator();
      using (XmlWriter writer = outputNavigator.AppendChild())
      {
        xsl.Transform(AlertHistory.Instance.XMLDoc, argList, writer);
      }
      webBrowser.DocumentText = outputDocument.OuterXml;

      this.activeTab = eTabs.eTab_Home;
    }

    private void contactsBtn_MouseDown(object sender, MouseEventArgs e)
    {
      contactsBtn.Left = contactsBtn.Left + 1;
      contactsBtn.Top = contactsBtn.Top + 1;
    }

    private void contactsBtn_MouseUp(object sender, MouseEventArgs e)
    {
      contactsBtn.Left = contactsBtn.Left - 1;
      contactsBtn.Top = contactsBtn.Top - 1;
    }

    private void contactsBtn_MouseClick(object sender, MouseEventArgs e)
    {
      DisplayContacts();
    }

    private void DisplayContacts()
    {
      XslCompiledTransform xsl = new XslCompiledTransform();
      xsl.Load(ApplicationSettings.Instance.AppPath + @"\contacts.xslt");
      // AppPath is startup and true app path

      //  if (webBrowser.Document != null)
      //  {
      //    m_scrollTop = webBrowser.Document.Body.ScrollTop;
      //    m_scrollLeft = webBrowser.Document.Body.ScrollLeft;
      //  }

      XsltArgumentList argList = new XsltArgumentList();
      argList.AddParam("systemPath", "", ApplicationSettings.Instance.AppPath);
      argList.AddParam("dataPath", "", ApplicationSettings.Instance.LocalDataPath);
      //argList.AddParam("contactFilter", "", m_contactFilter);
      //argList.AddParam("scrollTop", "", m_scrollTop.ToString());


      XmlDocument outputDocument = new XmlDocument();
      System.Xml.XPath.XPathNavigator outputNavigator = outputDocument.CreateNavigator();
      using (XmlWriter writer = outputNavigator.AppendChild())
      {
        xsl.Transform(Contacts.Instance.XMLDoc, argList, writer);
      }
      webBrowser.DocumentText = outputDocument.OuterXml;
    }

    public String Clipboard
    {
      get
      {
        string text = "";
        if (System.Windows.Forms.Clipboard.GetDataObject().GetDataPresent(DataFormats.UnicodeText))
        {
          text = System.Windows.Forms.Clipboard.GetDataObject().GetData(DataFormats.UnicodeText).ToString();
        }
        return text;
      }
      set
      {
        try
        {
          System.Windows.Forms.Clipboard.SetDataObject(value, true);
        }
        catch (Exception ex)
        {
          Console.WriteLine("Error Writting to Clipboard:" + Environment.NewLine + ex.ToString());
        }
      }
    }

    private void MainScreen_FormClosing(object sender, FormClosingEventArgs e)
    {
      // Remove ourselves from OS clipboard notifications
      ChangeClipboardChain(Handle, _nextClipboardViewer);

      HotKeysManager.Instance.ReleaseHotKeys();
      Device.Instance.Exiting();
      OnDisconnectMenu();
    }

    /// <summary>
    /// Handles the clipboard calls from the operating system and forwards them
    /// to our methods when appropriate.
    /// </summary>
    protected override void WndProc(ref Message m)
    {
      switch (m.Msg)
      {
        case 0x0308: // WM_DRAWCLIPBOARD
        {
          CheckClipboard();
          SendMessage(_nextClipboardViewer, m.Msg, m.WParam, m.LParam);
          break;
        }

        case 0x030D: // WM_CHANGECBCHAIN
        {
          if (m.WParam == _nextClipboardViewer)
          {
            _nextClipboardViewer = m.LParam;
          }
          else
          {
            SendMessage(_nextClipboardViewer, m.Msg, m.WParam, m.LParam);
          }
          break;
        }
        case 0x112: // WM_SYSCOMMAND
        {
          if (m.WParam == (IntPtr)0xF020) //SC_MINIMIZE
          {
            this.Hide();
            this.notifyIcon.Visible = true;
          }
          else
          {
            base.WndProc(ref m);
          }
          break;
        }

        default:
          base.WndProc(ref m);
          break;
      }
    }

    private void CheckClipboard()
    {
      IDataObject dataObject;

      try
      {
        dataObject = System.Windows.Forms.Clipboard.GetDataObject();
      }
      catch
      {
        // Please don't try this at home.
        return;
      }

      if (dataObject != null)
      {
        if (dataObject.GetDataPresent(DataFormats.Text))
        {
          string data = dataObject.GetData(DataFormats.Text) as string;

          if (data != null)
          {
            data = data.Trim();
            Console.WriteLine("Clipboard:" + data);

            Regex re = new Regex(@"^[01]?[- .]?\(?[2-9]\d{2}\)?[- .]?\d{3}[- .]?\d{4}$", RegexOptions.IgnoreCase);
            Match m = re.Match(data);

            if (m.Success)
            {
              PhoneNumberAction msg = new PhoneNumberAction(data);
              msg.ProcessMessage();
              IDisplayItem alert = (IDisplayItem)msg;
              this.displayQue.Add(alert);
              Invoke(new EventHandler(DisplayAlert));

              Console.WriteLine("Is a phone number!");
            }
            else
            {
              Console.WriteLine("NOT a phone number!");
            }

            
          }
        }
      }
    }

    private void OnSetupMenu()
    {
      SetupForm setup = new SetupForm();
      setup.ShowDialog();
    }

    private void OnConnectMenu()
    {
      Device.Instance.Connect();
    }

    private void OnDisconnectMenu()
    {
      Device.Instance.Disconnect();
    }

    /************************************************************************ 
    * 
    *                     Menu Handlers
    * 
    *************************************************************************/

    public void sendKeyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Device.Instance.PressSendKey();
    }

    public void menuKeyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Device.Instance.PressMenuKey();
    }

    public void escKeyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Device.Instance.PressEscKey();
    }

    public void endKeyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Device.Instance.PressEndKey();
    }

    public void clickToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Device.Instance.ClickTrackball();
    }

    public void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Device.Instance.MoveTrackballUp();
    }

    public void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Device.Instance.MoveTrackballDown();
    }

    public void moveLeftToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Device.Instance.MoveTrackballLeft();
    }

    public void moveRightToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Device.Instance.MoveTrackballRight();
    }

    private void buzzToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Device.Instance.Buzz();
    }

    private void hangUpToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Device.Instance.PressEndKey();
    }

    private void endCallToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Device.Instance.PressEndKey();
    }

    private void speakerphoneToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Device.Instance.PressSpeakerKey();
    }

    private void volumeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Device.Instance.PressVolumeUpKey();
    }

    private void volumeToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      Device.Instance.PressVolumeDownKey();
    }

    private void placeCallToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
      Device.Instance.DialPhone("");
      /*
      ContactDlg dlg = new ContactDlg(this, false);

      if (dlg.ShowDialog() == DialogResult.OK)
      {
        if (dlg.m_number != null && dlg.m_number.Length > 0)
        {
          Device.Instance.DialPhone( dlg.m_formatedNumber );
        }
      }
       * */

      /*
      DialDlg dlg = new DialDlg(this);
      Screen desktop = Screen.PrimaryScreen;
      dlg.Location = new Point(desktop.WorkingArea.Width - dlg.Width - 10, desktop.WorkingArea.Height - dlg.Height - 10);

      if (dlg.ShowDialog() == DialogResult.OK)
      {
        sendCommand("1", dlg.getPhoneNumber(), "", "");
      }
       * */
    }


    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AboutDlg dlg = new AboutDlg();
      dlg.ShowDialog();
    }

    private void helpToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        System.Diagnostics.Process.Start("http://www.mlhsoftware.com/blurts/help.html");
      }
      catch (Exception)
      {
        MessageBox.Show("Failed to open http://www.mlhsoftware.com/blurts/help.html");
      }
    }

    private void showLastMgToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AlertScreen.DisplayAlert(null);
    }

    private void sendSMSToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //FIX OnSendSMS("", "");
    }

    private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
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
        //FIX blackBerryToolStripMenuItem.Enabled = true;
      }
      else
      {
        phoneToolStripMenuItem.Enabled = false;
        sendSMSToolStripMenuItem.Enabled = false;
        buzzToolStripMenuItem.Enabled = false;
        //keyboardToolStripMenuItem.Enabled = false;
        screenShotToolStripMenuItem.Enabled = false;
        clipboardToolStripMenuItem.Enabled = false;
        runMacroToolStripMenuItem.Enabled = false;
        //FIX blackBerryToolStripMenuItem.Enabled = false;
      }

      if (m_pauseMsgs)
      {
        pauseMessagesToolStripMenuItem.Text = "Resume Messages";
      }
      else
      {
        pauseMessagesToolStripMenuItem.Text = "Suspend Messages";
      }

      if (m_toolbar.Visible)
      {
        toolbarToolStripMenuItem.Text = "Hide Toolbar";
      }
      else
      {
        toolbarToolStripMenuItem.Text = "Show Toolbar";
      }

      if (ApplicationSettings.Instance.EnableScript)
      {
        runMacroToolStripMenuItem.Visible = true;
      }
      else
      {
        runMacroToolStripMenuItem.Visible = false;
      }

    }

    private void pauseMessagesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      m_pauseMsgs = !m_pauseMsgs;
      setConnectedIcon();
    }

    private void screenShotToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Device.Instance.TakeScreenCapture();
    }



    private void keyboardToolStripMenuItem_Click(object sender, EventArgs e)
    {
      KeyboardDlg dlg = new KeyboardDlg();
      dlg.ShowDialog();
    }

    private void contactsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Device.Instance.DownloadContacts();
    }

    private void sendToBlackBerryToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Device.Instance.ReadClipboard();
    }

    private void sendToPCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Device.Instance.WriteClipboard();
    }

    private void disconnectMenuItem_Click(object sender, EventArgs e)
    {
      OnDisconnectMenu();
    }

    private void connectMenuItem_Click(object sender, EventArgs e)
    {
      OnConnectMenu();
    }

    private void setupMenuItem_Click(object sender, EventArgs e)
    {
      OnSetupMenu();
    }

    private void notifyIcon_DoubleClick(object sender, EventArgs e)
    {
      if (ApplicationSettings.Instance.ShowLastMsg)
      {
        AlertScreen.DisplayAlert(null);
      }
      else
      {
        this.Show();
        this.notifyIcon.Visible = false;
        if (m_toolbar.Visible)
        {
          m_toolbar.hideToolbar();
        }
        else
        {
          m_toolbar.showToolbar();
        }
      }
    }

    private void runMacroToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Device.Instance.RunMacro("OnMacro");
    }

    private void toolbarToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (m_toolbar.Visible)
      {
        m_toolbar.hideToolbar();
      }
      else
      {
        m_toolbar.showToolbar();
      }
    }


    private void contactsToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
      ContactDlg dlg = new ContactDlg();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
      }
    }

    [ComVisible(true)]
    public void OnSaveImage()
    {
      SaveFileDialog dlg = new SaveFileDialog();

      //dlg.InitialDirectory = Environment.GetEnvironmentVariable("SystemRoot") + @"\Media";

      // Allow selection of .jpg files only.
      dlg.Filter = "JPEG files (*.jpg)|*.jpg";
      dlg.DefaultExt = ".jpg";

      // Activate the file selection dialog.
      if (dlg.ShowDialog() == DialogResult.OK)
      {
 //FIX       File.Copy(imageName, dlg.FileName);
      }
    }

    private void screenCaptureBtn_Click(object sender, EventArgs e)
    {
      screenShotToolStripMenuItem_Click(sender, e);
    }

    private void phoneBtn_Click(object sender, EventArgs e)
    {
      webBrowser.Hide();
      SMSContactDlg dlg = new SMSContactDlg(false);
      dlg.MdiParent = this;

      dlg.Top = blurtsIcon.Bottom + 1;
      dlg.Height = ClientSize.Height - (blurtsIcon.Bottom + 1);
      dlg.Width = ClientSize.Width;

      dlg.Show();
      //DisplayCallScreen();
    }

    private void DisplayCallScreen()
    {
      XslCompiledTransform xsl = new XslCompiledTransform();
      xsl.Load(ApplicationSettings.Instance.AppPath + @"\smscontact.xslt");
      // AppPath is startup and true app path


      XsltArgumentList argList = new XsltArgumentList();
      argList.AddParam("systemPath", "", ApplicationSettings.Instance.AppPath);
      argList.AddParam("dataPath", "", ApplicationSettings.Instance.LocalDataPath);
  //    argList.AddParam("contactFilter", "", m_contactFilter);


      XmlDocument outputDocument = new XmlDocument();
      System.Xml.XPath.XPathNavigator outputNavigator = outputDocument.CreateNavigator();
      using (XmlWriter writer = outputNavigator.AppendChild())
      {
   //     if (m_contactFilter.Length > 0 || m_tab == eTabs.eTab_All)
        {
          xsl.Transform(Contacts.Instance.XMLDoc, argList, writer);
        }
   //     else if (m_tab == eTabs.eTab_Favorite)
   //     {
   //       argList.AddParam("favoriteFilter", "", "true");
   //       xsl.Transform(Contacts.Instance.XMLDoc, argList, writer);
   //     }
   //     else
   //     {
   //       xsl.Transform(m_HxXmlDoc, argList, writer);
   //     }
      }

      webBrowser.DocumentText = outputDocument.OuterXml;
    }

    private void homeBtn_Click(object sender, EventArgs e)
    {
      Display();
    }

    private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        System.Diagnostics.Process.Start("http://www.mlhsoftware.com/blurts/help.html");
      }
      catch (Exception)
      {
        MessageBox.Show("Failed to open http://www.mlhsoftware.com/blurts/help.html");
      }
    }

    private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
      AboutDlg dlg = new AboutDlg();
      dlg.ShowDialog();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}