using System;

using System.Xml;
using System.Xml.Xsl;
using System.Collections;
using System.Runtime.InteropServices;

namespace Blurts
{
  [ComVisible(true)]
  public class CmdBase : JSONObject
  {
    // Key Names
    private static String KEY_CMD_TYPE = "CmdType";

    // Command IDs
    public const int CMD_NOTPRO = -2;
    public const int CMD_UNKNOWN = 0;
    public const int CMD_PLACECALL = 1;
    public const int CMD_PRESSKEY = 2;
    public const int CMD_BUZZ = 3;
    public const int CMD_SENDSMS = 4;
    public const int CMD_CONTACTSEARCH = 5;
    public const int CMD_SCREENSHOT = 6;
    public const int CMD_CREATECONTACT = 7;
    public const int CMD_READCLIPBOARD = 8;
    public const int CMD_WRITECLIPBOARD = 9;
    public const int CMD_LEVEL = 10;
    public const int CMD_DTMF = 11;
    public const int CMD_SENDEMAIL = 12;
    public const int CMD_LOCATEME = 13;
    public const int CMD_READFILE = 14;

    public CmdBase(String s)
      : base(s)
    {
    }

    public CmdBase(int cmdType)
    {
      try
      {
        put(KEY_CMD_TYPE, cmdType);
      }
      catch (JSONException e)
      {
        Console.WriteLine(e.ToString());
      }
    }

    [ComVisible(true)]
    public int CmdType
    {
      get
      {
        return optInt(KEY_CMD_TYPE);
      }
    }

    public Boolean isProCmd()
    {
      Boolean retVal = true;
      switch ( CmdType )
      {
        case CMD_BUZZ:
        case CMD_LEVEL:
        {
          retVal = false;
          break;
        }
      }
      return retVal;
    }
  }
}
