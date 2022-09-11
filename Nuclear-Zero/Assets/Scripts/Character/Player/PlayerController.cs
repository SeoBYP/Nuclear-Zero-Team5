using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour
{
    [Header("Grounded")]
    [SerializeField] Transform groundCheck;
    [SerializeField] private LayerMask GroundLayer;
    private bool Grounded;

    [Header("Toped")]
    [SerializeField] Transform topCheck;
    private bool Toped;


    [Header("RightAndLeft")]
    [SerializeField] Transform rightCheck;
    [SerializeField] Transform leftCheck;
    private bool Right;
    private bool Left;


    [Header("PlayerJump")]
    [SerializeField] private float jumpTime;
    [SerializeField] private float JumpHeight;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float jumpMultiplier;
    private float defaultJumpHeight;

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
        isJumped = false;
        
    }

    public void Init()
    {
        defaultJumpHeight = JumpHeight;
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
            animationController.PlayerRun(false, _flipX);
            return;
        }
        CheckGrounded();
        Jump();
        Move(_joystick.Direction);
        SetGoalDistance();
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
        if(CheckRight() && CheckLeft() == false)
        {
            if (dir.x > 0)
                movepos = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y);
        }
        if (CheckRight() == false && CheckLeft())
        {
            if (dir.x < 0)
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
        if (isJumped == true)
            JumpHeight = jumpBlockPower;
        if (Grounded)
        {
            if (joyButton.Pressed || isJumped)
            {
                Vector2 jumpPos = new Vector2(0, JumpHeight);
                _rigidbody2D.velocity = jumpPos;
                animationController.PlayerJump();
                isJumping = true;
                jumpCounter = 0;
            }
        }
        if (_rigidbody2D.velocity.y > 0)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime)
            {
                isJumping = false;
                isJumped = false;
                JumpHeight = defaultJumpHeight;
            }
            _rigidbody2D.velocity += vecGrabity * jumpMultiplier * Time.deltaTime;
        }
        if(joyButton.Pressed == false)
        {
            isJumping = false;
        }
        if(_rigidbody2D.velocity.y < 0)
        {
            _rigidbody2D.velocity -= vecGrabity * fallMultiplier * Time.deltaTime;
            isJumping = false;
            isJumped = false;
            JumpHeight = defaultJumpHeight;
            animationController._isJumpSound = false;
        }
    }

    private bool CheckRight()
    {
        return Physics2D.OverlapCapsule(rightCheck.position, new Vector2(0.05f, 3.35f), CapsuleDirection2D.Vertical, 0, GroundLayer);
    }

    private bool CheckLeft()
    {
        return Physics2D.OverlapCapsule(leftCheck.position, new Vector2(0.05f, 3.35f), CapsuleDirection2D.Vertical, 0, GroundLayer);
    }

    private void CheckGrounded()
    {
        Grounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.85f, 0.2f), CapsuleDirection2D.Horizontal,0, GroundLayer);
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
                isJumping = false;
                //isJumped = false;
                JumpHeight = 37f;
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
            GameManager.Instance.GameOver();
        }
    }

    public void TakeDamage()
    {
        if (_isPause || _isDead)
            return;
        if (gameUI == null)
        {
            gameUI = UIManager.Instance.Get<GameUI>();
        }
        if (_isShield)
        {
            _isShield = false;
            _shieldprite.gameObject.SetActive(false);
            GameAudioManager.Instance.Play2DSound("ShieldBreak");
            Hited = true;
            StartCoroutine(DontActive());
            return;
        }
        if (Hited == true)
            return;
        StartCoroutine(DontActive());
        GameAudioManager.Instance.SetVibration();
        GameAudioManager.Instance.Play2DSound("PlayerHited");
        gameUI.DeleteHeart();
        Hited = true;
        animationController.Hited();
        
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

    private bool isJumped;
    private float jumpBlockPower;
    public void Jump(float jumpPower)
    {
        jumpBlockPower = jumpPower;
        JumpHeight = jumpPower;
        isJumped = true;
        Grounded = true;
        Jump();
        //_rigidbody2D.velocity = Vector2.zero;
        //Vector2 jumpPos = new Vector2(0, jumpPower);
        //_rigidbody2D.velocity = jumpPos;
        //animationController.PlayerJump();
        //isJumping = true;
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
        _isShield = state;
        _shieldprite.gameObject.SetActive(state);
    }

    private void SetMagnetSprite(bool state)
    {
        _isMagnet = state;
        _magnetsprite.gameObject.SetActive(state);
    }

}
