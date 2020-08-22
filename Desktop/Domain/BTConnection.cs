using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Configuration;
using System.Windows.Forms;

using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using InTheHand.Net;
using System.Net.Sockets;

using System.IO;
using System.IO.Ports;
using System.Net;


namespace Blurts
{
  public delegate void MsgArrivedEventHandler(object msg);
  public delegate void LevelStatusEventHandler(int battery, int signal);
  public delegate void BluetoothConnectEventHandler();
  public delegate void BluetoothDisconnectEventHandler();

  public class BTConnection
  {
    // static public Guid SERVICE_UUID_STRING = new Guid("c25d5973-8ba8-48ea-9863-54fcd69fc6a4");
    static public Guid SERVICE_UUID_STRING = new Guid("00001101-0000-1000-8000-00805F9B34FB");


    public event MsgArrivedEventHandler MsgArrivedEvent;
    public event BluetoothConnectEventHandler BluetoothConnectEvent;
    public event BluetoothDisconnectEventHandler BluetoothDisconnectEvent;

    Thread m_dataTread;

    const int MessageHeaderSize = 10;
    static private BluetoothClient s_client = null;
    static private SerialPort s_serialPort = null;
    static public Stream s_stream = null;   //NetworkStream
    static List<string> s_rawData = new List<string>();
    public List<byte[]> m_messages = null;
    private Boolean m_connected;
    private int m_channel;


    int m_textBytesReceived;
    int m_binaryBytesReceived;

    enum State
    {
      eHeader = 1,
      eTextMsg = 2,
      eBinaryMsg = 3,
      ePostMsg = 4,
      eFlushBuffer = 5,
      eMarker1 = 6
    }

    byte[] m_dataBuffer;
    State m_state;
    int m_msgIndex;
    byte[] m_headerBuffer;

    int m_textMessageSize;
    byte[] m_textMsgBuffer;

    int m_binaryMessageSize;
    byte[] m_binaryMsgBuffer;

    long m_msgCount;
    long m_dataCount;

    object m_threadLock;
    Boolean m_exiting;


    public BTConnection()
    {
      m_msgCount = 0;
      m_dataCount = 0;
      m_threadLock = new object();
      m_channel = -1;
      m_connected = false;
      m_dataTread = null;
      m_messages = new List<byte[]>();
      m_textMessageSize = 0;
      m_textBytesReceived = 0;
      m_binaryMessageSize = 0;
      m_binaryBytesReceived = 0;
      m_msgIndex = 0;
      m_exiting = false;
      m_state = State.eHeader;
      m_dataBuffer = new byte[5000];
    }

    public void Connect()
    {



      if (m_dataTread == null)
      {
        m_dataTread = new Thread(connectDevice);
        m_dataTread.Start();
      }
    }

    public void Exiting()
    {
      m_exiting = true;
    }

    public void Disconnect()
    {
      if (s_stream != null)
      {
        s_stream.Close();
        s_stream = null;
      }

      if (s_client != null)
      {
        s_client.Close();
        s_client = null;
      }

      if (s_serialPort != null)
      {
        s_serialPort.Close();
        s_serialPort = null;
      }
    }

    public Boolean IsConnected()
    {
      Boolean retVal = false;
      if (m_dataTread != null && m_connected)
      {
        retVal = true;
      }
      return retVal;
    }

    public void sendCommand(CmdBase msg)
    {
      if (s_stream != null && IsConnected())
      {
        if (ApplicationSettings.Instance.Active || !msg.isProCmd())
        {
          System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
          byte[] data = encoding.GetBytes(msg.ToString());

          byte[] dataLen = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((int)data.Length));
          byte[] binaryLen = BitConverter.GetBytes((int)0);

          byte[] rv = new byte[dataLen.Length + binaryLen.Length + data.Length];
          System.Buffer.BlockCopy(dataLen, 0, rv, 0, dataLen.Length);
          System.Buffer.BlockCopy(binaryLen, 0, rv, dataLen.Length, binaryLen.Length);
          System.Buffer.BlockCopy(data, 0, rv, dataLen.Length + binaryLen.Length, data.Length);

          if (Directory.Exists(ApplicationSettings.Instance.LocalDataPath + @"\debug\raw"))
          {
            FileStream stream = new FileStream(ApplicationSettings.Instance.LocalDataPath + @"\debug\raw\data_out.bin", FileMode.OpenOrCreate);
            BinaryWriter w = new BinaryWriter(stream);
            w.Seek((int)stream.Length, SeekOrigin.Begin);
            w.Write(rv, 0, rv.Length);
            w.Close();
          }

          s_stream.Write(rv, 0, rv.Length);
        }
        else
        {
          BuyDlg dlg = new BuyDlg();
          dlg.ShowDialog();
          //MessageBox.Show("Upgrade to Blurts Pro to enable this feature.", "Blurts - by MLH Software");
        }
      }
      else
      {
        MessageBox.Show("BlackBerry not connected.", "Blurts - by MLH Software");
      }
    }

    // Runs on m_dataTread tread
    public void connectDevice()
    {

      //readFile(@"C:\Documents and Settings\Michael\My Documents\Downloads\Testdata.bin");

      try
      {
        s_serialPort = null;
        s_client = null;
        string address = ApplicationSettings.Instance.DeviceAddress;
        if (address.Length > 0)
        {
          if (address.StartsWith("COM", true, null))
          {
            try
            {
              s_serialPort = new SerialPort(address, 115200, Parity.None, 8, StopBits.One);
              s_serialPort.ReceivedBytesThreshold = 30;
              s_serialPort.Open();
              s_stream = s_serialPort.BaseStream;
              s_serialPort.ReadTimeout = 300;

              readSerialPortLoop();
            }
            catch (Exception ex)
            {
              Console.WriteLine("SerialPort Error:" + Environment.NewLine + ex.ToString());
            }
            finally
            {
              s_serialPort.Close();
              s_serialPort = null;
            }
          }
          else
          {
            BluetoothAddress btAddress = BluetoothAddress.Parse(address);
            if (m_channel == -1)
            {
              m_channel = GetRfcommChannelNumber(btAddress);
            }
            if (m_channel > -1)
            {
              BluetoothEndPoint endPoint = new BluetoothEndPoint(btAddress, SERVICE_UUID_STRING, m_channel);

              s_client = new BluetoothClient();
              s_client.Connect(endPoint);
              s_stream = s_client.GetStream();
              readStreamLoop();
            }
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Connect To Device Error:" + Environment.NewLine + ex.ToString());
      }

      try
      {
        if (BluetoothDisconnectEvent != null && m_connected && !m_exiting)
        {
          BluetoothDisconnectEvent();
        }
        //m_form.Invoke(new EventHandler(m_form.OnDisconnect));
      }
      catch (Exception ex)
      {
        Console.WriteLine("BluetoothDisconnectEvent Error:" + Environment.NewLine + ex.ToString());
      }
      finally
      {
        m_connected = false;
        m_dataTread = null;
      }
    }

    private int GetRfcommChannelNumber(BluetoothAddress btAddress)
    {
      int channel = -1;
      try
      {
        BluetoothDeviceInfo device = new BluetoothDeviceInfo(btAddress);
        ServiceRecord[] records = device.GetServiceRecords(BTConnection.SERVICE_UUID_STRING);
        foreach (ServiceRecord curRecord in records)
        {
          ServiceAttribute attribute = curRecord.GetAttributeById((ServiceAttributeId)0x0100);
          if (attribute.Value.GetValueAsString(Encoding.Default) == "Blurts")
          {
            channel = ServiceRecordHelper.GetRfcommChannelNumber(curRecord);
          }
        }
      }
      catch (Exception ex)
      {

        Console.WriteLine("GetRfcommChannelNumber: " + ex.ToString());
      }
      return channel;
    }


    public void readStreamLoop()
    {
      try
      {
        if (s_stream != null)
        {
          m_state = State.eHeader;
          m_textMessageSize = 0;
          m_binaryMessageSize = 0;

          // Check to see if this Stream is readable.
          if (s_stream.CanRead)
          {
            m_connected = true;
            if (BluetoothConnectEvent != null)
            {
              BluetoothConnectEvent();
            }
            //m_form.Invoke(new EventHandler(m_form.OnConnect));
            int numberOfBytesRead = 0;

            do
            {
              numberOfBytesRead = 0;
              numberOfBytesRead = s_stream.Read(m_dataBuffer, 0, m_dataBuffer.Length);

              if (numberOfBytesRead > 0)
              {
                OnDataReceived(numberOfBytesRead);
              }
            }
            while (numberOfBytesRead != 0); // loop until connection is closed
          }
          else
          {
            Console.WriteLine("Cannot read from this NetworkStream.");
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Connect To Device Error:" + Environment.NewLine + ex.ToString());
      }
    }

    public void readSerialPortLoop()
    {
      m_connected = false;
      Thread.Sleep(400);
      try
      {
        if (s_serialPort != null)
        {
          m_state = State.eHeader;
          m_textMessageSize = 0;
          m_binaryMessageSize = 0;

          int numberOfBytesRead = 0;
          do
          {

            numberOfBytesRead = 0;
            // Incoming message may be larger than the buffer size.

            try
            {
              numberOfBytesRead = s_serialPort.Read(m_dataBuffer, 0, m_dataBuffer.Length);
            }
            catch (TimeoutException)
            {
              if (s_serialPort.CtsHolding == true)
              {
                numberOfBytesRead = -1;
              }
            }

            if (numberOfBytesRead > 0)
            {
              if (!m_connected)
              {
                m_connected = true;
                if (BluetoothConnectEvent != null)
                {
                  BluetoothConnectEvent();
                }
                //m_form.Invoke(new EventHandler(m_form.OnConnect));
              }
              OnDataReceived(numberOfBytesRead);
            }
            else if (!m_connected)
            {
              numberOfBytesRead = 0;
            }
          }
          while (numberOfBytesRead != 0); // loop until connection is closed

        }// if (s_serialPort != null)
      }
      catch (Exception ex)
      {
        Console.WriteLine("Connect To Device Error:" + Environment.NewLine + ex.ToString());
      }
    }


    /*
     *    Message Structure:
     *        First two bytes = 0xFF (Message marker)
     *        Next four bytes is the JSON payload size
     *        Next four bytes is the binary payload size
     *        Followed by JSON payload then binary payload
     * 
     *        Minimum message size 10 bytes
     * 
     * */

    public void OnDataReceived(int numberOfBytesRead)
    {
      m_dataCount++;
      Console.WriteLine(String.Format("OnDataReceived: {0} bytes recieved", numberOfBytesRead));
      Boolean debugLogging = false;
      if (Directory.Exists(ApplicationSettings.Instance.LocalDataPath + @"\debug\raw"))
      {
        debugLogging = true;
      }


      try
      {
        lock (m_threadLock)
        {
          try
          {
            if (debugLogging)
            {
              FileStream stream = new FileStream(ApplicationSettings.Instance.LocalDataPath + @"\debug\raw\data.bin", FileMode.OpenOrCreate);
              BinaryWriter w = new BinaryWriter(stream);
              w.Seek((int)stream.Length, SeekOrigin.Begin);
              w.Write(m_dataBuffer, 0, numberOfBytesRead);
              w.Close();

              FileStream stream2 = new FileStream(ApplicationSettings.Instance.LocalDataPath + @"\debug\raw\packet" + m_dataCount + ".bin", FileMode.OpenOrCreate);
              BinaryWriter w2 = new BinaryWriter(stream2);
              w2.Seek((int)stream2.Length, SeekOrigin.Begin);
              w2.Write(m_dataBuffer, 0, numberOfBytesRead);
              w2.Close();
            }
          }
          catch (Exception e)
          {
            Console.WriteLine(e.ToString());
          }


          int bufferIndex = 0;

          // loop until m_dataBuffer is empty
          do
          {
            if (m_msgCount == 200)
            {
              Console.WriteLine("break");
            }


            if (m_state == State.eHeader)
            {
              // Read Message Header
              if (m_msgIndex < MessageHeaderSize)
              {
                if (m_msgIndex == 0)
                {
                  m_headerBuffer = new byte[MessageHeaderSize];
                }
                int copyLen = Math.Min(numberOfBytesRead - bufferIndex, MessageHeaderSize - m_msgIndex);
                Buffer.BlockCopy(m_dataBuffer, bufferIndex, m_headerBuffer, m_msgIndex, copyLen);

                try
                {
                  if (debugLogging)
                  {
                    FileStream stream2 = new FileStream(ApplicationSettings.Instance.LocalDataPath + @"\debug\raw\msg" + m_msgCount + ".bin", FileMode.OpenOrCreate);
                    BinaryWriter w2 = new BinaryWriter(stream2);
                    w2.Seek((int)stream2.Length, SeekOrigin.Begin);
                    w2.Write(m_headerBuffer, 0, copyLen);
                    w2.Close();
                  }
                }
                catch (Exception e)
                {
                  Console.WriteLine(e.ToString());
                }


                m_msgIndex += copyLen;
                bufferIndex += copyLen;
              }

              // Have complete header, now decode the header and allocate arrays for JSON/Binary payloads
              if (m_msgIndex == MessageHeaderSize)
              {
                if (DecodeHeader(m_headerBuffer))
                {
                  m_textMsgBuffer = new byte[m_textMessageSize];
                  m_binaryMsgBuffer = new byte[m_binaryMessageSize];

                  m_textBytesReceived = 0;
                  m_binaryBytesReceived = 0;

                  m_state = State.eTextMsg;

                  Console.WriteLine(String.Format("m_textMessageSize: {0} bytes", m_textMessageSize));
                  Console.WriteLine(String.Format("m_binaryMessageSize: {0} bytes", m_binaryMessageSize));
                }
                else
                {
                  // bad header, flush buffer to next header marker
                  m_state = State.eFlushBuffer;
                }
              }
            }

            if (m_state == State.eFlushBuffer)
            {
              // flush buffer till we found the first header marker
              while (bufferIndex < numberOfBytesRead && m_state != State.eMarker1)
              {
                if (m_dataBuffer[bufferIndex] == 0xFF)
                {
                  m_state = State.eMarker1;
                }
                bufferIndex++;
              }
            }

            if ( m_state == State.eMarker1 )
            {
              if (bufferIndex < numberOfBytesRead)
              {
                if (m_dataBuffer[bufferIndex] == 0xFF)
                {
                  bufferIndex++;
                  m_msgIndex = 2;
                  m_headerBuffer = new byte[MessageHeaderSize];
                  m_headerBuffer[0] = 0xFF;
                  m_headerBuffer[1] = 0xFF;

                  m_textMsgBuffer = null;
                  m_binaryMsgBuffer = null;
                  m_textBytesReceived = 0;
                  m_binaryBytesReceived = 0;
                  m_textMessageSize = 0;
                  m_binaryMessageSize = 0;
                  m_state = State.eHeader;
                  
                }
                else
                {
                  m_state = State.eFlushBuffer;
                }
              }
              // now look for second header marker
            }

            if (m_state == State.eTextMsg)
            {
              if (m_textMessageSize > 0)
              {
                int copyLen = Math.Min(numberOfBytesRead - bufferIndex, m_textMessageSize - m_textBytesReceived);
                Buffer.BlockCopy(m_dataBuffer, bufferIndex, m_textMsgBuffer, m_textBytesReceived, copyLen);

                try
                {
                  if (debugLogging)
                  {
                    FileStream stream2 = new FileStream(ApplicationSettings.Instance.LocalDataPath + @"\debug\raw\msg" + m_msgCount + ".bin", FileMode.OpenOrCreate);
                    BinaryWriter w2 = new BinaryWriter(stream2);
                    w2.Seek(MessageHeaderSize, SeekOrigin.Begin);
                    w2.Write(m_textMsgBuffer, 0, copyLen + m_textBytesReceived);
                    w2.Close();
                  }
                }
                catch (Exception e)
                {
                  Console.WriteLine(e.ToString());
                }


                m_textBytesReceived += copyLen;
                bufferIndex += copyLen;
                Console.WriteLine(String.Format("TextMsg BlockCopy: {0}, {1}, {2}", bufferIndex, m_textBytesReceived, copyLen));
              }
              if (m_textBytesReceived == m_textMessageSize)
              {
                m_state = State.eBinaryMsg;
              }
            }

            if (m_state == State.eBinaryMsg)
            {
              if (m_binaryMessageSize > 0)
              {
                int copyLen = Math.Min(numberOfBytesRead - bufferIndex, m_binaryMessageSize - m_binaryBytesReceived);
                Buffer.BlockCopy(m_dataBuffer, bufferIndex, m_binaryMsgBuffer, m_binaryBytesReceived, copyLen);

                try
                {
                  if (debugLogging)
                  {
                    FileStream stream2 = new FileStream(ApplicationSettings.Instance.LocalDataPath + @"\debug\raw\msg" + m_msgCount + ".bin", FileMode.OpenOrCreate);
                    BinaryWriter w2 = new BinaryWriter(stream2);
                    w2.Seek(MessageHeaderSize + m_textBytesReceived, SeekOrigin.Begin);
                    w2.Write(m_binaryMsgBuffer, 0, copyLen + m_binaryBytesReceived);
                    w2.Close();
                  }
                }
                catch (Exception e)
                {
                  Console.WriteLine(e.ToString());
                }

                m_binaryBytesReceived += copyLen;
                bufferIndex += copyLen;
                Console.WriteLine(String.Format("BinaryMsg BlockCopy: {0}, {1}, {2}", bufferIndex, m_binaryBytesReceived, copyLen));
              }
              if (m_binaryBytesReceived == m_binaryMessageSize)
              {
                m_state = State.ePostMsg;
              }
            }

            if (m_state == State.ePostMsg)
            {
              try
              {
                if (MsgArrivedEvent != null)
                {
                  string data = Encoding.UTF8.GetString(m_textMsgBuffer, 0, m_textMsgBuffer.Length);
                  AlertBase msg = AlertBase.CreateAlert( data );
                  if (m_binaryMessageSize > 0)
                  {
                    string fileName = ApplicationSettings.Instance.LocalDataPath + "\\" + Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".dat";
                    FileStream fs = File.OpenWrite(fileName);
                    fs.Write(m_binaryMsgBuffer, 0, m_binaryMsgBuffer.Length);
                    fs.Close();
                    msg.ImageFile = fileName;
                  }

                  MsgArrivedEvent(msg);
                  Console.WriteLine("MsgArrivedEvent fired");
                }

                
              }
              catch (Exception ex)
              {
                Console.WriteLine("OnDataReceived MsgArrivedEvent:" + Environment.NewLine + ex.ToString());
              }

              m_msgIndex = 0;
              m_headerBuffer = null;
              m_textMsgBuffer = null;
              m_binaryMsgBuffer = null;
              m_textBytesReceived = 0;
              m_binaryBytesReceived = 0;
              m_textMessageSize = 0;
              m_binaryMessageSize = 0;
              m_state = State.eHeader;
              m_msgCount++;
            }

          } while (bufferIndex < numberOfBytesRead);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("OnDataReceived Error:" + Environment.NewLine + ex.ToString());
      }
    }

    Boolean DecodeHeader(byte[] packet)
    {
      Boolean retVal = true;
      m_textMessageSize = 0;
      m_binaryMessageSize = 0;

      if (packet[0] == 0xFF && packet[1] == 0xFF)
      {
        // Get Text payload size
        byte[] msgSize = new byte[4];
        msgSize[0] = packet[2];
        msgSize[1] = packet[3];
        msgSize[2] = packet[4];
        msgSize[3] = packet[5];
        m_textMessageSize = BitConverter.ToInt32(msgSize, 0);

        // Get Binary payload size
        msgSize[0] = packet[6];
        msgSize[1] = packet[7];
        msgSize[2] = packet[8];
        msgSize[3] = packet[9];
        m_binaryMessageSize = BitConverter.ToInt32(msgSize, 0);
      }
      else
      {
        retVal = false;
        Console.WriteLine("DecodeHeader Error");
      }
      /*
      GCHandle pinnedPacket = GCHandle.Alloc(packet, GCHandleType.Pinned);
      MessageHeader header = (MessageHeader)Marshal.PtrToStructure(pinnedPacket.AddrOfPinnedObject(), typeof(MessageHeader));        
      pinnedPacket.Free();
       * */
      return retVal;
    }


    void readFile(string fileName)
    {
      try
      {
        FileStream stream = new FileStream(fileName, FileMode.Open);
        BinaryReader r = new BinaryReader(stream);

        long len = stream.Length;
        long totalRead = 0;
        while (totalRead < len)
        {
          int numberOfBytesRead = (int)(len - totalRead);
          if (numberOfBytesRead > m_dataBuffer.Length)
          {
            numberOfBytesRead = m_dataBuffer.Length;
          }
          m_dataBuffer = r.ReadBytes(numberOfBytesRead);
          OnDataReceived(numberOfBytesRead);
          totalRead += numberOfBytesRead;
        }

        stream.Close();
      }
      catch (Exception )
      {
      }
    }
  }
}
