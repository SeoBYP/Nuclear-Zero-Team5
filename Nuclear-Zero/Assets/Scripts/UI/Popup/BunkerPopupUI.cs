using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class BunkerPopupUI : PopupUI
{
    enum GameObjects
    {
        TargetObject,
    }

    enum Buttons
    {
        Close,
    }

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));

        BindEvent(GetGameObject((int)GameObjects.TargetObject), OnTargeted, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Close).gameObject, OnClose, UIEvents.Click);
        UIManager.Instance.ShowPopupUi<TalkTextPopupUI>();
    }

    private void OnTargeted(PointerEventData data)
    {
        UIManager.Instance.ShowPopupUi<GetItemPopupUI>();
        GameManager.IsGetItem = true;
    }

    public void OnExit()
    {
        StartCoroutine(CloseBunkerPopupUI());
    }

    private void OnClose(PointerEventData data)
    {
        if(GameManager.IsGetItem == false)
        {
            UIManager.Instance.ShowPopupUi<BunkerExitPopupUI>();
            return;
        }
        OnExit();
    }
    IEnumerator CloseBunkerPopupUI()
    {
        UIManager.Instance.FadeOut();
        yield return YieldInstructionCache.WaitForSeconds(1.1f);
        UIManager.Instance.Get<FadePopupUI>().ClosePopupUI();
        ClosePopupUI();
        UIManager.Instance.FadeIn();

    }
}
