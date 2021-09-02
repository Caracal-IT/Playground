<xsl:stylesheet version="1.0"
                xmlns:scripts="utility:hashing/v1"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:decimal-format
            name="Cz2"
            grouping-separator=" "
            decimal-separator=","/>
    
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
                <reference><xsl:value-of select="payload/export-data/@reference"/></reference>
                <amount><xsl:value-of select="payload/export-data/@amount"/></amount>
                <hash><xsl:value-of select="scripts:Hash256(payload/export-data/@reference)"/></hash>               
                <meta-data>
                    <xsl:attribute name="name">Reference</xsl:attribute>
                    <xsl:attribute name="value"><xsl:value-of select="payload/export-data/@reference"/></xsl:attribute>
                </meta-data>
                <meta-data>
                    <xsl:attribute name="name">Amount</xsl:attribute>
                    <xsl:attribute name="value"><xsl:value-of select="format-number(number(payload/export-data/@amount) * 100, '# ##0.00', 'Cz2')"/></xsl:attribute>
                </meta-data>

                <xsl:for-each select="payload/export-data/meta-data">
                    <xsl:choose>
                        <xsl:when test="@name = 'account-holder'">
                            <meta-data>
                                <xsl:attribute name="name">Account Holder</xsl:attribute>
                                <xsl:attribute name="value"><xsl:value-of select="@value"/></xsl:attribute>
                            </meta-data>
                        </xsl:when>
                        <xsl:otherwise>
                            <meta-data>
                                <xsl:attribute name="name"><xsl:value-of select="@name"/></xsl:attribute>
                                <xsl:attribute name="value"><xsl:value-of select="@value"/></xsl:attribute>
                            </meta-data>
                        </xsl:otherwise>
                    </xsl:choose>
                </xsl:for-each>
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