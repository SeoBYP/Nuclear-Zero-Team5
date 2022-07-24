using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;
public class GameUIItemButton : SubUI
{
    Image _image;
    Text _text;
    Button _itemButton;
    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        _image = GetComponent<Image>();
        _itemButton = GetComponent<Button>();
        _text = GetComponentInChildren<Text>();

        BindEvent(_itemButton.gameObject, OnUseItem, UIEvents.Click);
    }

    public void SetItem()
    {
        //???????? ?????? ?????? ???????? ????????.

        _text.text = "??";
    }

    private void OnUseItem(PointerEventData data)
    {
        //UIManager.Instance.Get<GameUI>().ReSetItmeButtons();
        _text.text = "Item";
    }
}
