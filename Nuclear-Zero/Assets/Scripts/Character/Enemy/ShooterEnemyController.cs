using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemyController : EnemyController
{
    public float timer;

    private bool _isshoting = false;

    private float nextShotTime = 3f;
    private float prevTime = 0;
    public override void Init()
    {
        base.Init();
        _isshoting = true;
        StartCoroutine(Shoting());
    }

    protected override void Run()
    {
       // Shoting();
    }

    IEnumerator Shoting()
    {
        while (_isshoting)
        {
            ResourcesManager.Instance.Instantiate("Weapon/Arrow").transform.position = transform.position;

            yield return YieldInstructionCache.WaitForSeconds(timer);
        }
    }

    //private void Shoting()
    //{
    //    float elapsedTime = Time.deltaTime - prevTime;
    //    if(elapsedTime > nextShotTime)
    //    {
    //        print("SHoting");
    //        ResourcesManager.Instance.Instantiate("Weapon/Arrow").transform.position = transform.position;
    //        prevTime = Time.time;
    //    }
    //}

}
