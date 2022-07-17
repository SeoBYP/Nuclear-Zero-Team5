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
        // 플레이어에게 아이템을 주는 코드
        SetItemInfo();

        //플레이어에게 아이템을 주고 아이템을 파괴한다.
        Destroy(this.gameObject);
    }

    protected virtual void SetItemInfo()
    {
        UIManager.Instance.Get<GameUI>().SetItemButtons();
    }
}
