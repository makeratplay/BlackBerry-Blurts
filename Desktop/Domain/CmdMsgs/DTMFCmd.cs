using System;

using System.Xml;
using System.Xml.Xsl;
using System.Collections;
using System.Runtime.InteropServices;

namespace Blurts
{
  [ComVisible(true)]
  public class DTMFCmd : CmdBase
  {
    // Tag Names
    private static String KEY_DTMF = "DTMFTones";


    public DTMFCmd(String s)
      : base(s)
    {
    }

    public DTMFCmd()
      : base(CMD_DTMF)
    {
    }

    [ComVisible(true)]
    public String DTMFTones
    {
      get
      {
        return optString(KEY_DTMF);
      }
      set
      {
        try
        {
          remove(KEY_DTMF);
          put(KEY_DTMF, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }
  }
}
