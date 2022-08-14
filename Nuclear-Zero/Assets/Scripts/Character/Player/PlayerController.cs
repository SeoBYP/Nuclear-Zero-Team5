using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour
{
    [Header("Grounded")]
    private bool Grounded;
    private float GroundedOffset = 2f;
    private Vector2 GroundedSize = new Vector2(1.85f, 0.2f);
    public LayerMask GroundLayer;

    [Header("PlayerJump")]
    private float JumpHeight = 8f;
    private float Gravity = -100;
    private float JumpTimeout = 0.5f;
    private float _jumpTimeoutDelta;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    
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
    private static bool _haveMagnet = false;
    [SerializeField] private SpriteRenderer _shieldprite;
    private static bool _isShield = false;


    private void Start()
    {
        _isStart = false;
        _isPause = false;
        _haveMagnet = true;
        _isKnockBack = false;
        //targetPos = Vector3.zero;
    }

    public void Init()
    {
        Hited = false;
        _jumpTimeoutDelta = JumpTimeout;
        _shieldprite.gameObject.SetActive(false);
        animationController = GetComponentInChildren<PlayerAnimationController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (animationController != null)
            animationController.Init();
        gameUI = UIManager.Instance.Get<GameUI>();
        joyButton = Utils.FindObjectOfType<JoyButton>();
        _joystick = Utils.FindObjectOfType<Joystick>(true);
        SetRaderSprite();
    }

    public static void SetStart(bool state) { _isStart = state; }
    public static void SetPause(bool state) { _isPause = state; }
    public static void SetShield(bool state) { _isShield = state; }
    public static void SetMagnet(bool state) { _haveMagnet = state; }

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
        
        Move(_joystick.Direction);
        Jump();
        CheckGrounded();
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
                JumpHeight = 8f;
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
    }

    private void OnDrawGizmos()
    {
        Vector2 boxPosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset);
        Gizmos.DrawWireCube(boxPosition, GroundedSize);
        Gizmos.color = Color.red;
    }

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
            GameAudioManager.Instance.Play2DSound("Shield");
            _shieldprite.gameObject.SetActive(true);
        }
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

    private bool isJumped = false;
    public void Jump(float jumpPower)
    {
        if(isJumped == false)
        {
            print("Jump : " + Grounded);
            JumpHeight = jumpPower;
            isJumped = true;
            Jump();
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

    private void SetRaderSprite()
    {
        _magnetsprite.gameObject.SetActive(false);
        //if (_haveMagnet)
        //{
        //    _magnetsprite.gameObject.SetActive(false);
        //    //ItemController.SetPlayerHasMagnet(true);
        //}
    }

}
