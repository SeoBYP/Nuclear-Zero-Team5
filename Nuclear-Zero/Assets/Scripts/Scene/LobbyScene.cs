using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Events;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        UIManager.Instance.ShowSceneUi<LobbyUI>();
        if (DataManager.Instance.playerInfo.LookTutorial == false)
        {
            UIManager.Instance.ShowPopupUi<TutorialPopupUI>();
            DataManager.Instance.playerInfo.LookTutorial = true;
        }
        if (DataManager.Instance.playerInfo.LookPrologue == false)
        {
            UIManager.Instance.ShowPopupUi<ProloguePopupUI>();
            DataManager.Instance.playerInfo.LookPrologue = true;
        }
        if (GameManager.Instance.OpenStagePopup)
        {
            UIManager.Instance.ShowPopupUi<StagePopupUI>();
            GameManager.Instance.OpenStagePopup = false;
        }
        UIManager.Instance.FadeIn();
        //GPGSBinder.Instance.IncrementEvent(GPGSIds.event_test, 0);
        //GPGSBinder.Instance.LoadEvent(GPGSIds.event_test, Onevnet);
    }

    private void Onevnet(bool state,IEvent _event)
    {
        Debug.Log($"{_event.Name}");
    }

    public override void Clear()
    {
        UIManager.Instance.Clear();
    }
}
