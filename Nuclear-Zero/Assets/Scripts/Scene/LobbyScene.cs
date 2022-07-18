using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        UIManager.Instance.ShowSceneUi<LobbyUI>();
    }

    public override void Clear()
    {
        
    }
}
