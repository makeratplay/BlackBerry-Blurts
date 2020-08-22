<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:param name="systemPath" select="''"/>
  <xsl:param name="dataPath" select="''"/>
  <xsl:param name="meImage" select="''"/>
  <xsl:param name="showImages" select="'true'"/>
  
  <xsl:param name="bgColor" select="'white'"/>
  <xsl:param name="fontColor" select="'black'"/>
  

  <xsl:template match="/">
    <html>
      <head>
        <style type="text/css">
          body
          {
            font-family:Tahoma;
            font-size: 10pt;
          }
          
          .senderText
          {
            color: <xsl:value-of select="$fontColor"/>; 
            font-size: 8pt; 
            font-weight:bold;
          }
          
          .msgText
          {
            color: <xsl:value-of select="$fontColor"/>;; 
            font-size: 10pt; 
            width: 100%;
          }
          
          .msgBorder
          {
            border-bottom: 1px solid <xsl:value-of select="$fontColor"/>;
          }
        </style>

        <script>
          document.onreadystatechange = pageLoad;

          function pageLoad()
          {
            if ( document.readyState == "complete" )
            {
               var el = document.getElementById('lastItem');
               el.scrollIntoView(true);
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

      <body scroll="yes" bgcolor="{$bgColor}" color="{$fontColor}" link="{$fontColor}"  cellpadding="0" cellspacing="0"   
        topmargin='4' leftmargin='0' bottommargin='0' rightmargin='0' >
          <table width='100%' border='0' cellpadding='1' cellspacing='0'>
            <xsl:apply-templates select='BlurtsData/Messages/SMS' />
          </table>
          <span id='lastItem'></span>
      </body>
    </html>
  </xsl:template>
  
  <xsl:template match="SMS">
    <xsl:apply-templates select="self::node()[Sender]" mode="me"/>
    <xsl:apply-templates select="self::node()[SenderAddress]" mode="notme"/>
  </xsl:template>
  

  <xsl:template match="SMS" mode="me">
      <tr>
        <td  align="right" width="1%" class="msgBorder">
          <xsl:if test="$showImages='true'">
            <xsl:choose>
              <xsl:when test="Sender">
                <img src="{$meImage}" height="50px"/>
              </xsl:when>
              <xsl:otherwise>
                <img src="{$systemPath}/you.gif" height="50px"/>
              </xsl:otherwise>
            </xsl:choose>
            <xsl:text disable-output-escaping="yes"> </xsl:text>
          </xsl:if>
        </td>
        
        <td  valign="top" class="msgBorder" style="padding-left: 5px;">
          <table width="100%">
            <tr>
              
              <td align="left">
                <div class="senderText"  >
                  <xsl:value-of select="Sender"/>
                </div>                
              </td>
              <td align="right">
                <div class="senderText" title="{@timestamp}">
                  <xsl:apply-templates select="@timestamp"/>
                </div>                  
              </td>
            </tr>
          </table>
          

          <div class="msgText">
            <xsl:value-of select="BodyText"/>
          </div>          
        </td>
      </tr>
  </xsl:template>
  
  <xsl:template match="SMS" mode="notme">
      <tr>
        <td  align="right" width="1%" class="msgBorder">
          <xsl:if test="$showImages='true'">
           <xsl:choose>
              <xsl:when test="Photo/@imageFile">
                <img src="{$dataPath}{Photo/@imageFile}"  height="50px"/>
              </xsl:when>
              <xsl:when test="ImageFile != ''">
                <img src="{$dataPath}{ImageFile}"  height="50px"/>
              </xsl:when>
            </xsl:choose>            
            <xsl:text disable-output-escaping="yes"> </xsl:text>
          </xsl:if>
        </td>
        
        <td  valign="top"  class="msgBorder" style="padding-left: 5px;">
          <table width="100%">
            <tr>
              <td align="left">
                <div class="senderText">
                  <xsl:choose>
                    <xsl:when test="SenderName">
                      <xsl:value-of select="SenderName"/>
                    </xsl:when>
                    <xsl:otherwise>
                      <xsl:value-of select="SenderAddress"/>
                    </xsl:otherwise>
                  </xsl:choose>
                </div>                
              </td>
              <td align="right">
                <div class="senderText" title="{@timestamp}">
                  <xsl:apply-templates select="@timestamp"/>
                </div>                  
              </td>
            </tr>
          </table>
          

          <div class="msgText">
            <xsl:value-of select="BodyText"/>
          </div>          
        </td>
      </tr>
  </xsl:template>  

  <xsl:template match="@timestamp">
    <!-- timestamp="12/10/2009 9:05:00 PM" -->
    <xsl:value-of select="substring-before(substring-after(.,' '),':')"/>
    <xsl:text>:</xsl:text>
    <xsl:value-of select="substring-before(substring-after(substring-after(.,' '),':'),':')"/>
    <xsl:text> </xsl:text>
    <xsl:value-of select="substring-after(substring-after(substring-after(.,' '),':'),' ')"/>
  </xsl:template>
  
  

  
</xsl:stylesheet> 
