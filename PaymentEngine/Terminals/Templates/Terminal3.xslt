<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:template match="export-data">
        <request>
            <trans-ref><xsl:value-of select="@reference"/></trans-ref>
            <amount><xsl:value-of select="@amount"/></amount>
        </request>
    </xsl:template>

    <xsl:template match="response">
        <export-response>
            <terminal>Terminal3</terminal>
            <name><xsl:value-of select="@name"/></name>
            <reference><xsl:value-of select="trans-ref"/></reference>
            <code>00</code>
            <message>From Terminal 3 Response</message>
        </export-response>
    </xsl:template>
</xsl:stylesheet>