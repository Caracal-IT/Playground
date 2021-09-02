<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

    <xsl:template match="request[@name='ProcessUseCase']">
        <xsl:if test="config">
            <config>
                <settings>
                    <setting name="url" value="https://localhost:5002/CustomProvider/Process"/>
                    <setting name="content-type" value="application/xml"/>
                </settings>
            </config>
        </xsl:if>
        
        <xsl:if test="payload">
            <request>
                <xsl:attribute name="reference"><xsl:value-of select="payload/export-data/@reference"/></xsl:attribute>
                <code>00</code>
                <amount><xsl:value-of select="payload/export-data/@amount"/></amount>
            </request>
        </xsl:if>
        <xsl:if test="response">
            <response>
                <terminal-response>
                    <xsl:attribute name="success">true</xsl:attribute>
                    <xsl:attribute name="code"><xsl:value-of select="response/code"/></xsl:attribute>
                    <xsl:attribute name="reference"><xsl:value-of select="response/@reference"/></xsl:attribute>
                </terminal-response>
                <response>
                    <terminal>CustomTerminal</terminal>
                    <name><xsl:value-of select="response/name"/></name>
                    <reference><xsl:value-of select="response/@reference"/></reference>
                    <code><xsl:value-of select="response/code"/></code>
                    <message>From Custom Terminal</message>
                </response>
            </response>
        </xsl:if>
    </xsl:template>
</xsl:stylesheet>