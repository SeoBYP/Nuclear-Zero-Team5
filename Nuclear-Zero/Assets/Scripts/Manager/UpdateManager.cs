using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : Managers<UpdateManager>
{
    public delegate void DelegateUpdate();
    public event DelegateUpdate _OnUpdate;

    public override void Init()
    {
        base.Init();
    }

    public void Listener(IUpdate update)
    {
        _OnUpdate += update.OnUpdate;
    }

    public void DeleteListener(IUpdate update)
    {
        _OnUpdate -= update.OnUpdate;
    }

    private void Update()
    {
        if (null != _OnUpdate)
            _OnUpdate();
    }
}
