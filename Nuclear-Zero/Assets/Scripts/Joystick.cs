using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Joystick : SubUI, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    private Transform _button;
    private Transform _backGround;
    private Vector3 _startPos = Vector3.zero;

    private float _length;
    private Vector2 _direction;

    public Vector2 Dir
    {
        get { return _direction; }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _direction = Vector2.zero;
        _button.position = _startPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        _button.position = eventData.position;

        Vector3 dir = _button.position - transform.position;
        if (dir.magnitude > _length)
        {
            dir.Normalize();
            dir *= _length;
            dir.z = 0;
            
            _button.position = _startPos + dir;    
        }
        _direction = dir.normalized;
    }

    public override void Init()
    {
        _button = transform.Find("JoystickButton");
        _startPos = _button.position;
        Transform pivot = transform.Find("Pivot");
        _length = Vector3.Distance(_startPos, pivot.position);
    }
}
