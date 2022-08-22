using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    
    //string log;
    protected override void Init()
    {
        base.Init();
        GameAudioManager.Instance.PlayBackGround("LobbyBGM");
        UIManager.Instance.ShowSceneUi<TitleUI>();
        //GPGSBinder.Instance.Login((success, localUser) => Debug.Log($"{success}, {localUser.userName}, {localUser.id}, {localUser.state}, {localUser.underage}"));
    }

    public override void Clear()
    {
        //UIManager.Instance.Clear();
    }
}
