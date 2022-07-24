using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;
public class GameUI : SceneUI
{
    enum Buttons
    {
        Jump,
    }

    private int count;

    public bool GameOver { get; private set; }
    private PlayerController player;
    private List<PlayerHeart> playerHearts = new List<PlayerHeart>();
    private Joystick joystick;

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        //Bind<Button>(typeof(Buttons));
        InitPlayerHearts();
        player = Utils.FindObjectOfType<PlayerController>();
        joystick = GetComponentInChildren<Joystick>();
        if (joystick != null)
            joystick.Init();
        //BindEvent(GetButton((int)Buttons.Jump).gameObject, OnJump, UIEvents.Click);
    }

    private void InitPlayerHearts()
    {
        PlayerHeart[] hearts = GetComponentsInChildren<PlayerHeart>();
        for(int i = 0; i < hearts.Length; i++)
        {
            hearts[i].Init();
            playerHearts.Add(hearts[i]);
        }
        count = hearts.Length - 1;
    }

    //private void OnJump(PointerEventData data)
    //{
    //    player.Jump();//ref IsClicked);
    //}

    public void DeleteHeart()
    {
        if ((count <= 0) == false)
        {
            playerHearts[count].SetGrayHeart();
            count--;
        }
       else
        {
            playerHearts[count].SetGrayHeart();
            Utils.FindObjectOfType<PlayerController>().Dead();
            //UIManager.Instance.ShowPopupUi<ResurrectionPopupUI>();
        }
    }
    public void AddHeart()
    {
        count++;
        if (count > 2)
        {
            count = 2;
            return;
        }
        playerHearts[count].SetLifeHeart();
    }
}
