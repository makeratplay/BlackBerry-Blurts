using System;

using System.Xml;
using System.Xml.Xsl;
using System.Collections;
using System.Runtime.InteropServices;

namespace Blurts
{
  [ComVisible(true)]
  public class CallCmd : CmdBase
  {
    // Tag Names
    private static String  KEY_NUMBER = "PhoneNumber";


    public CallCmd(String s)
      : base(s)
    {
    }

    public CallCmd()
      : base(CMD_PLACECALL)
    {
    }

    [ComVisible(true)]
    public String PhoneNumber
    {
      get
      {
        return optString(KEY_NUMBER);
      }
      set
      {
        try
        {
          remove(KEY_NUMBER);
          put(KEY_NUMBER, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }
  }
}

