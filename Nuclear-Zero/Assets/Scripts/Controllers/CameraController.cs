using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController _player;
    public Vector3 Offset;

    private void LateUpdate()
    {
        if(_player != null)
        {
            transform.position = _player.transform.position + Offset;
        }

    }
}
