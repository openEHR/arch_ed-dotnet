<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="2.0"
    xmlns:oe="http://schemas.openehr.org/v1">
    <xsl:param name="language" select="/oe:archetype/oe:original_language/oe:code_string"/>
    <xsl:param name="contact-email">info@openehr.org</xsl:param>
    <xsl:param name="contact-text">Email comments to info@openehr.org</xsl:param>
    <xsl:param name="copyright-text">© Copyright openEHR Foundation 2008</xsl:param>
    
    <xsl:param name="show-terminology-flag">true</xsl:param> <!-- send in anything that's not 'true' to hide terminology -->
    <xsl:param name="show-comments-flag">true</xsl:param> <!-- send in anything that's not 'true' to hide comments -->
    
    <xsl:param name="css-path">default.css</xsl:param>
    <xsl:param name="images-path">images</xsl:param>
       
    <xsl:template match="/">
        <html>
            <head>
                <title><xsl:value-of select="oe:archetype/oe:ontology/oe:term_definitions/oe:items[@code='at0000']/oe:items[@id='text']"/> - <xsl:value-of select="oe:archetype/oe:archetype_id/oe:value"/></title>
                <meta http-equiv="Content-Language" content="{$language}"/>
                <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                <link rel="stylesheet" type="text/css" media="all" href="{$css-path}"/>
                <!--<link rel="stylesheet" type="text/css" media="print" href="printable.css"/>-->
            </head>
            <body>
                <div id="adlhtml-body">
                    <xsl:call-template name="generate-header"/>
                    <xsl:call-template name="generate-terminology"/>
                    <xsl:call-template name="generate-definition"/>
                    <xsl:call-template name="generate-footer"/>
                </div>
            </body>
        </html>        
    </xsl:template>
    
    <!-- **** Archetype definition templates **** -->
    
    <!-- match for all C_OBJECTs definition or children nodes0-->
    <xsl:template match="*[name()='definition' or name()='children']">
        <div id="adlhtml-C_OBJECT" class="adlhtml-{oe:rm_type_name/text()}">
            <p>Show stuff about this C_OBJECT:<br/>
                - RM attribute name: <xsl:value-of select="parent::node()/oe:rm_attribute_name"/><br/>
                - RM type name: <xsl:value-of select="oe:rm_type_name"/><br/>
                - AT code of node: <xsl:value-of select="oe:node_id"/><br/>
                - default name of node: 
                <xsl:call-template name="get-archetype-term">
                    <xsl:with-param name="at-code" select="oe:node_id"/>
                </xsl:call-template>
            </p>
            <xsl:apply-templates select="oe:attributes"/>
        </div>
    </xsl:template>
    
    <xsl:template match="*[name()='attributes' and not(oe:rm_attribute_name='name')]">
        <xsl:apply-templates select="oe:children"/>
    </xsl:template>
    
    <!-- **** generate HTML section templates **** -->
    
    <xsl:template name="generate-header">
        <div id="adlhtml-header">
            <table>
                <tbody>
                    <tr>
                        <td colspan="3">
                            <xsl:call-template name="get-archetype-term">
                                <xsl:with-param name="at-code" select="/oe:archetype/oe:concept"/>
                            </xsl:call-template>:
                            <xsl:call-template name="get-archetype-rm-type">
                                <xsl:with-param name="archetype-id" select="/oe:archetype/oe:archetype_id/oe:value"/>
                            </xsl:call-template></td>
                    </tr>
                    <tr>
                        <td>Using ADL version <xsl:value-of select="/oe:archetype/oe:adl_version"/></td>
                        <td><a href="mailto:{$contact-email}"><xsl:value-of select="$contact-text"/></a></td>
                        <td><xsl:value-of select="$copyright-text"/></td>
                    </tr>
                </tbody>
            </table>
            <table>
                <tbody>
                    <tr>
                        <td>Concept</td>
                        <td>Archetype ID</td>
                        <td>Structure</td>
                    </tr>
                    <tr>
                        <td><xsl:call-template name="get-archetype-term">
                            <xsl:with-param name="at-code" select="/oe:archetype/oe:concept"/>
                        </xsl:call-template></td>
                        <td><xsl:value-of select="/oe:archetype/oe:archetype_id/oe:value"/></td>
                        <td>[structure goes here]</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </xsl:template>
    
    <xsl:template name="generate-terminology">
        <div id="adlhtml-terminology">
            <table>
                <tbody>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </xsl:template>
    
    <xsl:template name="generate-definition">
        <div id="adlhtml-definition">
            <xsl:apply-templates select="/oe:archetype/oe:definition"/>
        </div>
    </xsl:template>
    
    <xsl:template name="generate-footer">
        <div id="adlhtml-footer">
            <table>
                <tbody>
                    <tr>
                        <td><a href="http://svn.openehr.org/knowledge/archetypes/dev/html/Datatypes.html">What do the images mean?</a></td>
                    </tr>
                    <tr>
                        <td>Generated by Ocean HTML generator</td>
                    </tr>
                </tbody>
            </table>
        </div>        
    </xsl:template>
    
    
    <!-- **** helper templates **** -->
    
    <xsl:template name="get-archetype-rm-type">
        <xsl:param name="archetype-id" required="yes"/> <!-- eg. openEHR-EHR-COMPOSITION.Report.v1 -->
        <xsl:variable name="id-minus-authority" select="substring-after($archetype-id, '-')"/> <!-- eg EHR-COMPOSITION.Report.v1 -->
        <xsl:variable name="id-minus-rm" select="substring-after($id-minus-authority, '-')"/> <!-- eg. COMPOSITION.Report.v1 -->
        <xsl:value-of select="substring-before($id-minus-rm, '.')"/> <!-- eg. COMPOSITION -->
    </xsl:template>
    
    <xsl:template name="get-archetype-term">
        <xsl:param name="at-code" required="yes"/>
        <xsl:value-of select="/oe:archetype/oe:ontology/oe:term_definitions/oe:items[@code=$at-code/text()]/oe:items[@id='text']"/>
    </xsl:template>
    
    <xsl:template name="get-archetype-description">
        <xsl:param name="at-code" required="yes"/>
        <xsl:value-of select="/oe:archetype/oe:ontology/oe:term_definitions/oe:items[@code=$at-code/text()]/oe:items[@id='description']"/>
    </xsl:template>
    

    <!-- *** test templates ****
        
    <xsl:template match="oe:archetype/oe:archetype_id">
        <p>Archetype ID: <xsl:value-of select="oe:value"/></p>
    </xsl:template>
    
    <xsl:template match="oe:archetype/oe:original_language">
        <p>Original language: <xsl:value-of select="oe:original_language/oe:value"/></p>
    </xsl:template>
    
    <xsl:template match="oe:archetype/oe:description">
        <p>Description: ...</p>
    </xsl:template>
    
    <xsl:template match="oe:archetype/oe:adl_version">
        <p>ADL version: <xsl:value-of select="."/></p>
    </xsl:template>
    
    <xsl:template match="oe:archetype/oe:concept">
        <p>Concept: <xsl:value-of select="."/></p>
    </xsl:template>
    
    <xsl:template match="oe:archetype/oe:definition">
        <p>Definition ...</p>
    </xsl:template>

    <xsl:template match="oe:archetype/oe:ontology">
        <p>Ontology ...</p>
    </xsl:template>
    -->
</xsl:stylesheet>
