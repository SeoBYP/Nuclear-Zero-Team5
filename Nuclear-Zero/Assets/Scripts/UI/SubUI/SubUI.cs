using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubUI : BaseUI
{
    public override void Init()
    {
        
    }

    public void SetActive(bool state)
    {
        this.gameObject.SetActive(state);
    }
}
