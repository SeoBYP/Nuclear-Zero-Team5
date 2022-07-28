using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour
{
    [Header("Grounded")]
    private bool Grounded;
    private float GroundedOffset = 2f;
    private Vector2 GroundedSize = new Vector2(1.5f, 0.2f);
    public LayerMask GroundLayer;

    [Header("PlayerJump")]
    private float JumpHeight = 10f;
    private float Gravity = -100;
    private float JumpTimeout = 0.5f;
    private float _jumpTimeoutDelta;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    private bool IsMapLaftCollision = false;
    private bool IsMapRightCollision = false;
    private bool Hited = false;
    private bool _flipX = false;

    public float Timer = 3;
    //public float linearDrag = 4f;

    private PlayerAnimationController animationController;
    private Rigidbody2D _rigidbody2D;
    private AnimatorStateInfo _aniStateInfo;
    private Animator _animator;
    private GameUI gameUI;
    private JoyButton joyButton;
    private bool isJumped = false;

    private int _shieldCount = 0;

    public float speed;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        IsMapLaftCollision = false;
        IsMapRightCollision = false;
        Hited = false;
        _jumpTimeoutDelta = JumpTimeout;

        animationController = GetComponentInChildren<PlayerAnimationController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        if (animationController != null)
            animationController.Init();
        gameUI = UIManager.Instance.Get<GameUI>();
        joyButton = Utils.FindObjectOfType<JoyButton>();
    }

    private void Update()
    {
        if (GameManager.Instance.IsStart == false)
            return;
        if (GameManager.Instance.IsClear || GameManager.Instance.IsGameOver)
        {
            _rigidbody2D.velocity = Vector2.zero;
            return;
        }
        _aniStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        CheckGrounded();
        Jump();
    }
    #region PlayerMovement
    public void Move(Vector2 dir)
    {
        if(dir.x > 0)
        {
            dir.x = 1;
            _flipX = false;
        }
        else if(dir.x < 0)
        {
            dir.x = -1;
            _flipX = true;
        }

        if ((IsMapLaftCollision && dir.x < 0) || (IsMapRightCollision && dir.x > 0))
            dir.x = 0;

        dir.x *= speed;
        dir.y = _verticalVelocity;
        _rigidbody2D.velocity = dir;
        if (dir.x != 0)
            animationController.PlayerRun(true, _flipX);
        else
            animationController.PlayerRun(false, _flipX);
    }

    private void Jump()
    {
        if (Grounded)
        {
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }
            if ((joyButton.Pressed || isJumped) && _jumpTimeoutDelta <= 0.0f)
            {
                _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                animationController.PlayerJump();
                JumpHeight = 10;
                isJumped = false;
            }
            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }
    }

    private void CheckGrounded()
    {
        Vector2 boxPosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset);
        Grounded = Physics2D.OverlapBox(boxPosition, GroundedSize,LayerMask.NameToLayer("Floor") >> 1);
        animationController.IsGrounded(Grounded);
        //print(Grounded);
        //Debug.DrawLine(transform.position,boxPosition, Color.red);
    }

    //private void OnDrawGizmos()
    //{
    //    Vector2 boxPosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset);
    //    Gizmos.DrawWireCube(boxPosition, GroundedSize);
    //    Gizmos.color = Color.red;
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            if(_verticalVelocity > 0)
            {
                _verticalVelocity = transform.position.y;
            }
        }
    }
    #endregion

    #region PlayerDamaged
    public void Dead()
    {
        GameManager.Instance.IsGameOver = true;
        UIManager.Instance.ShowPopupUi<ResurrectionPopupUI>();
    }

    public void TakeDamage()
    {
        if (gameUI == null)
        {
            gameUI = UIManager.Instance.Get<GameUI>();
        }
        if (Hited == true)
            return;
        if (_shieldCount >= 1)
        {
            _shieldCount--;
            print("Shield");
            return;
        }
        Handheld.Vibrate();
        gameUI.DeleteHeart();
        Hited = true;
        animationController.Hited();
        StartCoroutine(DontActive());
    }

    IEnumerator DontActive()
    {
        yield return YieldInstructionCache.WaitForSeconds(Timer);
        Hited = false;
        animationController.Return();
    }
    #endregion

    #region BlockEffects
    public void Shieded()
    {
        _shieldCount++;
    }

    public void SetFast(float value)
    {
        StartCoroutine(Fast(value));
    }
    [SerializeField] private float fastTime;
    IEnumerator Fast(float value)
    {
        float temp = speed;
        speed = value;
        yield return YieldInstructionCache.WaitForSeconds(fastTime);
        speed = temp;
    }

    public void SetSlow(float value)
    {
        StartCoroutine(Slow(value));
    }
    [SerializeField] private float slowTime;
    IEnumerator Slow(float value)
    {
        float temp = speed;
        speed = value;
        yield return YieldInstructionCache.WaitForSeconds(slowTime);
        speed = temp;
    }

    public void Jump(float jumpPower)
    {
        JumpHeight = jumpPower;
        isJumped = true;
        Jump();
    }
    #endregion
}
