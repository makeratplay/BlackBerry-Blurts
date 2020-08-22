using System;
using System.Runtime.InteropServices;
using System.IO;
using Blurts.Domain;

namespace Blurts
{
  [ComVisible(true)]
  public class ContactAlert : AlertBase
  {
    // Tag Names
    private static String ALERT_NAME = "Connect";
    private static String KEY_INDEX  = "Index";
    //private static String KEY_UID         = "Uid";
    //private static String KEY_PREFIXNAME  = "PrefixName";
    //private static String KEY_FIRSTNAME   = "FirstName";
    //private static String KEY_LASTNAME    = "LastName";
    //private static String KEY_DISPLAYNAME = "DisplayName";
    private static String KEY_PHOTO       = "Photo";
    //private static String KEY_ORG         = "Org";
    //private static String KEY_PIN         = "Pin";


    public ContactAlert(String s)
      : base(s)
    {
    }

    public ContactAlert()
      : base(DisplayItemType.STATUS, ALERT_NAME)
    {
    }

    [ComVisible(true)]
    public String ImageData
    {
      get
      {
        return optString(KEY_PHOTO);
      }
      set
      {
        try
        {
          remove(KEY_PHOTO);
          put(KEY_PHOTO, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    public String Index
    {
      get
      {
        return optString(KEY_INDEX);
      }
      set
      {
        try
        {
          remove(KEY_INDEX);
          put(KEY_INDEX, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    public override Boolean ProcessMessage()
    {
      this.DisplayAlert = false;
      this.PlaySound = false;

      if (this.ImageData.Length > 0)
      {
        string fileName = @"\Contacts\image_" + Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".jpg";
        Base64ToImage(this.ImageData, fileName);
        this.ImageFile = fileName;
        this.ImageData = "";
      }

      try
      {
        string xml = this.XML;
        Contacts.Instance.UpdateContact(xml);
      }
      catch (Exception exc)
      {
        Console.WriteLine(exc.ToString());
      }

      runScript( "OnContactAlert" );
      return true;
    }

    private string Base64ToImage(string base64String, string fileName)
    {
      string imageFile = ApplicationSettings.Instance.LocalDataPath + fileName;

      // Convert Base64 string to byte[]
      byte[] imageBytes = Convert.FromBase64String(base64String);
      MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

      FileStream fs = File.OpenWrite(imageFile);
      byte[] data = ms.ToArray();
      fs.Write(data, 0, data.Length);
      fs.Close();
      return imageFile;
    }

    static public string ImageToBase64(string fileName)
    {
      FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
      byte[] filebytes = new byte[fs.Length];
      fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
      string encodedData = Convert.ToBase64String(filebytes);
      return encodedData;
    }
  }
}