using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using System.Runtime.InteropServices;
namespace Blurts
{

  [ComVisible(true)]
  public class Device
  {

    public event MsgArrivedEventHandler MsgArrivedEvent;
    public event LevelStatusEventHandler LevelStatusEvent;
    public event BluetoothConnectEventHandler BluetoothConnectEvent;
    public event BluetoothDisconnectEventHandler BluetoothDisconnectEvent;



    private static Device instance;

    public static Device Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new Device();
        }
        return instance;
      }
    }

    private BTConnection btConnection;
    private AlertBase alert;

    [ComVisible(true)]
    public AlertBase Alert
    {
      get
      {
        return this.alert; //FIX
      }
    }

    private Device()
    {
      this.btConnection = new BTConnection();

      this.btConnection.MsgArrivedEvent += new MsgArrivedEventHandler(OnMsgArrivedEvent);
      this.btConnection.BluetoothConnectEvent += new BluetoothConnectEventHandler(OnBluetoothConnectEvent);
      this.btConnection.BluetoothDisconnectEvent += new BluetoothDisconnectEventHandler(OnBluetoothDisconnectEvent);
    }

    public void Exiting()
    {
      this.btConnection.Exiting();
    }

    private void OnBluetoothConnectEvent()
    {
      if (BluetoothConnectEvent != null)
      {
        BluetoothConnectEvent();
      }
    }

    private void OnBluetoothDisconnectEvent()
    {
      DisconnectAlert alert = new DisconnectAlert();
      this.alert = alert;
      alert.ProcessMessage();

      try
      {
        if (MsgArrivedEvent != null)
        {
          MsgArrivedEvent(alert);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("MsgArrivedEvent:" + Environment.NewLine + ex.ToString());
      }

      if (BluetoothDisconnectEvent != null)
      {
        BluetoothDisconnectEvent();
      }
    }

    private void OnMsgArrivedEvent(object msg)
    {
      AlertBase alert = (AlertBase)msg;
      this.alert = alert;
      alert.ProcessMessage();

      try
      {
        if (LevelStatusEvent != null)
        {
          LevelStatusEvent(alert.BatteryLevel, alert.SignalLevel);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("LevelStatusEvent:" + Environment.NewLine + ex.ToString());
      }

      try
      {
        if (MsgArrivedEvent != null)
        {
          MsgArrivedEvent(alert);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("MsgArrivedEvent:" + Environment.NewLine + ex.ToString());
      }

    }

    [ComVisible(true)]
    public Boolean IsConnected()
    {
      return this.btConnection.IsConnected();
    }

    [ComVisible(true)]
    public void Connect()
    {
      this.btConnection.Connect();
    }

    [ComVisible(true)]
    public void Disconnect()
    {
      this.btConnection.Disconnect();
    }

    [ComVisible(true)]
    public void PressMenuKey()
    {
      PressKeyCmd cmd = new PressKeyCmd();
      cmd.FunctionKey = 2;
      cmd.KeyCode = 0;
      cmd.StatusCode = 0;
      sendCommand(cmd);
      //sendCommand("2", "2", "", "0");
    }

    [ComVisible(true)]
    public void PressEscKey()
    {
      PressKeyCmd cmd = new PressKeyCmd();
      cmd.FunctionKey = 3;
      cmd.KeyCode = 0;
      cmd.StatusCode = 0;
      sendCommand(cmd);
      //sendCommand("2", "3", "", "0");
    }

    [ComVisible(true)]
    public void ClickTrackball()
    {
      PressKeyCmd cmd = new PressKeyCmd();
      cmd.FunctionKey = 5;
      cmd.KeyCode = 0;
      cmd.StatusCode = 0;
      sendCommand(cmd);
      //sendCommand("2", "5", "", "0");
    }

    [ComVisible(true)]
    public void MoveTrackballUp()
    {
      PressKeyCmd cmd = new PressKeyCmd();
      cmd.FunctionKey = 6;
      cmd.KeyCode = 0;
      cmd.StatusCode = 0;
      sendCommand(cmd);
      //sendCommand("2", "6", "", "0");
    }

    [ComVisible(true)]
    public void MoveTrackballDown()
    {
      PressKeyCmd cmd = new PressKeyCmd();
      cmd.FunctionKey = 7;
      cmd.KeyCode = 0;
      cmd.StatusCode = 0;
      sendCommand(cmd);
      //sendCommand("2", "7", "", "0");
    }

    [ComVisible(true)]
    public void MoveTrackballLeft()
    {
      PressKeyCmd cmd = new PressKeyCmd();
      cmd.FunctionKey = 8;
      cmd.KeyCode = 0;
      cmd.StatusCode = 0;
      sendCommand(cmd);
      //sendCommand("2", "8", "", "0");
    }

    [ComVisible(true)]
    public void MoveTrackballRight()
    {
      PressKeyCmd cmd = new PressKeyCmd();
      cmd.FunctionKey = 9;
      cmd.KeyCode = 0;
      cmd.StatusCode = 0;
      sendCommand(cmd);
      //sendCommand("2", "9", "", "0");
    }

    [ComVisible(true)]
    public void Buzz()
    {
      BuzzCmd cmd = new BuzzCmd();
      sendCommand(cmd);
    }

    [ComVisible(true)]
    public void PressSendKey()
    {
      PressKeyCmd cmd = new PressKeyCmd();
      cmd.FunctionKey = 1;
      cmd.KeyCode = 0;
      cmd.StatusCode = 0;
      sendCommand(cmd);
      //sendCommand("2", "1", "", "0");
    }

    [ComVisible(true)]
    public void PressEndKey()
    {
      PressKeyCmd cmd = new PressKeyCmd();
      cmd.FunctionKey = 4;
      cmd.KeyCode = 0;
      cmd.StatusCode = 0;
      sendCommand(cmd);
      //sendCommand("2", "4", "", "0");
    }

    [ComVisible(true)]
    public void PressSpeakerKey()
    {
      PressKeyCmd cmd = new PressKeyCmd();
      cmd.FunctionKey = 16;
      cmd.KeyCode = 0;
      cmd.StatusCode = 0;
      sendCommand(cmd);
      //sendCommand("2", "16", "", "0");
    }

    [ComVisible(true)]
    public void PressMuteKey()
    {
      PressKeyCmd cmd = new PressKeyCmd();
      cmd.FunctionKey = 11;
      cmd.KeyCode = 0;
      cmd.StatusCode = 0;
      sendCommand(cmd);
      //sendCommand("2", "11", "", "0");
    }

    [ComVisible(true)]
    public void PressVolumeUpKey()
    {
      PressKeyCmd cmd = new PressKeyCmd();
      cmd.FunctionKey = 12;
      cmd.KeyCode = 0;
      cmd.StatusCode = 0;
      sendCommand(cmd);
      //sendCommand("2", "12", "", "0");
    }

    [ComVisible(true)]
    public void PressVolumeDownKey()
    {
      PressKeyCmd cmd = new PressKeyCmd();
      cmd.FunctionKey = 13;
      cmd.KeyCode = 0;
      cmd.StatusCode = 0;
      sendCommand(cmd);
    }

    [ComVisible(true)]
    public void PressKey( String key )
    {
      if (key != null && key.Length > 0)
      {
        PressKeyCmd cmd = new PressKeyCmd();
        cmd.FunctionKey = 17;
        cmd.StatusCode = 0; 
        cmd.KeyCode = (int)key[0];
        sendCommand(cmd);
      }
    }

    [ComVisible(true)]
    public void PressKeyEx(int key, int status)
    {
      PressKeyCmd cmd = new PressKeyCmd();
      cmd.FunctionKey = 17;
      cmd.StatusCode = status;
      cmd.KeyCode = key;
      sendCommand(cmd);
    }

    [ComVisible(true)]
    public void TakeScreenCapture()
    {
      ScreenCaptureCmd cmd = new ScreenCaptureCmd();
      cmd.Quality = 100;
      sendCommand(cmd);
    }

    [ComVisible(true)]
    public void SendDTMF( String tones )
    {
      DTMFCmd cmd = new DTMFCmd();
      cmd.DTMFTones = tones;
      sendCommand(cmd);
    }

    [ComVisible(true)]
    public void DownloadContacts()
    {
      if (ApplicationSettings.Instance.Active && IsConnected())
      {
        Contacts.Instance.DeleteAllContacts();
        ContactCmd cmd = new ContactCmd();
        sendCommand(cmd);
      }
    }

    [ComVisible(true)]
    public void CreateContact()
    {
      /*
      for (int x = 200; x < 1000; x++)
      {
        CreateContactCmd cmd = new CreateContactCmd();
        cmd.PrefixName = "Mr";
        cmd.FirstName = "John " + x.ToString();
        cmd.LastName = "Smith " + x.ToString();
        cmd.PhoneWork = "(555) 555-1212";
        cmd.PhoneMobile = "(444) 444-1212";
        cmd.Email = "testemail@test.com";
        cmd.Org = "Test Org " + x.ToString();
        cmd.Pin = "A2222222";
        cmd.Photo = MessageFactory.ImageToBase64(@"C:\Documents and Settings\Michael\Local Settings\Application Data\MLHSoftware\Blurts\contactPhoto.jpg");
        sendCommand(cmd);
      }
       * */
    }

    [ComVisible(true)]
    public void WriteClipboard()
    {
      if (Clipboard.GetDataObject().GetDataPresent(DataFormats.UnicodeText))
      {
        string text = Clipboard.GetDataObject().GetData(DataFormats.UnicodeText).ToString();
        if (text.Length > 1024)
        {
          text = text.Substring(0, 1024);
        }
        WriteClipboardCmd cmd = new WriteClipboardCmd();
        cmd.Text = text;
        sendCommand(cmd);
      }
    }

    [ComVisible(true)]
    public void WriteClipboard( String text )
    {
      if (text.Length > 1024)
      {
        text = text.Substring(0, 1024);
      }
      WriteClipboardCmd cmd = new WriteClipboardCmd();
      cmd.Text = text;
      sendCommand(cmd);
    }

    [ComVisible(true)]
    public void ReadClipboard()
    {
      ReadClipboardCmd cmd = new ReadClipboardCmd();
      sendCommand(cmd);
    }

    [ComVisible(true)]
    public void GetLevelStatus()
    {
      LevelCmd cmd = new LevelCmd();
      sendCommand(cmd);
    }

    [ComVisible(true)]
    public void ReadFile(String fileName )
    {
      ReadFileCmd cmd = new ReadFileCmd();
      cmd.FileName = fileName;
      sendCommand(cmd);
    }

    [ComVisible(true)]
    public void SendSMS(string address, string text)
    {
      if (ApplicationSettings.Instance.Active)
      {
        Boolean bPresentDlg = true;
        if ( address != null && text != null )
        {
          if (address.Length != 0 && text.Length != 0)
          {
            SMSCmd cmd = new SMSCmd();
            cmd.Address = address;
            cmd.Text = text;
            sendCommand(cmd);
            bPresentDlg = false;
          }
        }

        if (bPresentDlg)
        {
          SMSDlg dlg = new SMSDlg();
          dlg.setAddress(address);
          dlg.setText(text);
          dlg.Show();
        }
      }
      else
      {
        BuyDlg dlg = new BuyDlg();
        dlg.ShowDialog();
        //MessageBox.Show("Upgrade to Blurts Pro to enable this feature.", "Blurts - by MLH Software");
      }
    }

    [ComVisible(true)]
    public void DialPhone(string phoneNumber)
    {
      if (ApplicationSettings.Instance.Active)
      {
        if (phoneNumber != null && phoneNumber.Length > 0)
        {
          CallCmd cmd = new CallCmd();
          cmd.PhoneNumber = phoneNumber;
          sendCommand(cmd);
        }
        else
        {
          SMSContactDlg dlg = new SMSContactDlg( false );
          //DialDlg dlg = new DialDlg();
          //Screen desktop = Screen.PrimaryScreen;
          //dlg.Location = new Point(desktop.WorkingArea.Width - dlg.Width - 10, desktop.WorkingArea.Height - dlg.Height - 10);

          if (dlg.ShowDialog() == DialogResult.OK)
          {
            String number = dlg.PhoneNumber;
            if (number.Length > 0)
            {
              CallCmd cmd = new CallCmd();
              cmd.PhoneNumber = number;
              sendCommand(cmd);
            }
          }
        }
      }
      else
      {
        BuyDlg dlg = new BuyDlg();
        dlg.ShowDialog();
        //MessageBox.Show("Upgrade to Blurts Pro to enable this feature.", "Blurts - by MLH Software");
      }
    }

    [ComVisible(true)]
    public void RunMacro(string macroName)
    {
      if ( IsConnected() )
      {
        if (ApplicationSettings.Instance.Active )
        {
          MacroAlert alert = new MacroAlert();
          alert.MacroName = macroName;
          alert.ProcessMessage();
        }
        else
        {
          BuyDlg dlg = new BuyDlg();
          dlg.ShowDialog();
          //MessageBox.Show("Upgrade to Blurts Pro to enable this feature.", "Blurts - by MLH Software");
        }
      }
      else
      {
        MessageBox.Show("BlackBerry not connected.", "Blurts - by MLH Software");
      }
    }

    private void sendCommand(CmdBase msg)
    {
      if (this.btConnection != null)
      {
        this.btConnection.sendCommand(msg);
      }
    }

  }
}
