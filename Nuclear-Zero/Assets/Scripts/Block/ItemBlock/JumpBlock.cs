using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBlock : BlockController
{
    [SerializeField] private float jumpPower;
    protected override void Init()
    {
        base.Init();
    }

    ////private void OnTriggerStay2D(Collider2D collision)
    ////{
    ////    //SetJump();
    ////}

    public override void OnSteped()
    {
        base.OnSteped();
        SetJump();
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{

    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        Rigidbody2D rigidbody = collision.GetComponent<Rigidbody2D>();
    //        if(rigidbody != null)
    //        {
    //            rigidbody.AddForce(Vector2.up * jumpPower * Time.deltaTime, ForceMode2D.Force);
    //        }
    //    }
    //}

    private void SetJump()
    {
        if (_player != null)
        {
            _player.Jump(jumpPower);
        }
    }
}
