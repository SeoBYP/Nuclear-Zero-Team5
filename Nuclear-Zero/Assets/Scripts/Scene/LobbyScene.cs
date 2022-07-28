using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        UIManager.Instance.ShowSceneUi<LobbyUI>();
        if(GameData.IsLookPrologue == false)
        {
            UIManager.Instance.ShowPopupUi<ProloguePopupUI>();
        }
        UIManager.Instance.FadeIn();
        
        //DataManager.Instance.LoadText(TextType.Chapter1);
    }

    public override void Clear()
    {
        
    }
}
