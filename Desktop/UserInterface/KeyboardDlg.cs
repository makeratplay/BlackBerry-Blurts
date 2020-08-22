using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Blurts
{
  public partial class KeyboardDlg : Form
  {
    // 36 x 58
    private Boolean m_formActive;
    private static KeyboardDlg s_dlg;
    private const int WH_KEYBOARD_LL = 13;
    private const int WM_KEYDOWN = 0x0100;
    private const int WM_KEYUP = 0x0101;
    private static LowLevelKeyboardProc _proc = HookCallback;
    private static IntPtr _hookID = IntPtr.Zero;
    private int m_highLightKey;
    Hashtable m_myKeyCode = new Hashtable();
    Hashtable m_myKeyCodeShift = new Hashtable();
    Boolean m_shiftKeyDown;


    public enum eKeyboard
    {
      eTab_KeyPad,
      eTab_NumPad,
      eTab_SymPad
    }

    internal class Key
    {
      public char letter;
      public eKeyboard keybaord;
      public Rectangle rect;
      public Boolean shift;
      public int vkCode;
      public Key(char letter, eKeyboard keybaord, Rectangle rect, Boolean shift, int vkCode)
      {
        this.letter = letter;
        this.keybaord = keybaord;
        this.rect = rect;
        this.shift = shift;
        this.vkCode = vkCode;
      }
    }

    private eKeyboard m_currentKeyboard;

    // mouse states
    //private Boolean m_mousedown;

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    public KeyboardDlg()
    {
      m_currentKeyboard = eKeyboard.eTab_KeyPad;
      //m_mousedown = false;
      InitializeComponent();
      s_dlg = this;
      m_formActive = true;
      m_highLightKey = -1;
      m_myKeyCode = new Hashtable();
      m_shiftKeyDown = false;

      m_myKeyCode.Add(81, new Key('q', eKeyboard.eTab_KeyPad, new Rectangle(  0, 0, 36, 58), false, 81)); // Q
      m_myKeyCode.Add(87, new Key('w', eKeyboard.eTab_KeyPad, new Rectangle(36, 0, 36, 58), false, 87)); // W
      m_myKeyCode.Add(69, new Key('e', eKeyboard.eTab_KeyPad, new Rectangle(72, 0, 36, 58), false, 69)); // E
      m_myKeyCode.Add(82, new Key('r', eKeyboard.eTab_KeyPad, new Rectangle(108, 0, 36, 58), false, 82)); // R
      m_myKeyCode.Add(84, new Key('t', eKeyboard.eTab_KeyPad, new Rectangle(144, 0, 36, 58), false, 84)); // T
      m_myKeyCode.Add(89, new Key('y', eKeyboard.eTab_KeyPad, new Rectangle(180, 0, 36, 58), false, 89)); // Y
      m_myKeyCode.Add(85, new Key('u', eKeyboard.eTab_KeyPad, new Rectangle(216, 0, 36, 58), false, 85)); // U
      m_myKeyCode.Add(73, new Key('i', eKeyboard.eTab_KeyPad, new Rectangle(252, 0, 36, 58), false, 73)); // I
      m_myKeyCode.Add(79, new Key('o', eKeyboard.eTab_KeyPad, new Rectangle(288, 0, 36, 58), false, 79)); // O
      m_myKeyCode.Add(80, new Key('p', eKeyboard.eTab_KeyPad, new Rectangle(324, 0, 36, 58), false, 80)); // P

      m_myKeyCode.Add(65, new Key('a', eKeyboard.eTab_KeyPad, new Rectangle(0, 58, 36, 58), false, 65)); // A
      m_myKeyCode.Add(83, new Key('s', eKeyboard.eTab_KeyPad, new Rectangle(36, 58, 36, 58), false, 83)); // S
      m_myKeyCode.Add(68, new Key('d', eKeyboard.eTab_KeyPad, new Rectangle(72, 58, 36, 58), false, 68)); // D
      m_myKeyCode.Add(70, new Key('f', eKeyboard.eTab_KeyPad, new Rectangle(108, 58, 36, 58), false, 70)); // F
      m_myKeyCode.Add(71, new Key('g', eKeyboard.eTab_KeyPad, new Rectangle(144, 58, 36, 58), false, 71)); // G
      m_myKeyCode.Add(72, new Key('h', eKeyboard.eTab_KeyPad, new Rectangle(180, 58, 36, 58), false, 72)); // H
      m_myKeyCode.Add(74, new Key('j', eKeyboard.eTab_KeyPad, new Rectangle(216, 58, 36, 58), false, 74)); // J
      m_myKeyCode.Add(75, new Key('k', eKeyboard.eTab_KeyPad, new Rectangle(252, 58, 36, 58), false, 75)); // K
      m_myKeyCode.Add(76, new Key('l', eKeyboard.eTab_KeyPad, new Rectangle(288, 58, 36, 58), false, 76)); // L
      m_myKeyCode.Add(8, new Key('\b', eKeyboard.eTab_KeyPad, new Rectangle(324, 58, 36, 58), false, 8)); // Backspace

      m_myKeyCode.Add(144, new Key('?', eKeyboard.eTab_KeyPad, new Rectangle(0, 116, 36, 58), false, 144)); // Num Key
      m_myKeyCode.Add(90, new Key('z', eKeyboard.eTab_KeyPad, new Rectangle(36, 116, 36, 58), false, 90)); // Z
      m_myKeyCode.Add(88, new Key('x', eKeyboard.eTab_KeyPad, new Rectangle(72, 116, 36, 58), false, 88)); // X
      m_myKeyCode.Add(67, new Key('c', eKeyboard.eTab_KeyPad, new Rectangle(108, 116, 36, 58), false, 67)); // C
      m_myKeyCode.Add(86, new Key('v', eKeyboard.eTab_KeyPad, new Rectangle(144, 116, 36, 58), false, 86)); // V
      m_myKeyCode.Add(66, new Key('b', eKeyboard.eTab_KeyPad, new Rectangle(180, 116, 36, 58), false, 66)); // B
      m_myKeyCode.Add(78, new Key('n', eKeyboard.eTab_KeyPad, new Rectangle(216, 116, 36, 58), false, 78)); // N
      m_myKeyCode.Add(77, new Key('m', eKeyboard.eTab_KeyPad, new Rectangle(252, 116, 36, 58), false, 77)); // N
      m_myKeyCode.Add(28, new Key('.', eKeyboard.eTab_KeyPad, new Rectangle(288, 116, 36, 58), false, 28)); // .
      m_myKeyCode.Add(13, new Key('\n', eKeyboard.eTab_KeyPad, new Rectangle(324, 116, 36, 58), false, 13)); // Enter


      m_myKeyCode.Add(160, new Key('.', eKeyboard.eTab_KeyPad, new Rectangle(288, 174, 36, 58), false, 160)); // LShiftKey
      //m_myKeyCode.Add(160, new Key('.', eKeyboard.eTab_KeyPad, new Rectangle(288, 174, 36, 58), false, 160)); // SYMBOL
      m_myKeyCode.Add(32, new Key(' ', eKeyboard.eTab_KeyPad, new Rectangle(288, 174, 36, 58), false, 32)); // Space
      //m_myKeyCode.Add(160, new Key('.', eKeyboard.eTab_KeyPad, new Rectangle(288, 174, 36, 58), false, 160)); // SYMBOL
      m_myKeyCode.Add(161, new Key('.', eKeyboard.eTab_KeyPad, new Rectangle(288, 174, 36, 58), false, 161)); // RShiftKey



      m_myKeyCodeShift.Add(51, new Key('#', eKeyboard.eTab_NumPad, new Rectangle(0, 0, 36, 58), true, 51)); // 
      m_myKeyCode.Add(49, new Key('1', eKeyboard.eTab_NumPad, new Rectangle(36, 0, 36, 58), false, 49)); // 
      m_myKeyCode.Add(50, new Key('2', eKeyboard.eTab_NumPad, new Rectangle(72, 0, 36, 58), false, 50)); // 
      m_myKeyCode.Add(51, new Key('3', eKeyboard.eTab_NumPad, new Rectangle(108, 0, 36, 58), false, 51)); // 
      m_myKeyCodeShift.Add(57, new Key('(', eKeyboard.eTab_NumPad, new Rectangle(144, 0, 36, 58), true, 57)); // 
      m_myKeyCodeShift.Add(48, new Key(')', eKeyboard.eTab_NumPad, new Rectangle(180, 0, 36, 58), true, 48)); // 
      m_myKeyCodeShift.Add(189, new Key('_', eKeyboard.eTab_NumPad, new Rectangle(216, 0, 36, 58), true, 189)); // 
      m_myKeyCode.Add(189, new Key('-', eKeyboard.eTab_NumPad, new Rectangle(252, 0, 36, 58), false, 189)); // 
      m_myKeyCodeShift.Add(187, new Key('+', eKeyboard.eTab_NumPad, new Rectangle(288, 0, 36, 58), true, 187)); // 
      m_myKeyCodeShift.Add(50, new Key('@', eKeyboard.eTab_NumPad, new Rectangle(324, 0, 36, 58), true, 50)); // 

      m_myKeyCodeShift.Add(56, new Key('*', eKeyboard.eTab_NumPad, new Rectangle(0, 58, 36, 58), true, 56)); // 
      m_myKeyCode.Add(52, new Key('4', eKeyboard.eTab_NumPad, new Rectangle(36, 58, 36, 58), false, 52)); // 
      m_myKeyCode.Add(53, new Key('5', eKeyboard.eTab_NumPad, new Rectangle(72, 58, 36, 58), false, 53)); // 
      m_myKeyCode.Add(54, new Key('6', eKeyboard.eTab_NumPad, new Rectangle(108, 58, 36, 58), false, 54)); // 
      m_myKeyCode.Add(191, new Key('/', eKeyboard.eTab_NumPad, new Rectangle(144, 58, 36, 58), false, 191)); // 
      m_myKeyCodeShift.Add(186, new Key(':', eKeyboard.eTab_NumPad, new Rectangle(180, 58, 36, 58), true, 186)); // 
      m_myKeyCode.Add(186, new Key(';', eKeyboard.eTab_NumPad, new Rectangle(216, 58, 36, 58), false, 186)); // 
      m_myKeyCode.Add(222, new Key('\'', eKeyboard.eTab_NumPad, new Rectangle(252, 58, 36, 58), false, 222)); // 
      m_myKeyCodeShift.Add(222, new Key('"', eKeyboard.eTab_NumPad, new Rectangle(288, 58, 36, 58), true, 222)); // 
      //m_myKeyCode.Add(8, new Key('\b', eKeyboard.eTab_NumPad, new Rectangle(324, 58, 36, 58), false, 8)); // Backspace

      //m_myKeyCode.Add(144, new Key('?', eKeyboard.eTab_NumPad, new Rectangle(0, 116, 36, 58), false, 144)); // Num Key
      m_myKeyCode.Add(55, new Key('7', eKeyboard.eTab_NumPad, new Rectangle(36, 116, 36, 58), false, 55)); // 
      m_myKeyCode.Add(56, new Key('8', eKeyboard.eTab_NumPad, new Rectangle(72, 116, 36, 58), false, 56)); // 
      m_myKeyCode.Add(57, new Key('9', eKeyboard.eTab_NumPad, new Rectangle(108, 116, 36, 58), false, 57)); // 
      m_myKeyCodeShift.Add(52, new Key('$', eKeyboard.eTab_NumPad, new Rectangle(144, 116, 36, 58), true, 52)); // 
      m_myKeyCodeShift.Add(191, new Key('?', eKeyboard.eTab_NumPad, new Rectangle(180, 116, 36, 58), true, 191)); // 
      m_myKeyCodeShift.Add(49, new Key('!', eKeyboard.eTab_NumPad, new Rectangle(216, 116, 36, 58), true, 49)); // 
      m_myKeyCode.Add(188, new Key(',', eKeyboard.eTab_NumPad, new Rectangle(252, 116, 36, 58), false, 188)); // 
      m_myKeyCode.Add(187, new Key('=', eKeyboard.eTab_NumPad, new Rectangle(288, 116, 36, 58), false, 187)); // 
      //m_myKeyCode.Add(13, new Key('\n', eKeyboard.eTab_NumPad, new Rectangle(324, 116, 36, 58), false, 13)); // Enter


      m_myKeyCodeShift.Add(81, new Key('Q', eKeyboard.eTab_KeyPad, new Rectangle(0, 0, 36, 58), true, 81)); // Q
      m_myKeyCodeShift.Add(87, new Key('W', eKeyboard.eTab_KeyPad, new Rectangle(36, 0, 36, 58), true, 87)); // W
      m_myKeyCodeShift.Add(69, new Key('E', eKeyboard.eTab_KeyPad, new Rectangle(72, 0, 36, 58), true, 69)); // E
      m_myKeyCodeShift.Add(82, new Key('R', eKeyboard.eTab_KeyPad, new Rectangle(108, 0, 36, 58), true, 82)); // R
      m_myKeyCodeShift.Add(84, new Key('T', eKeyboard.eTab_KeyPad, new Rectangle(144, 0, 36, 58), true, 84)); // T
      m_myKeyCodeShift.Add(89, new Key('Y', eKeyboard.eTab_KeyPad, new Rectangle(180, 0, 36, 58), true, 89)); // Y
      m_myKeyCodeShift.Add(85, new Key('U', eKeyboard.eTab_KeyPad, new Rectangle(216, 0, 36, 58), true, 85)); // U
      m_myKeyCodeShift.Add(73, new Key('I', eKeyboard.eTab_KeyPad, new Rectangle(252, 0, 36, 58), true, 73)); // I
      m_myKeyCodeShift.Add(79, new Key('O', eKeyboard.eTab_KeyPad, new Rectangle(288, 0, 36, 58), true, 79)); // O
      m_myKeyCodeShift.Add(80, new Key('P', eKeyboard.eTab_KeyPad, new Rectangle(324, 0, 36, 58), true, 80)); // P

      m_myKeyCodeShift.Add(65, new Key('A', eKeyboard.eTab_KeyPad, new Rectangle(0, 58, 36, 58), true, 65)); // A
      m_myKeyCodeShift.Add(83, new Key('S', eKeyboard.eTab_KeyPad, new Rectangle(36, 58, 36, 58), true, 83)); // S
      m_myKeyCodeShift.Add(68, new Key('D', eKeyboard.eTab_KeyPad, new Rectangle(72, 58, 36, 58), true, 68)); // D
      m_myKeyCodeShift.Add(70, new Key('F', eKeyboard.eTab_KeyPad, new Rectangle(108, 58, 36, 58), true, 70)); // F
      m_myKeyCodeShift.Add(71, new Key('G', eKeyboard.eTab_KeyPad, new Rectangle(144, 58, 36, 58), true, 71)); // G
      m_myKeyCodeShift.Add(72, new Key('H', eKeyboard.eTab_KeyPad, new Rectangle(180, 58, 36, 58), true, 72)); // H
      m_myKeyCodeShift.Add(74, new Key('J', eKeyboard.eTab_KeyPad, new Rectangle(216, 58, 36, 58), true, 74)); // J
      m_myKeyCodeShift.Add(75, new Key('K', eKeyboard.eTab_KeyPad, new Rectangle(252, 58, 36, 58), true, 75)); // K
      m_myKeyCodeShift.Add(76, new Key('L', eKeyboard.eTab_KeyPad, new Rectangle(288, 58, 36, 58), true, 76)); // L

      m_myKeyCodeShift.Add(90, new Key('Z', eKeyboard.eTab_KeyPad, new Rectangle(36, 116, 36, 58), true, 90)); // Z
      m_myKeyCodeShift.Add(88, new Key('X', eKeyboard.eTab_KeyPad, new Rectangle(72, 116, 36, 58), true, 88)); // X
      m_myKeyCodeShift.Add(67, new Key('C', eKeyboard.eTab_KeyPad, new Rectangle(108, 116, 36, 58), true, 67)); // C
      m_myKeyCodeShift.Add(86, new Key('V', eKeyboard.eTab_KeyPad, new Rectangle(144, 116, 36, 58), true, 86)); // V
      m_myKeyCodeShift.Add(66, new Key('B', eKeyboard.eTab_KeyPad, new Rectangle(180, 116, 36, 58), true, 66)); // B
      m_myKeyCodeShift.Add(78, new Key('N', eKeyboard.eTab_KeyPad, new Rectangle(216, 116, 36, 58), true, 78)); // N
      m_myKeyCodeShift.Add(77, new Key('M', eKeyboard.eTab_KeyPad, new Rectangle(252, 116, 36, 58), true, 77)); // N



      //m_myKeyCode.Add(91, 0); // LWin
      //m_myKeyCode.Add(93, 0); // Apps
      //m_myKeyCode.Add(92, 0); // RWin

    }

    private void KeyboardDlg_Paint(object sender, PaintEventArgs e)
    {
      Graphics g = e.Graphics;
      Image keyboardImg = global::Blurts.Properties.Resources.keybaord_1;
      Image keyboardImgSel = global::Blurts.Properties.Resources.keybaord_1_sel;
      if (m_currentKeyboard == eKeyboard.eTab_NumPad)
      {
        keyboardImg = global::Blurts.Properties.Resources.keybaord_2;
        keyboardImgSel = global::Blurts.Properties.Resources.keybaord_2_sel;
      }
      else if (m_currentKeyboard == eKeyboard.eTab_SymPad)
      {
        keyboardImg = global::Blurts.Properties.Resources.keybaord_3;
        keyboardImgSel = global::Blurts.Properties.Resources.keybaord_3_sel;
      }


      g.DrawImage(keyboardImg, 0, 0);


      if (m_highLightKey != -1)
      {
        // Create rectangle for displaying image.
        Rectangle destRect = getKeyRect(m_highLightKey);
        GraphicsUnit units = GraphicsUnit.Pixel;

        // Draw image to screen.
        e.Graphics.DrawImage(keyboardImgSel, destRect, destRect, units);
        timer1.Interval = 500;
        timer1.Start();
      }
    }

    Rectangle getKeyRect(int key)
    {
      int x = 0;
      int y = 0;
      int w = 36;

      if (key < 10)
      {
        y = 0;
        x = key * 36;
      }
      else if (key < 20)
      {
        y = 58;
        x = (key - 10) * 36;
      }
      else if (key < 30)
      {
        y = 58 * 2;
        x = (key - 20) * 36;
      }
      else if (key < 40)
      {
        y = 58 * 3;
        switch (key)
        {
          case 30:
          {
            x = 0;
            w = 72;
            break;
          }
          case 31:
          {
            x = 72;
            w = 48;
            break;
          }
          case 32:
          {
            x = 120;
            w = 120;
            break;
          }
          case 33:
          {
            x = 240;
            w = 48;
            break;
          }
          case 34:
          {
            x = 288;
            w = 72;
            break;
          }
        }
      }

      return new Rectangle(x, y, w, 58);
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

    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
      Console.WriteLine("wParam:" + wParam);
      if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
      {
        int vkCode = Marshal.ReadInt32(lParam);
        //Console.WriteLine((Keys)vkCode);
        s_dlg.OnKeyDown(vkCode);
      }
      else if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP)
      {
        int vkCode = Marshal.ReadInt32(lParam);
        //Console.WriteLine((Keys)vkCode);
        s_dlg.OnKeyUp(vkCode);
      }

      return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }

    private void OnKeyUp(int keyCode)
    {
      if (keyCode == 160 || keyCode == 161)
      {
        m_shiftKeyDown = false;
      }
    }

    private void OnKeyDown(int keyCode)
    {
      if (m_formActive)
      {
        m_highLightKey = -1;

        KeysConverter kc = new KeysConverter();
        Console.WriteLine("m_myKeyCode.Add( " + keyCode + ", 0); // " +  kc.ConvertToString(keyCode));


        if (Control.IsKeyLocked(Keys.CapsLock))
        {
          
          Console.WriteLine("CapsLock");
        }

        if (m_formActive)
        {
          switch (keyCode)
          {
            case 160: // left shift key
            case 161: // right shift key
            {
              m_shiftKeyDown = true;
              break;
            }
            case 37: // left arrow
            {
              Device.Instance.MoveTrackballLeft();
              break;
            }
            case 38: // up arrow
            {
              Device.Instance.MoveTrackballUp();
              break;
            }
            case 39: // right arrow
            {
              Device.Instance.MoveTrackballRight();
              break;
            }
            case 40: // down arrow
            {
              Device.Instance.MoveTrackballDown();
              break;
            }
            default:
            {
              if ( m_shiftKeyDown )
              {
                if (m_myKeyCodeShift.Contains(keyCode))
                {
                  Key keyObj = (Key)m_myKeyCodeShift[keyCode];
                  Device.Instance.PressKey(keyObj.letter.ToString());
                }
              }
              else
              {
                if (m_myKeyCode.Contains(keyCode))
                {
                  Key keyObj = (Key)m_myKeyCode[keyCode];
                  Device.Instance.PressKey(keyObj.letter.ToString());
                }
              }

              Invalidate();



              //KeysConverter kc2 = new KeysConverter();
              //Device.Instance.PressKey(kc2.ConvertToString(keyCode));
              break;
            }
          }
        }
      }
    }

    private void KeyboardDlg_FormClosing(object sender, FormClosingEventArgs e)
    {
      UnhookWindowsHookEx(_hookID);
    }

    private void KeyboardDlg_Load(object sender, EventArgs e)
    {
      this.SetClientSizeCore(360, 233);
      _hookID = SetHook(_proc);
    }

    private void KeyboardDlg_Activated(object sender, EventArgs e)
    {
      m_formActive = true;
    }

    private void KeyboardDlg_Deactivate(object sender, EventArgs e)
    {
      m_formActive = false;
    }

    private void KeyboardDlg_MouseDown(object sender, MouseEventArgs e)
    {
      //m_mousedown = true;
    }

    private void KeyboardDlg_MouseMove(object sender, MouseEventArgs e)
    {
      /*
      if (m_mousedown)
      {
        int oldKey = m_highLightKey;
        m_highLightKey = getBtnFromPt(e.X, e.Y);
        if (oldKey != m_highLightKey)
        {
          Invalidate(getKeyRect(oldKey));
          Invalidate(getKeyRect(m_highLightKey));
        }
      }
       * */
    }

    private void KeyboardDlg_MouseUp(object sender, MouseEventArgs e)
    {
      //m_mousedown = false;


    }

    private void KeyboardDlg_MouseClick(object sender, MouseEventArgs e)
    {
      int oldKey = m_highLightKey;
      m_highLightKey = getBtnFromPt(e.X, e.Y);
      if (oldKey != m_highLightKey)
      {
        Invalidate(getKeyRect(oldKey));
        Invalidate(getKeyRect(m_highLightKey));

        if (m_highLightKey == 31 || m_highLightKey == 33)
        {
          if (m_currentKeyboard == eKeyboard.eTab_KeyPad)
          {
            m_currentKeyboard = eKeyboard.eTab_SymPad;
          }
          else
          {
            m_currentKeyboard = eKeyboard.eTab_KeyPad;
          }
          Invalidate();
        }
        else if (m_highLightKey == 20)
        {
          if (m_currentKeyboard == eKeyboard.eTab_KeyPad)
          {
            m_currentKeyboard = eKeyboard.eTab_NumPad;
          }
          else
          {
            m_currentKeyboard = eKeyboard.eTab_KeyPad;
          }
          Invalidate();
        }
      }
    }

    private int getBtnFromPt(int x, int y)
    {
      int retVal = -1;
      if (y < 59)
      {
        retVal = (x / 36);
      }
      else if (y < 119)
      {
        retVal = (x / 36) + 10;
      }
      else if (y < 179)
      {
        retVal = (x / 36) + 20;
      }
      else
      {
        if (x < 72)
        {
          retVal = 30;
        }
        else if (x < 118)
        {
          retVal = 31;
        }
        else if (x < 239)
        {
          retVal = 32;
        }
        else if (x < 287)
        {
          retVal = 33;
        }
        else
        {
          retVal = 34;
        }
      }
      return retVal;
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      int oldKey = m_highLightKey;
      m_highLightKey = -1;
      if (oldKey != m_highLightKey)
      {
        Invalidate(getKeyRect(oldKey));
        Invalidate(getKeyRect(m_highLightKey));
      }
      timer1.Stop();
    }


  }
}