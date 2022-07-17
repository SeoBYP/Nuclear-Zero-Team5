using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public float speed;

    public void MapMove()
    {
        transform.position -= new Vector3(1, 0, 0) * speed * Time.deltaTime;
    }
}
