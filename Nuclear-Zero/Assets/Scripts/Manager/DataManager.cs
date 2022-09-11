using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Bson;
using static Define;
#region PlayerInfomation
[System.Serializable]
public class PlayerStages
{
    public int StageIndex;
    public string StageName;
    public int ResultStar;
    public bool Cleared;

    public PlayerStages(JObject data, int index)
    {
        StageIndex = data["PlayerStages"][index]["StageIndex"].ToObject<int>();
        StageName = data["PlayerStages"][index]["StageName"].ToObject<string>();
        ResultStar = data["PlayerStages"][index]["ResultStar"].ToObject<int>();
        Cleared = data["PlayerStages"][index]["Cleared"].ToObject<bool>();
    }

    public void SetStageClear(int starscount)
    {
        if(ResultStar <= starscount)
        {
            ResultStar = starscount;
        }
        if(Cleared == false)
        {
            Cleared = true;
        }
    }
}
[System.Serializable]
public class PlayerChapter
{
    public int ChapterIndex;
    public string ChapterName;
    public bool BunkerItem1;
    public bool BunkerItem2;
    public bool BunkerItem3;
    public bool BunkerItem4;
    public bool BunkerItem5;
    public bool ChapterStory;
    public bool BunkerStory;
    public PlayerChapter(JObject data,int index)
    {
        ChapterIndex = data["PlayerChapters"][index]["ChapterIndex"].ToObject<int>();
        ChapterName = data["PlayerChapters"][index]["ChapterName"].ToObject<string>();
        BunkerItem1 = data["PlayerChapters"][index]["BunkerItem1"].ToObject<bool>();
        BunkerItem2 = data["PlayerChapters"][index]["BunkerItem2"].ToObject<bool>();
        BunkerItem3 = data["PlayerChapters"][index]["BunkerItem3"].ToObject<bool>();
        BunkerItem4 = data["PlayerChapters"][index]["BunkerItem4"].ToObject<bool>();
        BunkerItem5 = data["PlayerChapters"][index]["BunkerItem5"].ToObject<bool>();
        ChapterStory = data["PlayerChapters"][index]["ChapterStory"].ToObject<bool>();
        BunkerStory = data["PlayerChapters"][index]["BunkerStory"].ToObject<bool>();
    }
}
[System.Serializable]
public class PlayerInfo
{
    public int Gold;
    public string DialogueObjectName;
    public string Daily;
    public bool LookPrologue;
    public bool LookTutorial;
    public int PlayerStars;
    public int SelectStage;
    public int ClearStage;
    public int SelectChapter;
    public float BGMSound;
    public float EffectSound;
    public int MagnetItem;
    public int ShieldItem;
    public int LifeItem;
    public bool BadEnding;
    public bool NormalEnding;
    public bool HappyEnding;
    public bool DailyGift;
    public bool ADRemove;
    public bool Share;
    public bool Review;
    public List<PlayerChapter> PlayerChapters;
    public List<PlayerStages> PlayerStages;
    public PlayerInfo(string text)
    {
        var data = JObject.Parse(text);

        Gold = data["Gold"].ToObject<int>();
        DialogueObjectName = data["DialogueObjectName"].ToObject<string>();
        Daily = data["Daily"].ToObject<string>();
        LookPrologue = data["LookPrologue"].ToObject<bool>();
        LookTutorial = data["LookTutorial"].ToObject<bool>();
        PlayerStars = data["PlayerStars"].ToObject<int>();
        SelectStage = data["SelectStage"].ToObject<int>();
        ClearStage = data["ClearStage"].ToObject<int>();
        SelectChapter = data["SelectChapter"].ToObject<int>();
        BGMSound = data["BGMSound"].ToObject<float>();
        EffectSound = data["EffectSound"].ToObject<float>();
        MagnetItem = data["MagnetItem"].ToObject<int>();
        ShieldItem = data["ShieldItem"].ToObject<int>();
        LifeItem = data["LifeItem"].ToObject<int>();
        BadEnding = data["BadEnding"].ToObject<bool>();
        NormalEnding = data["NormalEnding"].ToObject<bool>();
        HappyEnding = data["HappyEnding"].ToObject<bool>();
        DailyGift = data["DailyGift"].ToObject<bool>();
        ADRemove = data["ADRemove"].ToObject<bool>();
        Share = data["Share"].ToObject<bool>();
        Review = data["Review"].ToObject<bool>();
        MatchCollection match = Regex.Matches(text, "ChapterName");
        if (match.Count != 0)
        {
            PlayerChapters = new List<PlayerChapter>();
            for (int i = 0; i < match.Count; i++)
            {
                PlayerChapters.Add(new PlayerChapter(data, i));
            }
        }
        MatchCollection match2 = Regex.Matches(text, "StageName");
        if (match2.Count != 0)
        {
            PlayerStages = new List<PlayerStages>();
            for (int i = 0; i < match2.Count; i++)
            {
                PlayerStages.Add(new PlayerStages(data, i));
            }
        }
    }

    public void SetClearStage(int selectStage, int starcount, int coincount)
    {
        Gold += coincount;
        for (int i = 0; i < PlayerStages.Count; i++)
        {
            if (PlayerStages[i].StageIndex == selectStage)
            {
                PlayerStages[i].SetStageClear(starcount);
                break;
            }
        }
        int clearstage = selectStage + 1;
        if (clearstage >= PlayerStages.Count)
            clearstage = PlayerStages.Count;
        if (clearstage > ClearStage)
            ClearStage = clearstage;
        SetPlayerStars();
    }

    public void SetCoin(int coincount)
    {
        Gold += coincount;
    }

    public PlayerStages GetPlayerStages(int stageIndex)
    {
        for (int i = 0; i < PlayerStages.Count; i++)
        {
            if (PlayerStages[i].StageIndex == stageIndex)
            {
                return PlayerStages[i];
            }
        }
        return null;
    }

    public PlayerChapter GetPlayerChapter(int chapterIndex)
    {
        for (int i = 0; i < PlayerChapters.Count; i++)
        {
            if (PlayerChapters[i].ChapterIndex == chapterIndex)
            {
                return PlayerChapters[i];
            }
        }
        return null;
    }

    public bool FindAllChapterItems(int chapterIndex)
    {
        PlayerChapter curchapter = GetPlayerChapter(chapterIndex);
        if (curchapter.BunkerItem1 && curchapter.BunkerItem2
            && curchapter.BunkerItem3 && curchapter.BunkerItem4 && curchapter.BunkerItem5)
            return true;
        return false;
    }

    private void SetPlayerStars()
    {
        PlayerStars = 0;
        foreach (PlayerStages stage in PlayerStages)
        {
            PlayerStars += stage.ResultStar;
        }
    }

    public void ShowSharePopup()
    {
        if (GetPlayerStages(4).Cleared)
        {
            if(Share == false)
            {
                Share = true;
                UIManager.Instance.ShowPopupUi<SharePopupUI>();
            }
        }
    }

    public void ShowEnding()
    {
        if (GameManager.Instance._ShowEndding)
        {
            if (GetPlayerStages(16).Cleared)
            {
                if (PlayerStars < 39)
                {
                    UIManager.Instance.ShowPopupUi<BadEndingPopupUI>();
                    BadEnding = true;
                }
                else if (PlayerStars >= 40 && PlayerStars <= 47)
                {
                    UIManager.Instance.ShowPopupUi<NormalEndingPopupUI>();
                    NormalEnding = true;
                }
                else
                {
                    UIManager.Instance.ShowPopupUi<HappyEndingPopupUI>();
                    HappyEnding = true;
                }
                GameManager.Instance._ShowEndding = false;
            }
            GPGSManager.Instance.SetStageAchiv();
        }
    }

    public void SetSoundSetting(float bgm, float effect)
    {
        BGMSound = bgm;
        EffectSound = effect;
    }

    public bool CheckChapterStageStart(int chapterindex)
    {
        if (GetPlayerChapter(chapterindex) != null)
        {
            int stars = 0;
            switch (chapterindex)
            {
                case 1:
                    stars += GetPlayerStages(1).ResultStar;
                    stars += GetPlayerStages(2).ResultStar;
                    stars += GetPlayerStages(3).ResultStar;
                    stars += GetPlayerStages(4).ResultStar;
                    return stars >= 12;
                case 2:
                    stars += GetPlayerStages(5).ResultStar;
                    stars += GetPlayerStages(6).ResultStar;
                    stars += GetPlayerStages(7).ResultStar;
                    stars += GetPlayerStages(8).ResultStar;
                    return stars >= 12;
                case 3:
                    stars += GetPlayerStages(9).ResultStar;
                    stars += GetPlayerStages(10).ResultStar;
                    stars += GetPlayerStages(11).ResultStar;
                    stars += GetPlayerStages(12).ResultStar;
                    return stars >= 12;
                case 4:
                    stars += GetPlayerStages(13).ResultStar;
                    stars += GetPlayerStages(14).ResultStar;
                    stars += GetPlayerStages(15).ResultStar;
                    stars += GetPlayerStages(16).ResultStar;
                    return stars >= 12;
                    //break;
            }
        }
        return false;
    }

    private bool CheckGold(int price)
    {
        if((Gold - price) > 0)
        {
            return true;
        }
        return false;
    }

    public void SetMagnetItemCount(int price,int _magnetitem)
    {
        if (CheckGold(price))
        {
            Gold -= price;
            MagnetItem += _magnetitem;
        }
        else
        {
            UIManager.Instance.ShowPopupUi<FailToBuyPopup>();
        }
    }

    public void SetMagnetItemCount(int _magnetitem)
    {
        MagnetItem += _magnetitem;
    }

    public void SetShieldItemCount(int price,int _shielditem)
    {
        if (CheckGold(price))
        {
            Gold -= price;
            ShieldItem += _shielditem;
        }
        else
        {
            UIManager.Instance.ShowPopupUi<FailToBuyPopup>();
        }
    }

    public void SetShieldItemCount(int _shielditem)
    {
        ShieldItem += _shielditem;
    }
    public void SetLifeItemCount(int price,int _lifeItem)
    {
        if (CheckGold(price))
        {
            Gold -= price;
            LifeItem += _lifeItem;
        }
        else
        {
            UIManager.Instance.ShowPopupUi<FailToBuyPopup>();
        }
    }
    public void SetLifeItemCount(int _lifeItem)
    {
        LifeItem += _lifeItem;
    }

    public void SetReview()
    {
        if(Review == false)
        {
            if (GetPlayerStages(2).Cleared)
            {
                UIManager.Instance.ShowPopupUi<ReviewPopupUI>();
                Review = true;
            }
        }
    }
}
#endregion
public class DataManager : Managers<DataManager>
{
    public static Dictionary<TableType, DataContents> TableDic = new Dictionary<TableType, DataContents>();
    public PlayerInfo playerInfo;

    private const string _filePath = "PlayerInfomation.json";

    public bool IsLoadPlayerInfo = false;

    public override void Init()
    {
        
    }

    public void LoadPlayerInfo()
    {
        if (IsLoadPlayerInfo)
            return;
        var filePath = Path.Combine(Application.persistentDataPath, _filePath);
        if (File.Exists(filePath))
        {
            string text = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(text))
            {
                LoadResourcesData();
                return;
            }
            byte[] bytes = System.Convert.FromBase64String(text);
            string jdata = System.Text.Encoding.UTF8.GetString(bytes);

            playerInfo = JsonUtility.FromJson<PlayerInfo>(jdata); // new PlayerInfo(text);//
            IsLoadPlayerInfo = true;
            return;
        }
        else
        {
            LoadResourcesData();
            return;
        }
    }

    private void LoadResourcesData()
    {
        TextAsset text = Resources.Load<TextAsset>("Data/PlayerInfomation");//ResourcesManager.Instance.Load<TextAsset>("Data/" + _filePath);
        if (text != null)
        {
            playerInfo = new PlayerInfo(text.text);
            IsLoadPlayerInfo = true;
        }
        else
        {
            print("불러오기 실패");
        }
    }

    public void SavePlayerInfo()
    {
        if (IsLoadPlayerInfo == false)
            return;
        var filePath = Path.Combine(Application.persistentDataPath, _filePath);

        string JsonData = JsonUtility.ToJson(playerInfo,true);
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(JsonData);
        string code = System.Convert.ToBase64String(bytes);

        File.WriteAllText(filePath, code);
    }

    public void StringToGameInfo(string localData)
    {
        print(localData);
        byte[] bytes = System.Convert.FromBase64String(localData);
        string jdata = System.Text.Encoding.UTF8.GetString(bytes);

        playerInfo = JsonUtility.FromJson<PlayerInfo>(jdata); // new PlayerInfo(text);//
        IsLoadPlayerInfo = true;
    }

    public string GameInfoToString()
    {
        string JsonData = JsonUtility.ToJson(playerInfo, true);
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(JsonData);
        string code = System.Convert.ToBase64String(bytes);
        return code;
    }

    public static int ToInter(TableType tableType, int tableIndex,string subject)
    {
        if (TableDic.ContainsKey(tableType))
            return TableDic[tableType].ToInter(tableIndex, subject);
        return 0;
    }
    public static float ToFloat(TableType tableType, int tableIndex, string subject)
    {
        if (TableDic.ContainsKey(tableType))
            return TableDic[tableType].Tofloat(tableIndex, subject);
        return 0;
    }
    public static string ToString(TableType tableType, int tableIndex, string subject)
    {
        if (TableDic.ContainsKey(tableType))
            return TableDic[tableType].Tostring(tableIndex, subject);
        return string.Empty;
    }
    public static bool ToBool(TableType tableType, int tableIndex, string subject)
    {
        if (TableDic.ContainsKey(tableType))
            return TableDic[tableType].Tobool(tableIndex, subject);
        return false;
    }
}
