using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{

    protected override void Init()
    {
        base.Init();
        //MapManager.Instance.LoadMap(Define.Map.Map1_1);
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {

        UIManager.Instance.FadeIn();
        yield return YieldInstructionCache.WaitForSeconds(0.9f);
        UIManager.Instance.ShowPopupUi<TutorialPopupUI>();
    }

    public static void Start()
    {
        UIManager.Instance.ShowSceneUi<GameUI>();
    }

    public override void Clear()
    {
        
    }
}
