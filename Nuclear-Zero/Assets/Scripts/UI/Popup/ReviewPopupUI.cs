using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;
public class ReviewPopupUI : PopupUI
{
    enum Buttons
    {
        Review,
    }

    enum GameObjects
    {
        BackGround,
    }

    [SerializeField] private string URL;

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        BindEvent(GetButton((int)Buttons.Review).gameObject, OnReview, UIEvents.Click);
        BindEvent(GetGameObject((int)GameObjects.BackGround), OnClose, UIEvents.Click);
    }

    private void OnReview(PointerEventData data)
    {
        DataManager.Instance.playerInfo.Review = true;
        Application.OpenURL(URL);
        ClosePopupUI();
        UIManager.Instance.ShowPopupUi<GameSharedPopupUI>();
    }

    private void OnClose(PointerEventData data) { ClosePopupUI(); }
}
