Public Class ADL_Description
    Inherits ArchetypeDescription

    Private mADL_Description As openehr.openehr.am.archetype.description.ARCHETYPE_DESCRIPTION

    Function ADL_Description() As openehr.openehr.am.archetype.description.ARCHETYPE_DESCRIPTION
        Return mADL_Description
    End Function

    Sub New(ByVal an_adl_archetype As openehr.openehr.am.archetype.ARCHETYPE)

        mADL_Description = an_adl_archetype.description
        'mADL_Version = mADL_Description.adl_version.to_cil ' set to 1.2 by default
        If Not mADL_Description.archetype_package_uri Is Nothing Then
            mArchetypePackageURI = mADL_Description.archetype_package_uri.as_string.to_cil
        End If
        MyBase.LifeCycleStateAsString = mADL_Description.lifecycle_state.to_cil
    End Sub

    Sub New()
        mADL_Description = openehr.openehr.am.archetype.description.Create.ARCHETYPE_DESCRIPTION.make_author(openehr.base.kernel.Create.STRING.make_from_cil(ArchetypeEditor.Instance.Options.UserName))
    End Sub
End Class
