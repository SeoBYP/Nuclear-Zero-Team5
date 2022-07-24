using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SceneUI : BaseUI
{
    //public UIList CurrentUIList;
    public override void Init()
    {
        UIManager.Instance.SetCanvas(gameObject);
    }
}
