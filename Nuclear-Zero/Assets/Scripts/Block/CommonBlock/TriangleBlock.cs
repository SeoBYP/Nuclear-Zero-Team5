using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleBlock : BlockController
{
    private bool _isEffect;
    protected override void Init()
    {
        base.Init();
        _isEffect = false;
    }


    public override void OnSteped()
    {
        base.OnSteped();
        _isEffect = true;
        StartCoroutine(TimeTakeDamages());
    }

    public override void OnExited()
    {
        base.OnExited();
        _isEffect = false;
        StopCoroutine(TimeTakeDamages());
    }
    IEnumerator TimeTakeDamages()
    {
        while (_isEffect)
        {
            if (_isEffect == false)
                yield break;
            PlayerTakeDamage();
            yield return YieldInstructionCache.WaitForSeconds(1f);
        }
    }

    private void PlayerTakeDamage()
    {
        if (_player != null)
            _player.TakeDamage();
    }
}
