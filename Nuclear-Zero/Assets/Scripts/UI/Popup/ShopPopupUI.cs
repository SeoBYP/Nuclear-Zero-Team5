using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPopupUI : PopupUI
{

    public Animation popupAni;

    public override void Init()
    {
        base.Init();
        popupAni.Play();
    }

    
}
