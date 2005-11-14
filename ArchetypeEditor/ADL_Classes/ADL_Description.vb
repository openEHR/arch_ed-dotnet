Public Class ADL_Description
    Inherits ArchetypeDescription

    Private mADL_Description As openehr.openehr.am.archetype.description.ARCHETYPE_DESCRIPTION

    Sub SaveToADL()
        ' TODO
    End Sub

    Sub New(ByVal an_adl_archetype As openehr.openehr.am.archetype.ARCHETYPE)
        mADL_Description = an_adl_archetype.description

        ' mADL_Version = mADL_Description.adl_version.to_cil ' set to 1.2 by default
        ' mArchetypePackageURI = mADL_Description.archetype_package_uri.to_cil
        ' MyBase.LifeCycleStateAsString = mADL_Description.lifecycle_state.to_cil

    End Sub
End Class
