using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour
{
    [Header("Grounded")]
    private bool Grounded;
    //private float GroundedOffset = 1.95f;
    [SerializeField] Transform groundCheck;//private Vector2 GroundedSize = new Vector2(1.9f, 0.05f);
    public LayerMask GroundLayer;

    [Header("Toped")]
    private bool Toped;
    [SerializeField] Transform topCheck;

    [Header("RightAndLeft")]
    private bool Right;
    [SerializeField] Transform rightCheck;
    private bool Left;
    [SerializeField] Transform leftCheck;

    [Header("PlayerJump")]
    [SerializeField] private float jumpTime;
    [SerializeField] private float JumpHeight;
    //private float Gravity = -100f;
    //private float JumpTimeout = 0.4f;
    //private float _jumpTimeoutDelta;
    //private float _verticalVelocity;
    //private float _terminalVelocity = 53.0f;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float jumpMultiplier;

    private Vector2 vecGrabity;
    private bool isJumping;
    private float jumpCounter;

    [Header("PlayerComponent")]
    private PlayerAnimationController animationController;
    private Rigidbody2D _rigidbody2D;
    private GameUI gameUI;
    private JoyButton joyButton;
    private Joystick _joystick;

    [Header("PlayerMovement")]
    private bool _flipX = false;
    public float speed;

    [Header("PlayerState")]
    private bool Hited = false;
    
    [SerializeField] private float Timer = 1;

    [Header("GameState")]
    private static bool _isStart = false;
    private static bool _isPause = false;
    private bool _isKnockBack = false;

    [Header("PlayerItems")]
    [SerializeField] private SpriteRenderer _magnetsprite;
    private static bool _isMagnet = false;
    [SerializeField] private SpriteRenderer _shieldprite;
    private static bool _isShield = false;


    private void Start()
    {
        _isStart = false;
        _isPause = false;
        _isKnockBack = false;
        _shieldprite.gameObject.SetActive(false);
        _magnetsprite.gameObject.SetActive(false);

        SetSheildSprite(GameManager.Instance._shield);
        SetMagnetSprite(GameManager.Instance._magnet);
        Init();
    }

    public void Init()
    {
        Hited = false;
        JumpHeight = 30f;
        vecGrabity = new Vector2(0, -Physics2D.gravity.y);
        animationController = GetComponentInChildren<PlayerAnimationController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (animationController != null)
            animationController.Init();
        gameUI = UIManager.Instance.Get<GameUI>();
        joyButton = Utils.FindObjectOfType<JoyButton>();
        _joystick = Utils.FindObjectOfType<Joystick>(true);
    }

    public static void SetStart(bool state) { _isStart = state; }
    public static void SetPause(bool state) { _isPause = state; }

    private void FixedUpdate()
    {
        if (_joystick == null)
            Utils.FindObjectOfType<Joystick>(true);
        if (_isStart == false)
            return;
        if (_isPause)
        {
            _rigidbody2D.velocity = Vector2.zero;
            return;
        }
        CheckGrounded();
        Jump();
        Move(_joystick.Direction);
        SetGoalDistance();
    }

    private void Update()
    {
        
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
        Vector2 movepos = new Vector2(dir.x, _rigidbody2D.velocity.y);
        //if(_rigidbody2D.velocity.x == 0 )
        //{
        //    if (dir.x != 0)
        //        return;
        //    
        //}
        if (CheckCollider())
        {
            //if(dir.x != 0)
            movepos = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y);
        }
        _rigidbody2D.velocity = movepos;
        if (dir.x != 0)
            animationController.PlayerRun(true, _flipX);
        else
            animationController.PlayerRun(false, _flipX);
    }

    private void Jump()
    {
        if (Grounded)
        {
            if (joyButton.Pressed || isJumped)
            {
                Vector2 jumpPos = new Vector2(_rigidbody2D.velocity.x, JumpHeight);
                _rigidbody2D.velocity = jumpPos;
                isJumping = true;
                isJumped = false;
                jumpCounter = 0;
                JumpHeight = 30f;
            }
        }

        if (_rigidbody2D.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) isJumping = false;
            _rigidbody2D.velocity += vecGrabity * jumpMultiplier * Time.deltaTime;
        }

        if(joyButton.Pressed == false)
        {
            isJumping = false;
        }

        if(_rigidbody2D.velocity.y < 0)
        {
            _rigidbody2D.velocity -= vecGrabity * fallMultiplier * Time.deltaTime;
        }
    }

    private bool CheckCollider()
    {
        if(CheckRight() != null || CheckLeft() != null)
        {
            if (Grounded)
                return false;
            else
                return true;
        }
        return false;
    }

    private Collider2D CheckRight()
    {
        return Physics2D.OverlapCapsule(rightCheck.position, new Vector2(0.1f, 3.45f), CapsuleDirection2D.Vertical, 0, GroundLayer);
    }

    private Collider2D CheckLeft()
    {
        return Physics2D.OverlapCapsule(leftCheck.position, new Vector2(0.2f, 3.45f), CapsuleDirection2D.Vertical, 0, GroundLayer);
    }

    private void CheckGrounded()
    {
        Grounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.9f, 0.2f), CapsuleDirection2D.Horizontal,0, GroundLayer);
        animationController.IsGrounded(Grounded);
    }
    private void CheckTop()
    {
        Toped = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.7f, 0.2f), CapsuleDirection2D.Horizontal, 0, GroundLayer);
        if (Toped)
        {
            if (_rigidbody2D.velocity.y > 0)
            {
                Vector2 jumpPos = new Vector2(_rigidbody2D.velocity.x, transform.position.y);
                _rigidbody2D.velocity = jumpPos;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckTop();
        if (collision.gameObject.CompareTag("Border"))
        {
            switch (collision.gameObject.name)
            {
                case "Right":
                    GameManager.Instance.GameClear();
                    break;
                case "Button":
                    GameAudioManager.Instance.Play2DSound("PlayerFallDown");
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
            //GameManager.Instance.IsGameOver = true;
            GameManager.Instance.GameOver();
            //UIManager.Instance.ShowPopupUi<ResurrectionPopupUI>();
        }
    }

    public void TakeDamage()
    {
        if (_isPause)
            return;
        if (gameUI == null)
        {
            gameUI = UIManager.Instance.Get<GameUI>();
        }
        if (Hited == true)
            return;
        if (_isShield)
        {
            _isShield = false;
            _shieldprite.gameObject.SetActive(false);
            return;
        }
        GameAudioManager.Instance.SetVibration();
        GameAudioManager.Instance.Play2DSound("PlayerHited");
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
        StopCoroutine(DontActive());
    }
    #endregion

    #region BlockEffects
    public void Shieded()
    {
        if (_isShield == false)
        {
            _isShield = true;
            _shieldprite.gameObject.SetActive(true);
        }
    }
    private bool _isfast = false;
    public void SetFast(float value)
    {
        if (_isfast == false)
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

    private bool isJumped = false;
    public void Jump(float jumpPower)
    {
        if(isJumped == false)
        {
            //print("Jump : " + Grounded);
            JumpHeight = jumpPower;
            isJumped = true;
            //Jump();
        }
    }
    #endregion

    private GameObject _goal;
    private float _defaultDistance;
    public void SetDefaultGoalDistance()
    {
        if (_goal == null)
            _goal = GameObject.FindGameObjectWithTag("Goal");
        _defaultDistance = Vector2.Distance(transform.position, _goal.transform.position);
    }

    private void SetGoalDistance()
    {
        if (_goal == null)
            _goal = GameObject.FindGameObjectWithTag("Goal");
        if(gameUI == null)
            gameUI = UIManager.Instance.Get<GameUI>();
        float curdistance = Vector2.Distance(transform.position, _goal.transform.position);

        float value = 1 - (curdistance / _defaultDistance);
        if(value != 0 && gameUI != null)
        {
            gameUI.SetPlayerProGressBar(value);
        }
    }

    private void SetSheildSprite(bool state)
    {
        if (state)
        {
            _isShield = state;
            _shieldprite.gameObject.SetActive(state);
        }
        else
        {
            _isShield = state;
            _shieldprite.gameObject.SetActive(state);
        }
    }

    private void SetMagnetSprite(bool state)
    {
        if (state)
        {
            _isMagnet = state;
            _magnetsprite.gameObject.SetActive(state);
        }
        else
        {
            _isMagnet = state;
            _magnetsprite.gameObject.SetActive(state);
        }
    }

}
