using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        UIManager.Instance.ShowSceneUi<TitleUI>();
    }

    public override void Clear()
    {
        
    }
}
