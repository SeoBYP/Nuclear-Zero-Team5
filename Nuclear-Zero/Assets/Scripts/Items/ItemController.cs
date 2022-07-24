using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    SpriteRenderer _sprite;
    private void Start()
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

    private void ItemDestroy()
    {
        // ???????????? ???????? ???? ????
        SetItemInfo();

        //???????????? ???????? ???? ???????? ????????.
        Destroy(this.gameObject);
    }

    protected virtual void SetItemInfo()
    {
        //UIManager.Instance.Get<GameUI>().SetItemButtons();
    }
}
