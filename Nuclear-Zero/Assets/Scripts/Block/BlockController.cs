using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class BlockController : MonoBehaviour
{
    [SerializeField] protected BoxCollider2D _collider2D;
    [SerializeField] protected BoxCollider2D _trriger2D;
    protected PlayerController _player;
    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            if (_player == null)
                _player = collision.gameObject.GetComponent<PlayerController>();
        OnSteped();
    }

    public virtual void OnSteped()
    {

    }
}
