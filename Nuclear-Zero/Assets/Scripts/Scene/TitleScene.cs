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
        GPGSBinder.Instance.LogIn();
    }

    public override void Clear()
    {
        //UIManager.Instance.Clear();
    }
}
