<xsl:stylesheet version="1.0" 
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:template match="catalog">
<html>
    <head>Testing</head>
    <body>
        <table border="1">
            <tr>
                <th align="left">Title</th>
                <th align="left">Author</th>
                <th align="left">Genre</th>
                <th align="left">Publish Date</th>
                <th align="left">Price</th>
            </tr>
            <xsl:for-each select="book">
                <tr>
                    <td>
                        <xsl:value-of select="title"/>
                    </td>
                    <td>
                        <xsl:value-of select="author"/>
                    </td>
                    <td>
                        <xsl:value-of select="genre"/>
                    </td>
                    <td>
                        <xsl:value-of select="publish_date"/>
                    </td>
                    <xsl:choose>
                        <xsl:when test="genre = 'Fantasy'">
                            <td>
                                <xsl:value-of select="price"/>
                            </td>
                        </xsl:when>
                        <xsl:otherwise>
                            <td>
                                <xsl:value-of select="price"/>
                            </td>
                        </xsl:otherwise>
                    </xsl:choose>
                </tr>
            </xsl:for-each>
        </table>
    </body>
</html>
</xsl:template>
</xsl:stylesheet> 