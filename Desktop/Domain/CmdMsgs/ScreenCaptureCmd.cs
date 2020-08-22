using System;

using System.Xml;
using System.Xml.Xsl;
using System.Collections;
using System.Runtime.InteropServices;

namespace Blurts
{
  [ComVisible(true)]
  public class ScreenCaptureCmd : CmdBase
  {
    // Tag Names
  private static String KEY_QUALITY = "Quality";
  private static String KEY_TOP = "Top";
  private static String KEY_LEFT = "Left";
  private static String KEY_WIDTH = "Width";
  private static String KEY_HEIGHT = "Height";


    public ScreenCaptureCmd(String s)
      : base(s)
    {
      Top = 0;
      Left = 0;
      Width = -1;
      Height = -1;
    }

    public ScreenCaptureCmd()
      : base(CMD_SCREENSHOT)
    {
      Top = 0;
      Left = 0;
      Width = -1;
      Height = -1;
    }

    [ComVisible(true)]
    public int Quality
    {
      get
      {
        return optInt(KEY_QUALITY);
      }
      set
      {
        try
        {
          remove(KEY_QUALITY);
          put(KEY_QUALITY, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    [ComVisible(true)]
    public int Top
    {
      get
      {
        return optInt(KEY_TOP);
      }
      set
      {
        try
        {
          remove(KEY_TOP);
          put(KEY_TOP, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    [ComVisible(true)]
    public int Left
    {
      get
      {
        return optInt(KEY_LEFT);
      }
      set
      {
        try
        {
          remove(KEY_LEFT);
          put(KEY_LEFT, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    [ComVisible(true)]
    public int Width
    {
      get
      {
        return optInt(KEY_WIDTH);
      }
      set
      {
        try
        {
          remove(KEY_WIDTH);
          put(KEY_WIDTH, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    [ComVisible(true)]
    public int Height
    {
      get
      {
        return optInt(KEY_HEIGHT);
      }
      set
      {
        try
        {
          remove(KEY_HEIGHT);
          put(KEY_HEIGHT, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }
  }
}
