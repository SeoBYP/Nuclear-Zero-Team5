using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class ItemContents : SubUI
{
    enum ItemPackages
    {
        ItemPackage1,
        ItemPackage2,
        ItemPackage3,
    }

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<ItemPackage>(typeof(ItemPackages));

        FindItemPackages();
    }

    private void FindItemPackages()
    {
        for (int i = 0; i <= (int)ItemPackages.ItemPackage3; i++)
        {
            Get<ItemPackage>(i).Init();
        }
    }
}
