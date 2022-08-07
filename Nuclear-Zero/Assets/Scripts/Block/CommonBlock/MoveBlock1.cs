using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock1 : BlockController
{
    [SerializeField] private Transform _startPos;
    [SerializeField] private Transform _endPos;
    private Transform _desPos;
    public float _speed;

    protected override void Init()
    {
        base.Init();
        transform.position = _startPos.position;
        _desPos = _endPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //_trriger2D.enabled = false;
            collision.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //_trriger2D.enabled = true;
            collision.transform.SetParent(null);
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, _desPos.position, Time.deltaTime * _speed);

        if(Vector2.Distance(transform.position,_desPos.position) < 0.05f)
        {
            if (_desPos == _endPos) _desPos = _startPos;
            else _desPos = _endPos;
        }
    }
}
