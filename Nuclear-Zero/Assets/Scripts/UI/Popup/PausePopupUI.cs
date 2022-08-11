using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;
public class PausePopupUI : PopupUI
{
    enum Buttons
    {
        Replay,
        ReStart,
        Exit,
    }

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.Replay).gameObject, OnReplay, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.ReStart).gameObject, OnReStart, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Exit).gameObject, OnExit, UIEvents.Click);
    }

    private void OnExit(PointerEventData data)
    {
        //print("OnExit");
        GameManager.Instance.GamePause();
        ClosePopupUI();
        GameManager.Instance.OpenStagePopup = true;
        EnemyController.SetPause(true);
        SceneManagerEx.Instance.LoadScene(Scene.Lobby);
    }

    private void OnReStart(PointerEventData data)
    {
        GameManager.Instance.GamePause();
        GameManager.Instance.OpenStagePopup = false;
        SceneManagerEx.Instance.ReLoadScene(Scene.Game);
    }

    private void OnReplay(PointerEventData data)
    {
        ClosePopupUI();
        GameManager.Instance.OpenStagePopup = false;
        UIManager.Instance.ShowPopupUi<CountDownPopupUI>();
    }
}
