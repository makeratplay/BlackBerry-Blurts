using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;

namespace Blurts
{
  public partial class DeviceListDlg : Form
  {
    public BluetoothDeviceInfo selectedDevice;
    public DeviceListDlg( BluetoothDeviceInfo[] deviceList )
    {
      selectedDevice = null;
      InitializeComponent();
      devicelistBox.DisplayMember = "DeviceName";
      devicelistBox.ValueMember = null;
      devicelistBox.DataSource = deviceList;
    }

    private void devicelistBox_SelectedValueChanged(object sender, EventArgs e)
    {
      if (devicelistBox.SelectedIndex != -1)
      {
        selectedDevice = (BluetoothDeviceInfo)devicelistBox.SelectedItem;
      }
    }

    private void DeviceListDlg_Load(object sender, EventArgs e)
    {

    }

    private void okBtn_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}