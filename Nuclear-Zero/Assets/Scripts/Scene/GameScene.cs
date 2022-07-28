using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{

    protected override void Init()
    {
        base.Init();
        UIManager.Instance.ShowPopupUi<TutorialPopupUI>();
        if(GameData.dialogueObjectName != string.Empty)
        {
            UIManager.Instance.ShowPopupUi<DialoguePopupUI>();
        }
        UIManager.Instance.FadeIn();
    }

    public override void Clear()
    {
        
    }
}
