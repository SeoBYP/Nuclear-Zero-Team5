using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        GameAudioManager.Instance.PlayBackGround("LobbyBGM");
        UIManager.Instance.ShowSceneUi<TitleUI>();
        GPGSBinder.Instance.Login();
    }

    public override void Clear()
    {
        //UIManager.Instance.Clear();
    }
}
