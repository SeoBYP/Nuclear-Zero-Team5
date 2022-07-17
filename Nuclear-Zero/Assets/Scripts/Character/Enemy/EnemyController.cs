using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour//, IUpdate
{
    public float speed = 6;

    Rigidbody2D _rigidbody2D;
    public bool DoingDash;

    EnemyAnimationController animationController;

    public static bool IsStart { get; set; } = false;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        animationController = GetComponentInChildren<EnemyAnimationController>();
        if (animationController != null)
            animationController.Init();
        //UpdateManager.Instance.Listener(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            if (collision.gameObject.name == "Buttom")
            {
                Clear();
            }
        }
    }

    private void Run()
    {
        if (IsStart == false)
            return;
        if (DoingDash)
        {
            DoDash();
        }
    }

    public void DoShot()
    {
        animationController.PlayEnemyShoot();
        ResourcesManager.Instance.Instantiate("Weapon/Arrow").transform.position = transform.position;
    }

    private void DoDash()
    {
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(-1f, 0, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;
    }

    private void Update()
    {
        Run();
    }

    public void Clear()
    {
        //UpdateManager.Instance.DeleteListener(this);
        gameObject.SetActive(false);
    }
}
