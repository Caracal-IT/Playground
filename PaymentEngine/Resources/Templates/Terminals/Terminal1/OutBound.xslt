<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:template match="export-data">
        <request>
            <trans-ref><xsl:value-of select="@reference"/></trans-ref>
            <amount><xsl:value-of select="@amount"/></amount>
        </request>
    </xsl:template>
</xsl:stylesheet>