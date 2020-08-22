using System;

using System.Xml;
using System.Xml.Xsl;
using System.Collections;
using System.Runtime.InteropServices;

namespace Blurts
{
  [ComVisible(true)]
  public class SMSCmd : CmdBase
  {
    // Tag Names
    private static String KEY_ADDRESS = "SenderAddress";
    private static String KEY_BODY = "BodyText";


    public SMSCmd(String s)
      : base(s)
    {
    }

    public SMSCmd()
      : base(CMD_SENDSMS)
    {
    }

    [ComVisible(true)]
    public String Address
    {
      get
      {
        return optString(KEY_ADDRESS);
      }
      set
      {
        try
        {
          remove(KEY_ADDRESS);
          put(KEY_ADDRESS, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    [ComVisible(true)]
    public String Text
    {
      get
      {
        return optString(KEY_BODY);
      }
      set
      {
        try
        {
          remove(KEY_BODY);
          put(KEY_BODY, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }
  }
}
