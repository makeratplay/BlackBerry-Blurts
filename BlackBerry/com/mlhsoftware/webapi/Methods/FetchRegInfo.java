//#preprocess
/*
 * FetchRegInfo.java
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



public class FetchRegInfo extends MethodsBase
{
  // Key Names
  private static String KEY_METHOD_NAME = "fetchRegInfo";
  private static String KEY_LAST_NAME = "LastName";
  private static String KEY_FIRST_NAME = "FirstName";
  private static String KEY_EMAIL = "Email";
  private static String KEY_DEVICE_ID = "DeviceId";
  private static String KEY_REG_KEY = "RegKey";


  public FetchRegInfo(String s)  throws JSONException
  {
    super(s);
  }

  public FetchRegInfo()
  {
    setMethod( KEY_METHOD_NAME );
  }

   
  public static String getType()
  {
    return KEY_METHOD_NAME;
  }

  
  public String getLastName()
  {
    return optString(KEY_LAST_NAME);
  }
  public void setLastName( String value )
  {
    try
    {
      remove(KEY_LAST_NAME);
      put(KEY_LAST_NAME, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }

  
  public String getFirstName()
  {
    return optString(KEY_FIRST_NAME);
  }
  public void setFirstName( String value )
  {
    try
    {
      remove(KEY_FIRST_NAME);
      put(KEY_FIRST_NAME, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }

  
  public String getEmail()
  {
    return optString(KEY_EMAIL);
  }
  public void setEmail( String value )
  {
    try
    {
      remove(KEY_EMAIL);
      put(KEY_EMAIL, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
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

  
  public String getRegKey()
  {
    return optString(KEY_REG_KEY);
  }
  public void setRegKey( String value )
  {
    try
    {
      remove(KEY_REG_KEY);
      put(KEY_REG_KEY, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }

  public String getResultText()
  {
    String retVal = "";
    JSONArray results = _response.optJSONArray( "ResultSet" );
    if ( results != null )
    {
      int len = results.length();
      for ( int i = 0; i < len; i++ )
      {
        try
        {
          JSONObject result = results.getJSONObject(i);

          retVal += result.optString("Product_Name");
          retVal += "\nKey: ";
          retVal += result.optString("Reg_Key");
          retVal += "\nPIN: ";
          retVal += result.optString("Device_ID");
          retVal += "\nDate: ";
          retVal += result.optString("Order_Date");
          retVal += "\nStore: ";
          retVal += result.optString("Channel");
          retVal += "\n\n";
        }
        catch( JSONException e )
        {
        }
      }
    }
    return retVal;
  }
}
