using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        UIManager.Instance.ShowSceneUi<LobbyUI>();
        UIManager.Instance.FadeIn();

        //StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {

        yield return YieldInstructionCache.WaitForSeconds(1.1f);
        //UIManager.Instance.Get<FadePopupUI>().ClosePopupUI();
        //UIManager.Instance.ShowSceneUi<LobbyUI>();
    }

    public override void Clear()
    {
        
    }
}
