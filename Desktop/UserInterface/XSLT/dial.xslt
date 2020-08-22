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

        
        
          <div style="height: 255px; overflow: auto; border-top: solid 2px black;">
            <xsl:choose>
              <xsl:when test="$contactFilter = ''">
                <center>
                  <span style="color: #777777; font-size: 10pt;">Enter phone number or contact name above</span>
                </center>
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
                  <center>*No matching contacts*</center>
                </xsl:if>
                
              </xsl:otherwise>
            </xsl:choose>
          </div>
      </body>
    </html>
  </xsl:template>
  
  <xsl:template match="Contact">
    <xsl:if test="count(PhoneNumbers/PhoneNumber) &gt; 0">
      <div onclick="toggleContactDetails( this );" style="border-bottom: solid 2px black;">
        <div style="color: Black; font-size: 10pt; font-weight: normal; height: 15px;" >
          <xsl:value-of select="DisplayName"/>
        </div>
        <div style="color: #777777; font-size: 10pt; height: 15px;" >
          <xsl:value-of select="Org"/>
        </div>        
        <div style="display:inline;">
          <xsl:apply-templates select="PhoneNumbers/PhoneNumber"/>
        </div>
      </div>
    </xsl:if>
  </xsl:template>  
  
  <xsl:template match="PhoneNumber">
     <div onclick="window.external.selectPhoneNumber('{Number}')"  style="border-top: solid 1px #777777; padding-left: 10px;">
        <div style="color: Black; font-size: 10pt; font-weight: normal; height: 15px;" >
          <xsl:value-of select="Type"/>
        </div>
        <div style="color: #777777; font-size: 10pt; height: 15px; cursor: hand;" >
          <xsl:value-of select="Number"/>
        </div>        
      </div>    
  </xsl:template>
  
</xsl:stylesheet> 
