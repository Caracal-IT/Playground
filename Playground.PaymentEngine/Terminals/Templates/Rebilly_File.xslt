<xsl:stylesheet version="1.0"
                xmlns:scripts="utility:hashing/v1"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:template match="request[@name='ProcessUseCase']">
        <xsl:if test="config">
            <config>
                <settings>
                    <setting name="url" value="https://localhost:5001/Rebilly/Process"/>
                    <setting name="header:api-key" value="6565744884400474747"/>
                </settings>
            </config>
        </xsl:if>
        
        <xsl:if test="payload">            
            <request>
                <code>00</code>
                <reference><xsl:value-of select="payload/@reference"/></reference>
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
                <response>
                    <terminal>Rebilly_File</terminal>
                    <name>Rebilly_File</name>
                    <code><xsl:value-of select="response/code"/></code>
                    <reference><xsl:value-of select="response/@reference"/></reference>
                    <amount><xsl:value-of select="response/amount"/></amount>
                    <message>Exported</message>
                    <meta-data>
                        <meta-data-item>
                            <xsl:attribute name="name">file-name</xsl:attribute>
                            <xsl:attribute name="value"><xsl:value-of select="response/file-name"/></xsl:attribute>
                        </meta-data-item>
                    </meta-data>
                </response>
            </response>
        </xsl:if>
    </xsl:template>
</xsl:stylesheet>