using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayer : MonoBehaviour
{
    //public bool IsDashAttack;
    public bool IsShootAttack;
    public EnemyController enemyController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //if (IsDashAttack)
            //{
            //    enemyController.DoingDash = true;
            //}
            if (IsShootAttack)
            {
                enemyController.DoShot();
            }
        }
    }

}
