using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class ResurrectionPopupUI : PopupUI
{
    enum Buttons
    {
        ADButton,
        Exit,
    }

    enum Texts
    {
        Timer,
    }

    private int timer;
    private bool IsExit;

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        BindEvent(GetButton((int)Buttons.ADButton).gameObject, OnAD, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Exit).gameObject, OnExit, UIEvents.Click);
        timer = 5;
        StartCoroutine(WaitForAD());
    }
    private void OnAD(PointerEventData data)
    {
        Debug.Log("???? ???? ?? ????");
    }

    private void OnExit(PointerEventData data)
    {
        ClosePopupUI();
        UIManager.Instance.ShowPopupUi<GameOverPopupUI>();
    }

    IEnumerator WaitForAD()
    {
        GetText((int)Texts.Timer).text = timer.ToString();
        while (timer >= 0)
        {
            if (IsExit)
                yield break;

            yield return YieldInstructionCache.WaitForSeconds(1);
            timer--;
            GetText((int)Texts.Timer).text = timer.ToString();
            if (timer == 0)
                break;
        }
        ClosePopupUI();
        UIManager.Instance.ShowPopupUi<GameOverPopupUI>();
    }
}
