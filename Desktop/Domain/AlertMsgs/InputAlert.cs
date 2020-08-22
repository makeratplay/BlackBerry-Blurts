using System;
using System.Collections;
using System.Runtime.InteropServices;
using Blurts.Domain;

namespace Blurts
{
  [ComVisible(true)]
  public class InputAlert : AlertBase
  {
    // Tag Names
    private static String ALERT_NAME = "Input";
    private static String KEY_TYPE = "Type";      // mouse, keyboard
    private static String KEY_ACTION = "Action";  
    private static String KEY_DX = "dx";
    private static String KEY_DY = "dy";
    private static String KEY_KEYCODE = "KeyCode";



    public const String MOVE = "MOVE"; /* mouse move */
    public const String LEFTCLICK = "LEFTCLICK"; /* left button down */
    public const String LEFTDOWN = "LEFTDOWN"; /* left button down */
    public const String LEFTUP = "LEFTUP"; /* left button up */
    public const String RIGHTCLICK = "RIGHTCLICK"; /* left button down */
    public const String RIGHTDOWN = "RIGHTDOWN"; /* right button down */
    public const String RIGHTUP = "RIGHTUP"; /* right button up */
    public const String MIDDLEDOWN = "MIDDLEDOWN"; /* middle button down */
    public const String MIDDLEUP = "MIDDLEUP"; /* middle button up */
    public const String XDOWN = "XDOWN"; /* x button down */
    public const String XUP = "XUP"; /* x button down */
    public const String WHEEL = "WHEEL"; /* wheel button rolled */
    //public const int VIRTUALDESK = 0x4000; /* map to entire virtual desktop */
    //public const int ABSOLUTE = 0x8000; /* absolute move */


    Hashtable requiresShift = new Hashtable();

    public InputAlert(String s)
      : base(s)
    {
      requiresShift = new Hashtable();
      requiresShift.Add('~', 1);
      requiresShift.Add('!', 1);
      requiresShift.Add('@', 1);
      requiresShift.Add('#', 1);
      requiresShift.Add('$', 1);
      requiresShift.Add('%', 1);
      requiresShift.Add('^', 1);
      requiresShift.Add('&', 1);
      requiresShift.Add('*', 1);
      requiresShift.Add('(', 1);
      requiresShift.Add(')', 1);
      requiresShift.Add('_', 1);
      requiresShift.Add('+', 1);
      requiresShift.Add('Q', 1);
      requiresShift.Add('W', 1);
      requiresShift.Add('E', 1);
      requiresShift.Add('R', 1);
      requiresShift.Add('T', 1);
      requiresShift.Add('Y', 1);
      requiresShift.Add('U', 1);
      requiresShift.Add('I', 1);
      requiresShift.Add('O', 1);
      requiresShift.Add('P', 1);
      requiresShift.Add('{', 1);
      requiresShift.Add('}', 1);
      requiresShift.Add('|', 1);
      requiresShift.Add('A', 1);
      requiresShift.Add('S', 1);
      requiresShift.Add('D', 1);
      requiresShift.Add('F', 1);
      requiresShift.Add('G', 1);
      requiresShift.Add('H', 1);
      requiresShift.Add('J', 1);
      requiresShift.Add('K', 1);
      requiresShift.Add('L', 1);
      requiresShift.Add(':', 1);
      requiresShift.Add('"', 1);
      requiresShift.Add('Z', 1);
      requiresShift.Add('X', 1);
      requiresShift.Add('C', 1);
      requiresShift.Add('V', 1);
      requiresShift.Add('B', 1);
      requiresShift.Add('N', 1);
      requiresShift.Add('M', 1);
      requiresShift.Add('<', 1);
      requiresShift.Add('>', 1);
      requiresShift.Add('?', 1);
    }

    public InputAlert()
      : base(DisplayItemType.STATUS, ALERT_NAME)
    {
    }

    [ComVisible(true)]
    public int Type
    {
      get
      {
        int retVal = INPUT.MOUSE;
        String tmp = optString(KEY_TYPE);
        if (tmp == "KEYBOARD")
        {
          retVal = INPUT.KEYBOARD;
        }
        return retVal;
      }
    }

    [ComVisible(true)]
    public int Action
    {
      get
      {
        int retVal = 0;
        String tmp = optString(KEY_ACTION);
        if ( tmp == "MOVE" )
        {
          retVal = 0x0001; /* mouse move */
        }
        else if (tmp == "LEFTCLICK")
        {
          retVal = 0x0002 | 0x0004; /* left button down | left button up */
        }
        else if ( tmp == "LEFTDOWN" )
        {
          retVal = 0x0002; /* left button down */
        }
        else if ( tmp == "LEFTUP" )
        {
          retVal = 0x0004; /* left button up */
        }
        else if (tmp == "RIGHTCLICK")
        {
          retVal = 0x0008 | 0x0010; /* right button down | right button up */
        }
        else if ( tmp == "RIGHTDOWN" )
        {
          retVal = 0x0008; /* right button down */
        }
        else if ( tmp == "RIGHTUP" )
        {
          retVal = 0x0010; /* right button up */
        }
        else if ( tmp == "MIDDLEDOWN" )
        {
          retVal = 0x0020; /* middle button down */
        }
        else if ( tmp == "MIDDLEUP" )
        {
          retVal = 0x0040; /* middle button up */
        }
        else if ( tmp == "XDOWN" )
        {
          retVal = 0x0080; /* x button down */
        }
        else if ( tmp == "XUP" )
        {
          retVal = 0x0100; /* x button down */
        }
        else if ( tmp == "WHEEL" )
        {
          retVal = 0x0800; /* wheel button rolled */
        }
        else if ( tmp == "VIRTUALDESK" )
        {
          retVal = 0x4000; /* map to entire virtual desktop */
        }
        else if ( tmp == "ABSOLUTE" )
        {
          retVal = 0x8000; /* absolute move */
        }
        return retVal;
      }
    }

    [ComVisible(true)]
    public int dx
    {
      get
      {
        return optInt(KEY_DX);
      }
      set
      {
        try
        {
          remove(KEY_DX);
          put(KEY_DX, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    [ComVisible(true)]
    public int dy
    {
      get
      {
        return optInt(KEY_DY);
      }
      set
      {
        try
        {
          remove(KEY_DY);
          put(KEY_DY, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    [ComVisible(true)]
    public char KeyCode
    {
      get
      {
        char retVal = char.MinValue;
        String tmp = optString(KEY_KEYCODE);
        if ( tmp.Length > 0 )
        {
          retVal = tmp[0];
        }
        return retVal;
      }
      set
      {
        try
        {
          remove(KEY_KEYCODE);
          put(KEY_KEYCODE, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    public override Boolean ProcessMessage()
    {
      //m_scriptMethodName = "OnInputAlert";

      if (Type == INPUT.MOUSE)
      {
        Input[] input = new Input[1];
        input[0].type = INPUT.MOUSE;
        input[0].mi.dx = dx * 10;
        input[0].mi.dy = dy * 10;
        input[0].mi.dwFlags = Action;
        UInt32 r = User32.SendInput((UInt32)input.Length, input, Marshal.SizeOf(input[0]));
      }
      else if (Type == INPUT.KEYBOARD)
      {
        int keyStroke = 0;
        Input[] input = new Input[4];
        if (requiresShift.ContainsKey(KeyCode))
        {
          input[keyStroke].type = INPUT.KEYBOARD;
          input[keyStroke].ki.wVk = 0xA0; // VK_LSHIFT	
          input[keyStroke].ki.dwFlags = KEYEVENTF.KEYEVENTF_KEYDOWN;
          input[keyStroke].ki.time = 0;
          input[keyStroke].ki.wScan = 0;
          input[keyStroke].ki.dwExtraInfo = IntPtr.Zero;
          keyStroke++;
        }


        input[keyStroke].type = INPUT.KEYBOARD;
        input[keyStroke].ki.wVk = User32.VkKeyScan(KeyCode);
        input[keyStroke].ki.dwFlags = KEYEVENTF.KEYEVENTF_KEYDOWN;
        input[keyStroke].ki.time = 0;
        input[keyStroke].ki.wScan = 0;
        input[keyStroke].ki.dwExtraInfo = IntPtr.Zero;
        keyStroke++;

        input[keyStroke].ki.wVk = User32.VkKeyScan(KeyCode);
        input[keyStroke].ki.dwFlags = KEYEVENTF.KEYEVENTF_KEYUP;
        input[keyStroke].ki.time = 0;
        input[keyStroke].ki.wScan = 0;
        input[keyStroke].ki.dwExtraInfo = IntPtr.Zero;
        keyStroke++;

        if (requiresShift.ContainsKey(KeyCode))
        {
          input[keyStroke].type = INPUT.KEYBOARD;
          input[keyStroke].ki.wVk = 0xA0; // VK_LSHIFT	
          input[keyStroke].ki.dwFlags = KEYEVENTF.KEYEVENTF_KEYUP;
          input[keyStroke].ki.time = 0;
          input[keyStroke].ki.wScan = 0;
          input[keyStroke].ki.dwExtraInfo = IntPtr.Zero;
          keyStroke++;
        }
        UInt32 r = User32.SendInput((UInt32)keyStroke, input, Marshal.SizeOf(input[0]));
      }
      return true;
    }

    public class INPUT
    {
        public const int MOUSE = 0;
        public const int KEYBOARD = 1;
        public const int HARDWARE = 2;
    }

    public class MOUSEEVENTF
    {
        public const int MOVE = 0x0001; /* mouse move */
        public const int LEFTDOWN = 0x0002; /* left button down */
        public const int LEFTUP = 0x0004; /* left button up */
        public const int RIGHTDOWN = 0x0008; /* right button down */
        public const int RIGHTUP = 0x0010; /* right button up */
        public const int MIDDLEDOWN = 0x0020; /* middle button down */
        public const int MIDDLEUP = 0x0040; /* middle button up */
        public const int XDOWN = 0x0080; /* x button down */
        public const int XUP = 0x0100; /* x button down */
        public const int WHEEL = 0x0800; /* wheel button rolled */
        public const int VIRTUALDESK = 0x4000; /* map to entire virtual desktop */
        public const int ABSOLUTE = 0x8000; /* absolute move */
    }

    public class KEYEVENTF
    {
      public const int KEYEVENTF_KEYDOWN = 0x0000;
      public const int KEYEVENTF_EXTENDEDKEY = 0x0001;
      public const int KEYEVENTF_KEYUP = 0x0002;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public int mouseData;
        public int dwFlags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct KEYBDINPUT
    {
        public short wVk;
        public short wScan;
        public int dwFlags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct HARDWAREINPUT
    {
        public int uMsg;
        public short wParamL;
        public short wParamH;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct Input
    {
        [FieldOffset(0)]
        public int type;
        [FieldOffset(4)]
        public MOUSEINPUT mi;
        [FieldOffset(4)]
        public KEYBDINPUT ki;
        [FieldOffset(4)]
        public HARDWAREINPUT hi;
    }

    internal class User32
    {
        private User32() { }

        [DllImport("User32.dll")]
        public static extern UInt32 SendInput
        (
            UInt32 nInputs,
            Input[] pInputs,
            Int32 cbSize
        );

        [DllImport("user32.dll")]
        public static extern short VkKeyScan(char ch);
    }

    /*   http://www.codeproject.com/KB/system/keyboard.aspx?display=Print
    VK_NUMPAD7	0x67	VK_BACK	0x08
VK_NUMPAD8	0x68	VK_TAB	0x09
VK_NUMPAD9	0x69	VK_RETURN	0x0D
VK_MULTIPLY	0x6A	VK_SHIFT	0x10
VK_ADD	0x6B	VK_CONTROL	0x11
VK_SEPARATOR	0x6C	VK_MENU	0x12
VK_SUBTRACT	0x6D	VK_PAUSE	0x13
VK_DECIMAL	0x6E	VK_CAPITAL	0x14
VK_DIVIDE	0x6F	VK_ESCAPE	0x1B
VK_F1	0x70	VK_SPACE	0x20
VK_F2	0x71	VK_END	0x23
VK_F3	0x72	VK_HOME	0x24
VK_F4	0x73	VK_LEFT	0x25
VK_F5	0x74	VK_UP	0x26
VK_F6	0x75	VK_RIGHT	0x27
VK_F7	0x76	VK_DOWN	0x28
VK_F8	0x77	VK_PRINT	0x2A
VK_F9	0x78	VK_SNAPSHOT	0x2C
VK_F10	0x79	VK_INSERT	0x2D
VK_F11	0x7A	VK_DELETE	0x2E
VK_F12	0x7B	VK_LWIN	0x5B
VK_NUMLOCK	0x90	VK_RWIN	0x5C
VK_SCROLL	0x91	VK_NUMPAD0	0x60
VK_LSHIFT	0xA0	VK_NUMPAD1	0x61
VK_RSHIFT	0xA1	VK_NUMPAD2	0x62
VK_LCONTROL	0xA2	VK_NUMPAD3	0x63
VK_RCONTROL	0xA3	VK_NUMPAD4	0x64
VK_LMENU	0xA4	VK_NUMPAD5	0x65
VK_RMENU	0xA5	VK_NUMPAD6	0x66

    */
  }
}