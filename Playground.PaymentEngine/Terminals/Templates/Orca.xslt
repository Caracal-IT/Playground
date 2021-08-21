<xsl:stylesheet version="1.0"
                xmlns:scripts="utility:hashing/v1"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:template match="request[@name='FraudDetection']">
        <xsl:if test="config">
            <config>
                <settings>
                    <setting name="url" value="https://localhost:5001/Compliance/Fraud/Validate"/>
                </settings>
            </config>
        </xsl:if>

        <xsl:if test="payload">
            <request>
                <customerId><xsl:value-of select="payload/rule-input/@customer-id"/></customerId>
                <amount><xsl:value-of select="payload/rule-input/@amount"/></amount>
            </request>
        </xsl:if>
        <xsl:if test="response">
            <response>
                <terminal-response>
                    <xsl:attribute name="success">true</xsl:attribute>
                </terminal-response>
                <terminal-result>
                    <xsl:attribute name="valid"><xsl:value-of select="response/isValid"/></xsl:attribute>
                </terminal-result>
            </response>
        </xsl:if>
    </xsl:template>
</xsl:stylesheet>