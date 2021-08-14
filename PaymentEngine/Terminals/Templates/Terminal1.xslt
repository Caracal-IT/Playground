<xsl:stylesheet version="1.0"
                xmlns:scripts="utility:terminal1/v1"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:template match="request[@name='ProcessUseCase']">
        <xsl:if test="config">
            <config>
                <xsl:attribute name="name"><xsl:value-of select="@name"/></xsl:attribute>
                <settings>
                    <setting name="url" value="https://apple.com"/>
                </settings>
            </config>
        </xsl:if>
        
        <xsl:if test="payload">            
            <request>               
                <trans-ref><xsl:value-of select="payload/@reference"/></trans-ref>
                <amount><xsl:value-of select="payload/@amount"/></amount>
                <hash><xsl:value-of select="scripts:Hash256(@reference)"/></hash>
                <xsl:for-each select="payload/meta-data">
                    <xsl:choose>
                        <xsl:when test="@name = 'account-holder'">
                            <card-holder><xsl:value-of select="@value"/></card-holder>
                        </xsl:when>
                    </xsl:choose>
                </xsl:for-each>                
            </request>
        </xsl:if>

        <xsl:if test="response">            
            <response>
                <terminal>Terminal1</terminal>
                <name><xsl:value-of select="response/@name"/></name>
                <reference><xsl:value-of select="response/trans-ref"/></reference>
                <code>00</code>
                <message>From Terminal 1 Response ($ <xsl:value-of select="response/amount"/>)</message>
            </response>
        </xsl:if>
    </xsl:template>

    <xsl:template match="request[@name='CallbackUseCase']">
        <xsl:if test="payload">
            <callback-request>
                <xsl:attribute name="reference"><xsl:value-of select="payload/callback/ref"/></xsl:attribute>
                <xsl:attribute name="code"><xsl:value-of select="payload/callback/status"/></xsl:attribute>
            </callback-request>
        </xsl:if>

        <xsl:if test="callback-response">
            <response>
                <terminal-response success="true">
                    <xsl:attribute name="reference-number"><xsl:value-of select="callback-response/callback-request/@reference"/></xsl:attribute>
                    <xsl:attribute name="return-code"><xsl:value-of select="callback-response/callback-request/@code"/></xsl:attribute>
                </terminal-response>
                <response>
                    <xsl:attribute name="reference"><xsl:value-of select="callback-response/callback-request/@reference"/></xsl:attribute>
                    <xsl:attribute name="code"><xsl:value-of select="callback-response/callback-request/@code"/></xsl:attribute>
                    <user>
                        <name>Kate G</name>
                    </user>
                </response>
            </response>
        </xsl:if>
    </xsl:template>
</xsl:stylesheet>