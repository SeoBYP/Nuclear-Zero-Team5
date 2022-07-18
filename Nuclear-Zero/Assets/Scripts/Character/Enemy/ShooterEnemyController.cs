using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemyController : EnemyController
{
    protected override void Init()
    {
        base.Init();
    }

    protected override void Run()
    {

    }

    public void DoShot()
    {
        //animationController.PlayEnemyShoot();
        ResourcesManager.Instance.Instantiate("Weapon/Arrow").transform.position = transform.position;
    }
}
