<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

    <xsl:template match="request[@name='ProcessUseCase']">
        <xsl:if test="config">
            <config>
                <settings>
                    <setting name="url" value="https://localhost:5001/PayPal/Process"/>
                </settings>
            </config>
        </xsl:if>
        
        <xsl:if test="payload">
            <request>
                <reference><xsl:value-of select="payload/@reference"/></reference>
                <code>00</code>
                <amount><xsl:value-of select="payload/@amount"/></amount>
            </request>
        </xsl:if>
        <xsl:if test="response">
            <response>
                <response>
                    <terminal>CustomTerminal</terminal>
                    <name><xsl:value-of select="response/name"/></name>
                    <reference><xsl:value-of select="response/reference"/></reference>
                    <code><xsl:value-of select="response/code"/></code>
                    <message>From Terminal 2</message>
                </response>
            </response>
        </xsl:if>
    </xsl:template>
</xsl:stylesheet>