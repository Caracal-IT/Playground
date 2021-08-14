<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

    <xsl:template match="request[@name='ProcessUseCase']">
        <xsl:if test="payload">
            <request>
                <trans-ref><xsl:value-of select="payload/@reference"/></trans-ref>
                <amount><xsl:value-of select="payload/@amount"/></amount>
            </request>
        </xsl:if>
        <xsl:if test="response">
            <response>
                <response>
                    <terminal>Terminal2</terminal>
                    <name><xsl:value-of select="@name"/></name>
                    <reference><xsl:value-of select="response/trans-ref"/></reference>
                    <code>00</code>
                    <message>From Terminal 2</message>
                </response>
            </response>
        </xsl:if>
    </xsl:template>
</xsl:stylesheet>