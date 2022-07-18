using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameUI : SceneUI
{
    enum Images
    {
        Heart1,
        Heart2,
        Heart3,
    }

    private int count = 2;

    public bool GameOver { get; private set; }
    private PlayerController player;
    private GameUIItemButton[] itemButtons;

    private int currentItemCount;

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Image>(typeof(Images));

        player = Utils.FindObjectOfType<PlayerController>();

        itemButtons = GetComponentsInChildren<GameUIItemButton>();
        for(int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].Init();
        }
        currentItemCount = 0;
    }

    public void DeleteHeart()
    {
        if(count > 0)
        {
            GetImage(count).gameObject.SetActive(false);
            count--;
        }
        else
        {
            if(count > -1)
                GetImage(count).gameObject.SetActive(false);
            GameManager.Instance.GameOver();
        }
    }
    //?????? ?????? ?????? ???? ?????????? ???????? ???????? ?????? ????????.
    public void SetItemButtons()
    {
        //???? ?????? ?????? ?????? ???? ???????? ???? ????????.
        if (currentItemCount >= itemButtons.Length)
        {
            Debug.Log("?????? ???? ??????????");
            return;
        }
        itemButtons[currentItemCount].SetItem();
        currentItemCount++;
    }

    public void ReSetItmeButtons()
    {
        if (currentItemCount <= 0)
            return;
        currentItemCount--;
    }
}
