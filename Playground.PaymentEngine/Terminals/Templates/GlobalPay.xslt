<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

    <xsl:template match="request[@name='ProcessUseCase']">
        <xsl:if test="config">
            <config>
                <settings>
                    <setting name="url" value="https://localhost:5001/GlobalPay/Process"/>
                </settings>
            </config>
        </xsl:if>
        
        <xsl:if test="payload">
            <request>
                <reference><xsl:value-of select="payload/@reference"/></reference>
                <amount><xsl:value-of select="payload/@amount"/></amount>
                <code>00</code>
            </request>
        </xsl:if>
        <xsl:if test="response">
            <response>
                <response>
                    <terminal>GlobalPay</terminal>
                    <name><xsl:value-of select="response/name"/></name>
                    <xsl:choose>
                        <xsl:when test="response/amount = 12"><code>05</code></xsl:when>
                        <xsl:otherwise><code><xsl:value-of select="response/code"/></code></xsl:otherwise>
                    </xsl:choose>
                    <reference><xsl:value-of select="response/reference"/></reference>
                    <amount><xsl:value-of select="response/amount"/></amount>
                    <message>From GlobalPay ($ <xsl:value-of select="response/amount"/>)</message>
                </response>
            </response>
        </xsl:if>
    </xsl:template>
</xsl:stylesheet>