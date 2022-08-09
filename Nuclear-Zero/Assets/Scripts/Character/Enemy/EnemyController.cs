using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IUpdate
{
    protected enum EnemyState
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Dead,
    }

    //protected BoxCollider2D _collider2D;
    protected EnemyState _state;
    protected float xIndex = 1;
    protected PlayerController _player;

    [SerializeField] protected float speed;

    private static bool _isStart = false;
    private static bool _isPause = false;

    private void Start()
    {
        Init();
    }

    public static void SetStart(bool state) { _isStart = state; }
    public static void SetPause(bool state) { _isPause = state; }

    public virtual void Init()
    {
        UpdateManager.Instance.Listener(this);
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _isStart = false;
        _isPause = false;
    }

    private void Update()
    {
        if (_isStart == false)
            return;
        if (_isPause)
        {
            return;
        }
        Run();
    }

    protected virtual void Run()
    {

    }

    

    public void OnUpdate()
    {
        //if (GameManager.Instance.IsStart == false)
        //    return;
        //if (GameManager.Instance.IsClear || GameManager.Instance.IsGameOver)
        //{
        //    return;
        //}
        //Run();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_player == null)
            {
                _player = collision.gameObject.GetComponent<PlayerController>();
                Damaged();
            }
            else
                Damaged();
        }
    }

    public virtual void Damaged()
    {
        
    }

    //private void CheckGrounded()
    //{
    //    Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
    //    Grounded = Physics2D.OverlapBox(spherePosition, GroundedSize, CheckLayer);
    //    //if (Grounded)
    //    //    isJumped = false;
    //}

    public void Clear()
    {
        UpdateManager.Instance.DeleteListener(this);
        gameObject.SetActive(false);
    }
}
