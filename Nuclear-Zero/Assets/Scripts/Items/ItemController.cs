using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    SpriteRenderer _sprite;
    private void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ItemDestroy();
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
        
    //}

    private void ItemDestroy()
    {
        SetItemInfo();

        this.gameObject.SetActive(false);
    }

    protected virtual void SetItemInfo()
    {
        
    }
}
