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
        //아이템의 이미지 파일을 불러와서 붙혀준다.

        _text.text = "획득";
    }

    private void OnUseItem(PointerEventData data)
    {
        UIManager.Instance.Get<GameUI>().ReSetItmeButtons();
        _text.text = "Item";
    }
}
