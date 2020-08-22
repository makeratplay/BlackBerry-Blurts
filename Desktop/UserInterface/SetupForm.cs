using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Media;

using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using InTheHand.Net;
using InTheHand.Net.Bluetooth.AttributeIds;
using IWshRuntimeLibrary;
using System.IO;

using System.IO.Ports;

namespace Blurts
{
  public partial class SetupForm : Form
  {
    private Boolean m_useCOMPort;

    private List<Label> m_labelHotKey;
    private List<ComboBox> m_cbHotKeyMod;
    private List<ComboBox> m_cbHotKey;

    public SetupForm()
    {
      m_useCOMPort = false;
      InitializeComponent();

      m_labelHotKey = new List<Label>();
      m_cbHotKeyMod = new List<ComboBox>();
      m_cbHotKey = new List<ComboBox>();

      bbNameTxt.Text = ApplicationSettings.Instance.DeviceName;
      disTimetxtBox.Text = ApplicationSettings.Instance.DisplayTime.ToString();
      smsTxtBox.Text = ApplicationSettings.Instance.MaxSMS.ToString();
      startUpChk.Checked = shortCutPresent();
      autoConnectChk.Checked = ApplicationSettings.Instance.AutoConnect;
      lockChkBox.Checked = ApplicationSettings.Instance.AutoLock;
      soundFileTxtBox.Text = ApplicationSettings.Instance.SoundFile;
      showPhotosChkBox.Checked = ApplicationSettings.Instance.ShowPhotos;
      smsMultiMsg.Checked = ApplicationSettings.Instance.SmsMultiMsg;
      smsImage.Checked = ApplicationSettings.Instance.SmsShowImages;
      enableScriptChkBx.Checked = ApplicationSettings.Instance.EnableScript;
      enableScriptErrMsgChkbx.Checked = ApplicationSettings.Instance.EnableScriptErrorMsg;
      scriptFileTextbox.Text = ApplicationSettings.Instance.ScriptFile;
      toolBarTopMostChkBox.Checked = ApplicationSettings.Instance.ToolbarTopMost;
      showToolbarOnConnectchkBox.Checked = ApplicationSettings.Instance.ShowToolbarOnConnect;
      dbClickShowMsg.Checked = ApplicationSettings.Instance.ShowLastMsg;

      tbToolbarOpacity.Text = ApplicationSettings.Instance.ToolbarOpacity.ToString();
      tbAlertOpacity.Text = ApplicationSettings.Instance.AlertOpacity.ToString();

      smsBgColor.Text = ApplicationSettings.Instance.SmsBackgroundColor;
      smsTextColor.Text = ApplicationSettings.Instance.SmsTextColor;

      initHotKeys();

      if (!ApplicationSettings.Instance.Active)
      {
        lockChkBox.Enabled = false;
        soundFileTxtBox.Enabled = false;
        soundFileBtn.Enabled = false;
        testSndBtn.Enabled = false;
        clearSndBtn.Enabled = false;
        enableScriptChkBx.Enabled = false;
        enableScriptErrMsgChkbx.Enabled = false;
        scriptFileTextbox.Enabled = false;
        scriptBtn.Enabled = false;
      }

      if (BluetoothRadio.PrimaryRadio == null)
      {
        helpTxtBox.Text = "Create a virtual COM Port for the Blurts BlackBerry service and then select the COM port below. Click Help (? button in titlebar) for more help.";
        bbLabel.Visible = false;
        bbNameTxt.Visible = false;
        adrLabel.Visible = false;
        addressTxt.Visible = false;
        searchBtn.Visible = false;
        pairBtn.Visible = false;
        m_useCOMPort = true;

        string[] ports = SerialPort.GetPortNames();
        portCmbBox.DataSource = ports;

        portCmbBox.Text = ApplicationSettings.Instance.DeviceAddress;
      }
      else
      {
        helpTxtBox.Text = "Select search to find your BlackBerry or type your BlackBerry's Bluetooth address below. Click Help (? button in titlebar) for more help.";
        comLabel.Visible = false;
        portCmbBox.Visible = false;
        addressTxt.Text = ApplicationSettings.Instance.DeviceAddress;
      }

      // Gets an array of all the screens connected to the system.
      Screen[] screens = Screen.AllScreens;
      for (int index = 0; index < screens.Length; index++)
      {
        screenCB.Items.Add(index+1);
      }
      if (ApplicationSettings.Instance.ScreenEnum < screens.Length)
      {
        screenCB.SelectedIndex = ApplicationSettings.Instance.ScreenEnum;
      }

      locationList.Items.Add("top left");
      locationList.Items.Add("top right");
      locationList.Items.Add("bottom left");
      locationList.Items.Add("bottom right");
      locationList.SelectedIndex = ApplicationSettings.Instance.LocationEnum;
    }

    private void searchBtn_Click(object sender, EventArgs e)
    {
      this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
      BluetoothClient client = new BluetoothClient();
      BluetoothDeviceInfo[] deviceList = client.DiscoverDevices();
      BluetoothDeviceInfo currDevice = null;

/*      if (deviceList.Length == 1)
      {
        currDevice = deviceList[0];
      }
      else
  */    {
        DeviceListDlg dlg = new DeviceListDlg(deviceList);
        dlg.ShowDialog();
        currDevice = dlg.selectedDevice;
      }

      if (currDevice != null)
      {
        bbNameTxt.Text = currDevice.DeviceName;
        addressTxt.Text = currDevice.DeviceAddress.ToString();
        pairBtn_Click(null, null);
      }
      else
      {
        // no device found
      }
      this.Cursor = System.Windows.Forms.Cursors.Default;
    }

    private int parseIntWithDefault( String text, int defaultValue )
    {
      int retVal = defaultValue;
      try
      {
        retVal = Int16.Parse(text);
      }
      catch (Exception)
      {
      }
      return retVal;
    }

    private void save()
    {
      ApplicationSettings.Instance.DisplayTime = parseIntWithDefault(disTimetxtBox.Text, 5);
      ApplicationSettings.Instance.MaxSMS = parseIntWithDefault(smsTxtBox.Text, 160);
      ApplicationSettings.Instance.ToolbarOpacity = parseIntWithDefault(tbToolbarOpacity.Text, 100);
      ApplicationSettings.Instance.AlertOpacity = parseIntWithDefault(tbAlertOpacity.Text, 100);

      ApplicationSettings.Instance.SoundFile = soundFileTxtBox.Text;
      ApplicationSettings.Instance.DeviceName = bbNameTxt.Text;
      ApplicationSettings.Instance.AutoConnect = autoConnectChk.Checked;
      ApplicationSettings.Instance.AutoLock = lockChkBox.Checked;
      ApplicationSettings.Instance.ShowPhotos = showPhotosChkBox.Checked;
      ApplicationSettings.Instance.SmsMultiMsg = smsMultiMsg.Checked;
      ApplicationSettings.Instance.SmsShowImages = smsImage.Checked;
      ApplicationSettings.Instance.ScreenEnum = screenCB.SelectedIndex;
      ApplicationSettings.Instance.LocationEnum = locationList.SelectedIndex;
      ApplicationSettings.Instance.EnableScript = enableScriptChkBx.Checked;
      ApplicationSettings.Instance.EnableScriptErrorMsg = enableScriptErrMsgChkbx.Checked;
      ApplicationSettings.Instance.ScriptFile = scriptFileTextbox.Text;
      ApplicationSettings.Instance.ToolbarTopMost = toolBarTopMostChkBox.Checked;
      ApplicationSettings.Instance.ShowToolbarOnConnect = showToolbarOnConnectchkBox.Checked;
      ApplicationSettings.Instance.ShowLastMsg = dbClickShowMsg.Checked;
      ApplicationSettings.Instance.SmsBackgroundColor = smsBgColor.Text;
      ApplicationSettings.Instance.SmsTextColor = smsTextColor.Text;

      try
      {
        if (ApplicationSettings.Instance.EnableScript)
        {
          if (!System.IO.File.Exists(scriptFileTextbox.Text))
          {
            string fileName = @"\script.js";
            if ("VBScript" == ApplicationSettings.Instance.ScriptLanguage)
            {
              fileName = @"\script.vbs";
            }
            System.IO.File.Copy(ApplicationSettings.Instance.AppPath + fileName, scriptFileTextbox.Text);
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine("Copy Script file Exception: " + e.ToString());
      }

      try
      {
        int count = m_labelHotKey.Count;
        ApplicationSettings.Instance.HotKeys = new ApplicationSettings.HotKeySettings[count];
        for (int i = 0; i < count; i++)
        {
          ApplicationSettings.Instance.HotKeys[i] = new ApplicationSettings.HotKeySettings();
          ApplicationSettings.Instance.HotKeys[i].Command = m_labelHotKey[i].Text;
          ApplicationSettings.Instance.HotKeys[i].Modifier = m_cbHotKeyMod[i].Text;
          ApplicationSettings.Instance.HotKeys[i].KeyCode = m_cbHotKey[i].Text;
        }
      }
      catch (Exception e )
      {
        Console.WriteLine("Saving HotKeys Exception: " + e.ToString());
      }


    //  ApplicationSettings.Instance.SendKeyHotKey.alt = true;
    //  ApplicationSettings.Instance.SendKeyHotKey.keyCode = Keys.PageUp;

      if (m_useCOMPort)
      {
        ApplicationSettings.Instance.DeviceAddress = portCmbBox.Text;
      }
      else
      {
        ApplicationSettings.Instance.DeviceAddress = addressTxt.Text;
      }

      addShortCut(startUpChk.Checked);

      ApplicationSettings.Instance.SaveAppSettings();

      HotKeysManager.Instance.SetHotKeys();
    }

    private void OkBtn_Click(object sender, EventArgs e)
    {
      save();
      Close();
    }

    private void pairBtn_Click(object sender, EventArgs e)
    {
      try
      {
        if (addressTxt.Text.Length > 0)
        {
          BluetoothAddress btAddress = BluetoothAddress.Parse(addressTxt.Text);
          BluetoothDeviceInfo currDevice = new BluetoothDeviceInfo(btAddress);
          BluetoothSecurity.PairRequest(currDevice.DeviceAddress, null);
          bbNameTxt.Text = currDevice.DeviceName;
        }
      }
      catch (System.FormatException )
      {
        MessageBox.Show("Not a valid device address format.");
      }
    }

    private void addressTxt_TextChanged(object sender, EventArgs e)
    {
      if (addressTxt.Text.Length > 0 && !m_useCOMPort)
      {
        pairBtn.Enabled = true;
      }
      else
      {
        pairBtn.Enabled = false;
      }
    }

    private void addShortCut ( bool add )
    {
      String StartupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
      String shortcutFilePath = StartupFolder + @"\Blurts.lnk";
      if ( add )
      {
        // Add/create the shortcut to the Startup folder
        WshShellClass WshShell = new WshShellClass();
        IWshRuntimeLibrary.IWshShortcut MyShortcut;
        
        // The shortcut will be created in the Startup folder

        MyShortcut = (IWshRuntimeLibrary.IWshShortcut)WshShell.CreateShortcut(shortcutFilePath);
        
        //Specify target file full path
        MyShortcut.TargetPath = Application.StartupPath + @"\Blurts.exe";
        MyShortcut.IconLocation = Application.StartupPath + @"\blue1_32x32.ico";
        MyShortcut.WorkingDirectory = Application.StartupPath;
        
        //e.g.  MyShortcut.TargetPath = "C:\WINDOWS\system32\calc.exe"
        MyShortcut.Save();
      }
      else
      {
        //Remove the shortcut from the Startup folder
        if (System.IO.File.Exists(shortcutFilePath))
        {
          System.IO.File.Delete(shortcutFilePath);
        }
      }
    }

    private bool shortCutPresent()
    {
      String StartupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
      String shortcutFilePath = StartupFolder + @"\Blurts.lnk";
      return System.IO.File.Exists(shortcutFilePath);
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {

    }

    private void soundFileBtn_Click(object sender, EventArgs e)
    {
      
      // Create a new OpenFileDialog.
      OpenFileDialog dlg = new OpenFileDialog();

      dlg.InitialDirectory = Environment.GetEnvironmentVariable("SystemRoot") + @"\Media";

      // Make sure the dialog checks for existence of the 
      // selected file.
      dlg.CheckFileExists = true;

      // Allow selection of .wav files only.
      dlg.Filter = "WAV files (*.wav)|*.wav";
      dlg.DefaultExt = ".wav";

      // Activate the file selection dialog.
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        // Get the selected file's path from the dialog.
        soundFileTxtBox.Text = dlg.FileName;
      }
    }

    private void SetupForm_Load(object sender, EventArgs e)
    {

    }

    private void testSndBtn_Click(object sender, EventArgs e)
    {
      try
      {
        SoundPlayer player;
        player = new SoundPlayer();
        player.SoundLocation = soundFileTxtBox.Text;
        player.Load();
        player.Play();
      }
      catch (Exception)
      {
        MessageBox.Show("Failed to play sound.");  
      }
    }

    private void clearSndBtn_Click(object sender, EventArgs e)
    {
      soundFileTxtBox.Text = "";
    }

    private void disTimetxtBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      try
      {
        if (e.KeyChar != 8)
        {
          int isNumber = 0;
          e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
        }
      }
      catch (Exception)
      {
      }
    }

    private void smsTxtBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      try
      {
        if (e.KeyChar != 8)
        {
          int isNumber = 0;
          e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
        }
      }
      catch (Exception)
      {
      }
    }


    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
     // System.Drawing.Drawing2D.LinearGradientBrush baseBackground;
     // baseBackground = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, ClientSize.Width, ClientSize.Height), Color.White, Color.FromArgb(113, 138, 244), 90, false);
     // e.Graphics.FillRectangle(baseBackground, ClientRectangle);
    }

    private void smsPage_Click(object sender, EventArgs e)
    {

    }

    private void selectImgBtn_Click(object sender, EventArgs e)
    {
      OpenFileDialog dlg = new OpenFileDialog();

      // Allow selection of .jpg files only.
      dlg.Filter = "JPEG files (*.jpg)|*.jpg";
      dlg.DefaultExt = ".jpg";

      // Activate the file selection dialog.
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        try
        {
          System.IO.File.Copy(dlg.FileName, ApplicationSettings.Instance.LocalDataPath + @"\me.jpg", true);
        }
        catch (Exception)
        {
        }
      }

    }



    private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void scriptBtn_Click(object sender, EventArgs e)
    {
      // Create a new OpenFileDialog.
      OpenFileDialog dlg = new OpenFileDialog();

      dlg.InitialDirectory = ApplicationSettings.Instance.LocalDataPath;

      // Make sure the dialog checks for existence of the 
      // selected file.
      dlg.CheckFileExists = true;

      // Allow selection of .wav files only.
      dlg.Filter = "JScript files (*.js)|*.js|VBScript (*.vbs)|*.vbs";
      dlg.DefaultExt = ".js";

      // Activate the file selection dialog.
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        // Get the selected file's path from the dialog.
        scriptFileTextbox.Text = dlg.FileName;
      }
    }


    private void SetupForm_HelpButtonClicked(object sender, CancelEventArgs e)
    {
      String url = "http://www.mlhsoftware.com/Blurts/options.html";
      switch (tabControl.SelectedIndex)
      {
        case 0:
        {
          if (m_useCOMPort)
          {
            url += "#BluetoothTabCOM";
          }
          else
          {
            url += "#BluetoothTab";
          }
          break;
        }
        case 1:
        {
          url += "#GeneralTab";
          break;
        }
        case 2:
        {
          url += "#AlertsTab";
          break;
        }
        case 3:
        {
          url += "#SMSTab";
          break;
        }
        case 4:
        {
          url += "#ScriptingTab";
          break;
        }
        case 5:
        {
          url += "#HotKeysTab";
          break;
        }
      }

      //
      try
      {
        System.Diagnostics.Process.Start(url);
      }
      catch (Exception)
      {
        //MessageBox.Show("Failed to open http://www.mlhsoftware.com/blurts/helpSetupDlg.html");
      }
    }


    private void initHotKeys()
    {
      int tabIndex = 1;
      int topPos = 42;

      int len = 0;

      if (ApplicationSettings.Instance.HotKeys != null)
      {
        len = ApplicationSettings.Instance.HotKeys.Length;
      }
      if (len < ApplicationSettings.HOTKEY_COUNT)
      {
        ApplicationSettings.Instance.initHotKeys();
        len = ApplicationSettings.Instance.HotKeys.Length;
      }


      for (int i = 0; i < len; i++)
      {
        addHotKey(ApplicationSettings.Instance.HotKeys[i], tabIndex, topPos);
        topPos += 25;
      }
    }

    private void addHotKey( ApplicationSettings.HotKeySettings hotkey, int tabIndex, int topPos)
    {


      Label labelHotKey = new Label();
      ComboBox cbHotKeyMod = new ComboBox();
      ComboBox cbHotKey = new ComboBox();
      

      m_labelHotKey.Add( labelHotKey );
      m_cbHotKeyMod.Add( cbHotKeyMod );
      m_cbHotKey.Add(cbHotKey);
      this.commadPage.Controls.Add( labelHotKey );
      this.commadPage.Controls.Add( cbHotKeyMod );
      this.commadPage.Controls.Add( cbHotKey );


      // 
      // labelHotKey
      // 
      labelHotKey.AutoSize = true;
      labelHotKey.Location = new System.Drawing.Point(38, topPos);
      //labelHotKey.Name = "label8";
      labelHotKey.Size = new System.Drawing.Size(53, 13);
      labelHotKey.TabIndex = tabIndex++;
      labelHotKey.Text = hotkey.Command;

      // 
      // cbHotKeyMod
      // 
      cbHotKeyMod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      cbHotKeyMod.FormattingEnabled = true;
      cbHotKeyMod.Location = new System.Drawing.Point(200, topPos);
      //cbHotKeyMod.Name = "cbHotKeyMod";
      cbHotKeyMod.Size = new System.Drawing.Size(82, 21);
      cbHotKeyMod.TabIndex = tabIndex++;
      cbHotKeyMod.Items.Add("");
      cbHotKeyMod.Items.Add("Alt");
      cbHotKeyMod.Items.Add("Ctrl");
      cbHotKeyMod.Items.Add("Win");
      cbHotKeyMod.SelectedItem = hotkey.Modifier;

      // 
      // cbHotKey
      // 
      cbHotKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      cbHotKey.FormattingEnabled = true;
      cbHotKey.Location = new System.Drawing.Point(cbHotKeyMod.Right + 10, topPos);
      //cbHotKey.Name = "cbHotKey";
      cbHotKey.Size = new System.Drawing.Size(76, 21);
      cbHotKey.TabIndex = tabIndex++;
      cbHotKey.Items.Add("");
      cbHotKey.Items.Add("F1");
      cbHotKey.Items.Add("F2");
      cbHotKey.Items.Add("F3");
      cbHotKey.Items.Add("F4");
      cbHotKey.Items.Add("F5");
      cbHotKey.Items.Add("F6");
      cbHotKey.Items.Add("F7");
      cbHotKey.Items.Add("F8");
      cbHotKey.Items.Add("F9");
      cbHotKey.Items.Add("F10");
      cbHotKey.Items.Add("F11");
      cbHotKey.Items.Add("F12");
      cbHotKey.SelectedItem = hotkey.KeyCode;
    }

    private void textColorBtn_Click(object sender, EventArgs e)
    {
      if (DialogResult.OK == colorDialog.ShowDialog())
      {
        smsTextColor.Text = "#" + colorDialog.Color.R.ToString("X2") + colorDialog.Color.G.ToString("X2") + colorDialog.Color.B.ToString("X2");
      }
    }

    private void bgColorBtn_Click(object sender, EventArgs e)
    {
      if (DialogResult.OK == colorDialog.ShowDialog())
      {
        smsBgColor.Text = "#" + colorDialog.Color.R.ToString("X2") + colorDialog.Color.G.ToString("X2") + colorDialog.Color.B.ToString("X2");
      }
    }

    private void dataFolderBtn_Click(object sender, EventArgs e)
    {
      try
      {
        System.Diagnostics.Process.Start("explorer.exe",ApplicationSettings.Instance.LocalDataPath);
      }
      catch (Exception)
      {
      }
    }
  }
}
