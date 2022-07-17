using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static bool IsMoveNext { get; set; }
    public static bool IsMovePrev { get; set; }
    public int moveIndex = 1900;
    public float lerp;
    public float speed;
    public void Start()
    {
        IsMoveNext = false;
    }

    private void Update()
    {
        if (IsMoveNext)
        {
            Vector3 movePos = new Vector3(transform.position.x + moveIndex, transform.position.y, transform.position.z);
            print(movePos);
            transform.position = Vector3.Lerp(transform.position, movePos, lerp);
            if (transform.position == movePos)
                IsMoveNext = false;
        }
        if (IsMovePrev)
        {
            Vector3 movePos = transform.position - new Vector3(moveIndex, 0, 0);
            transform.position = Vector3.Lerp(transform.position, movePos, lerp) * Time.deltaTime * speed;
            if (transform.position == movePos)
                IsMoveNext = false;
        }
    }

}
