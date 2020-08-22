using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Blurts
{
  public partial class BatteryLevelCtrl : UserControl
  {
    public event MouseEventHandler MouseDownEvent;

    private int batteryLevel;
    private ToolTip toolTip;

    public BatteryLevelCtrl()
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
        //int type = alert.ItemType;
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

    public void setBattery()
    {
      if (this.batteryLevel >= 95)
      {
        battery.Image = global::Blurts.Properties.Resources.battery5;
      }
      else if (this.batteryLevel >= 70)
      {
        battery.Image = global::Blurts.Properties.Resources.battery4;
      }
      else if (this.batteryLevel >= 55)
      {
        battery.Image = global::Blurts.Properties.Resources.battery3;
      }
      else if (this.batteryLevel >= 35)
      {
        battery.Image = global::Blurts.Properties.Resources.battery2;
      }
      else if (this.batteryLevel >= 15)
      {
        battery.Image = global::Blurts.Properties.Resources.battery1;
      }
      if (this.toolTip != null)
      {
        this.toolTip.SetToolTip(this.battery, this.batteryLevel + "%");
      }
      Invalidate();
    }


    private void BlackBerry_LevelStatusEvent(int batteryLevel, int signalLevel)
    {
      this.batteryLevel = batteryLevel;
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
      setBattery();
    }

    private void BatteryLevelCtrl_DoubleClick(object sender, EventArgs e)
    {

    }

    private void BatteryLevelCtrl_MouseDown(object sender, MouseEventArgs e)
    {
      if (MouseDownEvent != null)
      {
        MouseDownEvent( sender, e);
      }
    }

  }
}
