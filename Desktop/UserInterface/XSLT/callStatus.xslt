<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:param name="systemPath" select="'F:\BlackBerry\Development\Developement\Blurts\Desktop\bin\Debug'"/>
  <xsl:param name="dataPath" select="''"/>
  <xsl:param name="imageFile" select="''"/>
  <xsl:param name="bgColor" select="'#718AF4'"/>
  <xsl:param name="bgColorStart" select="'#FFFFFF'"/>
  <xsl:param name="displayIcon" select="'true'"/>

  <xsl:template match="/">
    <html>
      <head>
        <style type="text/css">
          body
          {
          font-family:Tahoma;
          font-size: 10pt;
          color: white;
          }
        </style>

      </head>

      <body bgcolor="#00000" cellpadding="0" cellspacing="0"   topmargin='0' leftmargin='0' bottommargin='0' rightmargin='0'>
        <div id="content" style="position: relative; top: 0px; left: 0px; width: 270px; height: 60px;">
          <xsl:apply-templates select='BlurtsData/Call' />
        </div>
      </body>
    </html>
  </xsl:template>

  <xsl:template match="Call">

    <xsl:choose>
      <xsl:when test="Action = 'Incoming' or not(Action)">
        <!--  Incoming Call -->
        <table>
          <tr>
            <td  valign="top">
              <xsl:choose>
                <xsl:when test="PhoneNumber/@imageFile">
                  <img src="{$dataPath}{PhoneNumber/@imageFile}" height="50px"/>
                </xsl:when>
                <xsl:when test="ImageFile != ''">
                  <img src="{$dataPath}{ImageFile}" height="50px"/>
                </xsl:when>
                <xsl:otherwise>
                  <img src="{$systemPath}/you.gif" height="50px"/>
                </xsl:otherwise>
              </xsl:choose>
            </td>
            <td  valign="top">
              <div style="color: white; font-size: 8pt;">
                Incoming Call:
              </div>               
              <div style="color: white; font-size: 12pt; font-weight:bold;">
                <xsl:value-of select="CallerName"/>
                <xsl:if test="CallerName">
                  <br/>
                </xsl:if>
                <xsl:value-of select="PhoneNumber"/>
              </div>
            </td>
          </tr>
        </table>

      </xsl:when>

      <xsl:when test="Action = 'Answered'">
        <!--  Call Answered -->
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
          <tr>
            <td  valign="top">
              <xsl:choose>
                <xsl:when test="PhoneNumber/@imageFile">
                  <img src="{$dataPath}{PhoneNumber/@imageFile}" height="50px"/>
                </xsl:when>
                <xsl:when test="ImageFile != ''">
                  <img src="{$dataPath}{ImageFile}" height="50px"/>
                </xsl:when>
                <xsl:otherwise>
                  <img src="{$systemPath}/you.gif" height="50px"/>
                </xsl:otherwise>
              </xsl:choose>
            </td>            
            <td>
              <div style="color: white; font-size: 8pt;">
                Call Answered:
              </div>                
              <div style="color: white; font-size: 12pt; font-weight:bold;">
                <xsl:value-of select="CallerName"/>
                <xsl:if test="CallerName">
                  <br/>
                </xsl:if>
                <xsl:value-of select="PhoneNumber"/>
              </div>
            </td>
          </tr>
        </table>

      </xsl:when>


      <xsl:when test="Action = 'Disconnected'">
        <!--  Call Disconnected -->
        <div style="color: white; font-size: 8pt;">
          Call Status:
        </div>        
        <center>
          <span style="color: white; font-size: 12pt; font-weight:bold;">Call Disconnected</span>
          <br/>
          <span style="color: white; font-size: 12pt; font-weight:bold;">
            <xsl:value-of select="CallerName"/>
            <xsl:if test="CallerName">
              <br/>
            </xsl:if>
            <xsl:value-of select="PhoneNumber"/>
          </span>
        </center>
      </xsl:when>

      <xsl:when test="Action = 'Initiated'">
        <!--  Call Initiated -->
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
          <tr>
            <td  valign="top">
              <xsl:choose>
                <xsl:when test="PhoneNumber/@imageFile">
                  <img src="{$dataPath}{PhoneNumber/@imageFile}" height="50px"/>
                </xsl:when>
                <xsl:when test="ImageFile != ''">
                  <img src="{$dataPath}{ImageFile}" height="50px"/>
                </xsl:when>
                <xsl:otherwise>
                  <img src="{$systemPath}/you.gif" height="50px"/>
                </xsl:otherwise>
              </xsl:choose>
            </td>            
            <td>
              <div style="color: white; font-size: 8pt;">
                Call Initiated:
              </div>               
              <div style="color: white; font-size: 12pt; font-weight:bold;">
                <xsl:value-of select="CallerName"/>
                <xsl:if test="CallerName">
                  <br/>
                </xsl:if>
                <xsl:value-of select="PhoneNumber"/>
              </div>
            </td>
          </tr>
        </table>
      </xsl:when>

      <xsl:when test="Action = 'Connected'">
        <!--  Call Connected -->
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
          <tr>
            <td  valign="top">
              <xsl:choose>
                <xsl:when test="PhoneNumber/@imageFile">
                  <img src="{$dataPath}{PhoneNumber/@imageFile}" height="50px"/>
                </xsl:when>
                <xsl:when test="ImageFile != ''">
                  <img src="{$dataPath}{ImageFile}" height="50px"/>
                </xsl:when>
                <xsl:otherwise>
                  <img src="{$systemPath}/you.gif" height="50px"/>
                </xsl:otherwise>
              </xsl:choose>
            </td>            
            <td>
              <div style="color: white; font-size: 8pt;">
                Call Connected:
              </div>               
              <div style="color: white; font-size: 12pt; font-weight:bold;">
                <xsl:value-of select="CallerName"/>
                <xsl:if test="CallerName">
                  <br/>
                </xsl:if>
                <xsl:value-of select="PhoneNumber"/>
              </div>
            </td>
          </tr>
        </table>

      </xsl:when>

      <xsl:when test="Action = 'Waiting'">
        <!--  Call Waiting -->
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
          <tr>
            <td  valign="top">
              <xsl:choose>
                <xsl:when test="PhoneNumber/@imageFile">
                  <img src="{$dataPath}{PhoneNumber/@imageFile}" height="50px"/>
                </xsl:when>
                <xsl:when test="ImageFile != ''">
                  <img src="{$dataPath}{ImageFile}" height="50px"/>
                </xsl:when>
                <xsl:otherwise>
                  <img src="{$systemPath}/you.gif" height="50px"/>
                </xsl:otherwise>
              </xsl:choose>
            </td>            
            <td>
              <div style="color: white; font-size: 8pt;">
                Call Waiting:
              </div>                
              <div style="color: white; font-size: 12pt; font-weight:bold;">
                <xsl:value-of select="CallerName"/>
                <xsl:if test="CallerName">
                  <br/>
                </xsl:if>
                <xsl:value-of select="PhoneNumber"/>
              </div>
            </td>
          </tr>
        </table>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
</xsl:stylesheet >

