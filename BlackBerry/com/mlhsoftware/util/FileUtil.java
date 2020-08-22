//#preprocess
/*
 * TitlebarManager.java
 *
 * MLH Software
 * Copyright 2010
 */

//#ifdef BLURTS
package com.mlhsoftware.util.blurts;
//#endif 

//#ifdef SIMPLYREMINDME
package com.mlhsoftware.util.SimplyRemindMe;
//#endif 

//#ifdef SIMPLYTASKS
package com.mlhsoftware.util.SimplyTasks;
//#endif 

import java.util.Enumeration;
import java.util.Vector;
import javax.microedition.io.Connector;
import javax.microedition.io.file.FileConnection;
import javax.microedition.io.file.FileSystemRegistry;
import java.io.*;

public class FileUtil 
{

  public FileUtil()
  {
  // default constructor
  }

  public final static Vector listRoots()
  {
    Vector roots = new Vector();
    Enumeration e = FileSystemRegistry.listRoots();
    while (e.hasMoreElements()) 
    {
      roots.addElement((String) e.nextElement());
    }
    return roots;
  }

  private final static String getRootFileSystem()
  {
    String root = null;
    Enumeration e = FileSystemRegistry.listRoots();
    while (e.hasMoreElements()) 
    {
      root = (String) e.nextElement();
      if( root.equalsIgnoreCase("sdcard/") ) 
      {
        return "file:///SDCard";
      } 
      else if( root.equalsIgnoreCase("store/") ) 
      {
        return "file:///store";
      }
    }
    return null;
  }

  public final static boolean checkExists( String passFileOrDirName )
  {
    boolean result = false;
    FileConnection fconn;
    try
    {
      fconn = (FileConnection)Connector.open( passFileOrDirName, Connector.READ );
      if (!fconn.exists())
      {
        result = false;
      }
      else
      {
        result = true;
      }
      fconn.close();
    }
    catch (Exception e) 
    {
      System.out.println("Error: " + e.getClass() + " " + e.getMessage());
    }
    return result;
  }

  public final static boolean createFileOrDir( String passFileOrDirName, boolean asFile )
  {
    boolean result = false;
    FileConnection fconn;
    try 
    {
      fconn = (FileConnection)Connector.open( passFileOrDirName, Connector.READ_WRITE );
      if (fconn.exists()) 
      {
        System.out.println( "File/Dir already exists: " + passFileOrDirName );
        fconn.close();
      }
      else
      {
        fconn.close();
        try 
        {
          fconn = (FileConnection)Connector.open( passFileOrDirName, Connector.READ_WRITE );
          if (asFile)
          {
            fconn.create();
          }
          else
          {
            fconn.mkdir();
          }
          result = true;
          fconn.close();
        }
        catch (Exception e) 
        {
          System.out.println("Error: " + e.getClass() + " " + e.getMessage());
        }
      }
    }
    catch (Exception e) 
    {
      System.out.println("Error accessing root filesystem " + e.toString());
    }
    return result;
  }

  public final static boolean writeTextFile( String passFname, String passContent )
  {
    boolean result = false;
    try 
    {
      System.out.println("Write File: " + passContent );
      FileConnection fconn = (FileConnection)Connector.open( passFname, Connector.READ_WRITE );
      // If no exception is thrown, then the URI is valid, but the file may or may not exist.
      if (!fconn.exists()) 
      {
        fconn.create();
      }

      DataOutputStream dos = fconn.openDataOutputStream();
      // dos.writeChars is used for example purposes only
      // to understand why, view the resultant file
      dos.writeUTF(passContent);
      dos.close();
      fconn.close();
      result = true;
    }
    catch (Exception e) 
    {
      System.out.println("Error accessing root filesystem: " + e.toString());
    }
    return result;
  }

  public final static String readTextFile( String passFname )
  {
    String result = null;
    try 
    {
      FileConnection fconn = (FileConnection)Connector.open( passFname, Connector.READ_WRITE );
      // If no exception is thrown, then the URI is valid, but the file may or may not exist.
      if (fconn.exists()) 
      {
        DataInputStream dos = fconn.openDataInputStream();
        result = dos.readUTF();
        dos.close();
        fconn.close();
      }
    }
    catch (Exception e) 
    {
      System.out.println("Error accessing root filesystem " + e.toString());
    }
    return result;
  }

  public final static String readBinaryFile( String passFname, byte[] data )
  {
    String result = null;
    try 
    {
      FileConnection fconn = (FileConnection)Connector.open( passFname, Connector.READ_WRITE );
      // If no exception is thrown, then the URI is valid, but the file may or may not exist.
      if (fconn.exists()) 
      {
        int length = (int)fconn.fileSize();
        DataInputStream dos = fconn.openDataInputStream();
        dos.readFully( data );
        dos.close();
        fconn.close();
      }
    }
    catch (Exception e) 
    {
      System.out.println("Error accessing root filesystem " + e.toString());
    }
    return result;
  }

  public final static int getFileSize( String passFname )
  {
    int length = 0;
    try 
    {
      FileConnection fconn = (FileConnection)Connector.open( passFname, Connector.READ_WRITE );
      // If no exception is thrown, then the URI is valid, but the file may or may not exist.
      if (fconn.exists()) 
      {
        length = (int)fconn.fileSize();
        fconn.close();
      }
    }
    catch (Exception e) 
    {
      System.out.println("Error accessing root filesystem " + e.toString());
    }
    return length;
  }
}
