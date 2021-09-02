<xsl:stylesheet version="1.0"
                xmlns:scripts="utility:hashing/v1"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:template match="request[@name='ProcessUseCase']">
        <xsl:if test="config">
            <config>
                <settings>
                    <setting name="url" value="https://localhost:5002/Rebilly/Process"/>
                    <setting name="header:api-key" value="6565744884400474747"/>
                </settings>
            </config>
        </xsl:if>
        
        <xsl:if test="payload">            
            <request>               
                <trans-ref><xsl:value-of select="payload/export-data/@reference"/></trans-ref>
                <amount><xsl:value-of select="payload/export-data/@amount"/></amount>
                <hash><xsl:value-of select="scripts:Hash256(export-data/@reference)"/></hash>
                <xsl:for-each select="payload/export-data/meta-data">
                    <xsl:choose>
                        <xsl:when test="@name = 'account-holder'">
                            <card-holder><xsl:value-of select="@value"/></card-holder>
                        </xsl:when>
                    </xsl:choose>
                </xsl:for-each>
                <xsl:choose>
                    <xsl:when test="payload/export-data/@amount = 10"><code>10</code></xsl:when>
                    <xsl:when test="payload/export-data/@amount = 12"><code>05</code></xsl:when>
                    <xsl:otherwise><code>00</code></xsl:otherwise>
                </xsl:choose>
            </request>
        </xsl:if>

        <xsl:if test="response">     
            <response>
                <terminal-response>
                    <xsl:attribute name="reference"><xsl:value-of select="response/trans-ref"/></xsl:attribute>
                    <xsl:choose>
                        <xsl:when test="response/code = '05'">
                            <xsl:attribute name="success">false</xsl:attribute>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:attribute name="success">true</xsl:attribute>
                        </xsl:otherwise>
                    </xsl:choose>
                </terminal-response>
                <response>
                    <terminal>Rebilly</terminal>
                    <name><xsl:value-of select="response/name"/></name>
                    <reference><xsl:value-of select="response/trans-ref"/></reference>
                    <code><xsl:value-of select="response/code"/></code>
                    <message>From Rebilly Response ($ <xsl:value-of select="response/amount"/>)</message>
                </response>
            </response>
        </xsl:if>
    </xsl:template>

    <xsl:template match="request[@name='callback']">
        <xsl:if test="config">
            <config>
                <settings>
                    <setting name="url" value="https://localhost:5002/Rebilly/Callback"/>
                </settings>
            </config>
        </xsl:if>
        
        <xsl:if test="payload">
            <request>
                <xsl:attribute name="reference"><xsl:value-of select="payload/callback/ref"/></xsl:attribute>
                <xsl:attribute name="code"><xsl:value-of select="payload/callback/status"/></xsl:attribute>
            </request>
        </xsl:if>

        <xsl:if test="response">
            <response>
                <terminal-response>
                    <xsl:attribute name="success">true</xsl:attribute>
                    <xsl:attribute name="code"><xsl:value-of select="response/code"/></xsl:attribute>
                    <xsl:attribute name="reference"><xsl:value-of select="response/reference"/></xsl:attribute>
                </terminal-response>
                <response>
                    <xsl:attribute name="reference"><xsl:value-of select="response/reference"/></xsl:attribute>
                    <xsl:attribute name="code"><xsl:value-of select="response/code"/></xsl:attribute>
                    <user>
                        <name>Kate G</name>
                    </user>
                </response>
            </response>
        </xsl:if>
    </xsl:template>
</xsl:stylesheet>