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
        UIManager.Instance.ShowPopupUi<PauseAndRePlayerPopupUI>().SetYesBtn(Exit);
    }

    private void Exit()
    {
        GameManager.Instance.GamePause();
        this.ClosePopupUI();
        GameManager.Instance.OpenStagePopup = true;
        EnemyController.SetPause(true);
        SceneManagerEx.Instance.LoadScene(Scene.Lobby);
    }

    private void OnReStart(PointerEventData data)
    {
        UIManager.Instance.ShowPopupUi<PauseAndRePlayerPopupUI>().SetYesBtn(Restart);
    }

    private void Restart()
    {
        GameManager.Instance.OpenStagePopup = false;
        StageSelectPopupUI popupUI = UIManager.Instance.ShowPopupUi<StageSelectPopupUI>();
        if (popupUI != null)
        {
            popupUI.SetSeleteStageText(DataManager.Instance.playerInfo.SelectStage);
        }
    }

    private void OnReplay(PointerEventData data)
    {
        ClosePopupUI();
        GameManager.Instance.OpenStagePopup = false;
        UIManager.Instance.ShowPopupUi<CountDownPopupUI>();
    }
}
