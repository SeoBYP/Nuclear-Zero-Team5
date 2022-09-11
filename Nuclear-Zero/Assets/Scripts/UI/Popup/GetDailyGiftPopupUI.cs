using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;
public class GetDailyGiftPopupUI : PopupUI
{
    enum Buttons
    {
        Close,
    }

    enum GameObjects
    {
        BackGround,
    }

    enum Images
    {
        ItemIcon,
    }

    enum Texts
    {
        GetItemRewardText,
    }

    enum Items
    {
        Shield,
        Magnet,
        Life,
    }

    [SerializeField] private Sprite _Shield;
    [SerializeField] private Sprite _Magnet;
    [SerializeField] private Sprite _Life;
    private Items rewarditem;

    [System.Obsolete]
    private void Start()
    {
        SetTodayTime();
    }

    public override void Init()
    {
        base.Init();
        Binds();
        SetItemRandom();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));

        BindEvent(GetButton((int)Buttons.Close).gameObject, OnClose, UIEvents.Click);
        BindEvent(GetGameObject((int)GameObjects.BackGround), OnClose, UIEvents.Click);
    }

    [System.Obsolete]
    private void SetTodayTime()
    {
        TimeProject time = Utils.FindObjectOfType<TimeProject>();
        if (time != null)
            time.SetToday();
    }

    private void SetItemRandom()
    {
        int random = Random.Range(0, 100);
        if (random < 40)
            rewarditem = Items.Shield;
        else if (random >= 40 && random < 75)
            rewarditem = Items.Magnet;
        else
            rewarditem = Items.Life;
        SetRandomItemIcon(rewarditem);
    }

    private void SetRandomItemIcon(Items item)
    {
        switch (item)
        {
            case Items.Shield:
                GetImage((int)Images.ItemIcon).sprite = _Shield;
                SetRandomItemCount(item,"쉴드",3,7);
                break;
            case Items.Magnet:
                GetImage((int)Images.ItemIcon).sprite = _Magnet;
                SetRandomItemCount(item,"마그넷", 1, 5);
                break;
            case Items.Life:
                GetImage((int)Images.ItemIcon).sprite = _Life;
                SetRandomItemCount(item,"베터리", 1, 5);
                break;
        }
    }

    private void SetRandomItemCount(Items item,string itemName,int randomMin,int randomMax)
    {
        int randomCount = Random.Range(randomMin, randomMax);
        string texts = $"{itemName}\n {randomCount}개    획득!";
        GetText((int)Texts.GetItemRewardText).text = texts;
        switch (item)
        {
            case Items.Shield:
                DataManager.Instance.playerInfo.SetShieldItemCount(randomCount);
                break;
            case Items.Magnet:
                DataManager.Instance.playerInfo.SetMagnetItemCount(randomCount);
                break;
            case Items.Life:
                DataManager.Instance.playerInfo.SetLifeItemCount(randomCount);
                break;
        }
    }

    private void OnClose(PointerEventData data)
    {
        ClosePopupUI();
    }
}
