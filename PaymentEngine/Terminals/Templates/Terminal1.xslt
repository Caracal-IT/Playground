<xsl:stylesheet version="1.0"
                xmlns:scripts="utility:terminal1/v1"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:template match="export-data">
        <request>
            <trans-ref><xsl:value-of select="@reference"/></trans-ref>
            <amount><xsl:value-of select="@amount"/></amount>
            <hash><xsl:value-of select="scripts:Hash256(@reference)"/></hash>
            <xsl:for-each select="meta-data">
                <xsl:choose>
                    <xsl:when test="@name = 'account-holder'">
                        <card-holder><xsl:value-of select="@value"/></card-holder>
                    </xsl:when>
                </xsl:choose>
            </xsl:for-each>
        </request>
    </xsl:template>

    <xsl:template match="response">
        <export-response>
            <terminal>Terminal1</terminal>
            <name><xsl:value-of select="@name"/></name>
            <reference><xsl:value-of select="trans-ref"/></reference>
            <code>00</code>
            <message>From Terminal 1 Response ($ <xsl:value-of select="amount"/>)</message>
        </export-response>
    </xsl:template>
    
    <xsl:template match="callback-request/callback">
        <callback-request>
            <xsl:attribute name="reference"><xsl:value-of select="ref"/></xsl:attribute>
            <xsl:attribute name="code"><xsl:value-of select="status"/></xsl:attribute>
        </callback-request>
    </xsl:template>
    
    <xsl:template match="callback-response/callback-request">
        <terminal-response success="true">
            <xsl:attribute name="reference-number"><xsl:value-of select="@reference"/></xsl:attribute>
            <xsl:attribute name="return-code"><xsl:value-of select="@code"/></xsl:attribute>
        </terminal-response>
    </xsl:template>
</xsl:stylesheet>