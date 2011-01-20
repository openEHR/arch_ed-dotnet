<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="2.0"
    xmlns:oe="http://schemas.openehr.org/v1" xmlns:term="http://openehr.org/Terminology.xsd"
    xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:xi="http://www.w3.org/2001/XMLSchema-instance">

    <!-- **** GLOBAL PARAMETERS **** -->

    <!-- CONTENT parameters -->
    <xsl:param name="transform-filename">adlxml-to-html.xsl</xsl:param>
    <xsl:param name="transform-version">1.0</xsl:param>
    <xsl:param name="language" select="oe:archetype/oe:original_language/oe:code_string"/>
    <xsl:param name="contact-email">info@openehr.org</xsl:param>
    <xsl:param name="contact-text">Email comments to info@openehr.org</xsl:param>
    <xsl:param name="copyright-text">&#169; Copyright openEHR Foundation 2011</xsl:param>
    <!-- send in anything that's not 'true' to hide terminology -->
    <xsl:param name="show-terminology-flag">true</xsl:param>
    <!-- send in anything that's not 'true' to hide comments -->
    <xsl:param name="show-comments-flag">true</xsl:param>
    <!-- send in anything that's not 'true' to hide cardinality meaning -->
    <xsl:param name="show-cardinality-meaning">true</xsl:param>
    <!-- send in anything that's not 'true' to hide purpose, use, misuse and reference archetype details -->
    <xsl:param name="show-purpose-flag">false</xsl:param>

    <!-- RESOURCE parameters -->
    <xsl:param name="css-path">images/default.css</xsl:param>
    <xsl:param name="images-path">images</xsl:param>
    <!-- lookup openEHR ('local') terminology from separate xml document (by default this is found in the Archetype Editor
            installation directory under 'Program Files') - also relative to where this XSLT transform is located. -->
    <xsl:param name="terminology-xml-document-path">terminology.xml</xsl:param>

    <!-- FORMATTING parameters -->
    <xsl:param name="border">0</xsl:param>
    <xsl:param name="frame">below</xsl:param>
    <xsl:param name="rules">rows</xsl:param>
    <xsl:param name="header-frame">border</xsl:param>
    <xsl:param name="header-rules">all</xsl:param>

    <xsl:variable name="lcletters">abcdefghijklmnopqrstuvwxyz</xsl:variable>
    <xsl:variable name="ucletters">ABCDEFGHIJKLMNOPQRSTUVWXYZ</xsl:variable>
    <xsl:variable name="ordered">
        <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
            <xsl:with-param name="original-rubric">ordered</xsl:with-param>
        </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="numerator-label">
        <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
        <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
            <xsl:with-param name="original-rubric">numerator</xsl:with-param>
        </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="denominator-label">
        <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
        <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
            <xsl:with-param name="original-rubric">denominator</xsl:with-param>
        </xsl:call-template>
    </xsl:variable>
    <!-- DV_PROPORTION kind/type values -->
    <xsl:variable name="ratio">
        <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
            <xsl:with-param name="original-rubric">Ratio</xsl:with-param>
        </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="as-percent">
        <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
            <xsl:with-param name="original-rubric">As percent</xsl:with-param>
        </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="fraction">
        <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
            <xsl:with-param name="original-rubric">Fraction</xsl:with-param>
        </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="integer-and-fraction">
        <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
            <xsl:with-param name="original-rubric">Integer and fraction</xsl:with-param>
        </xsl:call-template>
    </xsl:variable>

    <!-- **** Archetype definition templates **** -->

    <xsl:template match="oe:archetype">
        <html>
            <head>
                <title><!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                    <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                        <xsl:with-param name="original-rubric">
                            <xsl:call-template name="get-archetype-term">
                                <xsl:with-param name="at-code">
                                    <xsl:value-of select="oe:concept"/>
                                </xsl:with-param>
                            </xsl:call-template>
                        </xsl:with-param>
                    </xsl:call-template> - <xsl:value-of select="oe:archetype_id/oe:value"/></title>
                <meta http-equiv="Content-Language" content="{$language}"/>
                <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                <link rel="stylesheet" type="text/css" media="all" href="{$css-path}"/>
            </head>
            <body>
                <div id="adlhtml-body">
                    <xsl:call-template name="generate-header"/>
                    <xsl:call-template name="generate-terminology"/>
                    <div id="adlhtml-definition">
                        <xsl:apply-templates select="oe:definition"/>
                    </div>
                    <xsl:call-template name="generate-footer"/>
                </div>
            </body>
        </html>
    </xsl:template>

    <!-- **** COMPOSITION **** -->

    <!-- generate COMPOSITION root -->
    <xsl:template match="*[oe:rm_type_name='COMPOSITION']">
        <xsl:variable name="rm_type_name">
            <xsl:value-of
                select="oe:attributes[oe:rm_attribute_name='context']/oe:children/oe:attributes[oe:rm_attribute_name='other_context']/oe:children/oe:rm_type_name"
            />
        </xsl:variable>
        <xsl:variable name="rm-type">
            <xsl:call-template name="remove_underscore">
                <xsl:with-param name="string">
                    <xsl:value-of select="substring-after($rm_type_name, 'ITEM_')"/>
                </xsl:with-param>
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="translated-rm-type">
            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric" select="$rm-type"/>
            </xsl:call-template>
        </xsl:variable>

        <div id="adlhtml-header-CATEGORY" align="left"><!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">composition category</xsl:with-param>
            </xsl:call-template>: <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its concept ID -->
            <xsl:call-template name="LookupTerminologyAndTranslationByConceptId">
                <xsl:with-param name="concept-id">
                    <xsl:value-of
                        select="oe:attributes[oe:rm_attribute_name='category']/oe:children/oe:attributes[oe:rm_attribute_name='defining_code']/oe:children/oe:code_list"
                    />
                </xsl:with-param>
            </xsl:call-template></div>
        <table>
            <body>
                <tr>
                    <td/>
                </tr>
            </body>
        </table>
        <div id="adlhtml-header-CONTEXT" align="left"><!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">context</xsl:with-param>
            </xsl:call-template>:</div>
        <table>
            <body>
                <tr>
                    <td/>
                </tr>
            </body>
        </table>
        <div id="adlhtml-header-OTHER-CONTEXT" align="left"><!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">other context</xsl:with-param>
            </xsl:call-template>:<xsl:value-of select="$translated-rm-type"/></div>
        <table>
            <body>
                <tr>
                    <td/>
                </tr>
            </body>
        </table>
        <table width="100%" frame="{$frame}" rules="{$rules}">
            <tbody>
                <xsl:call-template name="generate-common-or-slot-header">
                    <xsl:with-param name="currNode" select="oe:attributes[oe:rm_attribute_name='context']/oe:children/oe:attributes[oe:rm_attribute_name='other_context']/oe:children/oe:attributes"/>
                </xsl:call-template>
                <xsl:apply-templates select="oe:attributes[oe:rm_attribute_name='context']/oe:children/oe:attributes[oe:rm_attribute_name='other_context']/oe:children/oe:attributes/oe:children"/>
            </tbody>
        </table>
    </xsl:template>

    <!-- **** SECTION **** -->

    <!-- generate SECTION root -->
    <xsl:template match="oe:definition[oe:rm_type_name='SECTION']">
        <table width="100%" border="{$border}" frame="{$frame}" rules="{$rules}">
            <tbody>
                <xsl:call-template name="generate-section-or-slot-header">
                    <xsl:with-param name="currNode" select="oe:attributes[oe:rm_attribute_name='items']"/>
                </xsl:call-template>
                <tr>
                    <xsl:apply-templates select="oe:attributes[oe:rm_attribute_name='items']/oe:children"/>
                </tr>
            </tbody>
        </table>
    </xsl:template>

    <!-- generate SECTION child -->
    <xsl:template match="oe:children[oe:rm_type_name='SECTION']">
        <xsl:variable name="cardinality">
            <xsl:call-template name="get-cardinality">
                <xsl:with-param name="lower" select="oe:occurrences/oe:lower"/>
                <xsl:with-param name="lower_included" select="oe:occurrences/oe:lower_included"/>
                <xsl:with-param name="lower_unbounded" select="oe:occurrences/oe:lower_unbounded"/>
                <xsl:with-param name="upper" select="oe:occurrences/oe:upper"/>
                <xsl:with-param name="upper_included" select="oe:occurrences/oe:upper_included"/>
                <xsl:with-param name="upper_unbounded" select="oe:occurrences/oe:upper_unbounded"/>
                <xsl:with-param name="is_ordered" select="oe:attributes/oe:cardinality/oe:is_ordered"/>
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="translated-type">
            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">Section</xsl:with-param>
            </xsl:call-template>
        </xsl:variable>
        <tr>
            <xsl:choose>
                <xsl:when test="$show-comments-flag='true'">
                    <xsl:text disable-output-escaping="yes">&lt;td colspan="6"&gt;</xsl:text>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:text disable-output-escaping="yes">&lt;td colspan="5"&gt;</xsl:text>
                </xsl:otherwise>
            </xsl:choose>

            <li class="sub-section-icon">
                <xsl:text disable-output-escaping="yes">&lt;b&gt;</xsl:text><xsl:call-template
                    name="get-archetype-term">
                    <xsl:with-param name="at-code" select="oe:node_id"/>
                </xsl:call-template>:
                    <xsl:text disable-output-escaping="yes">&lt;i&gt;</xsl:text><xsl:value-of
                    select="$translated-type"
                /><xsl:text disable-output-escaping="yes">&lt;/i&gt;&lt;/b&gt;</xsl:text>
                - <xsl:call-template name="get-archetype-description">
                    <xsl:with-param name="at-code" select="oe:node_id"/>
                </xsl:call-template>, <i>
                    <xsl:value-of select="$cardinality"/>
                    <xsl:text disable-output-escaping="yes">&lt;/i&gt;</xsl:text>
                </i>
            </li>
            <xsl:if test="oe:attributes[oe:rm_attribute_name='items']/oe:children">
                <table border="{$border}" cellpadding="2" width="100%"
                    style="margin-left:20px;margin-bottom:10px" bgcolor="white" frame="{$frame}"
                    rules="{$rules}">
                    <xsl:call-template name="generate-section-or-slot-header">
                        <xsl:with-param name="currNode" select="oe:attributes[oe:rm_attribute_name='items']"/>
                    </xsl:call-template>
                    <tr>
                        <xsl:apply-templates select="oe:attributes[oe:rm_attribute_name='items']/oe:children"/>
                    </tr>
                </table>
            </xsl:if>
            <xsl:text disable-output-escaping="yes">&lt;/td&gt;</xsl:text>
        </tr>
    </xsl:template>

    <!-- **** ARCHETYPE SLOTS **** -->

    <!-- generate ARCHETYPE SLOT -->
    <xsl:template match="oe:children[@xi:type='ARCHETYPE_SLOT']" priority="1">
        <xsl:variable name="cardinality">
            <xsl:call-template name="get-cardinality">
                <xsl:with-param name="lower" select="oe:occurrences/oe:lower"/>
                <xsl:with-param name="lower_included" select="oe:occurrences/oe:lower_included"/>
                <xsl:with-param name="lower_unbounded" select="oe:occurrences/oe:lower_unbounded"/>
                <xsl:with-param name="upper" select="oe:occurrences/oe:upper"/>
                <xsl:with-param name="upper_included" select="oe:occurrences/oe:upper_included"/>
                <xsl:with-param name="upper_unbounded" select="oe:occurrences/oe:upper_unbounded"/>
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="temp-type">
            <xsl:call-template name="remove_underscore">
                <xsl:with-param name="string">
                    <xsl:value-of select="oe:rm_type_name"/>
                </xsl:with-param>
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="type">
            <xsl:choose>
                <xsl:when test="$temp-type='ADMIN ENTRY'">Administration entry</xsl:when>
                <xsl:otherwise>
                    <xsl:value-of select="$temp-type"/>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:variable>
        <xsl:variable name="translated-type">
            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric" select="$type"/>
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="preceding-node-position" select="position()-1"/>
        <xsl:if test="(position()!=1) and (../node()[(position()=$preceding-node-position) and (@xs:type!='ARCHETYPE_SLOT')])">
            <tr id="adlhtml-header-GENERIC-tableHeader">
                <td>
                    <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                    <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                        <xsl:with-param name="original-rubric">Slot</xsl:with-param>
                    </xsl:call-template>
                </td>
                <xsl:call-template name="adlhtml-header-SLOT-tableHeader"/>
            </tr>
        </xsl:if>
        <tr>
            <xsl:choose>
                <xsl:when
                    test="$temp-type='SECTION'
                    or $temp-type='OBSERVATION'
                    or $temp-type='EVALUATION'
                    or $temp-type='INSTRUCTION'
                    or $temp-type='ADMIN ENTRY'
                    or $temp-type='ACTION'">
                    <td class="entry-icon">
                        <xsl:text disable-output-escaping="yes">&lt;b&gt;&lt;i&gt;</xsl:text>
                        <xsl:value-of select="$translated-type"/>
                        <xsl:text disable-output-escaping="yes">&lt;/i&gt;&lt;/b&gt;</xsl:text>
                    </td>
                </xsl:when>
                <xsl:when test="$temp-type='ITEM TREE' or $temp-type='ITEM LIST' or $temp-type='ITEM TABLE' or $temp-type='ITEM SINGLE'
                    or $temp-type='CLUSTER' or $temp-type='ELEMENT'">
                    <td>
                        <img border="0" src="{$images-path}/slot-small.gif" align="middle"/>
                        <xsl:text disable-output-escaping="yes">&lt;b&gt;&lt;i&gt;</xsl:text>
                        <xsl:value-of select="$translated-type"/>
                        <xsl:text disable-output-escaping="yes">&lt;/i&gt;&lt;/b&gt;</xsl:text>
                    </td>
                </xsl:when>
            </xsl:choose>
            <td width="30%">
                <xsl:if test="oe:includes">
                    <xsl:call-template name="get-include-or-exclude-archetypes">
                        <xsl:with-param name="rm-type-name" select="oe:rm_type_name"/>
                        <xsl:with-param name="include-or-exclude">include</xsl:with-param>
                        <xsl:with-param name="include-or-exclude-node" select="oe:includes"/>
                    </xsl:call-template>
                </xsl:if>
                <xsl:if test="oe:excludes">
                    <hr/>
                    <xsl:call-template name="get-include-or-exclude-archetypes">
                        <xsl:with-param name="rm-type-name" select="oe:rm_type_name"/>
                        <xsl:with-param name="include-or-exclude">exclude</xsl:with-param>
                        <xsl:with-param name="include-or-exclude-node" select="oe:excludes"/>
                    </xsl:call-template>
                </xsl:if>
            </td>
            <td width="30%">
                <xsl:text disable-output-escaping="yes">&lt;b&gt;&lt;i&gt;</xsl:text>
                <xsl:value-of select="$translated-type"/>
                <xsl:text disable-output-escaping="yes">&lt;/i&gt;&lt;/b&gt;&lt;br/&gt;</xsl:text>
                <xsl:value-of select="$cardinality"/>
            </td>
            <xsl:if test="$show-comments-flag='true'">
                <td/>
            </xsl:if>
        </tr>
    </xsl:template>

    <!-- **** ENTRIES **** -->

    <!-- generate OBSERVATION root -->
    <xsl:template match="oe:definition[oe:rm_type_name='OBSERVATION']">
        <xsl:for-each
            select="oe:attributes[oe:rm_attribute_name='data']/oe:children/oe:attributes[oe:rm_attribute_name='events']/oe:children[position()=1]">
            <xsl:variable name="rm_type_name">
                <xsl:value-of
                    select="oe:attributes[oe:rm_attribute_name='data']/oe:children/oe:rm_type_name"
                />
            </xsl:variable>
            <xsl:variable name="rm-type">
                <xsl:call-template name="remove_underscore">
                    <xsl:with-param name="string">
                        <xsl:value-of select="substring-after($rm_type_name, 'ITEM_')"/>
                    </xsl:with-param>
                </xsl:call-template>
            </xsl:variable>
            <xsl:variable name="translated-rm-type">
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric" select="$rm-type"/>
                </xsl:call-template>
            </xsl:variable>
            <div id="adlhtml-header-DATA" align="left"><!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">data</xsl:with-param>
                </xsl:call-template>: <xsl:value-of select="$translated-rm-type"/>
                <xsl:if test="oe:attributes[oe:rm_attribute_name='data']/oe:children/oe:attributes/oe:cardinality/oe:is_ordered='true'"> (<xsl:value-of select="$ordered"/>)</xsl:if>
            </div>
            
            <table width="100%" frame="{$frame}" rules="{$rules}">
                <tbody>
                    <xsl:call-template name="generate-common-or-slot-header">
                        <xsl:with-param name="currNode" select="oe:attributes[oe:rm_attribute_name='data']"/>
                    </xsl:call-template>
                    <xsl:apply-templates select="oe:attributes[oe:rm_attribute_name='data']/oe:children"/>
                </tbody>
            </table>
        </xsl:for-each>
        <xsl:for-each
            select="oe:attributes[oe:rm_attribute_name='data']/oe:children/oe:attributes[oe:rm_attribute_name='events']/oe:children/oe:attributes[oe:rm_attribute_name='state']/oe:children/oe:attributes">
            <xsl:variable name="rm-type">
                <xsl:call-template name="remove_underscore">
                    <xsl:with-param name="string">
                        <xsl:value-of select="substring-after(../oe:rm_type_name, 'ITEM_')"/>
                    </xsl:with-param>
                </xsl:call-template>
            </xsl:variable>
            <div id="adlhtml-header-STATE"><!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">state</xsl:with-param>
                </xsl:call-template>: <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric" select="$rm-type"/>
                </xsl:call-template>
                <xsl:if test="(parent::oe:children[@xi:type='C_COMPLEX_OBJECT']) and (oe:cardinality/oe:is_ordered='true')"> (<xsl:value-of select="$ordered"/>)</xsl:if>
            </div>
            <table width="100%" frame="{$frame}" rules="{$rules}">
                <tbody>
                    <tr id="adlhtml-header-GENERIC-tableHeader">
                        <xsl:choose>
                            <xsl:when test="oe:children[(position()=1) and (@xi:type='ARCHETYPE_SLOT')]">
                                <td>
                                    <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                    <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                                        <xsl:with-param name="original-rubric">Slot</xsl:with-param>
                                    </xsl:call-template>
                                </td>
                                <xsl:call-template name="adlhtml-header-COMMON-tableHeader"/>
                            </xsl:when>
                            <xsl:otherwise>
                                <td>
                                    <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                    <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                                        <xsl:with-param name="original-rubric">State</xsl:with-param>
                                    </xsl:call-template>
                                </td>
                                <xsl:call-template name="adlhtml-header-COMMON-tableHeader"/>
                            </xsl:otherwise>
                        </xsl:choose>
                    </tr>
                    <xsl:apply-templates select="parent::node()"/>
                </tbody>
            </table>
        </xsl:for-each>
        <xsl:if
            test="oe:attributes[oe:rm_attribute_name='data']/oe:children/oe:attributes[oe:rm_attribute_name='events']/oe:children">
            <div id="adlhtml-header-EVENTS"><!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:variable name="rm-type">
                    <xsl:value-of
                        select="oe:attributes[oe:rm_attribute_name='data']/oe:children/oe:rm_type_name"
                    />
                </xsl:variable>
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">events</xsl:with-param>
                </xsl:call-template>: <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric" select="$rm-type"/>
                </xsl:call-template></div>
            <table width="100%" frame="{$frame}" rules="{$rules}">
                <tbody>
                    <tr id="adlhtml-header-GENERIC-tableHeader">
                        <td>
                            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                                <xsl:with-param name="original-rubric">Events</xsl:with-param>
                            </xsl:call-template>
                        </td>
                        <xsl:call-template name="adlhtml-header-COMMON-tableHeader"/>
                    </tr>
                    <xsl:apply-templates
                        select="oe:attributes[oe:rm_attribute_name='data']/oe:children/oe:attributes[oe:rm_attribute_name='events']/oe:children"
                    />
                </tbody>
            </table>
        </xsl:if>
        <xsl:apply-templates select="oe:attributes[oe:rm_attribute_name='protocol']"/>
    </xsl:template>

    <!-- generate INSTRUCTION root -->
    <xsl:template match="oe:definition[oe:rm_type_name='INSTRUCTION']">
        <xsl:variable name="action-archetype-id">
            <xsl:if
                test="oe:attributes[oe:rm_attribute_name='activities']/oe:children/oe:attributes[oe:rm_attribute_name='action_archetype_id']/oe:children/oe:item/oe:pattern">
                <xsl:call-template name="trim-archetype-id-pattern">
                    <xsl:with-param name="pattern"
                        select="oe:attributes[oe:rm_attribute_name='activities']/oe:children/oe:attributes[oe:rm_attribute_name='action_archetype_id']/oe:children/oe:item/oe:pattern"
                    />
                </xsl:call-template>
            </xsl:if>
        </xsl:variable>
        <xsl:variable name="action-archetype-file-name">
            <xsl:if test="$action-archetype-id != ''">
                <xsl:value-of select="substring-before(//oe:archetype_id/oe:value, '-')"
                    />-EHR-ACTION.<xsl:value-of select="$action-archetype-id"/>
            </xsl:if>
        </xsl:variable>
        <xsl:variable name="cardinality">
            <xsl:call-template name="get-cardinality">
                <xsl:with-param name="lower" select="oe:occurrences/oe:lower"/>
                <xsl:with-param name="lower_included" select="oe:occurrences/oe:lower_included"/>
                <xsl:with-param name="lower_unbounded" select="oe:occurrences/oe:lower_unbounded"/>
                <xsl:with-param name="upper" select="oe:occurrences/oe:upper"/>
                <xsl:with-param name="upper_included" select="oe:occurrences/oe:upper_included"/>
                <xsl:with-param name="upper_unbounded" select="oe:occurrences/oe:upper_unbounded"/>
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="rm_type_name">
            <xsl:value-of
                select="oe:attributes[oe:rm_attribute_name='activities']/oe:children/oe:attributes[oe:rm_attribute_name='description']/oe:children/oe:rm_type_name"
            />
        </xsl:variable>
        <div id="adlhtml-header-DATA" align="left"><!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">action</xsl:with-param>
            </xsl:call-template>: <xsl:if test="$action-archetype-file-name != ''">
                <font size="3">
                    <i>
                        <xsl:value-of select="$action-archetype-file-name"/>
                    </i>
                </font>
            </xsl:if></div>
        <table width="100%" border="{$border}" frame="{$frame}" rules="{$rules}">
            <tbody>
                <tr>
                    <td/>
                </tr>
            </tbody>
        </table>

        <xsl:variable name="temp">
            <xsl:value-of select="substring-before(//oe:archetype_id/oe:value, '-')"
                />-EHR-<xsl:value-of select="$rm_type_name"/>
        </xsl:variable>
        <xsl:variable name="rm-type">
            <xsl:call-template name="remove_underscore">
                <xsl:with-param name="string">
                    <xsl:value-of select="substring-after($rm_type_name, 'ITEM_')"/>
                </xsl:with-param>
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="translated-rm-type">
            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric" select="$rm-type"/>
            </xsl:call-template>
        </xsl:variable>
        <div id="adlhtml-header-DATA" align="left"><!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">activity description</xsl:with-param>
            </xsl:call-template>: <xsl:value-of select="$translated-rm-type"/></div>

        <table width="100%" border="{$border}" frame="{$frame}" rules="{$rules}">
            <tbody>
                <xsl:call-template name="generate-common-or-slot-header">
                    <xsl:with-param name="currNode" select="oe:attributes[oe:rm_attribute_name='activities']/oe:children/oe:attributes[oe:rm_attribute_name='description']"/>
                </xsl:call-template>
                <xsl:apply-templates select="oe:attributes[oe:rm_attribute_name='activities']/oe:children/oe:attributes[oe:rm_attribute_name='description']/oe:children"/>
            </tbody>
        </table>
        <xsl:apply-templates select="oe:attributes[oe:rm_attribute_name='protocol']"/>
    </xsl:template>

    <!-- generate ACTION root -->
    <xsl:template match="oe:definition[oe:rm_type_name='ACTION']">
        <xsl:variable name="cardinality">
            <xsl:call-template name="get-cardinality">
                <xsl:with-param name="lower" select="oe:occurrences/oe:lower"/>
                <xsl:with-param name="lower_included" select="oe:occurrences/oe:lower_included"/>
                <xsl:with-param name="lower_unbounded" select="oe:occurrences/oe:lower_unbounded"/>
                <xsl:with-param name="upper" select="oe:occurrences/oe:upper"/>
                <xsl:with-param name="upper_included" select="oe:occurrences/oe:upper_included"/>
                <xsl:with-param name="upper_unbounded" select="oe:occurrences/oe:upper_unbounded"/>
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="rm_type_name">
            <xsl:value-of
                select="oe:attributes[oe:rm_attribute_name='description']/oe:children/oe:rm_type_name"
            />
        </xsl:variable>
        <xsl:variable name="temp">
            <xsl:value-of select="substring-before(//oe:archetype_id/oe:value, '-')"
                />-EHR-<xsl:value-of select="$rm_type_name"/>
        </xsl:variable>
        <xsl:variable name="rm-type">
            <xsl:call-template name="remove_underscore">
                <xsl:with-param name="string">
                    <xsl:value-of select="substring-after($rm_type_name, 'ITEM_')"/>
                </xsl:with-param>
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="translated-rm-type">
            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric" select="$rm-type"/>
            </xsl:call-template>
        </xsl:variable>

        <div id="adlhtml-header-DATA" align="left"><!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">activity description</xsl:with-param>
            </xsl:call-template>: <xsl:value-of select="$translated-rm-type"/></div>

        <table width="100%" border="{$border}" frame="{$frame}" rules="{$rules}">
            <tbody>
                <xsl:call-template name="generate-common-or-slot-header">
                    <xsl:with-param name="currNode" select="oe:attributes[oe:rm_attribute_name='description']"/>
                </xsl:call-template>
                <xsl:apply-templates select="oe:attributes[oe:rm_attribute_name='description']/oe:children"/>
            </tbody>
        </table>

        <!-- process ism_transition -->
        <div id="adlhtml-header-PATHWAY" align="left">
            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">pathway</xsl:with-param>
            </xsl:call-template>
        </div>
        <table border="1" width="100%" frame="{$header-frame}" rules="{$header-rules}">
            <tbody>
                <tr>
                    <td id="adlhtml-header-ISM-TRANSITION-tableHeader">
                        <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                        <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                            <xsl:with-param name="original-rubric">postponed</xsl:with-param>
                        </xsl:call-template>
                    </td>
                    <td id="adlhtml-header-ISM-TRANSITION-tableHeader">
                        <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                        <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                            <xsl:with-param name="original-rubric">suspended</xsl:with-param>
                        </xsl:call-template>
                    </td>
                </tr>
                <tr>
                    <td>
                        <xsl:variable name="steps">
                            <xsl:for-each
                                select="oe:attributes[oe:rm_attribute_name='ism_transition']/oe:children/oe:attributes[oe:rm_attribute_name='current_state']">
                                <xsl:if
                                    test="oe:children/oe:attributes/oe:children/oe:code_list='527'">
                                    <xsl:for-each
                                        select="../oe:attributes[oe:rm_attribute_name='careflow_step']/oe:children/oe:attributes/oe:children/oe:code_list">
                                        <xsl:text disable-output-escaping="yes">&lt;li&gt;</xsl:text>
                                        <xsl:variable name="pathway-step">
                                            <xsl:call-template name="get-archetype-term">
                                                <xsl:with-param name="at-code">
                                                  <xsl:value-of select="."/>
                                                </xsl:with-param>
                                            </xsl:call-template>
                                        </xsl:variable>
                                        <xsl:value-of select="$pathway-step"/>
                                        <xsl:text disable-output-escaping="yes">&lt;/li&gt;</xsl:text>
                                    </xsl:for-each>
                                </xsl:if>
                            </xsl:for-each>
                        </xsl:variable>
                        <xsl:if test="string-length($steps)>0">
                            <ul class="left-align">
                                <xsl:value-of select="$steps" disable-output-escaping="yes"/>
                            </ul>
                        </xsl:if>
                    </td>
                    <td>
                        <xsl:variable name="steps">
                            <xsl:for-each
                                select="oe:attributes[oe:rm_attribute_name='ism_transition']/oe:children/oe:attributes[oe:rm_attribute_name='current_state']">
                                <xsl:if
                                    test="oe:children/oe:attributes/oe:children/oe:code_list='530'">
                                    <xsl:for-each
                                        select="../oe:attributes[oe:rm_attribute_name='careflow_step']/oe:children/oe:attributes/oe:children/oe:code_list">
                                        <xsl:text disable-output-escaping="yes">&lt;li&gt;</xsl:text>
                                        <xsl:variable name="pathway-step">
                                            <xsl:call-template name="get-archetype-term">
                                                <xsl:with-param name="at-code">
                                                  <xsl:value-of select="."/>
                                                </xsl:with-param>
                                            </xsl:call-template>
                                        </xsl:variable>
                                        <xsl:value-of select="$pathway-step"/>
                                        <xsl:text disable-output-escaping="yes">&lt;/li&gt;</xsl:text>
                                    </xsl:for-each>
                                </xsl:if>
                            </xsl:for-each>
                        </xsl:variable>
                        <xsl:if test="string-length($steps)>0">
                            <ul class="left-align">
                                <xsl:value-of select="$steps" disable-output-escaping="yes"/>
                            </ul>
                        </xsl:if>
                    </td>
                </tr>
                <tr>
                    <td id="adlhtml-header-ISM-TRANSITION-tableHeader">
                        <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                        <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                            <xsl:with-param name="original-rubric">planned</xsl:with-param>
                        </xsl:call-template>
                    </td>
                    <td id="adlhtml-header-ISM-TRANSITION-tableHeader">
                        <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                        <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                            <xsl:with-param name="original-rubric">active</xsl:with-param>
                        </xsl:call-template>
                    </td>
                    <td id="adlhtml-header-ISM-TRANSITION-tableHeader">
                        <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                        <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                            <xsl:with-param name="original-rubric">completed</xsl:with-param>
                        </xsl:call-template>
                    </td>
                </tr>
                <tr>
                    <td>
                        <xsl:variable name="steps">
                            <xsl:for-each
                                select="oe:attributes[oe:rm_attribute_name='ism_transition']/oe:children/oe:attributes[oe:rm_attribute_name='current_state']">
                                <xsl:if
                                    test="oe:children/oe:attributes/oe:children/oe:code_list='526'">
                                    <xsl:for-each
                                        select="../oe:attributes[oe:rm_attribute_name='careflow_step']/oe:children/oe:attributes/oe:children/oe:code_list">
                                        <xsl:text disable-output-escaping="yes">&lt;li&gt;</xsl:text>
                                        <xsl:variable name="pathway-step">
                                            <xsl:call-template name="get-archetype-term">
                                                <xsl:with-param name="at-code">
                                                  <xsl:value-of select="."/>
                                                </xsl:with-param>
                                            </xsl:call-template>
                                        </xsl:variable>
                                        <xsl:value-of select="$pathway-step"/>
                                        <xsl:text disable-output-escaping="yes">&lt;/li&gt;</xsl:text>
                                    </xsl:for-each>
                                </xsl:if>
                            </xsl:for-each>
                        </xsl:variable>
                        <xsl:if test="string-length($steps)>0">
                            <ul class="left-align">
                                <xsl:value-of select="$steps" disable-output-escaping="yes"/>
                            </ul>
                        </xsl:if>
                    </td>
                    <td>
                        <xsl:variable name="steps">
                            <xsl:for-each
                                select="oe:attributes[oe:rm_attribute_name='ism_transition']/oe:children/oe:attributes[oe:rm_attribute_name='current_state']">
                                <xsl:if
                                    test="oe:children/oe:attributes/oe:children/oe:code_list='245'">
                                    <xsl:for-each
                                        select="../oe:attributes[oe:rm_attribute_name='careflow_step']/oe:children/oe:attributes/oe:children/oe:code_list">
                                        <xsl:text disable-output-escaping="yes">&lt;li&gt;</xsl:text>
                                        <xsl:variable name="pathway-step">
                                            <xsl:call-template name="get-archetype-term">
                                                <xsl:with-param name="at-code">
                                                  <xsl:value-of select="."/>
                                                </xsl:with-param>
                                            </xsl:call-template>
                                        </xsl:variable>
                                        <xsl:value-of select="$pathway-step"/>
                                        <xsl:text disable-output-escaping="yes">&lt;/li&gt;</xsl:text>
                                    </xsl:for-each>
                                </xsl:if>
                            </xsl:for-each>
                        </xsl:variable>
                        <xsl:if test="string-length($steps)>0">
                            <ul class="left-align">
                                <xsl:value-of select="$steps" disable-output-escaping="yes"/>
                            </ul>
                        </xsl:if>
                    </td>
                    <td>
                        <xsl:variable name="steps">
                            <xsl:for-each
                                select="oe:attributes[oe:rm_attribute_name='ism_transition']/oe:children/oe:attributes[oe:rm_attribute_name='current_state']">
                                <xsl:if
                                    test="oe:children/oe:attributes/oe:children/oe:code_list='532'">
                                    <xsl:for-each
                                        select="../oe:attributes[oe:rm_attribute_name='careflow_step']/oe:children/oe:attributes/oe:children/oe:code_list">
                                        <xsl:text disable-output-escaping="yes">&lt;li&gt;</xsl:text>
                                        <xsl:variable name="pathway-step">
                                            <xsl:call-template name="get-archetype-term">
                                                <xsl:with-param name="at-code">
                                                  <xsl:value-of select="."/>
                                                </xsl:with-param>
                                            </xsl:call-template>
                                        </xsl:variable>
                                        <xsl:value-of select="$pathway-step"/>
                                        <xsl:text disable-output-escaping="yes">&lt;/li&gt;</xsl:text>
                                    </xsl:for-each>
                                </xsl:if>
                            </xsl:for-each>
                        </xsl:variable>
                        <xsl:if test="string-length($steps)>0">
                            <ul class="left-align">
                                <xsl:value-of select="$steps" disable-output-escaping="yes"/>
                            </ul>
                        </xsl:if>
                    </td>
                </tr>
                <tr>
                    <td id="adlhtml-header-ISM-TRANSITION-tableHeader">
                        <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                        <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                            <xsl:with-param name="original-rubric">cancelled</xsl:with-param>
                        </xsl:call-template>
                    </td>
                    <td id="adlhtml-header-ISM-TRANSITION-tableHeader">
                        <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                        <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                            <xsl:with-param name="original-rubric">aborted</xsl:with-param>
                        </xsl:call-template>
                    </td>
                </tr>
                <tr>
                    <td>
                        <xsl:variable name="steps">
                            <xsl:for-each
                                select="oe:attributes[oe:rm_attribute_name='ism_transition']/oe:children/oe:attributes[oe:rm_attribute_name='current_state']">
                                <xsl:if
                                    test="oe:children/oe:attributes/oe:children/oe:code_list='528'">
                                    <xsl:for-each
                                        select="../oe:attributes[oe:rm_attribute_name='careflow_step']/oe:children/oe:attributes/oe:children/oe:code_list">
                                        <xsl:text disable-output-escaping="yes">&lt;li&gt;</xsl:text>
                                        <xsl:variable name="pathway-step">
                                            <xsl:call-template name="get-archetype-term">
                                                <xsl:with-param name="at-code">
                                                  <xsl:value-of select="."/>
                                                </xsl:with-param>
                                            </xsl:call-template>
                                        </xsl:variable>
                                        <xsl:value-of select="$pathway-step"/>
                                        <xsl:text disable-output-escaping="yes">&lt;/li&gt;</xsl:text>
                                    </xsl:for-each>
                                </xsl:if>
                            </xsl:for-each>
                        </xsl:variable>
                        <xsl:if test="string-length($steps)>0">
                            <ul class="left-align">
                                <xsl:value-of select="$steps" disable-output-escaping="yes"/>
                            </ul>
                        </xsl:if>
                    </td>
                    <td>
                        <xsl:variable name="steps">
                            <xsl:for-each
                                select="oe:attributes[oe:rm_attribute_name='ism_transition']/oe:children/oe:attributes[oe:rm_attribute_name='current_state']">
                                <xsl:if
                                    test="oe:children/oe:attributes/oe:children/oe:code_list='531'">
                                    <xsl:for-each
                                        select="../oe:attributes[oe:rm_attribute_name='careflow_step']/oe:children/oe:attributes/oe:children/oe:code_list">
                                        <xsl:text disable-output-escaping="yes">&lt;li&gt;</xsl:text>
                                        <xsl:variable name="pathway-step">
                                            <xsl:call-template name="get-archetype-term">
                                                <xsl:with-param name="at-code">
                                                  <xsl:value-of select="."/>
                                                </xsl:with-param>
                                            </xsl:call-template>
                                        </xsl:variable>
                                        <xsl:value-of select="$pathway-step"/>
                                        <xsl:text disable-output-escaping="yes">&lt;/li&gt;</xsl:text>
                                    </xsl:for-each>
                                </xsl:if>
                            </xsl:for-each>
                        </xsl:variable>
                        <xsl:if test="string-length($steps)>0">
                            <ul class="left-align">
                                <xsl:value-of select="$steps" disable-output-escaping="yes"/>
                            </ul>
                        </xsl:if>
                    </td>
                </tr>
            </tbody>
        </table>
        <!-- end of processing ism_transition -->

        <xsl:apply-templates select="oe:attributes[oe:rm_attribute_name='protocol']"/>
    </xsl:template>

    <!-- generate EVALUATION root -->
    <xsl:template match="oe:definition[oe:rm_type_name='EVALUATION']">
        <xsl:variable name="rm_type_name">
            <xsl:value-of
                select="oe:attributes[oe:rm_attribute_name='data']/oe:children/oe:rm_type_name"/>
        </xsl:variable>
        <xsl:variable name="rm-type">
            <xsl:call-template name="remove_underscore">
                <xsl:with-param name="string">
                    <xsl:value-of select="substring-after($rm_type_name, 'ITEM_')"/>
                </xsl:with-param>
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="translated-rm-type">
            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric" select="$rm-type"/>
            </xsl:call-template>
        </xsl:variable>
        <div id="adlhtml-header-DATA" align="left"><!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">data</xsl:with-param>
            </xsl:call-template>: <xsl:value-of select="$translated-rm-type"/>
            <xsl:if test="oe:attributes[oe:rm_attribute_name='data']/oe:children/oe:attributes/oe:cardinality/oe:is_ordered='true'"> (<xsl:value-of select="$ordered"/>)</xsl:if>    
        </div>
        <table width="100%" border="{$border}" frame="{$frame}" rules="{$rules}">
            <tbody>
                <xsl:call-template name="generate-common-or-slot-header">
                    <xsl:with-param name="currNode" select="oe:attributes[oe:rm_attribute_name='data']"/>
                </xsl:call-template>
                <xsl:apply-templates select="oe:attributes[oe:rm_attribute_name='data']/oe:children"/>
            </tbody>
        </table>
        <xsl:apply-templates select="oe:attributes[oe:rm_attribute_name='protocol']"/>
    </xsl:template>

    <!-- generate ADMIN_ENTRY root -->
    <xsl:template match="oe:definition[oe:rm_type_name='ADMIN_ENTRY']">
        <xsl:variable name="rm_type_name">
            <xsl:value-of
                select="oe:attributes[oe:rm_attribute_name='data']/oe:children/oe:rm_type_name"/>
        </xsl:variable>
        <xsl:variable name="rm-type">
            <xsl:call-template name="remove_underscore">
                <xsl:with-param name="string">
                    <xsl:value-of select="substring-after($rm_type_name, 'ITEM_')"/>
                </xsl:with-param>
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="translated-rm-type">
            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric" select="$rm-type"/>
            </xsl:call-template>
        </xsl:variable>
        <div id="adlhtml-header-DATA" align="left"><!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">data</xsl:with-param>
            </xsl:call-template>: <xsl:value-of select="$translated-rm-type"/>
            <xsl:if test="oe:attributes[oe:rm_attribute_name='data']/oe:children/oe:attributes/oe:cardinality/oe:is_ordered='true'"> (<xsl:value-of select="$ordered"/>)</xsl:if>
        </div>
        <table width="100%" border="{$border}" frame="{$frame}" rules="{$rules}">
            <tbody>
                <xsl:call-template name="generate-common-or-slot-header">
                    <xsl:with-param name="currNode" select="oe:attributes[oe:rm_attribute_name='data']"/>
                </xsl:call-template>
                <xsl:apply-templates select="oe:attributes[oe:rm_attribute_name='data']/oe:children"
                />
            </tbody>
        </table>
    </xsl:template>

    <!-- **** STRUCTURES  **** -->

    <!-- generate EVENT -->
    <xsl:template match="oe:children[oe:rm_type_name='EVENT']">
        <xsl:variable name="node_id">
            <xsl:value-of select="oe:node_id"/>
        </xsl:variable>
        <xsl:variable name="cardinality">
            <xsl:call-template name="get-cardinality">
                <xsl:with-param name="lower" select="oe:occurrences/oe:lower"/>
                <xsl:with-param name="lower_included" select="oe:occurrences/oe:lower_included"/>
                <xsl:with-param name="lower_unbounded" select="oe:occurrences/oe:lower_unbounded"/>
                <xsl:with-param name="upper" select="oe:occurrences/oe:upper"/>
                <xsl:with-param name="upper_included" select="oe:occurrences/oe:upper_included"/>
                <xsl:with-param name="upper_unbounded" select="oe:occurrences/oe:upper_unbounded"/>
            </xsl:call-template>
        </xsl:variable>
        <tr>
            <td class="event">
                <xsl:call-template name="get-archetype-term">
                    <xsl:with-param name="at-code" select="$node_id"/>
                </xsl:call-template>
            </td>
            <td>
                <xsl:call-template name="get-archetype-description">
                    <xsl:with-param name="at-code" select="$node_id"/>
                </xsl:call-template>
            </td>
            <td>
                <xsl:text disable-output-escaping="yes">&lt;b&gt;&lt;i&gt;</xsl:text>
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">Event</xsl:with-param>
                </xsl:call-template>
                <xsl:text disable-output-escaping="yes">&lt;/i&gt;&lt;/b&gt;</xsl:text>
                <xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text>
                <xsl:value-of select="$cardinality"/>
            </td>
            <td/>
            <xsl:if test="$show-comments-flag='true'">
                <td>
                    <xsl:call-template name="get-node-comment">
                        <xsl:with-param name="at-code" select="$node_id"/>
                    </xsl:call-template>
                </td>
            </xsl:if>
        </tr>
    </xsl:template>

    <!-- generate POINT_EVENT -->
    <xsl:template match="oe:children[oe:rm_type_name='POINT_EVENT']">
        <xsl:variable name="node_id">
            <xsl:value-of select="oe:node_id"/>
        </xsl:variable>
        <xsl:variable name="cardinality">
            <xsl:call-template name="get-cardinality">
                <xsl:with-param name="lower" select="oe:occurrences/oe:lower"/>
                <xsl:with-param name="lower_included" select="oe:occurrences/oe:lower_included"/>
                <xsl:with-param name="lower_unbounded" select="oe:occurrences/oe:lower_unbounded"/>
                <xsl:with-param name="upper" select="oe:occurrences/oe:upper"/>
                <xsl:with-param name="upper_included" select="oe:occurrences/oe:upper_included"/>
                <xsl:with-param name="upper_unbounded" select="oe:occurrences/oe:upper_unbounded"/>
            </xsl:call-template>
        </xsl:variable>
        <tr>
            <td class="point-event">
                <xsl:call-template name="get-archetype-term">
                    <xsl:with-param name="at-code" select="$node_id"/>
                </xsl:call-template>
            </td>
            <td>
                <xsl:call-template name="get-archetype-description">
                    <xsl:with-param name="at-code" select="$node_id"/>
                </xsl:call-template>
            </td>
            <td>
                <xsl:text disable-output-escaping="yes">&lt;b&gt;&lt;i&gt;</xsl:text>
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">Point-in-time event</xsl:with-param>
                </xsl:call-template>
                <xsl:text disable-output-escaping="yes">&lt;/i&gt;&lt;/b&gt;</xsl:text>
                <xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text>
                <xsl:value-of select="$cardinality"/>
            </td>
            <td>
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">Offset</xsl:with-param>
                </xsl:call-template>
                <xsl:text> = </xsl:text>
                <xsl:call-template name="format-duration">
                    <xsl:with-param name="duration"
                        select="oe:attributes[oe:rm_attribute_name='offset']/oe:children/oe:attributes/oe:children/oe:item/oe:range/oe:lower"
                    />
                </xsl:call-template>
            </td>
            <xsl:if test="$show-comments-flag='true'">
                <td>
                    <xsl:call-template name="get-node-comment">
                        <xsl:with-param name="at-code" select="$node_id"/>
                    </xsl:call-template>
                </td>
            </xsl:if>
        </tr>
    </xsl:template>

    <!-- generate INTERVAL_EVENT -->
    <xsl:template match="oe:children[oe:rm_type_name='INTERVAL_EVENT']">
        <xsl:variable name="node_id">
            <xsl:value-of select="oe:node_id"/>
        </xsl:variable>
        <xsl:variable name="cardinality">
            <xsl:call-template name="get-cardinality">
                <xsl:with-param name="lower" select="oe:occurrences/oe:lower"/>
                <xsl:with-param name="lower_included" select="oe:occurrences/oe:lower_included"/>
                <xsl:with-param name="lower_unbounded" select="oe:occurrences/oe:lower_unbounded"/>
                <xsl:with-param name="upper" select="oe:occurrences/oe:upper"/>
                <xsl:with-param name="upper_included" select="oe:occurrences/oe:upper_included"/>
                <xsl:with-param name="upper_unbounded" select="oe:occurrences/oe:upper_unbounded"/>
            </xsl:call-template>
        </xsl:variable>
        <tr>
            <td class="interval-event">
                <xsl:call-template name="get-archetype-term">
                    <xsl:with-param name="at-code" select="$node_id"/>
                </xsl:call-template>
            </td>
            <td>
                <xsl:call-template name="get-archetype-description">
                    <xsl:with-param name="at-code" select="$node_id"/>
                </xsl:call-template>
            </td>
            <td>
                <xsl:text disable-output-escaping="yes">&lt;b&gt;&lt;i&gt;</xsl:text>
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">Interval event</xsl:with-param>
                </xsl:call-template>
                <xsl:text disable-output-escaping="yes">&lt;/i&gt;&lt;/b&gt;</xsl:text>
                <xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text>
                <xsl:value-of select="$cardinality"/>
            </td>
            <td>
                <xsl:if test="oe:attributes/oe:rm_attribute_name='width'">
                    <xsl:text>Width = </xsl:text>
                    <xsl:call-template name="format-duration">
                        <xsl:with-param name="duration"
                            select="oe:attributes[oe:rm_attribute_name='width']/oe:children/oe:attributes/oe:children/oe:item/oe:range/oe:lower"
                        />
                    </xsl:call-template>
                </xsl:if>
                <xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text>
                <xsl:if test="oe:attributes[oe:rm_attribute_name='math_function']">
                    <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                    <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                        <xsl:with-param name="original-rubric">Event math function</xsl:with-param>
                    </xsl:call-template>
                    <xsl:text> = </xsl:text>
                    <xsl:variable name="mathFunction">
                        <xsl:for-each
                            select="oe:attributes[oe:rm_attribute_name='math_function']/oe:children/oe:attributes[oe:rm_attribute_name='defining_code']/oe:children/oe:code_list">
                            <xsl:text>, </xsl:text>
                            <xsl:call-template name="LookupTerminologyAndTranslationByConceptId">
                                <xsl:with-param name="concept-id" select="."/>
                            </xsl:call-template>
                        </xsl:for-each>
                    </xsl:variable>
                    <xsl:value-of select="substring-after($mathFunction, ', ')"/>
                </xsl:if>
            </td>
            <xsl:if test="$show-comments-flag='true'">
                <td>
                    <xsl:call-template name="get-node-comment">
                        <xsl:with-param name="at-code" select="$node_id"/>
                    </xsl:call-template>
                </td>
            </xsl:if>
        </tr>
    </xsl:template>

    <!-- generate Protocol Structure -->
    <xsl:template match="*[oe:rm_attribute_name='protocol']">
        <xsl:variable name="rm-type">
            <xsl:value-of select="substring-after(oe:children/oe:rm_type_name, 'ITEM_')"/>
        </xsl:variable>
        <div id="adlhtml-header-PROTOCOL" align="left"><!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">protocol</xsl:with-param>
            </xsl:call-template>: <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric" select="$rm-type"/>
            </xsl:call-template>
            <xsl:if test="oe:children/oe:attributes/oe:cardinality/oe:is_ordered='true'"> (<xsl:value-of select="$ordered"/>)</xsl:if>
        </div>
        <table width="100%" frame="{$frame}" rules="{$rules}">
            <tbody>
                <xsl:call-template name="generate-common-or-slot-header">
                    <xsl:with-param name="currNode" select="."/>
                </xsl:call-template>
                <xsl:apply-templates select="oe:children"/>
            </tbody>
        </table>
    </xsl:template>

    <!-- generate Root structure -->
    <xsl:template name="generate-root-structure">
        <xsl:param name="currNode"/>
        <xsl:param name="rm-attribute-name"/>

        <table width="100%" border="{$border}" frame="{$frame}" rules="{$rules}">
            <tbody>
                <tr id="adlhtml-header-GENERIC-tableHeader">
                    <td>
                        <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                        <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                            <xsl:with-param name="original-rubric">Concept</xsl:with-param>
                        </xsl:call-template>
                    </td>
                    <xsl:call-template name="adlhtml-header-COMMON-tableHeader"/>
                </tr>
                <xsl:apply-templates
                    select="oe:attributes[oe:rm_attribute_name=$rm-attribute-name]/oe:children"/>
            </tbody>
        </table>
    </xsl:template>

    <!-- generate ITEM_TREE root -->
    <xsl:template match="oe:definition[oe:rm_type_name='ITEM_TREE']">
        <xsl:call-template name="generate-root-structure">
            <xsl:with-param name="currNode" select="."/>
            <xsl:with-param name="rm-attribute-name">items</xsl:with-param>
        </xsl:call-template>
    </xsl:template>

    <!-- generate ITEM_TREE child -->
    <xsl:template match="oe:children[oe:rm_type_name='ITEM_TREE']">
        <xsl:apply-templates select="oe:attributes/oe:children"/>
    </xsl:template>

    <!-- generate ITEM_LIST root -->
    <xsl:template match="oe:definition[oe:rm_type_name='ITEM_LIST']">
        <xsl:call-template name="generate-root-structure">
            <xsl:with-param name="currNode" select="."/>
            <xsl:with-param name="rm-attribute-name">items</xsl:with-param>
        </xsl:call-template>
    </xsl:template>

    <!-- generate ITEM_LIST child -->
    <xsl:template match="oe:children[oe:rm_type_name='ITEM_LIST']">
        <xsl:apply-templates select="oe:attributes/oe:children"/>
    </xsl:template>

    <!-- generate ITEM_TABLE root -->
    <xsl:template match="oe:definition[oe:rm_type_name='ITEM_TABLE']">
        <xsl:call-template name="generate-root-structure">
            <xsl:with-param name="currNode" select="."/>
            <xsl:with-param name="rm-attribute-name">rows</xsl:with-param>
        </xsl:call-template>
    </xsl:template>

    <!-- generate ITEM_TABLE child -->
    <xsl:template match="oe:children[oe:rm_type_name='ITEM_TABLE']">
        <xsl:apply-templates select="oe:attributes/oe:children"/>
    </xsl:template>

    <!-- generate ITEM_SINGLE root -->
    <xsl:template match="oe:definition[oe:rm_type_name='ITEM_SINGLE']">
        <xsl:call-template name="generate-root-structure">
            <xsl:with-param name="currNode" select="."/>
            <xsl:with-param name="rm-attribute-name">item</xsl:with-param>
        </xsl:call-template>
    </xsl:template>

    <!-- generate ITEM_SINGLE child -->
    <xsl:template match="oe:children[oe:rm_type_name='ITEM_SINGLE']">
        <xsl:apply-templates select="oe:attributes/oe:children"/>
    </xsl:template>

    <!-- **** REPRESENTATION  **** -->

    <!-- generate CLUSTER root or child -->
    <xsl:template match="*[oe:rm_type_name='CLUSTER']">
        
        <xsl:choose>
            <!-- ROOT -->
            <xsl:when test="name()='definition'">
                <table width="100%" border="{$border}" frame="{$frame}" rules="{$rules}">
                    <tbody>
                        <tr id="adlhtml-header-GENERIC-tableHeader">
                            <td>
                                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                                    <xsl:with-param name="original-rubric">Concept</xsl:with-param>
                                </xsl:call-template>
                            </td>
                            <xsl:call-template name="adlhtml-header-COMMON-tableHeader"/>
                        </tr>
                        <xsl:call-template name="generate-cluster-details">
                            <xsl:with-param name="currNode" select="."/>
                        </xsl:call-template>
                    </tbody>
                </table>
            </xsl:when>
            <xsl:when test="name()='children'">
                <xsl:call-template name="generate-cluster-details">
                    <xsl:with-param name="currNode" select="."/>
                </xsl:call-template>
            </xsl:when>
        </xsl:choose>
        
            
    </xsl:template>

    <!-- generate CLUSTER details -->
    <xsl:template name="generate-cluster-details">
        <xsl:param name="currNode"/>
        
        <xsl:variable name="node_id">
            <xsl:value-of select="$currNode/oe:node_id"/>
        </xsl:variable>

        <xsl:variable name="cardinality">
            <xsl:call-template name="get-cardinality">
                <xsl:with-param name="lower" select="$currNode/oe:occurrences/oe:lower"/>
                <xsl:with-param name="lower_included" select="$currNode/oe:occurrences/oe:lower_included"/>
                <xsl:with-param name="lower_unbounded" select="$currNode/oe:occurrences/oe:lower_unbounded"/>
                <xsl:with-param name="upper" select="$currNode/oe:occurrences/oe:upper"/>
                <xsl:with-param name="upper_included" select="$currNode/oe:occurrences/oe:upper_included"/>
                <xsl:with-param name="upper_unbounded" select="$currNode/oe:occurrences/oe:upper_unbounded"/>
                <xsl:with-param name="is_ordered" select="$currNode/oe:attributes/oe:cardinality/oe:is_ordered"/>
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="translated-rm-type">
            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">cluster</xsl:with-param>
            </xsl:call-template>
        </xsl:variable>

        <tr>
            <xsl:choose>
                <xsl:when test="$show-comments-flag='true'">
                    <xsl:text disable-output-escaping="yes">&lt;td colspan="6"&gt;</xsl:text>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:text disable-output-escaping="yes">&lt;td colspan="5"&gt;</xsl:text>
                </xsl:otherwise>
            </xsl:choose>

            <li class="cluster-icon">
                <xsl:text disable-output-escaping="yes">&lt;b&gt;</xsl:text><xsl:call-template
                    name="get-archetype-term">
                    <xsl:with-param name="at-code" select="$node_id"/>
                </xsl:call-template><xsl:text disable-output-escaping="yes">&lt;/b&gt;</xsl:text>
                
                <xsl:if test="((name($currNode)='definition') and (contains(//oe:archetype/oe:concept, '.')))
                    or
                    ((name($currNode)='children') and (contains($node_id, '.')))">
                
                    <xsl:variable name="parent-node-id">
                        <xsl:choose>
                            <!-- check if this node is an Archetype Root node and if it is a specialisation -->
                            <xsl:when test="(name($currNode)='definition') and (contains(//oe:archetype/oe:concept, '.'))">
                                <!-- get parent archetype concept name from the ontology based on the concept (ID). -->
                                <!-- also handles 'multi-depth' archetype node ID: e.g. at0000.1.1 (Histological diagnosis) -->
                                <xsl:call-template name="get-parent-id-from-specialised-id">
                                    <xsl:with-param name="specialised-id" select="//oe:archetype/oe:concept"/>
                                    <xsl:with-param name="remaining-str" select="//oe:archetype/oe:concept"/>
                                    <xsl:with-param name="parent-node-id"/>
                                </xsl:call-template>
                            </xsl:when>
                            <!-- check if this node is a Child Node and if it is a specialisation -->
                            <xsl:when test="(name($currNode)='children') and (contains($node_id, '.'))">
                                <xsl:call-template name="get-parent-id-from-specialised-id">
                                    <xsl:with-param name="specialised-id" select="$node_id"/>
                                    <xsl:with-param name="remaining-str" select="$node_id"/>
                                    <xsl:with-param name="parent-node-id"/>
                                </xsl:call-template>
                            </xsl:when>
                        </xsl:choose>                    
                    </xsl:variable>
                    
                    <xsl:variable name="parentConceptName">
                        <xsl:call-template name="get-archetype-term">
                            <xsl:with-param name="at-code" select="$parent-node-id"/>
                        </xsl:call-template>
                    </xsl:variable>
                    <xsl:if test="string-length($parentConceptName)>0">
                        <xsl:text disable-output-escaping="yes">&lt;i&gt;</xsl:text><font class="specialisation"> (specialisation of: <xsl:value-of select="$parentConceptName"/>) </font><xsl:text disable-output-escaping="yes">&lt;/i&gt;</xsl:text>                
                    </xsl:if>
                </xsl:if>
                
                <xsl:text> - </xsl:text><xsl:call-template name="get-archetype-description">
                    <xsl:with-param name="at-code" select="$node_id"/>
                </xsl:call-template>, <xsl:text disable-output-escaping="yes">&lt;i&gt;&lt;b&gt;</xsl:text><xsl:value-of
                    select="$translated-rm-type"
                /><xsl:text>  </xsl:text><xsl:text disable-output-escaping="yes">&lt;/i&gt;&lt;/b&gt;</xsl:text><i>
                    <xsl:value-of select="$cardinality"/>
                </i>
            </li>
            <table border="{$border}" cellpadding="2" width="100%"
                style="margin-left:20px;margin-bottom:10px" bgcolor="white" frame="{$frame}"
                rules="{$rules}">
                <xsl:call-template name="generate-common-or-slot-header">
                    <xsl:with-param name="currNode" select="oe:attributes[oe:rm_attribute_name='items']"/>
                </xsl:call-template>
                <tr>
                    <xsl:apply-templates select="oe:attributes[oe:rm_attribute_name='items']/oe:children"/>
                </tr>
            </table>
            <xsl:text disable-output-escaping="yes">&lt;/td&gt;</xsl:text>
        </tr>
    </xsl:template>

    <!-- ARCHETYPE_INTERNAL_REF -->
    <xsl:template match="*[oe:rm_type_name='ELEMENT' and oe:target_path]" priority="1">
        <!-- process 'target_path' value by getting last substring within [] chars to get the leaf node_id
            then apply-templates to the 'children' node with matching 'node-id'. -->
        <!--e.g. <target_path>/data[at0001]/items[at0073]/items[at0104]/items[at0105]</target_path>-->
        <xsl:variable name="leaf-atcode">
            <xsl:call-template name="process-archetype-internal-ref-target-path">
                <xsl:with-param name="path" select="oe:target_path"/>
            </xsl:call-template>
        </xsl:variable>
        <xsl:apply-templates select="//oe:children[oe:node_id=$leaf-atcode]">
            <xsl:with-param name="isReferencedElement">true</xsl:with-param><!-- flag to indicate that this ELEMENT is a copy/reference. -->
        </xsl:apply-templates>
    </xsl:template>

    <!-- get leaf 'atcode' in archetype_internal_ref target path -->
    <xsl:template name="process-archetype-internal-ref-target-path">
        <xsl:param name="path"/>
        <xsl:choose>
            <xsl:when test="contains($path, '[')">
                <xsl:call-template name="process-archetype-internal-ref-target-path">
                    <xsl:with-param name="path" select="substring-after($path, '[')"/>
                </xsl:call-template>
            </xsl:when>
            <xsl:when test="contains($path, ']')">
                <xsl:call-template name="process-archetype-internal-ref-target-path">
                    <xsl:with-param name="path" select="substring-before($path, ']')"/>
                </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="$path"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <!-- generate ELEMENT root -->
    <xsl:template match="oe:definition[oe:rm_type_name='ELEMENT']">
        <table width="100%" border="{$border}" frame="{$frame}" rules="{$rules}">
            <tbody>
                <tr id="adlhtml-header-GENERIC-tableHeader">
                    <td>
                        <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                        <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                            <xsl:with-param name="original-rubric">Concept</xsl:with-param>
                        </xsl:call-template>
                    </td>
                    <xsl:call-template name="adlhtml-header-COMMON-tableHeader"/>
                </tr>
                <xsl:call-template name="generate-element-details">
                    <xsl:with-param name="currNode" select="."/>
                </xsl:call-template>
            </tbody>
        </table>
    </xsl:template>

    <!-- generate Element details -->
    <xsl:template name="generate-element-details">
        <xsl:param name="currNode"/>

        <xsl:variable name="node_id">
            <xsl:value-of select="oe:node_id"/>
        </xsl:variable>
        <xsl:variable name="cardinality">
            <xsl:call-template name="get-cardinality">
                <xsl:with-param name="lower" select="oe:occurrences/oe:lower"/>
                <xsl:with-param name="lower_included" select="oe:occurrences/oe:lower_included"/>
                <xsl:with-param name="lower_unbounded" select="oe:occurrences/oe:lower_unbounded"/>
                <xsl:with-param name="upper" select="oe:occurrences/oe:upper"/>
                <xsl:with-param name="upper_included" select="oe:occurrences/oe:upper_included"/>
                <xsl:with-param name="upper_unbounded" select="oe:occurrences/oe:upper_unbounded"/>
            </xsl:call-template>
        </xsl:variable>

        <xsl:variable name="datatype-icon">
            <xsl:choose>
                <xsl:when test="count(oe:attributes[oe:rm_attribute_name='value']/oe:children)>1"
                    ><!-- use multiple choice datatype icon -->choice</xsl:when>
                <xsl:when
                    test="starts-with(oe:attributes[oe:rm_attribute_name='value']/oe:children/oe:rm_type_name, 'DV_INTERVAL')"
                    ><!-- use interval icon -->interval</xsl:when>
                <xsl:when
                    test="(oe:attributes[oe:rm_attribute_name='value']) and (not(starts-with(oe:attributes[oe:rm_attribute_name='value']/oe:children/oe:rm_type_name, 'DV_INTERVAL')))">
                    <xsl:call-template name="replace_underscore_with_dash">
                        <xsl:with-param name="string">
                            <xsl:value-of
                                select="translate(substring-after(oe:attributes[oe:rm_attribute_name='value']/oe:children/oe:rm_type_name, 'DV_'),$ucletters,$lcletters)"
                            />
                        </xsl:with-param>
                    </xsl:call-template>
                </xsl:when>
                <xsl:otherwise><!-- any datatype -->any</xsl:otherwise>
            </xsl:choose>
        </xsl:variable>

        <tr>
            <td class="{$datatype-icon}-icon">
                <xsl:call-template name="get-archetype-term">
                    <xsl:with-param name="at-code" select="$node_id"/>
                </xsl:call-template>
                
                <xsl:call-template name="get-term-binding">
                    <xsl:with-param name="at-code" select="$node_id"/>
                </xsl:call-template>
                
                <xsl:if test="((name($currNode)='definition') and (contains(//oe:archetype/oe:concept, '.')))
                    or
                    ((name($currNode)='children') and (contains($node_id, '.')))">
                    
                    <xsl:variable name="parent-node-id">
                        <xsl:choose>
                            <!-- check if this node is an Archetype Root node and if it is a specialisation -->
                            <xsl:when test="(name($currNode)='definition') and (contains(//oe:archetype/oe:concept, '.'))">
                                <!-- get parent archetype concept name from the ontology based on the concept (ID). -->
                                <!-- also handles 'multi-depth' archetype node ID: e.g. at0000.1.1 (Histological diagnosis) -->
                                <xsl:call-template name="get-parent-id-from-specialised-id">
                                    <xsl:with-param name="specialised-id" select="//oe:archetype/oe:concept"/>
                                    <xsl:with-param name="remaining-str" select="//oe:archetype/oe:concept"/>
                                    <xsl:with-param name="parent-node-id"/>
                                </xsl:call-template>
                            </xsl:when>
                            <!-- check if this node is a Child Node and if it is a specialisation -->
                            <xsl:when test="(name($currNode)='children') and (contains($node_id, '.'))">
                                <xsl:call-template name="get-parent-id-from-specialised-id">
                                    <xsl:with-param name="specialised-id" select="$node_id"/>
                                    <xsl:with-param name="remaining-str" select="$node_id"/>
                                    <xsl:with-param name="parent-node-id"/>
                                </xsl:call-template>
                            </xsl:when>
                        </xsl:choose>                    
                    </xsl:variable>
                    
                    <xsl:variable name="parentConceptName">
                        <xsl:call-template name="get-archetype-term">
                            <xsl:with-param name="at-code" select="$parent-node-id"/>
                        </xsl:call-template>
                    </xsl:variable>
                    <xsl:if test="string-length($parentConceptName)>0">
                        <xsl:text disable-output-escaping="yes">&lt;i&gt;</xsl:text><font class="specialisation"> (specialisation of: <xsl:value-of select="$parentConceptName"/>) </font><xsl:text disable-output-escaping="yes">&lt;/i&gt;</xsl:text>                
                    </xsl:if>
                </xsl:if>
            </td>
            <td width="30%">
                <xsl:call-template name="get-archetype-description">
                    <xsl:with-param name="at-code" select="$node_id"/>
                </xsl:call-template>
            </td>
            <td width="30%">
                <xsl:variable name="datatype-constraint">
                    <xsl:choose>
                        <xsl:when test="oe:attributes[oe:rm_attribute_name='value']">
                            <xsl:for-each
                                select="oe:attributes[oe:rm_attribute_name='value']/oe:children">
                                <!-- handles multiple datatypes -->
                                <xsl:text disable-output-escaping="yes">&lt;hr/&gt;</xsl:text>
                                <xsl:variable name="temp-data-type">
                                    <xsl:call-template name="remove_underscore">
                                        <xsl:with-param name="string">
                                            <xsl:value-of
                                                select="substring-after(oe:rm_type_name, 'DV_')"/>
                                        </xsl:with-param>
                                    </xsl:call-template>
                                </xsl:variable>
                                <xsl:variable name="data-type">
                                    <xsl:choose>
                                        <xsl:when test="$temp-data-type='URI'"><xsl:value-of
                                                select="$temp-data-type"/> - resource identifier</xsl:when>
                                        <xsl:when test="$temp-data-type='DATE TIME'">Date and time</xsl:when>
                                        <xsl:when test="contains($temp-data-type, 'INTERVAL')">
                                            <xsl:variable name="interval-type">
                                                <xsl:call-template name="remove_underscore">
                                                  <xsl:with-param name="string"
                                                  select="translate(substring-after(current()[contains(oe:rm_type_name, 'DV_INTERVAL')]/oe:attributes[oe:rm_attribute_name='upper']/oe:children/oe:rm_type_name, 'DV_'),$ucletters,$lcletters)"
                                                  />
                                                </xsl:call-template>
                                            </xsl:variable>
                                            <xsl:variable name="rubric-of-type-in-terminology">
                                                <xsl:choose>
                                                  <xsl:when test="$interval-type='count'">integer
                                                  (count)</xsl:when>
                                                  <xsl:when test="$interval-type='date time'">date
                                                  or time</xsl:when>
                                                  <!-- no matching rubric in terminology so just pass in whatever was there -->
                                                  <xsl:otherwise>
                                                  <xsl:value-of select="$interval-type"/>
                                                  </xsl:otherwise>
                                                </xsl:choose>
                                            </xsl:variable>
                                            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                            <xsl:call-template
                                                name="LookupTerminologyAndTranslationByRubric">
                                                <xsl:with-param name="original-rubric">Interval of
                                                  <xsl:value-of
                                                  select="$rubric-of-type-in-terminology"
                                                /></xsl:with-param>
                                            </xsl:call-template>
                                        </xsl:when>
                                        <xsl:otherwise>
                                            <xsl:value-of select="$temp-data-type"/>
                                        </xsl:otherwise>
                                    </xsl:choose>
                                </xsl:variable>
                                <xsl:text disable-output-escaping="yes">&lt;b&gt;&lt;i&gt;</xsl:text>
                                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                                    <xsl:with-param name="original-rubric" select="$data-type"/>
                                </xsl:call-template>
                                <xsl:text disable-output-escaping="yes">&lt;/i&gt;&lt;/b&gt;</xsl:text>
                                <xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text>
                                <xsl:value-of select="$cardinality"/>
                            </xsl:for-each>
                        </xsl:when>
                        <!-- 'Any' datatype -->
                        <xsl:otherwise>
                            <xsl:text disable-output-escaping="yes">&lt;hr/&gt;</xsl:text>
                            <xsl:text disable-output-escaping="yes">&lt;b&gt;&lt;i&gt;</xsl:text>
                            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                                <xsl:with-param name="original-rubric">Any</xsl:with-param>
                            </xsl:call-template>
                            <xsl:text disable-output-escaping="yes">&lt;/i&gt;&lt;/b&gt;</xsl:text>
                            <xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text>
                            <xsl:value-of select="$cardinality"/>
                        </xsl:otherwise>
                    </xsl:choose>
                </xsl:variable>
                <xsl:value-of select="substring-after($datatype-constraint, '&lt;hr/&gt;')"
                disable-output-escaping="yes"/>
                <!-- show any runtime name constraints for this ELEMENT -->
                <xsl:if test="string-length(oe:attributes[oe:rm_attribute_name='name']/oe:children/oe:attributes[oe:rm_attribute_name='defining_code']/oe:children/oe:code_list)>0">
                    <xsl:text disable-output-escaping="yes">&lt;br/&gt;&lt;br/&gt;</xsl:text>
                    <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                    <font class="runtime-constraint-name-label"><xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                        <xsl:with-param name="original-rubric">Runtime name constraint:</xsl:with-param>
                    </xsl:call-template></font>
                    <xsl:text disable-output-escaping="yes">&lt;br/&gt;{</xsl:text>
                    <xsl:variable name="runtime-constraint-names">
                        <xsl:for-each select="oe:attributes[oe:rm_attribute_name='name']/oe:children/oe:attributes[oe:rm_attribute_name='defining_code']/oe:children/oe:code_list">
                            <xsl:call-template name="get-archetype-term">
                                <xsl:with-param name="at-code" select="current()"/>
                            </xsl:call-template><xsl:text>, </xsl:text>
                        </xsl:for-each>
                    </xsl:variable>
                    <xsl:value-of select="substring($runtime-constraint-names, 0, string-length($runtime-constraint-names)-1)"/><xsl:text>}</xsl:text>
                </xsl:if>
            </td>
            <td width="20%">
                <xsl:variable name="value-constraint">
                    <xsl:for-each select="oe:attributes[oe:rm_attribute_name='value']/oe:children">
                        <!-- handles multiple datatypes -->
                        <xsl:text disable-output-escaping="yes">&lt;hr/&gt;</xsl:text>
                        <xsl:apply-templates select="."/>
                    </xsl:for-each>
                </xsl:variable>
                <xsl:value-of select="substring-after($value-constraint, '&lt;hr/&gt;')"
                    disable-output-escaping="yes"/>
            </td>
            <xsl:if test="$show-comments-flag='true'">
                <td>
                    <xsl:call-template name="get-node-comment">
                        <xsl:with-param name="at-code" select="$node_id"/>
                    </xsl:call-template>
                </td>
            </xsl:if>
        </tr>
    </xsl:template>

    <!-- generate ELEMENT child -->
    <xsl:template match="oe:children[oe:rm_type_name='ELEMENT']">
        <xsl:call-template name="generate-element-details">
            <xsl:with-param name="currNode" select="."/>
        </xsl:call-template>
    </xsl:template>


    <!-- **** ELEMENT DATA_VALUE TYPES **** -->

    <!-- generate DV_ORDINAL -->
    <xsl:template match="*[oe:rm_type_name='DV_ORDINAL']">
        <xsl:for-each select="oe:list">
            <xsl:variable name="code_string">
                <xsl:value-of select="oe:symbol/oe:defining_code/oe:code_string"/>
            </xsl:variable>
            <xsl:value-of select="oe:value"/>:
                <xsl:text disable-output-escaping="yes">&lt;i&gt;</xsl:text><xsl:call-template
                name="get-archetype-term">
                <xsl:with-param name="at-code" select="$code_string"/>
            </xsl:call-template><xsl:text disable-output-escaping="yes">&lt;/i&gt;&lt;br/&gt;</xsl:text>
        </xsl:for-each>
    </xsl:template>

    <!-- generate DV_TEXT -->
    <xsl:template match="*[oe:rm_type_name='DV_TEXT']">
        <xsl:choose>
            <xsl:when test="string-length(oe:attributes[oe:rm_attribute_name='value']/oe:children[oe:rm_type_name='STRING']/oe:item[@xi:type='C_STRING']/oe:pattern)>0">
                <!--Example: <pattern>"Capsule", "Gel", "Solution (infusion or injection)", "Inhaler", "Nasal drops", "Nebuliser", "Oral solution", "Patch", "Pessary", "Spray", "Suppository", "Tablet", "Topical solution"</pattern>-->
                <!-- assumes string pattern for a DV_TEXT value will have a pattern similar to above example -->
                <xsl:call-template name="generate-list-of-text-values">
                    <xsl:with-param name="stringPattern" select="oe:attributes[oe:rm_attribute_name='value']/oe:children[oe:rm_type_name='STRING']/oe:item[@xi:type='C_STRING']/oe:pattern"/>
                </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">free or coded text</xsl:with-param>
                </xsl:call-template>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <!-- generate DV_CODED_TEXT Value -->
    <xsl:template match="*[oe:rm_type_name='DV_CODED_TEXT']">
        <xsl:apply-templates
            select="oe:attributes[oe:rm_attribute_name='defining_code']/oe:children"/>
    </xsl:template>

    <!-- generate (Local) DV_CODED_TEXT Value -->
    <xsl:template match="oe:children[@*='C_CODE_PHRASE']">
        <xsl:variable name="code_list">
            <xsl:for-each select="oe:code_list">
                <xsl:text disable-output-escaping="yes">&lt;br&gt;</xsl:text>
                <xsl:call-template name="get-archetype-term">
                    <xsl:with-param name="at-code" select="."/>
                </xsl:call-template>
            </xsl:for-each>
        </xsl:variable>
        <xsl:variable name="trimmedCodeList">
            <xsl:value-of select="substring-after($code_list, '&lt;br&gt;')"/>
        </xsl:variable>
        <xsl:value-of select="$trimmedCodeList" disable-output-escaping="yes"/>
    </xsl:template>

    <!-- generate (External terminology) DV_CODED_TEXT Value -->
    <xsl:template match="oe:children[@*='CONSTRAINT_REF']">
        <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
        <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
            <xsl:with-param name="original-rubric">Terminology</xsl:with-param>
        </xsl:call-template>
        <xsl:if test="string-length(oe:reference)>0">: <xsl:call-template
                name="get-archetype-constraint-definition">
                <xsl:with-param name="at-code" select="oe:reference"/>
            </xsl:call-template></xsl:if>
    </xsl:template>

    <!-- generate DV_URI -->
    <xsl:template match="*[oe:rm_type_name='DV_URI']">
        <xsl:if
            test="string-length(oe:attributes[oe:rm_attribute_name='value']/oe:children/oe:item/oe:pattern)>0"
            > URI Pattern: '<xsl:value-of
                select="oe:attributes[oe:rm_attribute_name='value']/oe:children/oe:item/oe:pattern"
            />' </xsl:if>
    </xsl:template>

    <!-- generate DV_EHR_URI -->
    <xsl:template match="*[oe:rm_type_name='DV_EHR_URI']">
        <xsl:if test="oe:attributes[oe:rm_attribute_name='value']/oe:children/oe:item/oe:pattern">
            EHR URI Pattern: '<xsl:value-of
                select="oe:attributes[oe:rm_attribute_name='value']/oe:children/oe:item/oe:pattern"
            />' </xsl:if>
    </xsl:template>

    <!-- generate DV_IDENTIFIER -->
    <xsl:template match="*[oe:rm_type_name='DV_IDENTIFIER']">
        <xsl:if test="oe:attributes[oe:rm_attribute_name='issuer']">
            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">Issuer</xsl:with-param>
            </xsl:call-template>: <xsl:value-of
                select="oe:attributes[oe:rm_attribute_name='issuer']/oe:children/oe:item/oe:pattern"/>
            <xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text>
        </xsl:if>
        <xsl:if test="oe:attributes[oe:rm_attribute_name='type']">
            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">Type</xsl:with-param>
            </xsl:call-template>: <xsl:value-of
                select="oe:attributes[oe:rm_attribute_name='type']/oe:children/oe:item/oe:pattern"/>
            <xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text>
        </xsl:if>
        <xsl:if test="oe:attributes[oe:rm_attribute_name='id']"> Id: <xsl:value-of
                select="oe:attributes[oe:rm_attribute_name='id']/oe:children/oe:item/oe:pattern"/>
            <xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text>
        </xsl:if>
    </xsl:template>

    <!-- generate DV_TIME -->
    <xsl:template match="*[oe:rm_type_name='DV_TIME']">
        <xsl:variable name="time-pattern">
            <xsl:value-of
                select="translate(oe:attributes[oe:rm_attribute_name='value']/oe:children[oe:rm_type_name='TIME']/oe:item/oe:pattern,$lcletters,$ucletters)"
            />
        </xsl:variable>
        <xsl:if test="string-length($time-pattern)>0">
            <xsl:choose>
                <xsl:when test="$time-pattern='HH:MM:??'">
                    <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                    <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                        <xsl:with-param name="original-rubric">Partial time with
                        minutes</xsl:with-param>
                    </xsl:call-template>
                </xsl:when>
                <xsl:when test="$time-pattern='HH:??:XX'">
                    <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                    <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                        <xsl:with-param name="original-rubric">Partial time</xsl:with-param>
                    </xsl:call-template>
                </xsl:when>
                <xsl:when test="$time-pattern='HH:MM:SS'">
                    <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                    <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                        <xsl:with-param name="original-rubric">Full time hh:mm:ss</xsl:with-param>
                    </xsl:call-template>
                </xsl:when>
                <xsl:when test="$time-pattern='HH:??:??'">
                    <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                    <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                        <xsl:with-param name="original-rubric">Time only</xsl:with-param>
                    </xsl:call-template>
                </xsl:when>
            </xsl:choose><xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text><xsl:value-of select="translate(time-pattern,$ucletters,$lcletters)"/>
        </xsl:if>
        <xsl:if test="string-length($time-pattern)=0">
            <!-- assume time only -->
            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">Time only</xsl:with-param>
            </xsl:call-template><xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text><xsl:value-of select="translate(time-pattern,$ucletters,$lcletters)"/>
        </xsl:if>
    </xsl:template>

    <!-- generate DV_DATE -->
    <xsl:template match="*[oe:rm_type_name='DV_DATE']">
        <xsl:variable name="date-pattern">
            <xsl:value-of
                select="translate(oe:attributes[oe:rm_attribute_name='value']/oe:children[oe:rm_type_name='DATE']/oe:item/oe:pattern,$lcletters,$ucletters)"
            />
        </xsl:variable>
        <xsl:if test="string-length($date-pattern)>0">
            <xsl:choose>
                <xsl:when
                    test="oe:attributes[oe:rm_attribute_name='value']/oe:children[oe:rm_type_name='DATE']/oe:item/oe:pattern='YYYY-MM-DD'">
                    <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                    <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                        <xsl:with-param name="original-rubric">Full date</xsl:with-param>
                    </xsl:call-template>
                </xsl:when>
                <xsl:when test="$date-pattern='YYYY-MM-??'">
                    <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                    <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                        <xsl:with-param name="original-rubric">Partial date with
                        month</xsl:with-param>
                    </xsl:call-template>
                </xsl:when>
                <xsl:when test="$date-pattern='YYYY-??-??'">
                    <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                    <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                        <xsl:with-param name="original-rubric">Date only</xsl:with-param>
                    </xsl:call-template>
                </xsl:when>
                <xsl:when test="$date-pattern='YYYY-??-XX'">
                    <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                    <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                        <xsl:with-param name="original-rubric">Partial date</xsl:with-param>
                    </xsl:call-template>
                </xsl:when>
            </xsl:choose><xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text><xsl:value-of select="translate($date-pattern,$ucletters,$lcletters)"/>
        </xsl:if>
        <xsl:if test="string-length($date-pattern)=0">
            <!-- assume date only -->
            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">Date only</xsl:with-param>
            </xsl:call-template><xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text><xsl:value-of select="translate($date-pattern,$ucletters,$lcletters)"/>
        </xsl:if>
    </xsl:template>

    <!-- generate DV_DATE_TIME -->
    <xsl:template match="*[oe:rm_type_name='DV_DATE_TIME']">
        <xsl:variable name="date-time-pattern">
            <xsl:value-of
                select="translate(oe:attributes[oe:rm_attribute_name='value']/oe:children[oe:rm_type_name='DATE_TIME']/oe:item/oe:pattern,$lcletters,$ucletters)"
            />
        </xsl:variable>
        <xsl:if test="string-length($date-time-pattern)>0">
            <xsl:choose>
                <xsl:when test="$date-time-pattern='YYYY-MM-DDTHH:MM:SS'">
                    <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                    <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                        <xsl:with-param name="original-rubric">Date and time</xsl:with-param>
                    </xsl:call-template>
                </xsl:when>
                <xsl:when test="$date-time-pattern='YYYY-MM-DDTHH:??:??'">
                    <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                    <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                        <xsl:with-param name="original-rubric">Date and partial
                        time</xsl:with-param>
                    </xsl:call-template>
                </xsl:when>
                <xsl:when test="$date-time-pattern='YYYY-??-??T??:??:??'">
                    <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                    <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                        <xsl:with-param name="original-rubric">Allow all</xsl:with-param>
                    </xsl:call-template>
                </xsl:when>
            </xsl:choose><xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text><xsl:value-of select="translate($date-time-pattern,$ucletters,$lcletters)"/>
        </xsl:if>
        <xsl:if test="string-length($date-time-pattern)=0">
            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">Allow all</xsl:with-param>
            </xsl:call-template><xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text><xsl:value-of select="translate($date-time-pattern,$ucletters,$lcletters)"/>
        </xsl:if>
    </xsl:template>

    <!-- generate DV_BOOLEAN -->
    <xsl:template match="*[oe:rm_type_name='DV_BOOLEAN']">
        <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
        <xsl:choose>
            <xsl:when
                test="(oe:attributes[oe:rm_attribute_name='value']/oe:children[oe:rm_type_name='BOOLEAN']/oe:item/oe:true_valid='true')
                and oe:attributes[oe:rm_attribute_name='value']/oe:children[oe:rm_type_name='BOOLEAN']/oe:item/oe:false_valid='true'">
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">True/False</xsl:with-param>
                </xsl:call-template>
            </xsl:when>
            <xsl:when
                test="(oe:attributes[oe:rm_attribute_name='value']/oe:children[oe:rm_type_name='BOOLEAN']/oe:item/oe:true_valid='true')
                and oe:attributes[oe:rm_attribute_name='value']/oe:children[oe:rm_type_name='BOOLEAN']/oe:item/oe:false_valid='false'">
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">True</xsl:with-param>
                </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">False</xsl:with-param>
                </xsl:call-template>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <!-- generate DV_MULTIMEDIA -->
    <xsl:template match="*[oe:rm_type_name='DV_MULTIMEDIA']">
        <xsl:variable name="multimedia-list">
            <xsl:for-each
                select="oe:attributes[oe:rm_attribute_name='media_type']/oe:children[oe:rm_type_name='CODE_PHRASE']/oe:code_list">
                <xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text>
                <xsl:call-template name="LookupTerminologyAndTranslationByConceptId">
                    <xsl:with-param name="concept-id" select="."/>
                </xsl:call-template>
            </xsl:for-each>
        </xsl:variable>
        <xsl:if test="string-length($multimedia-list)>0">
            <xsl:value-of select="substring-after($multimedia-list, '&lt;br/&gt;')"/>
        </xsl:if>
    </xsl:template>

    <!-- generate DV_DURATION -->
    <xsl:template match="*[oe:rm_type_name='DV_DURATION']">
        <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
        <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
            <xsl:with-param name="original-rubric">units</xsl:with-param>
        </xsl:call-template>
        <xsl:text>: </xsl:text>
        <xsl:call-template name="get-duration-units">
            <xsl:with-param name="duration"
                select="oe:attributes[oe:rm_attribute_name='value']/oe:children/oe:item/oe:pattern"
            />
        </xsl:call-template>
    </xsl:template>

    <!-- generate DV_QUANTITY -->
    <xsl:template match="*[oe:rm_type_name='DV_QUANTITY']">
        <xsl:variable name="concept-id">
            <xsl:value-of select="oe:property/oe:code_string"/>
        </xsl:variable>

        <xsl:if test="string-length($concept-id)>0">
            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">Property</xsl:with-param>
            </xsl:call-template> = <xsl:call-template
                name="LookupTerminologyAndTranslationByConceptId">
                <xsl:with-param name="concept-id" select="$concept-id"/>
            </xsl:call-template>
            <xsl:for-each select="oe:list">
                <xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text><!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">Units</xsl:with-param>
                </xsl:call-template> = <xsl:value-of select="oe:units"/>; <xsl:call-template
                    name="get-interval-range">
                    <xsl:with-param name="lower" select="oe:magnitude/oe:lower"/>
                    <xsl:with-param name="lower_included" select="oe:magnitude/oe:lower_included"/>
                    <xsl:with-param name="lower_unbounded" select="oe:magnitude/oe:lower_unbounded"/>
                    <xsl:with-param name="upper" select="oe:magnitude/oe:upper"/>
                    <xsl:with-param name="upper_included" select="oe:magnitude/oe:upper_included"/>
                    <xsl:with-param name="upper_unbounded" select="oe:magnitude/oe:upper_unbounded"
                    />
                </xsl:call-template>
            </xsl:for-each>
        </xsl:if>
    </xsl:template>

    <!-- generate DV_COUNT -->
    <xsl:template match="*[oe:rm_type_name='DV_COUNT']">
        <xsl:call-template name="get-interval-range">
            <xsl:with-param name="lower"
                select="oe:attributes[oe:rm_attribute_name='magnitude']/oe:children/oe:item/oe:range/oe:lower"/>
            <xsl:with-param name="lower_included"
                select="oe:attributes[oe:rm_attribute_name='magnitude']/oe:children/oe:item/oe:range/oe:lower_included"/>
            <xsl:with-param name="lower_unbounded"
                select="oe:attributes[oe:rm_attribute_name='magnitude']/oe:children/oe:item/oe:range/oe:lower_unbounded"/>
            <xsl:with-param name="upper"
                select="oe:attributes[oe:rm_attribute_name='magnitude']/oe:children/oe:item/oe:range/oe:upper"/>
            <xsl:with-param name="upper_included"
                select="oe:attributes[oe:rm_attribute_name='magnitude']/oe:children/oe:item/oe:range/oe:upper_included"/>
            <xsl:with-param name="upper_unbounded"
                select="oe:attributes[oe:rm_attribute_name='magnitude']/oe:children/oe:item/oe:range/oe:upper_unbounded"
            />
        </xsl:call-template>
    </xsl:template>

    <!-- generate DV_PROPORTION -->
    <xsl:template match="*[oe:rm_type_name='DV_PROPORTION']">
        <xsl:variable name="numerator">
            <xsl:call-template name="get-interval-range">
                <xsl:with-param name="lower"
                    select="oe:attributes[oe:rm_attribute_name='numerator']/oe:children/oe:item/oe:range/oe:lower"/>
                <xsl:with-param name="lower_included"
                    select="oe:attributes[oe:rm_attribute_name='numerator']/oe:children/oe:item/oe:range/oe:lower_included"/>
                <xsl:with-param name="lower_unbounded"
                    select="oe:attributes[oe:rm_attribute_name='numerator']/oe:children/oe:item/oe:range/oe:lower_unbounded"/>
                <xsl:with-param name="upper"
                    select="oe:attributes[oe:rm_attribute_name='numerator']/oe:children/oe:item/oe:range/oe:upper"/>
                <xsl:with-param name="upper_included"
                    select="oe:attributes[oe:rm_attribute_name='numerator']/oe:children/oe:item/oe:range/oe:upper_included"/>
                <xsl:with-param name="upper_unbounded"
                    select="oe:attributes[oe:rm_attribute_name='numerator']/oe:children/oe:item/oe:range/oe:upper_unbounded"
                />
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="denominator">
            <xsl:call-template name="get-interval-range">
                <xsl:with-param name="lower"
                    select="oe:attributes[oe:rm_attribute_name='denominator']/oe:children/oe:item/oe:range/oe:lower"/>
                <xsl:with-param name="lower_included"
                    select="oe:attributes[oe:rm_attribute_name='denominator']/oe:children/oe:item/oe:range/oe:lower_included"/>
                <xsl:with-param name="lower_unbounded"
                    select="oe:attributes[oe:rm_attribute_name='denominator']/oe:children/oe:item/oe:range/oe:lower_unbounded"/>
                <xsl:with-param name="upper"
                    select="oe:attributes[oe:rm_attribute_name='denominator']/oe:children/oe:item/oe:range/oe:upper"/>
                <xsl:with-param name="upper_included"
                    select="oe:attributes[oe:rm_attribute_name='denominator']/oe:children/oe:item/oe:range/oe:upper_included"/>
                <xsl:with-param name="upper_unbounded"
                    select="oe:attributes[oe:rm_attribute_name='denominator']/oe:children/oe:item/oe:range/oe:upper_unbounded"
                />
            </xsl:call-template>
        </xsl:variable>
        <xsl:if test="string-length(oe:attributes[oe:rm_attribute_name='type']/oe:children/oe:item/oe:list)>0">
            <xsl:variable name="allowed-values">
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">Allowed values</xsl:with-param>
                </xsl:call-template><xsl:text>: </xsl:text>
                <!--0=Ratio; 1=Unitary; 2=As percent; 3=Fraction; 4=Integer and fraction.-->
                <xsl:for-each select="oe:attributes[oe:rm_attribute_name='type']/oe:children/oe:item/oe:list">
                    <xsl:choose>
                        <xsl:when test="current()='1'"><xsl:value-of select="$ratio"/></xsl:when>
                        <xsl:when test="current()='2'"><xsl:value-of select="$as-percent"/></xsl:when>
                        <xsl:when test="current()='3'"><xsl:value-of select="$fraction"/></xsl:when>
                        <xsl:when test="current()='4'"><xsl:value-of select="$integer-and-fraction"/></xsl:when>
                    </xsl:choose><xsl:text>, </xsl:text>
                </xsl:for-each>
            </xsl:variable>
            <!-- trim comma at end of string -->
            <xsl:value-of select="substring($allowed-values, 0, string-length($allowed-values)-1)"/>
        </xsl:if>
        <xsl:choose>
            <xsl:when test="string-length($numerator)>0 and string-length($denominator)>0">
                <!--<xsl:value-of select="$numerator"/>
                    <xsl:text> : </xsl:text>
                    <xsl:value-of select="$denominator"/>-->
                <xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text>
                <xsl:value-of select="$numerator-label"/>: <xsl:value-of select="$numerator"/>
                <xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text>
                <xsl:value-of select="$denominator-label"/>: <xsl:value-of select="$denominator"/>
            </xsl:when>
            <xsl:when test="string-length($numerator)>0">
                <xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text>
                <xsl:value-of select="$numerator-label"/>: <xsl:value-of select="$numerator"/>
                <!--<xsl:text> : *</xsl:text>-->
                <xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text>
                <xsl:value-of select="$denominator-label"/>: *
            </xsl:when>
            <xsl:when test="string-length($denominator)>0">
                <!--<xsl:text>* : </xsl:text>-->
                <xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text>
                <xsl:value-of select="$numerator-label"/>: *
                <xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text>
                <xsl:value-of select="$denominator-label"/>: <xsl:value-of select="$denominator"/>
            </xsl:when>
        </xsl:choose>

    </xsl:template>

    <!-- generate DV_INTERVAL(DV_TYPE) -->
    <xsl:template match="*[contains(oe:rm_type_name, 'DV_INTERVAL')]">
        <xsl:variable name="lower-interval">
            <xsl:apply-templates select="oe:attributes[oe:rm_attribute_name='lower']/oe:children"/>
        </xsl:variable>
        <xsl:variable name="upper-interval">
            <xsl:apply-templates select="oe:attributes[oe:rm_attribute_name='upper']/oe:children"/>
        </xsl:variable>

        <xsl:choose>
            <xsl:when test="string-length($lower-interval)>0 and string-length($upper-interval)>0">
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:text disable-output-escaping="yes">&lt;i&gt;</xsl:text>
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">Lower</xsl:with-param>
                </xsl:call-template>
                <xsl:text disable-output-escaping="yes">: &lt;/i&gt;&lt;br/&gt;</xsl:text>
                <xsl:value-of select="$lower-interval"/>
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:text disable-output-escaping="yes">, &lt;i&gt;&lt;br/&gt;</xsl:text>
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">Upper</xsl:with-param>
                </xsl:call-template>
                <xsl:text disable-output-escaping="yes">: &lt;/i&gt;&lt;br/&gt;</xsl:text>
                <xsl:value-of select="$upper-interval"/>
            </xsl:when>
            <!-- no constraint -->
            <xsl:otherwise>
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:text disable-output-escaping="yes">&lt;i&gt;</xsl:text>
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">Lower</xsl:with-param>
                </xsl:call-template>
                <xsl:text disable-output-escaping="yes">: &lt;/i&gt;*</xsl:text>
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:text disable-output-escaping="yes">, &lt;i&gt;&lt;br/&gt;</xsl:text>
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">Upper</xsl:with-param>
                </xsl:call-template>
                <xsl:text disable-output-escaping="yes">: &lt;/i&gt;*</xsl:text>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <!-- **** generate HTML section templates **** -->

    <xsl:template name="generate-header">
        <xsl:variable name="rm_type">
            <xsl:call-template name="get-archetype-rm-type">
                <xsl:with-param name="archetype-id" select="oe:archetype_id/oe:value"/>
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="archetypeId">
            <xsl:value-of select="oe:archetype_id/oe:value"/>
        </xsl:variable>
        <div id="adlhtml-header-TITLE">
            <h1><xsl:call-template name="get-archetype-term">
                    <xsl:with-param name="at-code" select="oe:concept"/>
                </xsl:call-template>: <i>
                    <xsl:choose>
                        <xsl:when test="contains($rm_type, 'ITEM_')">
                            <xsl:variable name="no-item-rm-type">
                                <xsl:call-template name="remove_underscore">
                                    <xsl:with-param name="string">
                                        <xsl:value-of select="substring-after($rm_type, 'ITEM_')"/>
                                    </xsl:with-param>
                                </xsl:call-template>
                            </xsl:variable>
                            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                                <xsl:with-param name="original-rubric">
                                    <xsl:value-of select="$no-item-rm-type"/>
                                </xsl:with-param>
                            </xsl:call-template>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:variable name="no-item-rm-type">
                                <xsl:call-template name="remove_underscore">
                                    <xsl:with-param name="string">
                                        <xsl:value-of select="$rm_type"/>
                                    </xsl:with-param>
                                </xsl:call-template>
                            </xsl:variable>
                            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                                <xsl:with-param name="original-rubric">
                                    <xsl:value-of select="$no-item-rm-type"/>
                                </xsl:with-param>
                            </xsl:call-template>
                        </xsl:otherwise>
                    </xsl:choose><xsl:if test="//oe:definition/oe:attributes/oe:cardinality/oe:is_ordered='true'"> (<xsl:value-of select="$ordered"/>)</xsl:if>
                </i></h1>
        </div>
        <div id="adlhtml-header">
            <table width="100%">
                <tr>
                    <td align="left">
                        <i>Using ADL version <xsl:value-of select="oe:adl_version"/></i>
                    </td>
                    <td align="center">
                        <a href="mailto:{$contact-email}">
                            <i>
                                <xsl:value-of select="$contact-text"/>
                            </i>
                        </a>
                    </td>
                    <td align="right">
                        <xsl:text disable-output-escaping="yes">&lt;b&gt;</xsl:text>
                        <xsl:value-of select="$copyright-text"/>
                        <xsl:text disable-output-escaping="yes">&lt;/b&gt;</xsl:text>
                    </td>
                </tr>
            </table>
        </div>
        <table border="1" width="100%" frame="{$header-frame}" rules="{$header-rules}">
            <th align="left">
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">description</xsl:with-param>
                </xsl:call-template>
            </th>
            <th align="left">
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">identifier</xsl:with-param>
                </xsl:call-template>
            </th>
            <th align="left">
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">structure</xsl:with-param>
                </xsl:call-template>
            </th>
            <tbody>
                <tr>
                    <td width="33%">
                        <xsl:call-template name="get-archetype-description">
                            <xsl:with-param name="at-code">
                                <xsl:value-of select="oe:concept"/>
                            </xsl:with-param>
                        </xsl:call-template>
                    </td>
                    <td width="33%">
                        <i>Id: </i>
                        <xsl:value-of select="$archetypeId"/>
                        <p/>
                        <i>Reference model: </i>
                        <xsl:value-of select="substring-before($archetypeId, '-')"/>
                    </td>
                    <td width="33%">
                        <table width="100%">
                            <tr>
                                <xsl:choose>
                                    <xsl:when test="$rm_type='COMPOSITION'">
                                        <td>
                                            <p align="right">
                                                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                                <xsl:call-template
                                                  name="LookupTerminologyAndTranslationByRubric">
                                                  <xsl:with-param name="original-rubric"
                                                  >content</xsl:with-param>
                                                </xsl:call-template>
                                            </p>
                                            <p align="right">
                                                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                                <xsl:call-template
                                                  name="LookupTerminologyAndTranslationByRubric">
                                                  <xsl:with-param name="original-rubric"
                                                  >context</xsl:with-param>
                                                </xsl:call-template>
                                            </p>
                                        </td>
                                        <td width="100">
                                            <img border="0" src="{$images-path}/evaluation.gif"
                                                width="100" height="100" align="middle"/>
                                        </td>
                                        <td valign="bottom">
                                            <p align="left"/>
                                            <p>
                                                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                                <xsl:call-template
                                                  name="LookupTerminologyAndTranslationByRubric">
                                                  <xsl:with-param name="original-rubric">other
                                                  context</xsl:with-param>
                                                </xsl:call-template>
                                            </p>
                                        </td>
                                    </xsl:when>
                                    <xsl:when test="$rm_type='OBSERVATION'">
                                        <td>
                                            <p align="right">
                                                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                                <xsl:call-template
                                                  name="LookupTerminologyAndTranslationByRubric">
                                                  <xsl:with-param name="original-rubric"
                                                  >history</xsl:with-param>
                                                </xsl:call-template>
                                            </p>
                                            <p align="right">
                                                <xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text>
                                                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                                <xsl:call-template
                                                  name="LookupTerminologyAndTranslationByRubric">
                                                  <xsl:with-param name="original-rubric"
                                                  >protocol</xsl:with-param>
                                                </xsl:call-template>
                                            </p>
                                        </td>
                                        <td width="155">
                                            <img border="0" src="{$images-path}/observation.gif"
                                                width="150" height="100" align="middle"/>
                                        </td>
                                        <td valign="top">
                                            <p align="left">
                                                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                                <xsl:call-template
                                                  name="LookupTerminologyAndTranslationByRubric">
                                                  <xsl:with-param name="original-rubric"
                                                  >data</xsl:with-param>
                                                </xsl:call-template>
                                            </p>
                                            <p/>
                                            <p/>
                                            <p>
                                                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                                <xsl:call-template
                                                  name="LookupTerminologyAndTranslationByRubric">
                                                  <xsl:with-param name="original-rubric"
                                                  >state</xsl:with-param>
                                                </xsl:call-template>
                                            </p>
                                        </td>
                                    </xsl:when>
                                    <xsl:when test="$rm_type='EVALUATION'">
                                        <td>
                                            <p align="right">
                                                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                                <xsl:call-template
                                                  name="LookupTerminologyAndTranslationByRubric">
                                                  <xsl:with-param name="original-rubric"
                                                  >data</xsl:with-param>
                                                </xsl:call-template>
                                            </p>
                                            <p align="right">
                                                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                                <xsl:call-template
                                                  name="LookupTerminologyAndTranslationByRubric">
                                                  <xsl:with-param name="original-rubric"
                                                  >protocol</xsl:with-param>
                                                </xsl:call-template>
                                            </p>
                                        </td>
                                        <td>
                                            <img src="{$images-path}/evaluation.gif" align="middle"
                                                border="0" height="100" width="100"/>
                                        </td>
                                    </xsl:when>
                                    <xsl:when test="$rm_type='ACTION'">
                                        <td>
                                            <p align="right">
                                                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                                <xsl:call-template
                                                  name="LookupTerminologyAndTranslationByRubric">
                                                  <xsl:with-param name="original-rubric">activity
                                                  description</xsl:with-param>
                                                </xsl:call-template>
                                            </p>
                                            <p align="right">
                                                <xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text>
                                                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                                <xsl:call-template
                                                  name="LookupTerminologyAndTranslationByRubric">
                                                  <xsl:with-param name="original-rubric"
                                                  >protocol</xsl:with-param>
                                                </xsl:call-template>
                                            </p>
                                        </td>
                                        <td width="155">
                                            <img border="0" src="{$images-path}/instruction.gif"
                                                width="150" height="100" align="middle"/>
                                        </td>
                                        <td valign="top">
                                            <p/>
                                            <p/>
                                            <p>
                                                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                                <xsl:call-template
                                                  name="LookupTerminologyAndTranslationByRubric">
                                                  <xsl:with-param name="original-rubric"
                                                  >pathway</xsl:with-param>
                                                </xsl:call-template>
                                            </p>
                                        </td>
                                    </xsl:when>
                                    <xsl:when test="$rm_type='INSTRUCTION'">
                                        <td>
                                            <p align="right">
                                                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                                <xsl:call-template
                                                  name="LookupTerminologyAndTranslationByRubric">
                                                  <xsl:with-param name="original-rubric">activity
                                                  description</xsl:with-param>
                                                </xsl:call-template>
                                            </p>
                                            <p align="right">
                                                <xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text>
                                                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                                <xsl:call-template
                                                  name="LookupTerminologyAndTranslationByRubric">
                                                  <xsl:with-param name="original-rubric"
                                                  >protocol</xsl:with-param>
                                                </xsl:call-template>
                                            </p>
                                        </td>
                                        <td width="155">
                                            <img border="0" src="{$images-path}/instruction.gif"
                                                width="150" height="100" align="middle"/>
                                        </td>
                                        <td valign="top">
                                            <p/>
                                            <p/>
                                            <p>
                                                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                                <xsl:call-template
                                                  name="LookupTerminologyAndTranslationByRubric">
                                                  <xsl:with-param name="original-rubric"
                                                  >pathway</xsl:with-param>
                                                </xsl:call-template>
                                            </p>
                                        </td>
                                    </xsl:when>
                                    <xsl:when test="$rm_type='ADMIN_ENTRY'">
                                        <td align="center">
                                            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                            <xsl:call-template
                                                name="LookupTerminologyAndTranslationByRubric">
                                                <xsl:with-param name="original-rubric"
                                                >data</xsl:with-param>
                                            </xsl:call-template>
                                            <img border="0" src="{$images-path}/admin_entry.gif"
                                                align="middle"/>
                                        </td>
                                    </xsl:when>
                                    <xsl:when test="$rm_type='CLUSTER'">
                                        <td valign="top">
                                            <p align="right">
                                                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                                <xsl:call-template
                                                  name="LookupTerminologyAndTranslationByRubric">
                                                  <xsl:with-param name="original-rubric"
                                                  >cluster</xsl:with-param>
                                                </xsl:call-template>
                                                <br/>
                                                <br/>
                                            </p>
                                        </td>
                                        <td width="100px">
                                            <img border="0" src="{$images-path}/cluster.gif"
                                                width="100" height="100" align="middle"/>
                                        </td>
                                        <td valign="middle">
                                            <p align="left">
                                                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                                <xsl:call-template
                                                  name="LookupTerminologyAndTranslationByRubric">
                                                  <xsl:with-param name="original-rubric"
                                                  >element</xsl:with-param>
                                                </xsl:call-template>
                                                <br/>
                                                <br/>
                                            </p>
                                        </td>
                                    </xsl:when>
                                    <xsl:otherwise>Class diagram not available</xsl:otherwise>
                                </xsl:choose>
                            </tr>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
        <xsl:if test="$show-purpose-flag='true'">
            <xsl:call-template name="generate-purpose-use-and-misuse"/>
        </xsl:if>
    </xsl:template>

    <xsl:template name="generate-purpose-use-and-misuse">
        <table border="1" width="100%" frame="{$header-frame}" rules="{$header-rules}"
            id="adlhtml-header-PURPOSE">
            <th align="left">
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">Purpose</xsl:with-param>
                </xsl:call-template>
            </th>
            <th align="left">
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">Use</xsl:with-param>
                </xsl:call-template>
            </th>
            <th align="left">
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">Misuse</xsl:with-param>
                </xsl:call-template>
            </th>
            <th align="left">
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">References</xsl:with-param>
                </xsl:call-template>
            </th>
            <tbody>
                <tr>
                    <td width="25%">
                        <!-- use specified language in the input parameter, otherwise use the original language -->
                        <xsl:choose>
                            <xsl:when test="oe:description/oe:details[oe:language/oe:code_string=$language]">
                                <xsl:value-of select="oe:description/oe:details[oe:language/oe:code_string=$language]/oe:purpose"/>
                            </xsl:when>
                            <xsl:otherwise>
                                <xsl:variable name="original-language" select="//oe:original_language/oe:code_string"/>
                                <xsl:value-of select="oe:description/oe:details[oe:language/oe:code_string=$original-language]/oe:purpose"/>
                            </xsl:otherwise>
                        </xsl:choose>
                    </td>
                    <td width="25%">
                        <!-- use specified language in the input parameter, otherwise use the original language -->
                        <xsl:choose>
                            <xsl:when test="oe:description/oe:details[oe:language/oe:code_string=$language]">
                                <xsl:value-of select="oe:description/oe:details[oe:language/oe:code_string=$language]/oe:use"/>
                            </xsl:when>
                            <xsl:otherwise>
                                <xsl:variable name="original-language" select="//oe:original_language/oe:code_string"/>
                                <xsl:value-of select="oe:description/oe:details[oe:language/oe:code_string=$original-language]/oe:use"/>
                            </xsl:otherwise>
                        </xsl:choose>
                    </td>
                    <td width="25%">
                        <!-- use specified language in the input parameter, otherwise use the original language -->
                        <xsl:choose>
                            <xsl:when test="oe:description/oe:details[oe:language/oe:code_string=$language]">
                                <xsl:value-of select="oe:description/oe:details[oe:language/oe:code_string=$language]/oe:misuse"/>
                            </xsl:when>
                            <xsl:otherwise>
                                <xsl:variable name="original-language" select="//oe:original_language/oe:code_string"/>
                                <xsl:value-of select="oe:description/oe:details[oe:language/oe:code_string=$original-language]/oe:misuse"/>
                            </xsl:otherwise>
                        </xsl:choose>
                    </td>
                    <td width="25%">
                        <xsl:value-of select="oe:description/oe:other_details[@id='references']"/>
                    </td>
                </tr>
            </tbody>
        </table>
    </xsl:template>

    <xsl:template name="generate-adl-or-adl-html-path">
        <xsl:param name="directory"/>

        <xsl:variable name="archetype_rm_type">
            <xsl:call-template name="get-archetype-rm-type">
                <xsl:with-param name="archetype-id" select="oe:archetype_id/oe:value"/>
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="archetypeId">
            <xsl:value-of select="oe:archetype_id/oe:value"/>
        </xsl:variable>

        <xsl:value-of select="$directory"/>

        <xsl:choose>
            <xsl:when
                test="$archetype_rm_type='OBSERVATION' or 'INSTRUCTION' 
                or 'EVALUATION' or 'ADMIN_ENTRY' or 'ACTION'"
                    >/entry/<xsl:value-of select="$archetype_rm_type"/>/</xsl:when>
            <xsl:when test="$archetype_rm_type='CLUSTER'">/cluster/</xsl:when>
            <xsl:when test="$archetype_rm_type='ELEMENT'">/element/</xsl:when>
            <xsl:when test="'ITEM_TREE' or 'ITEM_LIST' or 'ITEM_TABLE' or 'ITEM_SINGLE'">/structure/</xsl:when>
            <xsl:when test="$archetype_rm_type='COMPOSITION'">/composition/</xsl:when>
            <xsl:when test="$archetype_rm_type='SECTION'">/section/</xsl:when>
        </xsl:choose>
        <xsl:value-of select="$archetypeId"/>
    </xsl:template>

    <xsl:template name="generate-terminology">
        <div id="adlhtml-terminology">
            <table>
                <tbody>
                    <tr>
                        <td> </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </xsl:template>

    <xsl:template name="generate-footer">
        <div id="adlhtml-footer">
            <table>
                <tbody>
                    <tr>
                        <td>Generated by XSLT (transform) file: <xsl:value-of
                                select="$transform-filename"/> - Version: <xsl:value-of
                                select="$transform-version"/></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </xsl:template>

    <!-- **** helper templates **** -->

    <!-- get list of archetype IDs to include or exclude (e.g. from section archetype slots) -->
    <xsl:template name="get-include-or-exclude-archetypes">
        <xsl:param name="rm-type-name"/>
        <xsl:param name="include-or-exclude"/>
        <xsl:param name="include-or-exclude-node"/>
        <xsl:variable name="temp">
            <xsl:value-of select="substring-before(//oe:archetype_id/oe:value, '-')"
                />-EHR-<xsl:value-of select="$rm-type-name"/>
        </xsl:variable>
        <xsl:variable name="lookup-rubric">
            <xsl:choose>
                <xsl:when test="$include-or-exclude='exclude'"><xsl:value-of
                        select="$include-or-exclude"/></xsl:when>
                <xsl:otherwise><xsl:value-of select="$include-or-exclude"/></xsl:otherwise>
            </xsl:choose>
        </xsl:variable>
        <xsl:text disable-output-escaping="yes">&lt;b&gt;</xsl:text><!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
        <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
            <xsl:with-param name="original-rubric">
                <xsl:value-of select="$lookup-rubric"/>
            </xsl:with-param>
        </xsl:call-template>:<xsl:text disable-output-escaping="yes">&lt;/b&gt;&lt;br/&gt;</xsl:text>
        <xsl:for-each select="$include-or-exclude-node">
            <xsl:variable name="archetype-id">
                <xsl:call-template name="trim-archetype-id-pattern">
                    <xsl:with-param name="pattern"
                        select="oe:expression/oe:right_operand/oe:item/oe:pattern"/>
                </xsl:call-template>
            </xsl:variable>
            <xsl:variable name="wildcard">.*</xsl:variable>
            <xsl:variable name="archetype-file-name">
                <xsl:choose>
                    <xsl:when test="$archetype-id=$wildcard">
                        <xsl:value-of select="$temp"/>\..*<!-- is a wildcard -->
                    </xsl:when>
                    <xsl:otherwise><xsl:value-of select="$temp"/>.<xsl:value-of
                            select="$archetype-id"/></xsl:otherwise>
                </xsl:choose>
            </xsl:variable>
            <ul class="left-align">
                <li>
                    <xsl:choose>
                        <xsl:when test="contains($archetype-id,$wildcard)">
                            <xsl:value-of select="$archetype-file-name"/>
                        </xsl:when>
                        <xsl:otherwise>
                            <a href="{$archetype-file-name}.html">
                                <xsl:value-of select="$archetype-file-name"/>
                            </a>
                        </xsl:otherwise>
                    </xsl:choose>
                </li>
            </ul>
        </xsl:for-each>
    </xsl:template>

    <!-- lookup Rubric value from separate terminology xml document by Concept ID and Language -->
    <xsl:template name="LookupTerminologyAndTranslationByConceptId">
        <xsl:param name="concept-id"/>
        <xsl:variable name="terminology-top"
            select="document($terminology-xml-document-path)/term:Terminology"/>
        <xsl:variable name="translation">
            <xsl:value-of
                select="$terminology-top/term:Concept[@Language=$language and @ConceptID=$concept-id]/@Rubric"
            />
        </xsl:variable>
        <xsl:variable name="original-language">
            <xsl:value-of select="//oe:archetype/oe:original_language/oe:code_string"/>
        </xsl:variable>
        <xsl:choose>
            <xsl:when test="string-length($translation)>0">
                <xsl:value-of select="$translation"/>
            </xsl:when>
            <!--no translation in specified language found so use original language as default -->
            <xsl:otherwise><xsl:value-of
                    select="$terminology-top/term:Concept[@Language=$original-language and @ConceptID=$concept-id]/@Rubric"
                    />(<xsl:value-of select="$original-language"/>)</xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <!-- lookup Rubric value from separate xml document firstly by English Rubric value to get the Concept ID and using the concept ID and 
        language to retrieve the Rubric in specified language -->
    <xsl:template name="LookupTerminologyAndTranslationByRubric">
        <xsl:param name="original-rubric"/>
        <xsl:variable name="terminology-top"
            select="document($terminology-xml-document-path)/term:Terminology"/>
        <xsl:variable name="original-language">
            <xsl:value-of select="//oe:archetype/oe:original_language/oe:code_string"/>
        </xsl:variable>
        <xsl:variable name="concept-id">
            <!--   'lower-case' function not supported by .NET 2.0 <xsl:value-of select="$terminology-top/term:Concept[@Language=$original-language and lower-case(@Rubric)=$original-rubric]/@ConceptID"/>-->
            <xsl:value-of
                select="$terminology-top/term:Concept[@Language=$original-language and translate(@Rubric,$ucletters,$lcletters)=translate($original-rubric,$ucletters,$lcletters)]/@ConceptID"
            />
        </xsl:variable>
        <xsl:variable name="translation">
            <xsl:value-of
                select="$terminology-top/term:Concept[@Language=$language and @ConceptID=$concept-id]/@Rubric"
            />
        </xsl:variable>
        <xsl:variable name="original-language-translation">
            <xsl:value-of
                select="$terminology-top/term:Concept[@Language=$original-language and @ConceptID=$concept-id]/@Rubric"
            />
        </xsl:variable>
        <xsl:variable name="english-translation">
            <xsl:value-of
                select="$terminology-top/term:Concept[@Language='en' and @ConceptID=$concept-id]/@Rubric"
            />
        </xsl:variable>
        <xsl:choose>
            <!-- translate to specified language -->
            <xsl:when test="string-length($translation)>0">
                <xsl:value-of select="$translation"/>
            </xsl:when>
            <!--no translation in specified language found so use original language as default -->
            <xsl:when test="string-length($original-language-translation)>0"><xsl:value-of
                    select="$original-language-translation"/>(<xsl:value-of
                    select="$original-language"/>)</xsl:when>
            <!-- no translation found in original language so use english (this is usually the case for words already in english and will always be found in english in the ADL XML like the terms 'property' and 'units' -->
            <xsl:when test="string-length($english-translation)>0"><xsl:value-of
                    select="$english-translation"/>(en)</xsl:when>
            <!-- no translation to either the specified language, original language or english, so just output whatever was passed in -->
            <xsl:otherwise>
                <xsl:value-of select="$original-rubric"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <xsl:template name="remove_underscore">
        <xsl:param name="string"/>
        <xsl:choose>
            <xsl:when test="contains($string, '_')">
                <xsl:call-template name="remove_underscore">
                    <xsl:with-param name="string">
                        <xsl:value-of select="substring-before($string, '_')"/>
                    </xsl:with-param>
                </xsl:call-template>
                <xsl:text> </xsl:text>
                <xsl:call-template name="remove_underscore">
                    <xsl:with-param name="string">
                        <xsl:value-of select="substring-after($string, '_')"/>
                    </xsl:with-param>
                </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="$string"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <!-- this is used for the data type images referred from the CSS file -->
    <xsl:template name="replace_underscore_with_dash">
        <xsl:param name="string"/>
        <xsl:choose>
            <xsl:when test="contains($string, '_')">
                <xsl:call-template name="replace_underscore_with_dash">
                    <xsl:with-param name="string">
                        <xsl:value-of select="substring-before($string, '_')"/>
                    </xsl:with-param>
                </xsl:call-template>
                <xsl:text>-</xsl:text>
                <xsl:call-template name="replace_underscore_with_dash">
                    <xsl:with-param name="string">
                        <xsl:value-of select="substring-after($string, '_')"/>
                    </xsl:with-param>
                </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="$string"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <xsl:template name="generate-common-or-slot-header">
        <xsl:param name="currNode"/>
        
        <tr id="adlhtml-header-GENERIC-tableHeader">
            <xsl:choose>
                <xsl:when test="$currNode/oe:children[(position()=1) and (@xi:type='ARCHETYPE_SLOT')]">
                    <td>
                        <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                        <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                            <xsl:with-param name="original-rubric">Slot</xsl:with-param>
                        </xsl:call-template>
                    </td>
                    <xsl:call-template name="adlhtml-header-SLOT-tableHeader"/>
                </xsl:when>
                <xsl:otherwise>
                    <td>
                        <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                        <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                            <xsl:with-param name="original-rubric">Concept</xsl:with-param>
                        </xsl:call-template>
                    </td>
                    <xsl:call-template name="adlhtml-header-COMMON-tableHeader"/>
                </xsl:otherwise>
            </xsl:choose>
        </tr>
    </xsl:template>
    
    <xsl:template name="generate-section-or-slot-header">
        <xsl:param name="currNode"/>
        
        <tr id="adlhtml-header-GENERIC-tableHeader">
                <xsl:if test="$currNode/oe:children[(position()=1) and (@xi:type='ARCHETYPE_SLOT')]">
                    <td>
                        <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                        <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                            <xsl:with-param name="original-rubric">Slot</xsl:with-param>
                        </xsl:call-template>
                    </td>
                    <xsl:call-template name="adlhtml-header-SLOT-tableHeader"/>
                </xsl:if>
                <!--<xsl:otherwise>
                    <td>-->
                        <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                        <!--<xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                            <xsl:with-param name="original-rubric">Section</xsl:with-param>
                        </xsl:call-template>
                    </td>
                    <xsl:call-template name="generate-section-table-header"/>
                </xsl:otherwise>
            </xsl:choose>-->
        </tr>
    </xsl:template>

    <xsl:template name="adlhtml-header-SLOT-tableHeader">
        <td width="30%">
            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">Include</xsl:with-param>
            </xsl:call-template>/<xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">Exclude</xsl:with-param>
            </xsl:call-template><xsl:text> </xsl:text><xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">archetype</xsl:with-param>
            </xsl:call-template>
        </td>
        <td width="30%">
            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">Constraints</xsl:with-param>
            </xsl:call-template>
        </td>
        <xsl:if test="$show-comments-flag='true'">
            <td>
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">Comments</xsl:with-param>
                </xsl:call-template>
            </td>
        </xsl:if>
    </xsl:template>

    <xsl:template name="adlhtml-header-COMMON-tableHeader">
        <td width="30%">
            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">Description</xsl:with-param>
            </xsl:call-template>
        </td>
        <td width="30%">
            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">Constraints</xsl:with-param>
            </xsl:call-template>
        </td>
        <td width="20%">
            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                <xsl:with-param name="original-rubric">Values</xsl:with-param>
            </xsl:call-template>
        </td>
        <xsl:if test="$show-comments-flag='true'">
            <td>
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">Comments</xsl:with-param>
                </xsl:call-template>
            </td>
        </xsl:if>
    </xsl:template>

    <xsl:template name="format-duration">
        <xsl:param name="duration"/>
        <xsl:choose>
            <xsl:when test="contains($duration, 'P')">
                <xsl:variable name="afterP">
                    <xsl:value-of select="substring-after($duration, 'P')"/>
                </xsl:variable>
                <xsl:variable name="beforeT">
                    <xsl:value-of select="substring-before($afterP, 'T')"/>
                </xsl:variable>

                <xsl:variable name="processYear">
                    <xsl:call-template name="process-duration">
                        <xsl:with-param name="durationString" select="$beforeT"/>
                        <xsl:with-param name="replacedLetter">Y</xsl:with-param>
                        <xsl:with-param name="replaceString">
                            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                                <xsl:with-param name="original-rubric">yr</xsl:with-param>
                            </xsl:call-template>
                        </xsl:with-param>
                    </xsl:call-template>
                </xsl:variable>
                <xsl:variable name="processMonth">
                    <xsl:call-template name="process-duration">
                        <xsl:with-param name="durationString" select="$processYear"/>
                        <xsl:with-param name="replacedLetter">M</xsl:with-param>
                        <xsl:with-param name="replaceString">
                            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                                <xsl:with-param name="original-rubric">mth</xsl:with-param>
                            </xsl:call-template>
                        </xsl:with-param>
                    </xsl:call-template>
                </xsl:variable>
                <xsl:variable name="processWeek">
                    <xsl:call-template name="process-duration">
                        <xsl:with-param name="durationString" select="$processMonth"/>
                        <xsl:with-param name="replacedLetter">W</xsl:with-param>
                        <xsl:with-param name="replaceString">
                            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                                <xsl:with-param name="original-rubric">wk</xsl:with-param>
                            </xsl:call-template>
                        </xsl:with-param>
                    </xsl:call-template>
                </xsl:variable>
                <xsl:variable name="processDay">
                    <xsl:call-template name="process-duration">
                        <xsl:with-param name="durationString" select="$processWeek"/>
                        <xsl:with-param name="replacedLetter">D</xsl:with-param>
                        <xsl:with-param name="replaceString">
                            <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                            <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                                <xsl:with-param name="original-rubric">days(s)</xsl:with-param>
                            </xsl:call-template>
                        </xsl:with-param>
                    </xsl:call-template>
                </xsl:variable>

                <xsl:if test="contains($duration, 'T')">
                    <xsl:variable name="afterT">
                        <xsl:value-of select="substring-after($duration, 'T')"/>
                    </xsl:variable>

                    <xsl:variable name="processHour">
                        <xsl:call-template name="process-duration">
                            <xsl:with-param name="durationString" select="$afterT"/>
                            <xsl:with-param name="replacedLetter">H</xsl:with-param>
                            <xsl:with-param name="replaceString">
                                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                                    <xsl:with-param name="original-rubric">hr</xsl:with-param>
                                </xsl:call-template>
                            </xsl:with-param>
                        </xsl:call-template>
                    </xsl:variable>
                    <xsl:variable name="processMinute">
                        <xsl:call-template name="process-duration">
                            <xsl:with-param name="durationString" select="$processHour"/>
                            <xsl:with-param name="replacedLetter">M</xsl:with-param>
                            <xsl:with-param name="replaceString">
                                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                                    <xsl:with-param name="original-rubric">min</xsl:with-param>
                                </xsl:call-template>
                            </xsl:with-param>
                        </xsl:call-template>
                    </xsl:variable>
                    <xsl:variable name="processSecond">
                        <xsl:call-template name="process-duration">
                            <xsl:with-param name="durationString" select="$processMinute"/>
                            <xsl:with-param name="replacedLetter">S</xsl:with-param>
                            <xsl:with-param name="replaceString">
                                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                                    <xsl:with-param name="original-rubric">sec</xsl:with-param>
                                </xsl:call-template>
                            </xsl:with-param>
                        </xsl:call-template>
                    </xsl:variable>

                    <xsl:value-of select="$processSecond"/>

                </xsl:if>

                <xsl:value-of select="$processDay"/>

            </xsl:when>

            <xsl:otherwise>
                <xsl:value-of select="$duration"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <xsl:template name="get-duration-units">
        <xsl:param name="duration"/>
        <xsl:variable name="durationUnits">
            <xsl:if test="contains($duration, 'Y')">
                <xsl:text>, </xsl:text>
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">yr</xsl:with-param>
                </xsl:call-template>
            </xsl:if>
            <xsl:if test="contains(substring-before($duration, 'T'), 'M')">
                <xsl:text>, </xsl:text>
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">mth</xsl:with-param>
                </xsl:call-template>
            </xsl:if>
            <xsl:if test="contains($duration, 'W')">
                <xsl:text>, </xsl:text>
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">wk</xsl:with-param>
                </xsl:call-template>
            </xsl:if>
            <xsl:if test="contains($duration, 'D')">
                <xsl:text>, </xsl:text>
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">day(s)</xsl:with-param>
                </xsl:call-template>
            </xsl:if>
            <xsl:if test="contains($duration, 'H')">
                <xsl:text>, </xsl:text>
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">hr</xsl:with-param>
                </xsl:call-template>
            </xsl:if>
            <xsl:if test="contains(substring-after($duration, 'T'), 'M')">
                <xsl:text>, </xsl:text>
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">min</xsl:with-param>
                </xsl:call-template>
            </xsl:if>
            <xsl:if test="contains($duration, 'S')">
                <xsl:text>, </xsl:text>
                <!-- lookup terminology concept from separate xml document for the data type translated in specified language by its rubric value -->
                <xsl:call-template name="LookupTerminologyAndTranslationByRubric">
                    <xsl:with-param name="original-rubric">sec</xsl:with-param>
                </xsl:call-template>
            </xsl:if>
        </xsl:variable>
        <xsl:value-of select="substring-after($durationUnits, ', ')"/>
    </xsl:template>

    <!-- generate list of DV_TEXT values from string pattern (if exists) -->
    <xsl:template name="generate-list-of-text-values">
        <xsl:param name="stringPattern"/>
        <xsl:param name="stringOutput"/>
        
        <!--Example: <pattern>"Capsule", "Gel", "Solution (infusion or injection)", "Inhaler", "Nasal drops", "Nebuliser", "Oral solution", "Patch", "Pessary", "Spray", "Suppository", "Tablet", "Topical solution"</pattern>-->
            <xsl:choose>
                <xsl:when test="contains($stringPattern, ', &quot;')">
                    <xsl:variable name="tempStr">
                        <xsl:variable name="trimmedTextValue1">
                            <xsl:value-of select="substring-after($stringPattern, '&quot;')"/>
                        </xsl:variable>
                        <xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text><xsl:value-of select="substring-before($trimmedTextValue1, '&quot;, &quot;')" disable-output-escaping="yes"/>
                    </xsl:variable>
                    <xsl:call-template name="generate-list-of-text-values">
                        <xsl:with-param name="stringPattern" select="substring-after($stringPattern, '&quot;, ')"/>
                        <xsl:with-param name="stringOutput" select="concat($stringOutput, $tempStr)"/>
                    </xsl:call-template>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:choose>
                        <xsl:when test="starts-with($stringOutput, '&lt;br/&gt;')">
                            <xsl:variable name="tmpLastStringValue" select="substring-after($stringPattern, '&quot;')"/>
                            <xsl:variable name="lastStringValue" select="substring-before($tmpLastStringValue, '&quot;')"/>
                            <xsl:variable name="finalStringOutput" select="concat($stringOutput, '&lt;br/&gt;', $lastStringValue)"/>
                            <xsl:value-of select="substring-after($finalStringOutput, '&lt;br/&gt;')"/>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:variable name="quoteRemoved" select="substring-after($stringOutput, '&quot;')"/>
                            <xsl:value-of select="substring-before($quoteRemoved, '&quot;')"/>
                        </xsl:otherwise>
                    </xsl:choose>
                </xsl:otherwise>
            </xsl:choose>
    </xsl:template>

    <xsl:template name="get-parent-id-from-specialised-id">
        <xsl:param name="specialised-id"/><!-- e.g. at0002.1.1 -->
        <xsl:param name="remaining-str"/>
        <xsl:param name="parent-node-id"/>
        
        <xsl:variable name="root-node-id" select="substring-before($specialised-id, '.')"/><!-- e.g. at0002 -->
               
        <xsl:choose>
            <xsl:when test="contains($remaining-str, '.')">
                
                <xsl:variable name="temp">
                    <xsl:choose>
                        <xsl:when test="(string-length($parent-node-id)>0) and (string-length(substring-before($remaining-str, '.'))>0)">
                            <xsl:value-of select="concat($parent-node-id, '.', substring-before($remaining-str, '.'))"/>
                        </xsl:when>
                        <xsl:when test="(string-length($parent-node-id)=0) and (string-length(substring-before($remaining-str, '.'))>0)">
                            <xsl:value-of select="substring-before($specialised-id, '.')"/>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:value-of select="$parent-node-id"/>
                        </xsl:otherwise>
                    </xsl:choose>
                </xsl:variable>
                
                <xsl:call-template name="get-parent-id-from-specialised-id">
                    <xsl:with-param name="specialised-id" select="$specialised-id"/>
                    <xsl:with-param name="remaining-str" select="substring-after($remaining-str, '.')"/><!-- e.g. 1.1.3 -->
                    <xsl:with-param name="parent-node-id" select="$temp"/>
                </xsl:call-template>
                
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="$parent-node-id"/>
            </xsl:otherwise>
        </xsl:choose>
        
    </xsl:template>

    <xsl:template name="process-duration">
        <xsl:param name="durationString"/>
        <xsl:param name="replacedLetter"/>
        <xsl:param name="replaceString"/>
        <xsl:choose>
            <xsl:when test="contains($durationString, $replacedLetter)">
                <xsl:variable name="number">
                    <xsl:value-of select="substring-before($durationString, $replacedLetter)"/>
                </xsl:variable>
                <xsl:value-of select="$number"/>
                <xsl:text> </xsl:text>
                <xsl:value-of select="$replaceString"/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="$durationString"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <!-- Removes forward slashes and backslashes from string -->
    <xsl:template name="trim-archetype-id-pattern">
        <!-- e.g. <pattern>/medication\.v1/</pattern>-->
        <xsl:param name="pattern"/>

        <xsl:choose>
            <xsl:when test="contains($pattern, '/')">
                <xsl:call-template name="trim-archetype-id-pattern">
                    <xsl:with-param name="pattern">
                        <xsl:value-of select="substring-before($pattern, '/')"/>
                    </xsl:with-param>
                </xsl:call-template>
                <xsl:call-template name="trim-archetype-id-pattern">
                    <xsl:with-param name="pattern">
                        <xsl:value-of select="substring-after($pattern, '/')"/>
                    </xsl:with-param>
                </xsl:call-template>
            </xsl:when>
            <xsl:when test="contains($pattern, '\')">
                <xsl:call-template name="trim-archetype-id-pattern">
                    <xsl:with-param name="pattern">
                        <xsl:value-of select="substring-before($pattern, '\')"/>
                    </xsl:with-param>
                </xsl:call-template>
                <xsl:call-template name="trim-archetype-id-pattern">
                    <xsl:with-param name="pattern">
                        <xsl:value-of select="substring-after($pattern, '\')"/>
                    </xsl:with-param>
                </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="$pattern"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <xsl:template name="get-cardinality">
        <xsl:param name="lower"/>
        <xsl:param name="lower_included"/>
        <xsl:param name="lower_unbounded"/>
        <xsl:param name="upper"/>
        <xsl:param name="upper_included"/>
        <xsl:param name="upper_unbounded"/>
        <xsl:param name="is_ordered" required="no"/><!-- only relevant for containers -->

        <xsl:choose>
            <xsl:when test="$lower_unbounded='false' and $upper_unbounded='false'">
                <xsl:value-of select="$lower"/>..<xsl:value-of select="$upper"/>
                <xsl:if test="$is_ordered='true'">, ordered</xsl:if>
                <xsl:if test="$show-cardinality-meaning='true'">
                    <xsl:choose>
                        <xsl:when test="$lower>0"> (<!-- lookup terminology  --><xsl:call-template
                                name="LookupTerminologyAndTranslationByRubric">
                                <xsl:with-param name="original-rubric">mandatory</xsl:with-param>
                            </xsl:call-template>, </xsl:when>
                        <xsl:otherwise> (<!-- lookup terminology  --><xsl:call-template
                                name="LookupTerminologyAndTranslationByRubric">
                                <xsl:with-param name="original-rubric">optional</xsl:with-param>
                            </xsl:call-template>, </xsl:otherwise>
                    </xsl:choose>
                    <xsl:choose>
                        <xsl:when test="$upper=1"><!-- lookup terminology  --><xsl:call-template
                                name="LookupTerminologyAndTranslationByRubric">
                                <xsl:with-param name="original-rubric">not
                                repeating</xsl:with-param>
                            </xsl:call-template>)</xsl:when>
                        <xsl:otherwise><!-- lookup terminology  --><xsl:call-template
                                name="LookupTerminologyAndTranslationByRubric">
                                <xsl:with-param name="original-rubric">repeating</xsl:with-param>
                            </xsl:call-template>)</xsl:otherwise>
                    </xsl:choose>
                </xsl:if>
            </xsl:when>
            <xsl:when test="$lower_unbounded='false' and $upper_unbounded='true'">
                <xsl:value-of select="$lower"/>
                <xsl:text>..*</xsl:text>
                <xsl:if test="$is_ordered='true'">, ordered</xsl:if>
                <xsl:if test="$show-cardinality-meaning='true'">
                        (<!-- lookup terminology  --><xsl:call-template
                        name="LookupTerminologyAndTranslationByRubric">
                        <xsl:with-param name="original-rubric">optional</xsl:with-param>
                    </xsl:call-template>, <!-- lookup terminology  --><xsl:call-template
                        name="LookupTerminologyAndTranslationByRubric">
                        <xsl:with-param name="original-rubric">repeating</xsl:with-param>
                    </xsl:call-template>)</xsl:if>
            </xsl:when>
        </xsl:choose>
    </xsl:template>

    <xsl:template name="get-interval-range">
        <xsl:param name="lower"/>
        <xsl:param name="lower_included"/>
        <xsl:param name="lower_unbounded"/>
        <xsl:param name="upper"/>
        <xsl:param name="upper_included"/>
        <xsl:param name="upper_unbounded"/>

        <!-- specify lower interval -->
        <xsl:choose>
            <xsl:when test="$lower_unbounded='false' and $lower_included='false'">
                <xsl:text disable-output-escaping="yes">&gt;</xsl:text>
                <xsl:value-of select="$lower"/>
            </xsl:when>
            <xsl:when test="$lower_unbounded='false' and $lower_included='true'">
                <xsl:text disable-output-escaping="yes">&gt;=</xsl:text>
                <xsl:value-of select="$lower"/>
            </xsl:when>
            <xsl:otherwise>*</xsl:otherwise>
        </xsl:choose>
        <xsl:text>; </xsl:text>
        <!-- specify upper interval -->
        <xsl:choose>
            <xsl:when test="$upper_unbounded='false' and $upper_included='false'">
                <xsl:text disable-output-escaping="yes">&lt;</xsl:text>
                <xsl:value-of select="$upper"/>
            </xsl:when>
            <xsl:when test="$upper_unbounded='false' and $upper_included='true'">
                <xsl:text disable-output-escaping="yes">&lt;=</xsl:text>
                <xsl:value-of select="$upper"/>
            </xsl:when>
            <xsl:otherwise>*</xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <xsl:template name="get-archetype-rm-type">
        <xsl:param name="archetype-id"/>
        <!-- eg. openEHR-EHR-COMPOSITION.Report.v1 -->
        <xsl:variable name="id-minus-authority" select="substring-after($archetype-id, '-')"/>
        <!-- eg EHR-COMPOSITION.Report.v1 -->
        <xsl:variable name="id-minus-rm" select="substring-after($id-minus-authority, '-')"/>
        <!-- eg. COMPOSITION.Report.v1 -->
        <xsl:value-of select="substring-before($id-minus-rm, '.')"/>
        <!-- eg. COMPOSITION -->
    </xsl:template>

    <xsl:template name="get-term-binding">
        <xsl:param name="at-code"/>
        
        <xsl:for-each select="//oe:ontology/oe:term_bindings">
            <xsl:if test="oe:items/@code=$at-code"><xsl:text disable-output-escaping="yes">&lt;br /&gt;&lt;br /&gt;</xsl:text><font class="term-binding">[<xsl:value-of select="@terminology"/>: <xsl:value-of select="oe:items/oe:value/oe:code_string"/>]</font></xsl:if>
        </xsl:for-each>
        
    </xsl:template>

    <xsl:template name="get-node-comment">
        <xsl:param name="at-code"/>
        <xsl:variable name="comment">
            <xsl:value-of select="//oe:ontology/oe:term_definitions[@language=$language]/oe:items[@code=$at-code]/oe:items[@id='comment']"/>
        </xsl:variable>
        <xsl:variable name="original-language">
            <xsl:value-of select="//oe:archetype/oe:original_language/oe:code_string"/>
        </xsl:variable>
        <xsl:choose>
            <xsl:when test="string-length($comment)>0">
                <xsl:value-of select="$comment"/>
            </xsl:when>
            <xsl:otherwise>
                <!-- default to original language -->
                <xsl:if test="string-length(//oe:ontology/oe:term_definitions[@language=$original-language]/oe:items[@code=$at-code]/oe:items[@id='comment'])>0">
                <xsl:value-of
                    select="//oe:ontology/oe:term_definitions[@language=$original-language]/oe:items[@code=$at-code]/oe:items[@id='comment']"
                />(<xsl:value-of select="$original-language"/>)</xsl:if></xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <xsl:template name="get-archetype-term">
        <xsl:param name="at-code"/>
        <xsl:variable name="term">
            <xsl:value-of
                select="//oe:ontology/oe:term_definitions[@language=$language]/oe:items[@code=$at-code]/oe:items[@id='text']"
            />
        </xsl:variable>
        <xsl:variable name="original-language">
            <xsl:value-of select="//oe:archetype/oe:original_language/oe:code_string"/>
        </xsl:variable>
        <xsl:choose>
            <xsl:when test="string-length($term)>0">
                <xsl:value-of select="$term"/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:if test="string-length(//oe:ontology/oe:term_definitions[@language=$original-language]/oe:items[@code=$at-code]/oe:items[@id='text'])>0">
                    <!-- default to original language -->
                    <xsl:value-of
                        select="//oe:ontology/oe:term_definitions[@language=$original-language]/oe:items[@code=$at-code]/oe:items[@id='text']"
                    />(<xsl:value-of select="$original-language"/>)
                </xsl:if>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <xsl:template name="get-archetype-constraint-definition">
        <xsl:param name="at-code"/>
        <xsl:variable name="constraint">
            <xsl:value-of
                select="//oe:ontology/oe:constraint_definitions[@language=$language]/oe:items[@code=$at-code]/oe:items[@id='text']"
            />
        </xsl:variable>
        <xsl:variable name="original-language">
            <xsl:value-of select="//oe:archetype/oe:original_language/oe:code_string"/>
        </xsl:variable>
        <xsl:choose>
            <xsl:when test="string-length($constraint)>0">
                <xsl:value-of select="$constraint"/>
            </xsl:when>
            <xsl:otherwise>
                <!-- default to original language -->
                <xsl:value-of
                    select="//oe:ontology/oe:constraint_definitions[@language=$original-language]/oe:items[@code=$at-code]/oe:items[@id='text']"
                    />(<xsl:value-of select="$original-language"/>)</xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <xsl:template name="get-archetype-description">
        <xsl:param name="at-code"/>
        <xsl:variable name="description">
            <xsl:value-of
                select="//oe:ontology/oe:term_definitions[@language=$language]/oe:items[@code=$at-code]/oe:items[@id='description']"
            />
        </xsl:variable>
        <xsl:variable name="original-language">
            <xsl:value-of select="//oe:archetype/oe:original_language/oe:code_string"/>
        </xsl:variable>
        <xsl:choose>
            <xsl:when test="string-length($description)>0">
                <xsl:value-of select="$description"/>
            </xsl:when>
            <xsl:otherwise>
                <!-- default to original language -->
                <xsl:value-of
                    select="//oe:ontology/oe:term_definitions[@language=$original-language]/oe:items[@code=$at-code]/oe:items[@id='description']"
                    />(<xsl:value-of select="$original-language"/>)</xsl:otherwise>
        </xsl:choose>
    </xsl:template>

</xsl:stylesheet>
