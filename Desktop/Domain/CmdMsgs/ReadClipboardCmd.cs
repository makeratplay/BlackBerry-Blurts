using System;

using System.Xml;
using System.Xml.Xsl;
using System.Collections;
using System.Runtime.InteropServices;

namespace Blurts
{
  [ComVisible(true)]
  public class ReadClipboardCmd : CmdBase
  {
    // Tag Names
    //private static String KEY_TEXT = "Text";


    public ReadClipboardCmd(String s)
      : base(s)
    {
    }

    public ReadClipboardCmd()
      : base(CMD_READCLIPBOARD)
    {
    }

  }
}
