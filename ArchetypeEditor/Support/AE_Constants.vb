'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Public Class AE_Constants

    Private mSpecialise As String = "Specialise"
    Private mSpecialisationsToo As String = "Use specialisations too?"
    Private mMustAddCriteria As String = "Must add criteria"
    Private mReduceTableToSimpleValue As String = "Reduce table to single value"
    Private mChooseMenuFileNew As String = "Choose Menu > File > New"
    Private mAndAllReferences As String = "and all references"
    Private mExpandAll As String = "Expand all"
    Private mCollapseAll As String = "Collapse all"
    Private mAddReference As String = "Add reference"
    Private mOceanArchetypeEditor As String = "Ocean archetype editor"
    Private mYouHaveChosenALanguage As String = "You have chosen a language "
    Private mAddTerminology As String = "Add terminology"
    Private mLanguageAdditionCancelled As String = "Adding language cancelled"
    Private mCurrentArchetype As String = "Current archetype"
    Private mDiscardChanges As String = "Discard changes"
    Private mCouldNotFind As String = "Could not find "
    Private mLocateFile As String = "Do you want to locate the file"
    Private mPleaseSetConceptText As String = "Please set concept text"
    Private mRemoveState As String = "Remove state information"
    Private mErrorLoading As String = "Error on load"
    Private mErrorSaving As String = "Error on save"
    Private mArchetypeNodeCodeNotPassed As String = "Archetype node code did not pass"
    Private mSaveChanges As String = "Save changes"
    Private mIncorrectFormat As String = "Incorrect format"
    Private mNoDefinitionFor As String = "No definition for "
    Private mRemove As String = "Remove"
    Private mObjectColumnNameAlreadyInUse As String = "Column name already in use"
    Private mReduceListToSingleValue As String = "Reduce list to single value (first only)"
    Private mSelectedNodeIsNotElement As String = "Selected node is not an element"
    Private mSelectItem As String = "Select item"
    Private mReduceTreeToList As String = "Reduce tree to list"
    Private mNameAlreadyInUse As String = "Name already in use"
    Private mProtocol As String = "Protocol"
    Private mPersonState As String = "State"
    Private mCannotDelete As String = "Cannot delete"
    Private mEnterNewName As String = "Enter new name"
    Private mCannotRename As String = "Cannot rename"
    Private mFeatureNotAvailable As String = "Feature not available"
    Private mConvertConstraint As String = "Convert constraint"
    Private mConvertStringToText As String = "Convert string to text"
    Private mConvertTextToStrings As String = "Convert text to strings"
    Private mConvertTextToOrdinals As String = "Convert text to ordinals"
    Private mConvertOrdinalToAllowableValues As String = "Convert ordinal to allowed values"
    Private mConvertInternalCodedToFreeText As String = "Convert internal codes to free text"
    Private mSlot As String = "Slot"
    Private mCluster As String = "Cluster"
    Private mSection As String = "Section"
    Private mText As String = "Text"
    Private mDescription As String = "Description"
    Private mCannotSpecialiseReference As String = "Cannot specialise a reference"
    Private mCount As String = "Count"
    Private mDuration As String = "Duration"
    Private mBoolean As String = "Boolean"
    Private mAny As String = "Any"
    Private mQuantity As String = "Quantity"
    Private mDateTime As String = "DateTime"
    Private mOrdinal As String = "Ordinal"
    Private mProportion As String = "Proportion"
    Private mUnit As String = "Unit"
    Private mMultiple As String = "Multiple"
    Private mRequiresSpecialisationToEdit As String = "Requires specialisation to edit"
    Private mInterval As String = "Interval"
    Private mIntervalCount As String = "Interval of Count"
    Private mIntervalQuantity As String = "Interval of Quantity"
    Private mIntervalDateTime As String = "Interval of DateTime"
    Private mSetAbsoluteMax As String = "Set absolute maximum"
    Private mSetAbsoluteMin As String = "Set absolute minimum"
    Private mOK As String = "OK"
    Private mCancel As String = "Cancel"
    Private mTerminology As String = "Terminology"
    Private mInternalCodes As String = "Internal codes"
    Private mMultiMedia As String = "Multimedia"
    Private mURI As String = "URI"
    Private mDragDropHere As String = "Drag and drop here"
    Private mReplaceTranslations As String = "Replace translations"
    Private mRename As String = "Rename"
    Private mChangeStructure As String = "Change structure"
    Private mLower As String = "Lower"
    Private mUpper As String = "Upper"
    Private mIntegral As String = "Integral"
    Private mUnitary As String = "Unitary"
    Private mIntegerFraction As String = "Integer and fraction"
    Private mFraction As String = "Fraction"
    Private mTrue As String = "True"
    Private mFalse As String = "False"
    Private mChangeDataType As String = "Change data type"
    Private mCardinality As String = "Cardinality"

    Friend ReadOnly Property Lower() As String
        Get
            Return mLower
        End Get
    End Property
    Friend ReadOnly Property Upper() As String
        Get
            Return mUpper
        End Get
    End Property
    Friend ReadOnly Property ReplaceTranslations() As String
        Get
            Return mReplaceTranslations
        End Get
    End Property

    Friend ReadOnly Property ChangeStructure() As String
        Get
            Return mChangeStructure
        End Get
    End Property
    Friend ReadOnly Property Rename() As String
        Get
            Return mRename
        End Get
    End Property

    Friend ReadOnly Property DragDropHere() As String
        Get
            Return mDragDropHere
        End Get
    End Property

    Friend ReadOnly Property Cardinality() As String
        Get
            Return mCardinality
        End Get
    End Property

    Friend ReadOnly Property URI() As String
        Get
            Return mURI
        End Get
    End Property
    Friend ReadOnly Property MultiMedia() As String
        Get
            Return mMultiMedia
        End Get
    End Property
    Friend ReadOnly Property InternalCodes() As String
        Get
            Return mInternalCodes
        End Get
    End Property
    Friend ReadOnly Property Terminology() As String
        Get
            Return mTerminology
        End Get
    End Property
    Friend ReadOnly Property Cancel() As String
        Get
            Return mCancel
        End Get
    End Property
    Friend ReadOnly Property OK() As String
        Get
            Return mOK
        End Get
    End Property
    Friend ReadOnly Property SetAbsoluteMax() As String
        Get
            Return mSetAbsoluteMax
        End Get
    End Property
    Friend ReadOnly Property SetAbsoluteMin() As String
        Get
            Return mSetAbsoluteMin
        End Get
    End Property
    Friend ReadOnly Property Multiple() As String
        Get
            Return mMultiple
        End Get
    End Property

    Friend ReadOnly Property Description() As String
        Get
            Return mDescription
        End Get
    End Property

    Friend ReadOnly Property Proportion() As String
        Get
            Return mProportion
        End Get
    End Property
    Friend ReadOnly Property Cannot_specialise_reference() As String
        Get
            Return mCannotSpecialiseReference
        End Get
    End Property

    Friend ReadOnly Property Unit() As String
        Get
            Return mUnit
        End Get
    End Property

    Friend ReadOnly Property Specialise() As String
        Get
            Return mSpecialise
        End Get
    End Property

    Friend ReadOnly Property SpecialisationsToo() As String
        Get
            Return mSpecialisationsToo
        End Get
    End Property

    Friend ReadOnly Property Reduce_table_to_simple() As String
        Get
            Return mReduceTableToSimpleValue
        End Get
    End Property

    Friend ReadOnly Property Interval() As String
        Get
            Return mInterval
        End Get
    End Property

    Friend ReadOnly Property IntervalQuantity() As String
        Get
            Return mIntervalQuantity
        End Get
    End Property

    Friend ReadOnly Property IntervalCount() As String
        Get
            Return mIntervalCount
        End Get
    End Property

    Friend ReadOnly Property IntervalDateTime() As String
        Get
            Return mIntervalDateTime
        End Get
    End Property

    Friend ReadOnly Property Duration() As String
        Get
            Return mDuration
        End Get
    End Property


    Friend ReadOnly Property Restart() As String
        Get
            Return mChooseMenuFileNew
        End Get
    End Property
    Friend ReadOnly Property All_References() As String
        Get
            Return mAndAllReferences
        End Get
    End Property
    Friend ReadOnly Property Expand_All() As String
        Get
            Return mExpandAll
        End Get
    End Property
    Friend ReadOnly Property Collapse_All() As String
        Get
            Return mCollapseAll
        End Get
    End Property
    Friend ReadOnly Property Add_Reference() As String
        Get
            Return mAddReference
        End Get
    End Property
    Friend ReadOnly Property MessageBoxCaption() As String
        Get
            Return mOceanArchetypeEditor
        End Get
    End Property

    Friend ReadOnly Property NewLanguage() As String
        Get
            Return mYouHaveChosenALanguage
        End Get
    End Property

    Friend ReadOnly Property NewTerminology() As String
        Get
            Return mAddTerminology & " - "
        End Get
    End Property

    Friend ReadOnly Property Language_addition_cancelled() As String
        Get
            Return mLanguageAdditionCancelled
        End Get
    End Property

    Friend ReadOnly Property Current_archetype() As String
        Get
            Return mCurrentArchetype
        End Get
    End Property

    Friend ReadOnly Property Discard_changes() As String
        Get
            Return mDiscardChanges
        End Get
    End Property

    Friend ReadOnly Property Could_not_find() As String
        Get
            Return mCouldNotFind
        End Get
    End Property

    Friend ReadOnly Property Locate_file_yourself() As String
        Get
            Return mLocateFile
        End Get
    End Property

    Friend ReadOnly Property Set_concept() As String
        Get
            Return mPleaseSetConceptText
        End Get
    End Property

    Friend ReadOnly Property Remove_state() As String
        Get
            Return mRemoveState
        End Get
    End Property
    Friend ReadOnly Property Error_loading() As String
        Get
            Return mErrorLoading
        End Get
    End Property
    Friend ReadOnly Property Error_saving() As String
        Get
            Return mErrorSaving
        End Get
    End Property
    Friend ReadOnly Property No_ID() As String
        Get
            Return mArchetypeNodeCodeNotPassed
        End Get
    End Property

    Friend ReadOnly Property Save_changes() As String
        Get
            Return mSaveChanges
        End Get
    End Property

    Friend ReadOnly Property Incorrect_format() As String
        Get
            Return mIncorrectFormat
        End Get
    End Property

    Friend ReadOnly Property No_definition() As String
        Get
            Return mNoDefinitionFor & " - "
        End Get
    End Property

    Friend ReadOnly Property Remove() As String
        Get
            Return mRemove & " "
        End Get
    End Property

    Friend ReadOnly Property Duplicate_Object_column_name() As String
        Get
            Return mObjectColumnNameAlreadyInUse
        End Get
    End Property
    Friend ReadOnly Property Reduce_list_to_simple() As String
        Get
            Return mReduceListToSingleValue
        End Get
    End Property

    Friend ReadOnly Property Not_element() As String
        Get
            Return mSelectedNodeIsNotElement
        End Get
    End Property

    Friend ReadOnly Property SelectItem() As String
        Get
            Return mSelectItem
        End Get
    End Property

    Friend ReadOnly Property False_() As String
        Get
            Return mFalse
        End Get
    End Property

    Friend ReadOnly Property True_() As String
        Get
            Return mTrue
        End Get
    End Property

    Friend ReadOnly Property Reduce_tree_to_list() As String
        Get
            Return mReduceTreeToList
        End Get
    End Property

    Friend ReadOnly Property Duplicate_name() As String
        Get
            Return mNameAlreadyInUse
        End Get
    End Property

    Friend ReadOnly Property Protocol() As String
        Get
            Return mProtocol
        End Get
    End Property

    Friend ReadOnly Property Person_state() As String
        Get
            Return mPersonState
        End Get
    End Property

    Friend ReadOnly Property Cannot_delete() As String
        Get
            Return mCannotDelete
        End Get
    End Property

    Friend ReadOnly Property New_name() As String
        Get
            Return mEnterNewName
        End Get
    End Property

    Friend ReadOnly Property Cannot_rename() As String
        Get
            Return mCannotRename
        End Get
    End Property

    Friend ReadOnly Property Feature_not_available() As String
        Get
            Return mFeatureNotAvailable
        End Get
    End Property

    Friend ReadOnly Property Convert_constraint_loose_data() As String
        Get
            Return mConvertConstraint
        End Get
    End Property

    Friend ReadOnly Property Convert_string_text() As String
        Get
            Return mConvertStringToText
        End Get
    End Property

    Friend ReadOnly Property Must_add_criteria() As String
        Get
            Return mMustAddCriteria
        End Get
    End Property

    Friend ReadOnly Property Convert_text_string() As String
        Get
            Return mConvertTextToStrings
        End Get
    End Property

    Friend ReadOnly Property Convert_text_ordinal() As String
        Get
            Return mConvertTextToOrdinals
        End Get
    End Property

    Friend ReadOnly Property Convert_ordinal_text() As String
        Get
            Return mConvertOrdinalToAllowableValues
        End Get
    End Property

    Friend ReadOnly Property Convert_internal_text() As String
        Get
            Return mConvertInternalCodedToFreeText
        End Get
    End Property

    Friend ReadOnly Property ChangeDataType() As String
        Get
            Return mChangeDataType
        End Get
    End Property
    Friend ReadOnly Property Slot() As String
        Get
            Return mSlot
        End Get
    End Property

    Friend ReadOnly Property Cluster() As String
        Get
            Return mCluster
        End Get
    End Property

    Friend ReadOnly Property Section() As String
        Get
            Return mSection
        End Get
    End Property
    Friend ReadOnly Property Text() As String
        Get
            Return mText
        End Get
    End Property
    Friend ReadOnly Property Count() As String
        Get
            Return mCount
        End Get
    End Property
    Friend ReadOnly Property Boolean_() As String
        Get
            Return mBoolean
        End Get
    End Property
    Friend ReadOnly Property Any() As String
        Get
            Return mAny
        End Get
    End Property
    Friend ReadOnly Property Quantity() As String
        Get
            Return mQuantity
        End Get
    End Property
    Friend ReadOnly Property DateTime() As String
        Get
            Return mDateTime
        End Get
    End Property
    Friend ReadOnly Property Ordinal() As String
        Get
            Return mOrdinal
        End Get
    End Property
    Friend ReadOnly Property RequiresSpecialisationToEdit() As String
        Get
            Return mRequiresSpecialisationToEdit
        End Get
    End Property
    Friend ReadOnly Property Integral() As String
        Get
            Return mIntegral
        End Get
    End Property
    Friend ReadOnly Property Unitary() As String
        Get
            Return mUnitary
        End Get
    End Property
    Friend ReadOnly Property Fraction() As String
        Get
            Return mFraction
        End Get
    End Property
    Friend ReadOnly Property IntegerFraction() As String
        Get
            Return mIntegerFraction
        End Get
    End Property




    ' AE_Constants Singleton
    Private Shared mInstance As AE_Constants
    Public Shared ReadOnly Property HasInstance() As Boolean
        Get
            Return Not mInstance Is Nothing
        End Get
    End Property
    Public Shared ReadOnly Property Instance() As AE_Constants
        Get
            If mInstance Is Nothing Then
                Throw New InvalidOperationException("AE_Constants instance not created")
            End If

            Return mInstance
        End Get
    End Property

    Public Shared Function Create(ByVal aLanguage As String) As AE_Constants
        mInstance = New AE_Constants(aLanguage)

        Return mInstance
    End Function

    Protected Sub New(ByVal Language As String)
        mSpecialise = TerminologyServer.Instance.RubricForCode(185, Language)
        mSpecialisationsToo = TerminologyServer.Instance.RubricForCode(669, Language)
        mReduceTableToSimpleValue = TerminologyServer.Instance.RubricForCode(277, Language)
        mChooseMenuFileNew = TerminologyServer.Instance.RubricForCode(279, Language)
        mAndAllReferences = TerminologyServer.Instance.RubricForCode(280, Language)
        mExpandAll = TerminologyServer.Instance.RubricForCode(281, Language)
        mCollapseAll = TerminologyServer.Instance.RubricForCode(282, Language)
        mAddReference = TerminologyServer.Instance.RubricForCode(283, Language)
        mOceanArchetypeEditor = TerminologyServer.Instance.RubricForCode(284, Language)
        mYouHaveChosenALanguage = TerminologyServer.Instance.RubricForCode(285, Language)
        mAddTerminology = TerminologyServer.Instance.RubricForCode(71, Language)
        mLanguageAdditionCancelled = TerminologyServer.Instance.RubricForCode(286, Language)
        mCurrentArchetype = TerminologyServer.Instance.RubricForCode(287, Language)
        mDiscardChanges = TerminologyServer.Instance.RubricForCode(288, Language)
        mCouldNotFind = TerminologyServer.Instance.RubricForCode(289, Language)        
        mPleaseSetConceptText = TerminologyServer.Instance.RubricForCode(290, Language)
        mRemoveState = TerminologyServer.Instance.RubricForCode(291, Language)
        mErrorLoading = TerminologyServer.Instance.RubricForCode(292, Language)
        mErrorSaving = TerminologyServer.Instance.RubricForCode(293, Language)
        mArchetypeNodeCodeNotPassed = TerminologyServer.Instance.RubricForCode(294, Language)
        mSaveChanges = TerminologyServer.Instance.RubricForCode(295, Language)
        mIncorrectFormat = TerminologyServer.Instance.RubricForCode(296, Language)
        mNoDefinitionFor = TerminologyServer.Instance.RubricForCode(297, Language)
        mRemove = TerminologyServer.Instance.RubricForCode(152, Language)
        mObjectColumnNameAlreadyInUse = TerminologyServer.Instance.RubricForCode(298, Language)
        mReduceListToSingleValue = TerminologyServer.Instance.RubricForCode(299, Language)
        mSelectedNodeIsNotElement = TerminologyServer.Instance.RubricForCode(300, Language)
        mSelectItem = TerminologyServer.Instance.RubricForCode(301, Language)
        mReduceTreeToList = TerminologyServer.Instance.RubricForCode(302, Language)
        mNameAlreadyInUse = TerminologyServer.Instance.RubricForCode(303, Language)
        mProtocol = TerminologyServer.Instance.RubricForCode(78, Language)
        mPersonState = TerminologyServer.Instance.RubricForCode(82, Language)
        mCannotDelete = TerminologyServer.Instance.RubricForCode(304, Language)
        mEnterNewName = TerminologyServer.Instance.RubricForCode(305, Language)
        mCannotRename = TerminologyServer.Instance.RubricForCode(306, Language)
        mFeatureNotAvailable = TerminologyServer.Instance.RubricForCode(307, Language)
        mConvertConstraint = TerminologyServer.Instance.RubricForCode(308, Language)
        mConvertStringToText = TerminologyServer.Instance.RubricForCode(309, Language)
        mConvertTextToStrings = TerminologyServer.Instance.RubricForCode(310, Language)
        mConvertTextToOrdinals = TerminologyServer.Instance.RubricForCode(317, Language)
        mConvertOrdinalToAllowableValues = TerminologyServer.Instance.RubricForCode(311, Language)
        mConvertInternalCodedToFreeText = TerminologyServer.Instance.RubricForCode(318, Language)
        mSlot = TerminologyServer.Instance.RubricForCode(312, Language)
        mCluster = TerminologyServer.Instance.RubricForCode(313, Language)
        mSection = TerminologyServer.Instance.RubricForCode(314, Language)
        mText = TerminologyServer.Instance.RubricForCode(91, Language)
        mCount = TerminologyServer.Instance.RubricForCode(120, Language)
        mBoolean = TerminologyServer.Instance.RubricForCode(315, Language)
        mAny = TerminologyServer.Instance.RubricForCode(316, Language)
        mQuantity = TerminologyServer.Instance.RubricForCode(115, Language)
        mDateTime = TerminologyServer.Instance.RubricForCode(161, Language)
        mDuration = TerminologyServer.Instance.RubricForCode(142, Language)
        mOrdinal = TerminologyServer.Instance.RubricForCode(156, Language)
        mRequiresSpecialisationToEdit = TerminologyServer.Instance.RubricForCode(319, Language)
        mProportion = TerminologyServer.Instance.RubricForCode(507, Language)
        mUnit = TerminologyServer.Instance.RubricForCode(117, Language)
        mMultiple = TerminologyServer.Instance.RubricForCode(320, Language)
        mCannotSpecialiseReference = TerminologyServer.Instance.RubricForCode(607, Language)
        mMustAddCriteria = TerminologyServer.Instance.RubricForCode(327, Language)
        mDescription = TerminologyServer.Instance.RubricForCode(113, Language)
        mInterval = TerminologyServer.Instance.RubricForCode(141, Language)
        mIntervalCount = TerminologyServer.Instance.RubricForCode(330, Language)
        mIntervalQuantity = TerminologyServer.Instance.RubricForCode(329, Language)
        mIntervalDateTime = TerminologyServer.Instance.RubricForCode(516, Language)
        mSetAbsoluteMax = TerminologyServer.Instance.RubricForCode(332, Language)
        mSetAbsoluteMin = TerminologyServer.Instance.RubricForCode(333, Language)
        mOK = TerminologyServer.Instance.RubricForCode(165, Language)
        mCancel = TerminologyServer.Instance.RubricForCode(166, Language)
        mTerminology = TerminologyServer.Instance.RubricForCode(47, Language)
        mInternalCodes = TerminologyServer.Instance.RubricForCode(150, Language)
        mMultiMedia = TerminologyServer.Instance.RubricForCode(386, Language)
        mURI = TerminologyServer.Instance.RubricForCode(430, Language)
        mDragDropHere = TerminologyServer.Instance.RubricForCode(440, Language)
        mReplaceTranslations = TerminologyServer.Instance.RubricForCode(442, Language)
        mRename = TerminologyServer.Instance.RubricForCode(325, Language)
        mChangeStructure = TerminologyServer.Instance.RubricForCode(326, Language)
        mLower = TerminologyServer.Instance.RubricForCode(641, Language)
        mUpper = TerminologyServer.Instance.RubricForCode(642, Language)
        mIntegral = TerminologyServer.Instance.RubricForCode(643, Language)
        mUnitary = TerminologyServer.Instance.RubricForCode(644, Language)
        mIntegerFraction = TerminologyServer.Instance.RubricForCode(645, Language)
        mFraction = TerminologyServer.Instance.RubricForCode(646, Language)
        mTrue = TerminologyServer.Instance.RubricForCode(159, Language)
        mFalse = TerminologyServer.Instance.RubricForCode(160, Language)
        mChangeDataType = TerminologyServer.Instance.RubricForCode(60, Language)
        mCardinality = TerminologyServer.Instance.RubricForCode(437, Language)
        'mLocateFile = TerminologyServer.Instance.RubricForCode(?, Language)
    End Sub
End Class

'
'***** BEGIN LICENSE BLOCK *****
'Version: MPL 1.1/GPL 2.0/LGPL 2.1
'
'The contents of this file are subject to the Mozilla Public License Version 
'1.1 (the "License"); you may not use this file except in compliance with 
'the License. You may obtain a copy of the License at 
'http://www.mozilla.org/MPL/
'
'Software distributed under the License is distributed on an "AS IS" basis,
'WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
'for the specific language governing rights and limitations under the
'License.
'
'The Original Code is Constants.vb.
'
'The Initial Developer of the Original Code is
'Sam Heard, Ocean Informatics (www.oceaninformatics.biz).
'Portions created by the Initial Developer are Copyright (C) 2004
'the Initial Developer. All Rights Reserved.
'
'Contributor(s):
'	Heath Frankel
'
'Alternatively, the contents of this file may be used under the terms of
'either the GNU General Public License Version 2 or later (the "GPL"), or
'the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
'in which case the provisions of the GPL or the LGPL are applicable instead
'of those above. If you wish to allow use of your version of this file only
'under the terms of either the GPL or the LGPL, and not to allow others to
'use your version of this file under the terms of the MPL, indicate your
'decision by deleting the provisions above and replace them with the notice
'and other provisions required by the GPL or the LGPL. If you do not delete
'the provisions above, a recipient may use your version of this file under
'the terms of any one of the MPL, the GPL or the LGPL.
'
'***** END LICENSE BLOCK *****
'
