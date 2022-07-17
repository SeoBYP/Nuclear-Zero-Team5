using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour
{
    public float speed;

    private bool IsCollisionButtom;
    private bool IsCollisionTop;
    private bool Hited = false;
    private bool IsMoveUp;
    private bool IsMoveDown;
    Vector2 targetPos;
    public float Xincrement;

    public float Timer = 3;

    private PlayerAnimationController animationController;

    private GameUI gameUI;

    private void Start()
    {
        IsMoveUp = false;
        IsMoveDown = false;
        Init();
    }

    public void Init()
    {
        animationController = GetComponentInChildren<PlayerAnimationController>();
        if (animationController != null)
            animationController.Init();
        gameUI = UIManager.Instance.Get<GameUI>();
        targetPos = transform.position;
    }

    private void Update()
    {
        if (GameManager.Instance.IsClear || GameManager.Instance.IsGameOver)
            return;
        if ((IsCollisionButtom && IsMoveDown) || (IsCollisionTop && IsMoveUp))
        {
            animationController.PlayRightWalk();
            return;
        }
            
        transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
    }

    public void MoveUp()
    {
        goUp();
    }

    public void MoveDown()
    {
        goDown();
    }

    private void goUp()
    {
        IsMoveUp = true;
        IsMoveDown = false;
        targetPos = new Vector2(transform.position.x, transform.position.y + Xincrement);
        animationController.PlayUpWalk();
    }
    private void goDown()
    {
        IsMoveUp = false;
        IsMoveDown = true;
        targetPos = new Vector2(transform.position.x, transform.position.y - Xincrement);
        animationController.PlayDownWalk();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            switch (collision.gameObject.name)
            {
                case "Buttom":
                    IsCollisionButtom = true;
                    break;
                case "Top":
                    IsCollisionTop = true;
                    break;
                case "Right":
                    GameManager.Instance.GameClear();
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            switch (collision.gameObject.name)
            {
                case "Buttom":
                    IsCollisionButtom = false;
                    break;
                case "Top":
                    IsCollisionTop = false;
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameUI == null)
        {
            gameUI = UIManager.Instance.Get<GameUI>();
        }
        if (Hited == true)
            return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (gameUI != null)
            {
                TakeDamage();
            }
        }
        else if (collision.gameObject.CompareTag("Weapon"))
        {
            if (gameUI != null)
            {
                TakeDamage();
            }
        }
    }

    private void TakeDamage()
    {
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
}
