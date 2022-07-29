using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
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

    public static bool IsStart { get; set; } = false;

    private void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        UpdateManager.Instance.Listener(this);
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    protected virtual void Run()
    {

    }

    private void Update()
    {
        if (GameManager.Instance.IsStart == false)
            return;
        if (GameManager.Instance.IsClear || GameManager.Instance.IsGameOver)
        {
            return;
        }
        Run();
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
