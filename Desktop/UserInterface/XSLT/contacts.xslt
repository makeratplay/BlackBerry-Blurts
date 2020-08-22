<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:param name="systemPath" select="''"/>
  <xsl:param name="dataPath" select="''"/>
  <xsl:param name="imageFile" select="''"/>
  <xsl:param name="contactFilter" select="''"/>
  <xsl:param name="editMode" select="'False'"/>
  <xsl:param name="scrollTop" select="0"/>
  

  <xsl:variable name="up" select="'ABCDEFGHIJKLMNOPQRSTUVWXYZ'"/>
  <xsl:variable name="lo" select="'abcdefghijklmnopqrstuvwxyz'"/>  

  <xsl:template match="/">
    <html>
      <head>
        <style type="text/css">
          body
          {
            font-family:Tahoma;
            font-size: 10pt;
            color: black;
            overflow: auto;
          }
          .Truncate
          {
            overflow: hidden;
            white-space: nowrap;
            text-overflow: ellipsis;
          }
        </style>
 
        <script type="text/javascript">
          document.onreadystatechange = pageLoad;

          function pageLoad()
          {
            if ( document.readyState == "complete" )
            {
              try
              {
                
                var scrollTop = <xsl:value-of select="$scrollTop"/>;
                setScrollPos( scrollTop )
                window.setTimeout("setScrollPos(" + scrollTop + ")", 1);
              }
              catch( e )
              {
              }
            }
          }  
          
          function setScrollPos( scrollTop )
          {
            window.scrollTo( 0, scrollTop );
          }
          
          
        </script>        
      </head>

      <body bgcolor="#FFFFFF" cellpadding="0" cellspacing="0" topmargin='0' leftmargin='0' bottommargin='0' rightmargin='0' onselectstart="window.event.returnValue = false;" >
        <!--    <div style="height: 271px; overflow: auto; border-top: solid 2px black;"> -->
            <xsl:choose>
              <xsl:when test ="count(BlurtsData/Contacts/Contact[@search]) &gt; 0">
                <center>
                  <b>Your contacts are in version 1.x. <br/>Download contacts again to convert to version 2.x</b>
                </center>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select="count(BlurtsData/Contacts/Contact)"/>
                <xsl:text> contacts</xsl:text>
              </xsl:otherwise>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="count(SMSHistory/History/Hx) &gt; 0">
                <xsl:apply-templates select="SMSHistory/History/Hx">
                  <xsl:sort select="@timestamp" data-type="number" order="descending" />
                </xsl:apply-templates>
              </xsl:when>              
              <xsl:when test="$contactFilter = ''">
                <xsl:apply-templates select="BlurtsData/Contacts/Contact">  
                  <xsl:sort select='DisplayName'/>
                </xsl:apply-templates>
              </xsl:when>
              <xsl:otherwise>
                <xsl:apply-templates select="BlurtsData/Contacts/Contact[ starts-with(translate(FirstName,$up,$lo), $contactFilter) or 
                                                                 starts-with(translate(LastName,$up,$lo), $contactFilter) or 
                                                                 starts-with(translate(DisplayName,$up,$lo), $contactFilter) or
                                                                 starts-with(translate(Org,$up,$lo), $contactFilter) or
                                                                 starts-with(PhoneNumbers/PhoneNumber/Number/@raw, $contactFilter)]  ">
                  <xsl:sort select='DisplayName'/>
                </xsl:apply-templates>

                <xsl:if test='count(BlurtsData/Contacts/Contact[ starts-with(translate(FirstName,$up,$lo), $contactFilter) or 
                                                                 starts-with(translate(LastName,$up,$lo), $contactFilter) or 
                                                                 starts-with(translate(DisplayName,$up,$lo), $contactFilter) or
                                                                 starts-with(translate(Org,$up,$lo), $contactFilter) or
                                                                 starts-with(PhoneNumbers/PhoneNumber/Number/@raw, $contactFilter)] ) = 0'>
                  <center>*No matching contacts*</center></xsl:if>
                
              </xsl:otherwise>
            </xsl:choose>
        <!--  </div>-->
      </body>
    </html>
  </xsl:template>
  

  <xsl:template match="Contact">
    <xsl:if test="count(PhoneNumbers/PhoneNumber) &gt; 0">
      <div style="border-bottom: solid 2px black;">
        <div style="color: Black; font-size: 10pt; font-weight: bold; height: 15px;" >
          <div style="float: left; width: 30px; background: black;">
            <center>
              <xsl:choose>
                <xsl:when test="Photo/@imageFile">
                  <img src="{$dataPath}{Photo/@imageFile}"  height="30px"/>
                </xsl:when>
                <xsl:when test="ImageFile != ''">
                  <img src="{$dataPath}{ImageFile}"  height="30px"/>
                </xsl:when>
                <xsl:otherwise>
                  <img src="{$systemPath}/you.gif" height="30px"/>
                </xsl:otherwise>             
              </xsl:choose>            
            </center>
          </div>
          <xsl:value-of select="DisplayName"/>  
          
          <xsl:choose>
            <xsl:when test="@status = 'hidden'">
              <span style="cursor:pointer; color:red; " onclick="window.external.showContact({Uid});">
                [restore]
              </span>
            </xsl:when>
            <xsl:otherwise>
              <span style="cursor:pointer; color:red; " onclick="window.external.hideContact({Uid});">
                [hide]
              </span>
            </xsl:otherwise>
          </xsl:choose>          
          
          <div style="color: #777777; font-size: 10pt; " >
            <xsl:value-of select="Org"/>
          </div>        
        </div>
        <xsl:apply-templates select="PhoneNumbers/PhoneNumber"/>
      </div>
    </xsl:if>
  </xsl:template>  
  
  <xsl:template match="PhoneNumber">
    <div style="border-top: solid 1px #777777; cursor: hand; padding-left: 8px;">
      <img src="{$systemPath}/greenPhone.png" height="30px" style="float: left; "/>

      <xsl:choose>
        <xsl:when test="@favorite = 'true'">
          <img src="{$systemPath}\fav_sel.png" onclick="window.external.clearFavoriteContact('{../../Uid}','{Number/@raw}')"/>
          <xsl:text> </xsl:text>
        </xsl:when>
        <xsl:otherwise>
          <img src="{$systemPath}\fav.png" onclick="window.external.favoriteContact('{../../Uid}','{Number/@raw}')" altImg="{$systemPath}\fav_mo.png" normalImg="{$systemPath}\fav.png" onmouseover="this.src=this.altImg;" onmouseout="this.src=this.normalImg;"/>
          <xsl:text> </xsl:text>
        </xsl:otherwise>
      </xsl:choose>      
      
      <span style="color: Black; font-size: 10pt; font-weight: normal; " >
        <xsl:value-of select="Type"/>
      </span>
      <br/>
      <span style="color: #777777; font-size: 10pt; " >
        <xsl:value-of select="Number"/>
      </span>     
    </div>
  </xsl:template>
  
</xsl:stylesheet> 
