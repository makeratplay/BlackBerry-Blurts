using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Blurts
{
  public partial class SignalLevelCtrl : UserControl
  {
    private int signalLevel;
    private ToolTip toolTip;

    public SignalLevelCtrl()
    {
      InitializeComponent();

      Device.Instance.BluetoothConnectEvent += new BluetoothConnectEventHandler(BlackBerry_BluetoothConnectEvent);
      Device.Instance.LevelStatusEvent += new LevelStatusEventHandler(BlackBerry_LevelStatusEvent);
      Device.Instance.BluetoothDisconnectEvent += new BluetoothDisconnectEventHandler(BlackBerry_BluetoothDisconnectEvent);
      Device.Instance.MsgArrivedEvent += new MsgArrivedEventHandler(BlackBerry_MsgArrivedEvent);

      // Create the ToolTip and associate with the Form container.
      this.toolTip = new ToolTip();
      // Set up the delays for the ToolTip.
      this.toolTip.AutoPopDelay = 5000;
      this.toolTip.InitialDelay = 1000;
      this.toolTip.ReshowDelay = 500;
      // Force the ToolTip text to be displayed whether or not the form is active.
      this.toolTip.ShowAlways = true;

      if (Device.Instance.IsConnected())
      {
        Device.Instance.GetLevelStatus();
      }
      else
      {
        onDisconnect(null, null);
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
        //DisplayItemType type = alert.ItemType;
        string data = msg.ToString();
      }
      catch (Exception ex)
      {
        Console.WriteLine("BlackBerry_MsgArrivedEvent:" + Environment.NewLine + ex.ToString());
      }
    }

    private void onConnect(object o, EventArgs e)
    {
      Visible = true;
      Invalidate();
    }

    private void onDisconnect(object o, EventArgs e)
    {
      Visible = false;
      Invalidate();
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

    public void setSignal()
    {
      if (this.signalLevel >= -77)
      {
        //5 bands
        signalImg.Image = global::Blurts.Properties.Resources.signal5;
      }
      else if (this.signalLevel >= -86)
      {
        //4 bands
        signalImg.Image = global::Blurts.Properties.Resources.signal4;
      }
      else if (this.signalLevel >= -92)
      {
        //3 bands
        signalImg.Image = global::Blurts.Properties.Resources.signal3;
      }
      else if (this.signalLevel >= -101)
      {
        //2 bands
        signalImg.Image = global::Blurts.Properties.Resources.signal2;
      }
      else if (this.signalLevel >= -120)
      {
        //1 band
        signalImg.Image = global::Blurts.Properties.Resources.signal1;
      }
      if (this.toolTip != null)
      {
        this.toolTip.SetToolTip(this.signalImg, this.signalLevel + " dBm");
      }

      Invalidate();
    }

    private void BlackBerry_LevelStatusEvent(int batteryLevel, int signalLevel)
    {
      this.signalLevel = signalLevel;
      if (this.InvokeRequired)
      {
        this.Invoke(new EventHandler(showLevels));
      }
      else
      {
        showLevels(null, null);
      }
    }

    private void showLevels(object o, EventArgs e)
    {
      Visible = true;
      setSignal();
    }
  }
}
