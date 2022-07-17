using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {
        UIManager.Instance.ShowSceneUi<LobbyUI>();
        UIManager.Instance.FadeIn();
        yield return YieldInstructionCache.WaitForSeconds(0.9f);
    }

    public override void Clear()
    {
        
    }
}
