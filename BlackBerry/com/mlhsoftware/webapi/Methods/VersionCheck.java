//#preprocess
/*
 * VersionCheck.java
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


public class VersionCheck extends MethodsBase
{
  // Key Names
  private static String KEY_METHOD_NAME = "versionCheck";
  private static String KEY_BETA = "Beta";
  private static String KEY_VERSION = "Version";

  // Result keys
  private static String KEY_RESULT_NEW = "New";
  private static String KEY_RESULT_VERSION = "Version";
  private static String KEY_RESULT_URL = "Url";

  public VersionCheck(String s)  throws JSONException
  {
    super(s);
  }

  public VersionCheck()
  {
    setMethod( KEY_METHOD_NAME );
  }

  
  static public  String getType()
  {
    return KEY_METHOD_NAME;
  }

  
  public String getVersion()
  {
    return optString(KEY_VERSION);
  }
  public void setVersion( String value )
  {
    try
    {
      remove(KEY_VERSION);
      put(KEY_VERSION, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }
  
  
  public boolean getBeta()
  {
    return optBoolean(KEY_BETA);
  }
  public void setBeta( boolean value )
  {
    try
    {
      remove(KEY_BETA);
      put(KEY_BETA, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }


  public boolean getNew()
  {
    return _response.optBoolean(KEY_RESULT_NEW);
  }

  public String getNewVersion()
  {
    return _response.optString(KEY_RESULT_VERSION);
  }

  public String getUrl()
  {
    return _response.optString(KEY_RESULT_URL);
  }
}
