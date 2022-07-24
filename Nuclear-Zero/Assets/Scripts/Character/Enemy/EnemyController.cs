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

    //[Header("Grounded")]
    //protected bool Grounded;
    //private float GroundedOffset = 3f;
    //private Vector2 GroundedSize = new Vector2(0.2f, 0.2f);
    //[SerializeField] private LayerMask CheckLayer;

    protected BoxCollider2D _collider2D;
    protected EnemyState _state;
    protected float xIndex = 1;
    protected PlayerController _player;

    [SerializeField] protected float speed;

    public static bool IsStart { get; set; } = false;

    public virtual void Init()
    {
        //_rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<BoxCollider2D>();
        _state = EnemyState.Idle;
        //animationController = GetComponentInChildren<EnemyAnimationController>();
        //if (animationController != null)
        //    animationController.Init();
        UpdateManager.Instance.Listener(this);
    }

    protected virtual void Run()
    {
        //CheckGrounded();
    }


    public void OnUpdate()
    {
        if (IsStart == false)
            return;
        Run();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_player == null)
            {
                _player = collision.gameObject.GetComponent<PlayerController>();
            }
            Damaged();
        }
    }

    protected virtual void Damaged()
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
