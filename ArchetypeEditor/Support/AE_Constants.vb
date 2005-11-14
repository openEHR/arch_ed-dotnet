'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$Source$"
'	revision:    "$Revision$"
'	last_change: "$Date$"
'
'

Public Class AE_Constants

    Private mSpecialise As String
    Private mMustAddCriteria As String
    Private mReduceTableToSimpleValue As String
    Private mChooseMenuFileNew As String
    Private mAndAllReferences As String
    Private mExpandAll As String
    Private mCollapseAll As String
    Private mAddReference As String
    Private mOceanArchetypeEditor As String
    Private mYouHaveChosenALanguage As String
    Private mAddTerminology As String
    Private mLanguageAdditionCancelled As String
    Private mCurrentArchetype As String
    Private mDiscardChanges As String
    Private mCouldNotFind As String
    Private mPleaseSetConceptText As String
    Private mRemoveState As String
    Private mErrorLoading As String
    Private mErrorSaving As String
    Private mArchetypeNodeCodeNotPassed As String
    Private mSaveChanges As String
    Private mIncorrectFormat As String
    Private mNoDefinitionFor As String
    Private mRemove As String
    Private mObjectColumnNameAlreadyInUse As String
    Private mReduceListToSingleValue As String
    Private mSelectedNodeIsNotElement As String
    Private mSelectItem As String
    Private mReduceTreeToList As String
    Private mNameAlreadyInUse As String
    Private mProtocol As String
    Private mPersonState As String
    Private mCannotDelete As String
    Private mEnterNewName As String
    Private mCannotRename As String
    Private mFeatureNotAvailable As String
    Private mConvertConstraint As String
    Private mConvertStringToText As String
    Private mConvertTextToStrings As String
    Private mConvertTextToOrdinals As String
    Private mConvertOrdinalToAllowableValues As String
    Private mConvertInternalCodedToFreeText As String
    Private mSlot As String
    Private mCluster As String
    Private mSection As String
    Private mText As String
    Private mDescription As String
    Private mCannotSpecialiseReference
    Private mCount As String
    Private mDuration As String
    Private mBoolean As String
    Private mAny As String
    Private mQuantity As String
    Private mDateTime As String
    Private mOrdinal As String
    Private mRatio As String
    Private mUnit As String
    Private mMultiple As String
    Private mRequiresSpecialisationToEdit As String
    Private mInterval As String
    Private mIntervalCount As String
    Private mIntervalQuantity As String
    Private mIntervalDateTime As String
    Private mSetAbsoluteMax As String
    Private mSetAbsoluteMin As String
    Private mOK As String
    Private mCancel As String
    Private mTerminology As String
    Private mInternalCodes As String
    Private mMultiMedia As String
    Private mURI As String
    Private mDragDropHere As String
    Private mReplaceTranslations As String

    Friend ReadOnly Property ReplaceTranslations() As String
        Get
            Return mReplaceTranslations
        End Get
    End Property

    Friend ReadOnly Property DragDropHere() As String
        Get
            Return mDragDropHere
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

    Friend ReadOnly Property Ratio() As String
        Get
            Return mRatio
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

    Friend ReadOnly Property Duplicate_Object_column_name()
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

    ' AE_Constants Singleton
    Private Shared mInstance As AE_Constants
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
        mRatio = TerminologyServer.Instance.RubricForCode(321, Language)
        mUnit = TerminologyServer.Instance.RubricForCode(117, Language)
        mMultiple = TerminologyServer.Instance.RubricForCode(320, Language)
        mCannotSpecialiseReference = TerminologyServer.Instance.RubricForCode(326, Language)
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