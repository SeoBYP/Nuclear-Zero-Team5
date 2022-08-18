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
        Pause,
    }

    enum Texts
    {
        StarCount,
        CoinCount,
    }
    enum Sliders
    {
        GameGoal,
        BackEnemyPos,
    }

    private int heartcount;
    private int defaultheartcount;
    private int starcount = 0;
    private int coincount = 0;

    public int CoinCount { get { return coincount; } }
    public int StarCount { get { return starcount; } }

    public bool GameOver { get; private set; }
    //private PlayerController player;
    private List<PlayerHeart> playerHearts = new List<PlayerHeart>();
    private Joystick joystick;

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        starcount = 0;
        coincount = 0;



        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Slider>(typeof(Sliders));

        InitPlayerHearts();
        //player = Utils.FindObjectOfType<PlayerController>();
        joystick = GetComponentInChildren<Joystick>();
        if (joystick != null)
            joystick.Init();
        BindEvent(GetButton((int)Buttons.Pause).gameObject, OnPause, UIEvents.Click);

        GetText((int)Texts.CoinCount).text = coincount.ToString();
        GetText((int)Texts.StarCount).text = starcount.ToString();
    }

    private void InitPlayerHearts()
    {
        PlayerHeart[] hearts = GetComponentsInChildren<PlayerHeart>();
        for(int i = 0; i < hearts.Length; i++)
        {
            hearts[i].Init();
            if (GameManager.Instance._life)
            {
                playerHearts.Add(hearts[i]);
            }
            else
            {
                if (hearts[i].gameObject.name == "PlaerHeart1")
                {
                    hearts[i].gameObject.SetActive(false);
                }
                else
                    playerHearts.Add(hearts[i]);
            }
        }
        
        heartcount = playerHearts.Count - 1;
        defaultheartcount = heartcount;
    }

    private void OnPause(PointerEventData data)
    {
        UIManager.Instance.ShowPopupUi<PausePopupUI>();
        GameManager.Instance.GamePause();
    }
    #region PlayerHP
    public void DeleteHeart()
    {
        if ((heartcount <= 0) == false)
        {
            playerHearts[heartcount].SetGrayHeart();
            heartcount--;
        }
       else
        {
            playerHearts[heartcount].SetGrayHeart();
            Utils.FindObjectOfType<PlayerController>().Dead();
        }
    }
    public void AddHeart()
    {
        heartcount++;
        if (heartcount > defaultheartcount)
        {
            heartcount = defaultheartcount;
            return;
        }
        playerHearts[heartcount].SetLifeHeart();
    }

    public void SetStar()
    {
        starcount++;
        GetText((int)Texts.StarCount).text = starcount.ToString();
    }
    public void SetCoin()
    {
        coincount++;
        GetText((int)Texts.CoinCount).text = coincount.ToString();
    }
    #endregion

    #region PlayerProgressBar
    private Slider playerSlider;
    public void SetPlayerProGressBar(float distance)
    {
        if (playerSlider == null)
            playerSlider = Get<Slider>((int)Sliders.GameGoal);
        playerSlider.value = distance;
    }

    private Slider _backEnemySlider;
    public void SetBackEnemyProGressBar(float distance)
    {
        if (_backEnemySlider == null)
            _backEnemySlider = Get<Slider>((int)Sliders.BackEnemyPos);
        _backEnemySlider.value = distance;
    }

    #endregion

}
