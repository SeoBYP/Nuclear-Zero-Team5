using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TestTouch : MonoBehaviour
{
    bool canClick = true;

    //Vector2 targetPos;
    public float Xincrement;
    public float speed;
    int point = 1;
    PlayerController player;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            int id = touch.fingerId;
            if (EventSystem.current.IsPointerOverGameObject(id) == false)
            {
                return;
            }
            else
            {
                if (Input.touchCount > 0)
                {
                    Touch t = Input.GetTouch(0);

                    if (t.phase == TouchPhase.Began)
                    {
                        if (t.position.y < Screen.height / 2 && canClick)
                        {
                            player.MoveDown();
                            canClick = false;
                            StartCoroutine(MyDelay(0.1f));
                        }
                        else if (t.position.y > Screen.height / 2 && canClick)
                        {
                            player.MoveUp();
                            canClick = false;
                            StartCoroutine(MyDelay(0.01f));
                        }
                    }
                }
            }
        }
    }

    private IEnumerator MyDelay(float sec)
    {
        yield return YieldInstructionCache.WaitForSeconds(sec);
        canClick = true;
    }
}
