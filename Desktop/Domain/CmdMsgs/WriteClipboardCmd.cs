using System;

using System.Xml;
using System.Xml.Xsl;
using System.Collections;
using System.Runtime.InteropServices;

namespace Blurts
{
  [ComVisible(true)]
  public class WriteClipboardCmd : CmdBase
  {
    // Tag Names
    private static String KEY_TEXT = "Text";


    public WriteClipboardCmd(String s)
      : base(s)
    {
    }

    public WriteClipboardCmd()
      : base(CMD_WRITECLIPBOARD)
    {
    }

    [ComVisible(true)]
    public String Text
    {
      get
      {
        return optString(KEY_TEXT);
      }
      set
      {
        try
        {
          remove(KEY_TEXT);
          put(KEY_TEXT, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }
  }
}
