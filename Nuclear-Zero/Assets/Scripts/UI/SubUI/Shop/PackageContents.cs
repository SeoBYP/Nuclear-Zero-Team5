using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class PackageContents : SubUI
{
    private PackagePurching packagePurching;
    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        packagePurching = GetComponent<PackagePurching>();
        if (packagePurching != null)
            packagePurching.InitPackage();
    }
}
