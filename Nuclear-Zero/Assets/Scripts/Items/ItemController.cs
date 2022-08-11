using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    SpriteRenderer _sprite;

    [SerializeField] private float _magnetRange;
    PlayerController _player;
    private float _speed = 35f;
    private static bool _playerHasMagnet;
    private bool _isMove;
    private void Start()
    {
        Init();
        _isMove = false;
    }

    public virtual void Init()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ItemDestroy();
        }
    }

    private void ItemDestroy()
    {
        SetItemInfo();
        this.gameObject.SetActive(false);
    }

    protected virtual void SetItemInfo()
    {
        
    }

    public static void SetPlayerHasMagnet(bool state) { _playerHasMagnet = state; }

    private void MoveToPlayer()
    {
        if(_isMove == false)
        {
            if (_player == null)
                _player = Utils.FindObjectOfType<PlayerController>();
            if (Vector2.Distance(transform.position, _player.transform.position) < _magnetRange)
            {
                _isMove = true;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
        }
    }

    private void Update()
    {
        //if (!_playerHasMagnet)
        //{
        //    print("_playerHasMagnet Is False");
        //    _playerHasMagnet = true;
        //}
        if (_playerHasMagnet)
        {
            MoveToPlayer();
        }
    }
}
