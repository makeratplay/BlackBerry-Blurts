<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:param name="systemPath" select="''"/>
  <xsl:param name="dataPath" select="''"/>
  <xsl:param name="themePath" select="''"/>
  <xsl:param name="imageFile" select="''"/>
  <xsl:param name="bgColor" select="'#718AF4'"/>
  <xsl:param name="bgColorStart" select="'#E1E7FD'"/>
  <xsl:param name="displayIcon" select="'true'"/>

  <xsl:template match="/">
    <html>
      <head>
        <style type="text/css">
          body
          {
            font-family:Arial;
            font-size: 10pt;
            color: black;
            border: 1px solid #000080;
            filter:progid:DXImageTransform.Microsoft.Gradient(endColorstr='<xsl:value-of select="$bgColor" />', startColorstr='<xsl:value-of select="$bgColorStart" />', gradientType='0')
          }
          .Truncate
          {
            overflow: hidden;
            white-space: nowrap;
            text-overflow: ellipsis;
          }
          
          .statusText
          {
            color: #003DB2; 
            font-size: 8pt;
          }
          
          .actionText
          {
            cursor:hand; 
            text-decoration: underline; 
            color:blue; 
            font-weight:bold; 
            font-size: 9pt;
          }
          
          .SenderName
          {
            color: Black; 
            font-size: 12pt; 
            font-weight:bold;
          }
          
          .SMSText
          {
            color: Black; 
            font-size: 10pt; 
            font-weight:normal; 
            width: 260px;
          }
          
          .EmailSubject
          {
            color: Black; 
            font-size: 9pt; 
            width: 260px; 
          }
          
          .EmailText
          {
            color: #003DB2; 
            font-size: 9pt; 
            width: 260px; 
          }
        </style>

        <script>
          document.onreadystatechange = pageLoad;

          function pageLoad()
          {
            if ( document.readyState == "complete" )
            {
              try
              {
                window.external.setClientSize( content.clientWidth + 4, content.clientHeight + 4 );
              }
              catch( e )
              {
              }
            }
          }

          function showMore( oCtrl, heightVal )
          {
            oCtrl.className='';
            oCtrl.style.width='230px'; 
            oCtrl.style.height= heightVal +'px';
            oCtrl.style.overflow='auto';
            
            window.event.cancelBubble = true;   
            try
            {
              window.external.setClientSize( content.clientWidth, content.clientHeight );       
            }
            catch( e )
            {
            }
            
          }
        </script>
      </head>

      <body bgcolor="#FFFFFF" cellpadding="0" cellspacing="0"   topmargin='0' leftmargin='0' bottommargin='0' rightmargin='0' 
        onmouseover="window.external.OnMouseOver();" onmouseout="window.external.OnMouseLeave();"  >
        <div id="content" style="width: 100%; height: 68px;">
          <div class="topbar"></div>
          <table width='100%' border='0'>
            <tr>
              <xsl:if test='not(BlurtsData/Screen)'>
                <td width='1%' valign='middle'>
                  <xsl:choose>
                    <xsl:when test="//@imageFile">
                      <img src="{$dataPath}{//@imageFile}" height="50px"/>
                    </xsl:when>
                    <xsl:when test="//ImageFile != ''">
                      <img src="{$dataPath}{//ImageFile}" height="50px"/>
                    </xsl:when>
                    <xsl:otherwise>
                      <xsl:if test="$displayIcon != 'false'">
                        <xsl:choose>
                          <xsl:when test="count(BlurtsData/Disconnect) = 1">
                            <img src='{$systemPath}\Blurts_red_35x35.png'/>
                          </xsl:when>
                          <xsl:otherwise>
                            <img src='{$systemPath}\Blurts_35x35.png'/>
                          </xsl:otherwise>
                        </xsl:choose>
                        
                      </xsl:if>
                    </xsl:otherwise>
                  </xsl:choose>             
              </td>         
              </xsl:if>
              <td width='99%' valign='top'>
                <xsl:apply-templates select='BlurtsData/SMS' />
                <xsl:apply-templates select='BlurtsData/Email' />
                <xsl:apply-templates select='BlurtsData/Status' />
                <xsl:apply-templates select='BlurtsData/Connect' />
                <xsl:apply-templates select='BlurtsData/Disconnect' />
                <xsl:apply-templates select='BlurtsData/Call' />
                <xsl:apply-templates select='BlurtsData/Screen' />
                <xsl:apply-templates select='BlurtsData/Contact' />
                <xsl:apply-templates select='BlurtsData/Clipboard' />
                <xsl:apply-templates select='BlurtsData/Macro' />
                <xsl:apply-templates select='BlurtsData/Level' />
                <xsl:apply-templates select='BlurtsData/PINMsg' />
                <xsl:apply-templates select='BlurtsData/Lock' />
              </td>
              <td width='99%' align="right" valign="top" style="cursor:pointer;"  onclick="window.external.OnCloseBtn();">
                <img src="{$systemPath}\close.png" altImg="{$systemPath}\close_sel.png" normalImg="{$systemPath}\close.png" onmouseover="this.src=this.altImg;" onmouseout="this.src=this.normalImg;" />
              </td>              
            </tr>
          </table>
        </div>
      </body>
    </html>
  </xsl:template>

  <xsl:template match="Email">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <!--       <tr>
        <td>
          <span class="statusText" >
            Email:
          </span>
        </td>
        <td>
        </td>
        <td>
        </td>
      </tr>-->
      <tr>
        <td colspan="3">
          <span class="SenderName">
            <xsl:choose>
              <xsl:when test="SenderName">
                <xsl:value-of select="SenderName"/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select="SenderAddress"/>
              </xsl:otherwise>
            </xsl:choose>
          </span>
        </td>
      </tr>
      <xsl:if test="Subject">      
        <tr>
          <td colspan="3">
            <div id="subjectText"  class="Truncate EmailSubject" onclick="showMore(this, 40);">
              <xsl:value-of select="Subject"/>
            </div>
          </td>
        </tr>
      </xsl:if>      
      <tr>
        <td colspan="3">
          <div id="bodyText"  class="Truncate EmailText" onclick="showMore(this, 80);">
            <xsl:value-of select="BodyText"/>
          </div>
        </td>
      </tr>      
    </table>      
  </xsl:template>

  <xsl:template match="PINMsg">
    <table>
      <tr>
        <td valign="top">
          <div id="senderText" style="color: black; font-size: 12pt; font-weight: bold;">
            <xsl:choose>
              <xsl:when test="SenderName">
                <xsl:value-of select="SenderName"/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select="SenderAddress"/>
              </xsl:otherwise>
            </xsl:choose>
          </div>
          <xsl:if test="Subject">
            <div id="subjectText"  class="Truncate" style="color: Black; font-size: 10pt; width: 250px; height: 14px; cursor:hand;" onclick="showMore(this, 40);">
              <xsl:value-of select="Subject"/>
            </div>
          </xsl:if>
          <div id="bodyText"  class="Truncate" style="color: Black; font-size: 10pt; width: 250px; height: 14px; cursor:hand;" onclick="showMore(this, 80);">
            <xsl:value-of select="BodyText"/>
          </div>
        </td>
      </tr>
    </table>    
  </xsl:template>  
  
  <xsl:template match="SMS">
    <xsl:variable name="SenderAddress">
      <xsl:call-template name="string-replace-all">
        <xsl:with-param name="text" select="SenderAddress"/>
        <xsl:with-param name="replace">
          <xsl:text>'</xsl:text>
        </xsl:with-param>
        <xsl:with-param name="by">
          <xsl:text>\'</xsl:text>
        </xsl:with-param>
      </xsl:call-template>
    </xsl:variable> 
    
    <table width="100%" border="0" cellspacing="0">
      <tr>
        <td>
          <span class="statusText" >
            SMS:
          </span>
        </td>
        <td>
        </td>
        <td>
          <span class="actionText" onclick="window.external.OnSendSMS( '{$SenderAddress}', '' ); window.event.cancelBubble = true;"  onselectstart="window.event.returnValue=false;">
            Reply
          </span>             
        </td>
      </tr>
      <tr>
        <td colspan="3">
          <span class="SenderName">
            <xsl:choose>
              <xsl:when test="SenderName">
                <xsl:value-of select="SenderName"/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select="SenderAddress"/>
              </xsl:otherwise>
            </xsl:choose>
          </span>
        </td>
      </tr>
      <tr>
        <td colspan="3">
          <div class="SMSText">
             <xsl:value-of select="BodyText"/>
          </div>
        </td>
      </tr>
    </table>     
  </xsl:template>  

  <xsl:template match="Clipboard">
    <table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td width="95%">
          <span class="statusText" >
            Text placed on clipboard:
          </span>
        </td>
      </tr>
    </table>
    <span style="color: Black; font-size: 10pt; font-weight: bold;">
      <xsl:value-of select="Text"/>
    </span>
  </xsl:template>  
  
  <xsl:template match="Status">
    <table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td width="95%">
          <span class="statusText" style="float: left;">
            Status Message:<br/>
            Version: <xsl:value-of select="../@version"/>
          </span>
          <span class="statusText" style="float: right;">
            <xsl:text>Sig: </xsl:text><xsl:value-of select="Signal"/><xsl:text> dBm </xsl:text>
            <br/>
            <xsl:text>Bat: </xsl:text><xsl:value-of select="Battery"/><xsl:text>% </xsl:text>
          </span>
        </td>
      </tr>
      <tr>
        <td>
          <span style="color: Black; font-size: 10pt; font-weight: bold;">
            <xsl:value-of select="Text"/>
          </span>          
        </td>
      </tr>
    </table>
  </xsl:template>
  
  <xsl:template match="Lock">
    <table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td width="95%">
          <span class="statusText" style="float: left;">
            Lock Workstation:<br/>
            Version: <xsl:value-of select="../@version"/>
          </span>
          <span class="statusText" style="float: right;">
            <xsl:text>Sig: </xsl:text><xsl:value-of select="Signal"/><xsl:text> dBm </xsl:text>
            <br/>
            <xsl:text>Bat: </xsl:text><xsl:value-of select="Battery"/><xsl:text>% </xsl:text>
          </span>
        </td>
      </tr>
      <tr>
        <td>
          <span style="color: Black; font-size: 10pt; font-weight: bold;">
            Locking workstation
          </span>          
        </td>
      </tr>
    </table>
  </xsl:template>  
  
  <xsl:template match="Level">
    <table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td width="95%">
          <span class="statusText" style="float: left;">
            Level Message:
          </span>
        </td>
      </tr>
    </table>
    <span class="statusText" >
      BlackBerry Blurts Version: <xsl:value-of select="../@version"/>
    </span>

    <br/>
    <span style="color: Black; font-size: 10pt; font-weight: bold;">
      <xsl:text>Battery: </xsl:text><xsl:value-of select="Battery"/><xsl:text>%</xsl:text><br/>
      <xsl:text>Signal: </xsl:text><xsl:value-of select="Signal"/><xsl:text> dBm</xsl:text><br/>
    </span>
  </xsl:template>  
  
  <xsl:template match="Macro">
    <table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td width="95%">
          <span class="statusText" >
            Running Macro:
          </span>
        </td>
      </tr>
    </table>
    <br/>
    <span style="color: Black; font-size: 10pt; font-weight: bold;">
      <xsl:value-of select="MacroName"/> - <xsl:value-of select="Text"/>
    </span>
  </xsl:template>  
  
 
  <xsl:template match="Disconnect">
    <table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td width="95%">
          <span class="statusText" >
            BlackBerry Disconnect:
          </span>
        </td>
      </tr>
    </table>

    <br/>
    <span style="color: Black; font-size: 10pt; font-weight: bold;">
      <xsl:value-of select="Text" disable-output-escaping="yes"/>
    </span>
  </xsl:template>    
  
  <xsl:template match="Connect">
    <table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td width="95%">
          <span class="statusText" >
            BlackBerry Connected:
          </span>
        </td>
      </tr>
    </table>
    <span class="statusText" >
      BlackBerry Blurts Version: <xsl:value-of select="../@version"/>
    </span>

    <br/>
    <span style="color: Black; font-size: 10pt; font-weight: bold;">
      <xsl:value-of select="Text" disable-output-escaping="yes"/>
    </span>
  </xsl:template>   

  <xsl:template match="Screen">
    <table width="100%" border="0" cellspacing="0">
      <tr>
        <td>
          <span class="statusText" >
            Screen Capture:
          </span>
        </td>
        <td>
        </td>
        <td>
          <span class="actionText" onclick="window.external.OnSaveImage(); window.event.cancelBubble = true;" onselectstart="window.event.returnValue=false;" >Save</span>
        </td>
      </tr>
      <tr>
        <td colspan="3">
          <img src="{$imageFile}"  style="border: 2px solid black;"/>
        </td>
      </tr>
    </table>      
  </xsl:template>  

  <xsl:template match="Call">
    <xsl:choose>
      <!--  Incoming Call -->
      <xsl:when test="Action = 'Incoming' or not(Action)"> 
        <table width="100%" border="0" cellspacing="0">
          <tr>
            <td>
              <span class="statusText" >
                Incoming Call:
              </span>
            </td>
            <td>
            <span class="actionText" onclick="window.external.BlackBerry.PressSendKey(); window.event.cancelBubble = true;" onselectstart="window.event.returnValue=false;" >Answer</span>
            </td>
            <td>
             <span class="actionText" onclick="window.external.BlackBerry.PressEndKey(); window.event.cancelBubble = true;" onselectstart="window.event.returnValue=false;" >Ignore</span>
            </td>
          </tr>
          <tr>
            <td colspan="3">
              <span style="color: Black; font-size: 12pt; font-weight:bold;">
                <xsl:value-of select="CallerName"/>
                <xsl:if test="not(CallerName)">
                  <xsl:value-of select="PhoneNumber"/>                    
                </xsl:if>
              </span>
            </td>
          </tr>
          <tr>
            <td colspan="3">
              <span style="color: Black; font-size: 10pt; font-weight:normal;">
                <xsl:if test="CallerName">
                  <xsl:value-of select="PhoneNumber"/>                    
                </xsl:if>
              </span>
            </td>
          </tr>
        </table>
      </xsl:when>

      <!--  Call Answered -->
      <xsl:when test="Action = 'Answered'">
        <table width="100%" border="0" cellspacing="0">
          <tr>
            <td>
              <span class="statusText" >
                Call Answered:
              </span>
            </td>
            <td>
            </td>
            <td>
              <span class="actionText" onclick="window.external.BlackBerry.PressEndKey(); window.event.cancelBubble = true;" onselectstart="window.event.returnValue=false;" >Disconnect</span>
            </td>
          </tr>
          <tr>
            <td colspan="3">
              <span style="color: Black; font-size: 12pt; font-weight:bold;">
                <xsl:value-of select="CallerName"/>
                <xsl:if test="not(CallerName)">
                  <xsl:value-of select="PhoneNumber"/>                    
                </xsl:if>
              </span>
            </td>
          </tr>
          <tr>
            <td colspan="3">
              <span style="color: Black; font-size: 10pt; font-weight:normal;">
                <xsl:if test="CallerName">
                  <xsl:value-of select="PhoneNumber"/>                    
                </xsl:if>
              </span>
            </td>
          </tr>
        </table>        
      </xsl:when>

      <!--  Call Disconnected -->
      <xsl:when test="Action = 'Disconnected'">
        <table width="100%" border="0" cellspacing="0">
          <tr>
            <td>
              <span class="statusText" >
                Call Status:
              </span>
            </td>
            <td>
            </td>
            <td>
            </td>
          </tr>
          <tr>
            <td colspan="3">
              <span style="color: Black; font-size: 12pt; font-weight:bold;">
                Call Disconnected
              </span>
            </td>
          </tr>
        </table>           
      </xsl:when>

      <!--  Call Initiated -->
      <xsl:when test="Action = 'Initiated'">
        <table width="100%" border="0" cellspacing="0">
          <tr>
            <td>
              <span class="statusText" >
                Call Initiated:
              </span>
            </td>
            <td>
            </td>
            <td>
              <span class="actionText" onclick="window.external.BlackBerry.PressEndKey(); window.event.cancelBubble = true;" onselectstart="window.event.returnValue=false;" >Disconnect</span>
            </td>
          </tr>
          <tr>
            <td colspan="3">
              <span style="color: Black; font-size: 12pt; font-weight:bold;">
                <xsl:value-of select="CallerName"/>
                <xsl:if test="not(CallerName)">
                  <xsl:value-of select="PhoneNumber"/>                    
                </xsl:if>
              </span>
            </td>
          </tr>
          <tr>
            <td colspan="3">
              <span style="color: Black; font-size: 10pt; font-weight:normal;">
                <xsl:if test="CallerName">
                  <xsl:value-of select="PhoneNumber"/>                    
                </xsl:if>
              </span>
            </td>
          </tr>
        </table>         
      </xsl:when>
 
      <!--  Call Connected -->
      <xsl:when test="Action = 'Connected'">
        <table width="100%" border="0" cellspacing="0">
          <tr>
            <td>
              <span class="statusText" >
                Call Connected:
              </span>
            </td>
            <td>
            </td>
            <td>
              <span class="actionText" onclick="window.external.BlackBerry.PressEndKey(); window.event.cancelBubble = true;" onselectstart="window.event.returnValue=false;" >Disconnect</span>
            </td>
          </tr>
          <tr>
            <td colspan="3">
              <span style="color: Black; font-size: 12pt; font-weight:bold;">
                <xsl:value-of select="CallerName"/>
                <xsl:if test="not(CallerName)">
                  <xsl:value-of select="PhoneNumber"/>                    
                </xsl:if>
              </span>
            </td>
          </tr>
          <tr>
            <td colspan="3">
              <span style="color: Black; font-size: 10pt; font-weight:normal;">
                <xsl:if test="CallerName">
                  <xsl:value-of select="PhoneNumber"/>                    
                </xsl:if>
              </span>
            </td>
          </tr>
        </table>          
      </xsl:when>

      <!--  Call Waiting -->
      <xsl:when test="Action = 'Waiting'">
        <table width="100%" border="0" cellspacing="0">
          <tr>
            <td>
              <span class="statusText" >
                Call Waiting:
              </span>
            </td>
            <td>
            </td>
            <td>
              <span class="actionText" onclick="window.external.BlackBerry.PressSendKey(); window.event.cancelBubble = true;" onselectstart="window.event.returnValue=false;" >Answer</span>
            </td>
          </tr>
          <tr>
            <td colspan="3">
              <span style="color: Black; font-size: 12pt; font-weight:bold;">
                <xsl:value-of select="CallerName"/>
                <xsl:if test="not(CallerName)">
                  <xsl:value-of select="PhoneNumber"/>                    
                </xsl:if>
              </span>
            </td>
          </tr>
          <tr>
            <td colspan="3">
              <span style="color: Black; font-size: 10pt; font-weight:normal;">
                <xsl:if test="CallerName">
                  <xsl:value-of select="PhoneNumber"/>                    
                </xsl:if>
              </span>
            </td>
          </tr>
        </table>           
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="Contact">
    <table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td width="95%">
          <span class="statusText" style="float: left;">
            Processing Contact:<br/>
            Version: <xsl:value-of select="../@version"/>
          </span>
          <span class="statusText" style="float: right;">
            <xsl:text>Sig: </xsl:text><xsl:value-of select="Signal"/><xsl:text> dBm </xsl:text>
            <br/>
            <xsl:text>Bat: </xsl:text><xsl:value-of select="Battery"/><xsl:text>% </xsl:text>
          </span>
        </td>
      </tr>
      <tr>
        <td>
          <span style="color: Black; font-size: 10pt; font-weight: bold;">
            <xsl:value-of select="DisplayName"/>
          </span>        
        </td>
      </tr>
    </table>    
  </xsl:template>  
  
  
 <xsl:template name="string-replace-all">
    <xsl:param name="text" />
    <xsl:param name="replace" />
    <xsl:param name="by" />
    <xsl:choose>
      <xsl:when test="contains($text, $replace)">
        <xsl:value-of select="substring-before($text,$replace)" />
        <xsl:value-of select="$by" />
        <xsl:call-template name="string-replace-all">
          <xsl:with-param name="text"
          select="substring-after($text,$replace)" />
          <xsl:with-param name="replace" select="$replace" />
          <xsl:with-param name="by" select="$by" />
        </xsl:call-template>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$text" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>    
</xsl:stylesheet> 
