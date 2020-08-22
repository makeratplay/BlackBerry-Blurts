<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:param name="systemPath" select="''"/>
  <xsl:param name="dataPath" select="''"/>
  <xsl:param name="imageFile" select="''"/>
  <xsl:param name="contactFilter" select="''"/>
  <xsl:param name="favoriteFilter" select="'False'"/>
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
          }
          .Truncate
          {
          overflow: hidden;
          white-space: nowrap;
          text-overflow: ellipsis;
          }
        </style>

        <script>
     
          
          function toggleContactDetails( oCtrl )
          {
           
            var oDetails = oCtrl.firstChild.nextSibling.nextSibling;
            if ( oDetails.style.display =='inline' )
            {
              oDetails.display ='none';
            }
            else
            {
              oDetails.display ='inline';
            }
          }
          
        </script>
      </head>

      <body bgcolor="#FFFFFF" cellpadding="0" cellspacing="0" topmargin='0' leftmargin='0' bottommargin='0' rightmargin='0' onselectstart="window.event.returnValue = false;" >

<!--
        <div style="border-bottom: solid 1px black; height: 40px;">
           <xsl:choose>
              <xsl:when test="$contactFilter = ''">
                <span style="color: #777777; font-size: 20pt; font-weight: normal;">type to search</span>
              </xsl:when>
              
              <xsl:otherwise>          
                <span style="color: Black; font-size: 24pt; font-weight: bold;"><xsl:value-of select="$contactFilter"/></span>
              </xsl:otherwise>
            </xsl:choose>
        </div>
        -->
        
          <div style="height: 252px; overflow: auto; border-top: solid 2px black;">
            <xsl:choose>
              
              <xsl:when test="count(HistoryRoot/History) &gt; 0">
                <xsl:apply-templates select="HistoryRoot/History/Hx">
                  <xsl:sort select="@timestamp" data-type="number" order="descending" />
                </xsl:apply-templates>
                <xsl:if test ="count(HistoryRoot/History/Hx) = 0">
                  <center>*No history*</center>
                </xsl:if>

              </xsl:when>
              
              <xsl:when test="$favoriteFilter = 'true'">
                <xsl:apply-templates select="BlurtsData/Contacts/Contact[ (not(@status) or @status != 'hidden') and PhoneNumbers/PhoneNumber/@favorite = 'true']"  mode="favorite">
                  <xsl:sort select='DisplayName'/>
                </xsl:apply-templates>
                <xsl:if test ="count(BlurtsData/Contacts/Contact[ (not(@status) or @status != 'hidden') and PhoneNumbers/PhoneNumber/@favorite = 'true']) = 0">
                  <center>*No favorite contacts*</center>
                </xsl:if>
                
              </xsl:when>
              <xsl:when test="$contactFilter = ''">
                <!-- 
                <center>
                 <span style="color: #777777; font-size: 10pt; border-bottom: solid 2px black;">Enter phone number or contact name above</span>
                </center>
                -->
                
                <xsl:apply-templates select="BlurtsData/Contacts/Contact[not(@status) or @status != 'hidden']">  
                  <xsl:sort select='DisplayName'/>
                </xsl:apply-templates>
                <xsl:if test ="count(BlurtsData/Contacts/Contact[not(@status) or @status != 'hidden']) = 0">
                  <center>*No contacts*</center>
                </xsl:if>      
                <xsl:if test ="count(BlurtsData/Contacts/Contact[@search]) &gt; 0">
                  <center>
                    <b>Your contacts are in version 1.x. <br/>Download contacts again to convert to version 2.x</b>
                  </center>
                </xsl:if>                 
              </xsl:when>
              
              <xsl:otherwise>
                <xsl:apply-templates select="BlurtsData/Contacts/Contact[ ( starts-with(translate(FirstName,$up,$lo), $contactFilter) or 
                                                                 starts-with(translate(LastName,$up,$lo), $contactFilter) or 
                                                                 starts-with(translate(DisplayName,$up,$lo), $contactFilter) or
                                                                 starts-with(translate(Org,$up,$lo), $contactFilter) or
                                                                 starts-with(PhoneNumbers/PhoneNumber/Number/@raw, $contactFilter) ) and (not(@status) or @status != 'hidden')]">
                  <xsl:sort select='DisplayName'/>
                </xsl:apply-templates>

                <xsl:if test='count(BlurtsData/Contacts/Contact[ ( starts-with(translate(FirstName,$up,$lo), $contactFilter) or 
                                                                 starts-with(translate(LastName,$up,$lo), $contactFilter) or 
                                                                 starts-with(translate(DisplayName,$up,$lo), $contactFilter) or
                                                                 starts-with(translate(Org,$up,$lo), $contactFilter) or
                                                                 starts-with(PhoneNumbers/PhoneNumber/Number/@raw, $contactFilter) )   ] ) = 0'>
                  <center>*No matching contacts*</center></xsl:if>
                
              </xsl:otherwise>
            </xsl:choose>
          </div>
      </body>
    </html>
  </xsl:template>
  
  <xsl:template match="Hx">
    <div onclick="window.external.selectPhoneNumber('{@number}')"  
       style="border-bottom: solid 2px black; cursor: hand; padding: 0px 0px 0px 0px; height: 30px;">
      <div style="float: left; width: 30px; background: black; border: ">
        <center>
         <xsl:choose>
            <xsl:when test="@image != ''">
              <img src="{$dataPath}{@image}"  height="30px"/>
            </xsl:when>
            <xsl:otherwise>
              <img src="{$systemPath}/you.gif" height="30px"/>
            </xsl:otherwise>             
          </xsl:choose>            
        </center>
      </div>
      <div style="float: right;  width: 245px; ">
        <div style="color: Black; font-size: 10pt; font-weight: normal; height: 15px;" >
          <span style="float: left;">
            <xsl:choose>
              <xsl:when test="@name != ''">
                <xsl:value-of select="@name"/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:text>Unknown</xsl:text>
              </xsl:otherwise>
            </xsl:choose>
          </span>
          <span style="float: right;">
            <xsl:value-of select="@date"/>
          </span>
        </div>
        <div style="color: #777777; font-size: 10pt; height: 15px;" >
          <span style="float: left;"><xsl:value-of select="@number"/></span>
          <span style="float: right;"><xsl:value-of select="@time"/></span>
        </div>        
      </div>        
    </div>
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
          <div style="color: #777777; font-size: 10pt; " >
            <xsl:value-of select="Org"/>
          </div>        
        </div>
        <xsl:apply-templates select="PhoneNumbers/PhoneNumber"/>
      </div>
    </xsl:if>
  </xsl:template>  
  
 <xsl:template match="Contact" mode="favorite">
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
          <div style="color: #777777; font-size: 10pt; " >
            <xsl:value-of select="Org"/>
          </div>        
        </div>
        <xsl:apply-templates select="PhoneNumbers/PhoneNumber[@favorite = 'true']"/>
      </div>
    </xsl:if>
  </xsl:template>    
  
  <xsl:template match="PhoneNumber">
    <div onclick="window.external.selectPhoneNumber('{Number}')"  style="border-top: solid 1px #777777; cursor: hand; padding-left: 8px;">
      <img src="{$systemPath}/greenPhone.png" height="30px" style="float: left; "/>
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
