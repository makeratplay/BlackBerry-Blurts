using System;

using System.Xml;
using System.Xml.Xsl;
using System.Collections;
using System.Runtime.InteropServices;

namespace Blurts
{
  [ComVisible(true)]
  public class BuzzCmd : CmdBase
  {
    // Tag Names
    private static String KEY_BUZZ_LENGTH = "BuzzLength";


    public BuzzCmd(String s)
      : base(s)
    {
    }

    public BuzzCmd()
      : base(CMD_BUZZ)
    {
      put(KEY_BUZZ_LENGTH, 600);
    }

    [ComVisible(true)]
    public int BuzzLength
    {
      get
      {
        return optInt(KEY_BUZZ_LENGTH);
      }
      set
      {
        try
        {
          remove(KEY_BUZZ_LENGTH);
          put(KEY_BUZZ_LENGTH, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }
  }
}
