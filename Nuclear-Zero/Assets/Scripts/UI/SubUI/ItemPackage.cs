using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class ItemPackage : SubUI
{
    Button button;
    [SerializeField] Sprite sprite;
    [SerializeField] Items item;
    public override void Init()
    {
        base.Init();
        button = Utils.BindingFunc(transform, OnItemPachage);
    }

    private void OnItemPachage()
    {
        GameAudioManager.Instance.Play2DSound("Touch");
        CumfumBuyPopupUI popupUI = UIManager.Instance.Get<CumfumBuyPopupUI>();
        if (popupUI != null)
        {
            popupUI.SetDefaultCount();
            popupUI.SetItemIcon(sprite, item);
            return;
        }
        UIManager.Instance.ShowPopupUi<CumfumBuyPopupUI>().SetItemIcon(sprite,item);
    }
}
