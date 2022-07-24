using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile<T> : MonoBehaviour,IUpdate where T : Component
{
    protected PlayerController _player;

    public virtual void Init()
    {
        UpdateManager.Instance.Listener(this);
    }

    public virtual void Excute()
    {

    }

    protected void Run()
    {
        Excute();
    }

    public virtual void Clear()
    {
        UpdateManager.Instance.DeleteListener(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_player == null)
                _player = collision.gameObject.GetComponent<PlayerController>();
            _player.TakeDamage();
        }
    }

    //private void Update()
    //{

    //}

    public void OnUpdate()
    {
        Run();
    }
}
