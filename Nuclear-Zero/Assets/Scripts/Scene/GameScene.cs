using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{

    protected override void Init()
    {
        base.Init();
        UIManager.Instance.ShowPopupUi<TutorialPopupUI>();
        UIManager.Instance.FadeIn();
        //MapManager.Instance.LoadMap(Define.Map.Map1_1);
        //StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {
        UIManager.Instance.FadeIn();
        yield return YieldInstructionCache.WaitForSeconds(1.1f);
        //UIManager.Instance.Get<FadePopupUI>().ClosePopupUI();

    }

    public override void Clear()
    {
        
    }
}
