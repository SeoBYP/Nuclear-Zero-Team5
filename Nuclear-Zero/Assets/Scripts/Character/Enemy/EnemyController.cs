using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IUpdate
{
    Rigidbody2D _rigidbody2D;
    protected EnemyAnimationController animationController;

    public static bool IsStart { get; set; } = false;

    public virtual void Init()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        //animationController = GetComponentInChildren<EnemyAnimationController>();
        //if (animationController != null)
        //    animationController.Init();
        UpdateManager.Instance.Listener(this);
    }

    
    protected virtual void Run()
    {

    }

    public void OnUpdate()
    {
        if (IsStart == false)
            return;
        Run();
    }

    public void Clear()
    {
        UpdateManager.Instance.DeleteListener(this);
        gameObject.SetActive(false);
    }
}
