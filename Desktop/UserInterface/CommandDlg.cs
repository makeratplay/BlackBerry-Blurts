using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Blurts
{
  public partial class CommandDlg : Form
  {

    private Boolean m_formActive;
    private static CommandDlg s_dlg;
    private const int WH_KEYBOARD_LL = 13;
    private const int WM_KEYDOWN = 0x0100;
    private static LowLevelKeyboardProc _proc = HookCallback;
    private static IntPtr _hookID = IntPtr.Zero;
    private MainScreen m_form;

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);


    public CommandDlg(MainScreen form)
    {
      InitializeComponent();
      m_form = form;
      s_dlg = this;
      m_formActive = true;
    }

    private static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                GetModuleHandle(curModule.ModuleName), 0);
        }
    }

    private delegate IntPtr LowLevelKeyboardProc( int nCode, IntPtr wParam, IntPtr lParam);

    private static IntPtr HookCallback( int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            Console.WriteLine((Keys)vkCode);
            s_dlg.OnKey(vkCode);
        }
        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }



    private void sendBtn_Click(object sender, EventArgs e)
    {
      m_form.sendKeyToolStripMenuItem_Click(null, null);
    }

    private void menuBtn_Click(object sender, EventArgs e)
    {
      m_form.menuKeyToolStripMenuItem_Click(null, null);
    }

    private void leftBtn_Click(object sender, EventArgs e)
    {
      m_form.moveLeftToolStripMenuItem_Click(null, null);
    }

    private void upBtn_Click(object sender, EventArgs e)
    {
      m_form.moveUpToolStripMenuItem_Click(null, null);
    }

    private void clickBtn_Click(object sender, EventArgs e)
    {
      m_form.clickToolStripMenuItem_Click(null, null);
    }

    private void downBtn_Click(object sender, EventArgs e)
    {
      m_form.moveDownToolStripMenuItem_Click(null, null);
    }

    private void rightBtn_Click(object sender, EventArgs e)
    {
      m_form.moveRightToolStripMenuItem_Click(null, null);
    }

    private void escBtn_Click(object sender, EventArgs e)
    {
      m_form.escKeyToolStripMenuItem_Click(null, null);
    }

    private void endBtn_Click(object sender, EventArgs e)
    {
      m_form.endKeyToolStripMenuItem_Click(null, null);
    }

    private void settingsBtn_Click(object sender, EventArgs e)
    {
      
    }

    private void button2_Click(object sender, EventArgs e)
    {
      //m_form.sendCommand("5", codeTxtBox.Text, "", "");
    }

    private void CommandDlg_KeyPress(object sender, KeyPressEventArgs e)
    {
      
    }

    private void OnKey(int keyCode)
    {
      if (m_formActive)
      {
        switch (keyCode)
        {
          case 37: // left arrow
            {
              displayKey.Text = "Left";
              m_form.moveLeftToolStripMenuItem_Click(null, null);
              break;
            }
          case 38: // up arrow
            {
              displayKey.Text = "Up";
              m_form.moveUpToolStripMenuItem_Click(null, null);
              break;
            }
          case 39: // right arrow
            {
              displayKey.Text = "Right";
              m_form.moveRightToolStripMenuItem_Click(null, null);
              break;
            }
          case 40: // down arrow
            {
              displayKey.Text = "Down";
              m_form.moveDownToolStripMenuItem_Click(null, null);
              break;
            }
          default:
            {

              KeysConverter kc = new KeysConverter();
              displayKey.Text = kc.ConvertToString(keyCode); //keyCode.ToString();
           //   m_form.sendCommand("2", "17", keyCode.ToString(), "0");
              break;
            }
        }
      }
    }

    private void CommandDlg_FormClosing(object sender, FormClosingEventArgs e)
    {
      UnhookWindowsHookEx(_hookID);
    }

    private void CommandDlg_Load(object sender, EventArgs e)
    {
      _hookID = SetHook(_proc);
    }

    private void CommandDlg_Activated(object sender, EventArgs e)
    {
      m_formActive = true;
    }

    private void CommandDlg_Deactivate(object sender, EventArgs e)
    {
      m_formActive = false;
    }
  }
}