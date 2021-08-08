<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:template match="response">
        <export-response>
            <name><xsl:value-of select="@name"/></name>
            <reference><xsl:value-of select="trans-ref"/></reference>
            <code>00</code>
            <message>From Terminal 2 Response</message>
        </export-response>
    </xsl:template>
</xsl:stylesheet>