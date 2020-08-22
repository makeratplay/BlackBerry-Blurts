//#preprocess
/*
 * FetchKey.java
 *
 * MLH Software
 * Copyright 2010
 */

//#ifdef BLURTS
package com.mlhsoftware.webapi.blurts;
//#endif 

//#ifdef SIMPLYREMINDME
package com.mlhsoftware.webapi.SimplyRemindMe;
//#endif 

//#ifdef SIMPLYTASKS
package com.mlhsoftware.webapi.SimplyTasks;
//#endif 

//#ifdef MLHKEY
package com.mlhsoftware.webapi.MLHKey;
//#endif 

import org.json.me.*;


public class FetchKey extends MethodsBase
{
  // Key Names
  private static String KEY_METHOD_NAME = "fetchKey";
  private static String KEY_DEVICE_ID = "DeviceId";
  private static String KEY_RESULT_TEXT = "Text";
  private static String KEY_RESULT_CODE = "Code";


  public FetchKey(String s) throws JSONException
  {
    super(s);
  }

  public FetchKey()
  {
    setMethod( KEY_METHOD_NAME );
  }

   
  public static String getType()
  {
    return KEY_METHOD_NAME;
  }



  
  public String getDeviceId()
  {
    return optString(KEY_DEVICE_ID);
  }
  public void setDeviceId( String value )
  {
    try
    {
      remove(KEY_DEVICE_ID);
      put(KEY_DEVICE_ID, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }


  public String getResultText()
  {
    return _response.optString(KEY_RESULT_TEXT);
  }

  public String getResultCode()
  {
    return _response.optString(KEY_RESULT_CODE);
  }
}
