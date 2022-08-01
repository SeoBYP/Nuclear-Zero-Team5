using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour
{
    [Header("Grounded")]
    private bool Grounded;
    private float GroundedOffset = 2f;
    private Vector2 GroundedSize = new Vector2(1.9f, 0.2f);
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
        //CheckFloor();
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

        dir.x *= speed;
        dir.y = _verticalVelocity;
        _rigidbody2D.velocity = dir;

        

        //print("velocity : " + _rigidbody2D.velocity.x);

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
        print(Grounded);
    }

    //private void CheckFloor()
    //{
    //    //Vector2.
    //    RaycastHit2D[] hit2D = Physics2D.RaycastAll(transform.position, transform.forward,2, LayerMask.NameToLayer("Floor"));
    //    Debug.DrawRay(transform.position, transform.forward, Color.red,2);
    //    foreach(RaycastHit2D hit in hit2D)
    //    {

    //    }
    //}

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
        if (collision.gameObject.CompareTag("Border"))
        {
            switch (collision.gameObject.name)
            {
                case "Right":
                    GameManager.Instance.GameClear();
                    break;
                case "Button":
                    GameManager.Instance.GameOver();
                    break;
            }
        }
    }
    #endregion

    #region PlayerDamaged
    private bool _isDead;
    public void Dead()
    {
        if (!_isDead)
        {
            _isDead = true;
            GameManager.Instance.IsGameOver = true;
            UIManager.Instance.ShowPopupUi<ResurrectionPopupUI>();
        }
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
    private bool _isfast = false;
    public void SetFast(float value)
    {
        if(_isfast == false)
        {
            StartCoroutine(Fast(value));
            _isfast = true;
        }
    }
    [SerializeField] private float fastTime;
    IEnumerator Fast(float value)
    {
        float temp = 0;
        temp = speed;
        speed = value;
        yield return YieldInstructionCache.WaitForSeconds(fastTime);
        if (temp != 0)
            speed = temp;
        _isfast = false;
        yield break;
    }

    public void SetSlow(float value)
    {
        if(_isSlow == false)
        {
            StartCoroutine(Slow(value));
            _isSlow = true;
        }

    }
    [SerializeField] private float slowTime;
    private bool _isSlow;
    IEnumerator Slow(float value)
    {
        float temp = 0;
        temp = speed;
        speed = value;
        yield return YieldInstructionCache.WaitForSeconds(slowTime);
        if(temp != 0)
            speed = temp;
        _isSlow = false;
        yield break;
    }

    public void Jump(float jumpPower)
    {
        if(isJumped == false)
        {
            JumpHeight = jumpPower;
            isJumped = true;
            Jump();
        }
    }
    #endregion
}
