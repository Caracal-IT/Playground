<xsl:stylesheet version="1.0"
                xmlns:scripts="utility:hash/v1"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:template match="catalog">
{
    "name" : "export",
    "items": [<xsl:for-each select="book">
        {
            "id": "<xsl:value-of select="@id"/>",
            "title": "<xsl:value-of select="title"/>",
            "author": "<xsl:value-of select="author"/>",
            "genre": "<xsl:value-of select="genre"/>",
            "publish_date": "<xsl:value-of select="publish_date"/>",
            "price": "<xsl:value-of select="price"/>",
            <xsl:choose>
                <xsl:when test="genre = 'Fantasy'">
                    "name": "Kate",
                    "author2": "<xsl:value-of select="author"/>",
                </xsl:when>
                <xsl:when test="genre = 'Romance'">
                    "name": "Cecilia",
                </xsl:when>
                <xsl:otherwise>
                    "name": "Ettiene",
                    "surname": "Mare",
                </xsl:otherwise>
            </xsl:choose>
            "hash": "<xsl:value-of select="scripts:Hash256(@id)"/>"
        }<xsl:if test="position() != last()">,</xsl:if>
    </xsl:for-each>
    ]
}
</xsl:template>
</xsl:stylesheet> 