<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                version="1.0">
	<xsl:template match="/">
		<html>
			<p>
				<xsl:value-of select="//firstname" />
			</p>
			<p>
				<xsl:value-of select="//lastname" />
			</p>
		</html>
	</xsl:template>
</xsl:stylesheet>