using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class JoyButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public bool Pressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        GameAudioManager.Instance.Play2DSound("Jump");
        Pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }
}
