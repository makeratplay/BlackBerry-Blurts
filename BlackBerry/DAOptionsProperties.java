package com.mlhsoftware.Blurts;

import com.mlhsoftware.ui.blurts.*;
import net.rim.device.api.system.*;
import net.rim.device.api.util.*;
import java.util.*;

import net.rim.device.api.system.DeviceInfo;
import net.rim.device.api.crypto.MD5Digest;
import net.rim.device.api.system.EventLogger;

//The configuration properties of the OptionsSample application. One instance holding
//the effective values resides in the persistent store.
class DAOptionsProperties implements Persistable
{

  static private DAOptionsProperties Instance = null;

  private boolean m_autoStart;
  private boolean m_callAlert;
  private boolean m_smsAlert;
  private boolean[] m_emailAlert = new boolean[10];
  private boolean m_everConnected;
  private boolean m_buzzOnConnection;
  private boolean m_levelAlert;
  private boolean m_pinMsgAlert;

  static public DAOptionsProperties GetInstance()
  {
    if ( Instance == null )
    {
      Instance = (DAOptionsProperties)store.getContents();
      //Instance = new DAOptionsProperties();
    }
    return Instance;
  }
  
  private static final long PERSISTENCE_ID = 0xf08896f4d2deed94L;   //Hash of com.mlhsoftware.Blurts.options.DAOptionsProperties


  //Persistent object wrapping the effective properties instance
  private static PersistentObject store;
  private static boolean firstRun = false;

    //Ensure that an effective properties set exists on startup.
  static
  {
    store = PersistentStore.getPersistentObject( PERSISTENCE_ID );
    synchronized ( store )
    {
      if ( store.getContents() == null )
      {
        firstRun = true;
        store.setContents( new DAOptionsProperties() );
        store.commit();
      }
    }
  }

  static void reset()
  {
    ActivationKeyStore.reset();
    store = PersistentStore.getPersistentObject( PERSISTENCE_ID );
    synchronized ( store )
    {
      firstRun = true;
      store.setContents( null );
      store.commit();
    }
    Instance = null;
  }

  // Constructs a properties set with default values.
  private DAOptionsProperties()
  {
    m_autoStart = true;
    m_callAlert = true;
    m_smsAlert = true;
    m_everConnected = false;
    m_buzzOnConnection = false;
    m_levelAlert = true;
    m_pinMsgAlert = true;
    for ( int i = 0; i < 10; i++ )
    {
      m_emailAlert[i] = true;
    }
  }

  //Cannonical copy constructor.
  private DAOptionsProperties( DAOptionsProperties other )
  {
    m_autoStart = other.m_autoStart;
    m_callAlert = other.m_callAlert;
    m_smsAlert = other.m_smsAlert;
    m_everConnected = other.m_everConnected;
    m_buzzOnConnection = other.m_buzzOnConnection;
    for ( int i = 0; i < 10; i++ )
    {
      m_emailAlert[i] = other.m_emailAlert[i];
    }
  }

  public static boolean isFirstRun()
  {
    return firstRun;
  }

  //Retrieves a copy of the effective properties set from storage.
 /* public static DAOptionsProperties fetch()
  {
    DAOptionsProperties savedProps = (DAOptionsProperties)store.getContents();
    return new DAOptionsProperties( savedProps );
  }
  * */

  //Causes the values within this instance to become the effective
  //properties for the application by saving this instance to the store.
  public void save()
  {
    store.setContents( this );
    store.commit();
  }

  public boolean autoStart()
  {
    if ( ActivationKeyStore.isKeyValid() )
    {
      return m_autoStart;
    }
    else
    {
      return false;
    }
  }

  public void setAutoStart( boolean val )
  {
    if ( ActivationKeyStore.isKeyValid() )
    {
      m_autoStart = val;
    }
  }

  public boolean displayCallAlert()
  {
    if ( ActivationKeyStore.isKeyValid() )
    {
      return m_callAlert;
    }
    else
    {
      return true;
    }
  }

  public void setCallAlert( boolean val )
  {
    if ( ActivationKeyStore.isKeyValid() )
    {
      m_callAlert = val;
    }
  }

  public boolean displaySMSAlert()
  {
    if ( ActivationKeyStore.isKeyValid() )
    {
      return m_smsAlert;
    }
    else
    {
      return false;
    }
  }

  public void setSMSAlert( boolean val )
  {
    if ( ActivationKeyStore.isKeyValid() )
    {
      m_smsAlert = val;
    }
  }

  public boolean displayEmailAlert( int index )
  {
    if ( ActivationKeyStore.isKeyValid() )
    {
      if ( index < 10 )
      {
        return m_emailAlert[index];
      }
      else
      {
        return false;
      }
    }
    else
    {
      if ( index == 0 )
      {
        return true;
      }
      else
      {
        return false;
      }
    }
  }

  public void setEmailAlert( boolean val, int index )
  {
    if ( ActivationKeyStore.isKeyValid() )
    {
      if ( index < 10 )
      {
        m_emailAlert[index] = val;
      }
    }
  }

  public boolean displayLevelAlert()
  {
    return m_levelAlert;
  }

  public void setLevelAlert( boolean val )
  {
    m_levelAlert = val;
  }


  public boolean displayPinMsgAlert()
  {
    return m_pinMsgAlert;
  }

  public void setPinMsgAlert( boolean val )
  {
    m_pinMsgAlert = val;
  }

  public boolean hasExpired()
  {
    boolean retVal = false;
    /*
    long now = new Date().getTime();
    Calendar expiration = Calendar.getInstance();
    expiration.set( Calendar.YEAR, 2009 );
    expiration.set( Calendar.MONTH, 11 );
    expiration.set( Calendar.DATE, 1 );
    expiration.set( Calendar.HOUR, 11 );
    expiration.set( Calendar.MINUTE, 00 );
    if ( now > expiration.getTime().getTime() )
    {
      retVal = true;
    }
     * */
    return retVal;
  }

  public void setEverConnected( boolean val )
  {
    m_everConnected = val;
  }

  public boolean everConnected()
  {
    return m_everConnected;
  }

  public void setBuzzOnConnection( boolean val )
  {
    m_buzzOnConnection = val;
  }

  public boolean buzzOnConnection()
  {
    return m_buzzOnConnection;
  }

  
}
