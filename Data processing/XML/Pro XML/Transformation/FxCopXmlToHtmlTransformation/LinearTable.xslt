<?xml version="1.0" encoding="windows-1251"?>

<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt"
                exclude-result-prefixes="msxsl">
    <xsl:output method="html" encoding="windows-1251" indent="yes" />
    <xsl:template match="/FxCopReport">
        <xsl:text disable-output-escaping="yes">&lt;!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"&gt;&#xa;</xsl:text>        
        <html xmlns="http://www.w3.org/1999/xhtml" lang="en">
            <head>
                <title>
                    <xsl:value-of select="/FxCopReport/Localized/String[@Key='ReportTitle']/text()" />
                </title>
                <meta http-equiv="content-type" content="text/html; charset=windows-1251" />
            </head>
            <body>
                <!-- Сборки -->
                <xsl:for-each select="/FxCopReport/Targets/Target">
                    <div style="border: 1px solid black;">
                        <h1>Target name: <xsl:value-of select="./@Name" /></h1>
                        <hr />
                        <div>
                            <div style="margin-left: 30px; margin-right: 10px;">
                                <!-- Модули сборки -->
                                <xsl:for-each select="./Modules/Module">
                                    <div style="text-indent: 50px; background: #E0E0E0; border: 1px double red;">
                                        <h3 style="text-decoration: underline;">
                                            Module: <xsl:value-of select="./@Name" />
                                        </h3>
                                        <div>
                                            <!-- Сообщения для модулей сборки -->
                                            <table border="1"
                                                   style="background: #FFFFCC; font-family: Verdana; font-size: 0.8em; border: 1px dotted #000099; text-indent: 0px; margin-left: 50px; margin-bottom: 10px;">
                                                <tr>
                                                    <th valign="top" align="left">Type name</th>
                                                    <th valign="top" align="left">Category</th>
                                                    <th valign="top" align="left">Check id</th>
                                                    <th valign="top" align="left">Status</th>
                                                    <th valign="top" align="left">Created date</th>
                                                    <th valign="top" align="left">Fix category</th>
                                                    <th valign="top" align="left">Certainty</th>
                                                    <th valign="top" align="left">Level</th>
                                                    <th valign="top" align="left">Issue message</th>
                                                </tr>
                                                <xsl:for-each select="./Messages/Message">
                                                    <tr>
                                                        <td valign="top" align="left">
                                                            <xsl:value-of
                                                                select="./@TypeName" />
                                                        </td>
                                                        <td valign="top" align="left">
                                                            <xsl:value-of
                                                                select="./@Category" />
                                                        </td>
                                                        <td valign="top" align="left">
                                                            <xsl:value-of select="./@CheckId" />
                                                        </td>
                                                        <td valign="top" align="left">
                                                            <xsl:value-of select="./@Status" />
                                                        </td>
                                                        <td valign="top" align="left">
                                                            <xsl:value-of select="./@Created" />
                                                        </td>
                                                        <td valign="top" align="left">
                                                            <xsl:value-of
                                                                select="./@FixCategory" />
                                                        </td>
                                                        <td valign="top" align="left">
                                                            <xsl:value-of
                                                                select="./Issue/@Certainty" />
                                                        </td>
                                                        <td valign="top" align="left">
                                                            <xsl:value-of
                                                                select="./Issue/@Level" />
                                                        </td>
                                                        <td valign="top" align="left">
                                                            <xsl:value-of
                                                                select="./Issue/text()" />
                                                        </td>
                                                    </tr>
                                                </xsl:for-each>
                                            </table>
                                        </div>
                                    </div>
                                    <!-- Пространства имен -->
                                    <div>
                                        <h3 style="text-decoration: underline;">Namespaces</h3>
                                        <xsl:for-each select="./Namespaces/Namespace">
                                            <table border="1" width="100%"
                                                   style="background: #FFFFCC; font-family: Verdana; font-size: 0.7em; text-indent: 0px; margin-bottom: 10px;">
                                                <thead>
                                                    <b>Namespace: <xsl:value-of select="./@Name" /></b>
                                                </thead>
                                                <tr>
                                                    <th valign="top" align="left" style="width: 10%;">Type</th>
                                                    <th valign="top" align="left" style="width: 90%;">Messages</th>
                                                </tr>
                                                <!-- Типы -->
                                                <xsl:for-each select="./Types/Type">
                                                    <tr>
                                                        <td valign="top" align="left" style="width: 10%;">
                                                            <xsl:value-of select="./@Name" />
                                                        </td>
                                                        <td valign="top" align="left" style="width: 90%;">
                                                            <ol>
                                                                <!-- Сообщения для членов -->
                                                                <xsl:for-each select="./Members/Member">
                                                                    <xsl:for-each select="./Messages/Message">
                                                                        <li>                                                                            
                                                                            <table width="100%"
                                                                                   style="display: inline; background: #FFFFCC; font-family: Consolas; font-size: 1.0em; text-indent: 0px; margin-bottom: 10px;">
                                                                                <tr>
                                                                                    <td valign="top" align="left">Kind:</td>
                                                                                    <td valign="top" align="left"><xsl:value-of select="./@TypeName" /></td>
                                                                                </tr>                                                                                
                                                                                <tr>
                                                                                    <td valign="top" align="left">Message:</td>
                                                                                    <td valign="top" align="left"><xsl:value-of select="./Issue/text()" /></td>
                                                                                </tr>
                                                                                <xsl:if test="./Issue/@Path">
                                                                                    <tr>
                                                                                        <td valign="top" align="left">Path:</td>
                                                                                        <td valign="top" align="left"><xsl:value-of select="./Issue/@Path" /></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td valign="top" align="left">File:</td>
                                                                                        <td valign="top" align="left"><xsl:value-of select="./Issue/@File" /></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td valign="top" align="left">Line:</td>
                                                                                        <td valign="top" align="left"><xsl:value-of select="./Issue/@Line" /></td>
                                                                                    </tr>
                                                                                </xsl:if>                                                                                
                                                                            </table>
                                                                            <hr />
                                                                        </li>
                                                                    </xsl:for-each>
                                                                </xsl:for-each>
                                                                <!-- Общие сообщения -->
                                                                <xsl:for-each select="./Messages/Message">
                                                                    <li>
                                                                        <table width="100%"
                                                                               style="display: inline; background: #FFFFCC; font-family: Consolas; font-size: 1.0em; text-indent: 0px; margin-bottom: 10px;">
                                                                            <tr>
                                                                                <td valign="top" align="left">Kind:</td>
                                                                                <td valign="top" align="left"><xsl:value-of select="./@TypeName" /></td>
                                                                            </tr>                                                                            
                                                                            <tr>
                                                                                <td valign="top" align="left">Message:</td>
                                                                                <td valign="top" align="left"><xsl:value-of select="./Issue/text()" /></td>
                                                                            </tr>
                                                                            <xsl:if test="./Issue/@Path">
                                                                                <tr>
                                                                                    <td valign="top" align="left">Path:</td>
                                                                                    <td valign="top" align="left">
                                                                                        <xsl:value-of select="./Issue/@Path" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td valign="top" align="left">File:</td>
                                                                                    <td valign="top" align="left">
                                                                                        <xsl:value-of select="./Issue/@File" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td valign="top" align="left">Line:</td>
                                                                                    <td valign="top" align="left">
                                                                                        <xsl:value-of select="./Issue/@Line" />
                                                                                    </td>
                                                                                </tr>
                                                                            </xsl:if>                                                                            
                                                                        </table>
                                                                        <hr />
                                                                    </li>
                                                                </xsl:for-each>
                                                            </ol>
                                                        </td>
                                                    </tr>
                                                </xsl:for-each>
                                            </table>
                                        </xsl:for-each>
                                    </div>
                                </xsl:for-each>
                            </div>
                        </div>
                    </div>
                </xsl:for-each>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>