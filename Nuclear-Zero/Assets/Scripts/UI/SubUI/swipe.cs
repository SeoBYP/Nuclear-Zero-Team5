using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swipe : MonoBehaviour
{
    public Color[] colors;
    public GameObject scrollbar, imageContent;
    private float scroll_pos = 0;
    float[] pos;
    private bool runIt = false;
    private float time;
    private Button takeTheBtn;
    int btnNumber;

    [SerializeField] private Button next;
    [SerializeField] private Button prev;
    // Start is called before the first frame update

    public void Init()
    {
        pos = new float[transform.childCount];
        scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        runIt = true;
        time = 0;
        scroll_pos = (pos[btnNumber]);
        SetBtn();
    }

    // Update is called once per frame
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        if (runIt)
        {
            GecisiDuzenle(distance, pos, takeTheBtn);
            time += Time.deltaTime;

            if (time > 1f)
            {
                time = 0;
                runIt = false;
            }
        }

        

        //if (Input.GetMouseButton(0))
        //{
        //    scroll_pos = scrollbar.GetComponent<Scrollbar>().value;

        //}
        //else
        
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2f))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.3f);// * 0.5f; 
                }
            }
        }

        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2f))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                imageContent.transform.GetChild(i).localScale = Vector2.Lerp(imageContent.transform.GetChild(i).localScale, new Vector2(1.2f, 1.2f), 0.3f);
                imageContent.transform.GetChild(i).GetComponent<Image>().color = colors[1];
                for (int j = 0; j < pos.Length; j++)
                {
                    if (j != i)
                    {
                        imageContent.transform.GetChild(j).GetComponent<Image>().color = colors[0];
                        imageContent.transform.GetChild(j).localScale = Vector2.Lerp(imageContent.transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.3f);
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
        }
    }

    private void GecisiDuzenle(float distance, float[] pos, Button btn)
    {
        //btnSayi = System.Int32.Parse(btn.transform.name);
        
        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2f))
            {
                scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[btnNumber], 1f * Time.deltaTime);
            }
        }

    }

    private void SetBtn()
    {
        if (btnNumber == 0)
        {
            prev.gameObject.SetActive(false);
            next.gameObject.SetActive(true);
            return;
        }      
        if (btnNumber == pos.Length - 1)
        {
            next.gameObject.SetActive(false);
            prev.gameObject.SetActive(true);
            return;
        }
        prev.gameObject.SetActive(true);
        next.gameObject.SetActive(true);
    }

    public void SetBtnPage(int chapter)
    {
        pos = new float[transform.childCount];
        scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        runIt = true;
        time = 0;

        float distance = 1f / (pos.Length - 1);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        btnNumber = chapter - 1;
        scroll_pos = (pos[btnNumber]);
        SetBtn();
    }

    public void NextBtnCliched()
    {
        btnNumber++;
        if (btnNumber > pos.Length - 1)
            btnNumber = pos.Length - 1;
        GameAudioManager.Instance.Play2DSound("Touch");
        time = 0;
        scroll_pos = (pos[btnNumber]);
        runIt = true;
        SetBtn();
    }

    public void PrevBtnClicked()
    {
        btnNumber--;
        if (btnNumber < 0)
            btnNumber = 0;
        GameAudioManager.Instance.Play2DSound("Touch");
        time = 0;
        scroll_pos = (pos[btnNumber]);
        runIt = true;
        SetBtn();
    }

}