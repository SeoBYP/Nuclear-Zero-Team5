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
public class PlayerChapter
{
    public int ChapterIndex;
    public string ChapterName;
    public bool BunkerItem1;
    public bool BunkerItem2;
    public bool BunkerItem3;
    public bool BunkerItem4;
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
        ChapterStory = data["PlayerChapters"][index]["ChapterStory"].ToObject<bool>();
        BunkerStory = data["PlayerChapters"][index]["BunkerStory"].ToObject<bool>();
    }
}

public class PlayerInfo
{
    public int Gold;
    public string DialogueObjectName;
    public bool LookPrologue;
    public int PlayerStars;
    public int SelectStage;
    public int ClearStage;
    public int SelectChapter;
    public float BGMSound;
    public float EffectSound;
    public int RaderItem;
    public int ShieldItem;
    public int LifeItem;
    public List<PlayerChapter> PlayerChapters;
    public List<PlayerStages> PlayerStages;
    public PlayerInfo(string text)
    {
        var data = JObject.Parse(text);
        Gold = data["Gold"].ToObject<int>();
        DialogueObjectName = data["DialogueObjectName"].ToObject<string>();
        LookPrologue = data["LookPrologue"].ToObject<bool>();
        PlayerStars = data["PlayerStars"].ToObject<int>();
        SelectStage = data["SelectStage"].ToObject<int>();
        ClearStage = data["ClearStage"].ToObject<int>();
        SelectChapter = data["SelectChapter"].ToObject<int>();
        BGMSound = data["BGMSound"].ToObject<float>();
        EffectSound = data["EffectSound"].ToObject<float>();
        RaderItem = data["RaderItem"].ToObject<int>();
        ShieldItem = data["ShieldItem"].ToObject<int>();
        LifeItem = data["LifeItem"].ToObject<int>();
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
            && curchapter.BunkerItem3 && curchapter.BunkerItem4)
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

    public void ShowEnding()
    {
        if (PlayerStars < 30)
        {
            UIManager.Instance.ShowPopupUi<BadEndingPopupUI>();
        }
        else if (PlayerStars < 48 && PlayerStars >= 30)
        {
            UIManager.Instance.ShowPopupUi<NormalEndingPopupUI>();
        }
        else
        {
            UIManager.Instance.ShowPopupUi<HappyEndingPopupUI>();
        }
    }

    public void SetSoundSetting(float bgm, float effect)
    {
        BGMSound = bgm;
        EffectSound = effect;
    }

    public void SetRaderItemCount(int _raderitem) { RaderItem = _raderitem; }
    public void SetShieldItemCount(int _shielditem) { ShieldItem = _shielditem; }
    public void SetLifeItemCount(int _lifeItem) { LifeItem = _lifeItem; }

    public int GetRaderItemCount() { return RaderItem; }
    public int GetShieldItemCount() { return ShieldItem; }
    public int GetLifeItemCount() { return LifeItem; }


}
#endregion
public class DataManager : Managers<DataManager>
{
    public static Dictionary<TableType, DataContents> TableDic = new Dictionary<TableType, DataContents>();
    public PlayerInfo playerInfo;

    private static bool IsLoadPlayerInfo = false;

    public override void Init()
    {
        //Load(TableType.Chapter1);
    }

    private void Load(TableType tableType)
    {
        string path = Application.persistentDataPath + "/Data/" + tableType.ToString();
        if (!TableDic.ContainsKey(tableType))
        {
            DataContents lowBase = new DataContents();
            lowBase.LoadData("Data/" + tableType.ToString());

            TableDic.Add(tableType, lowBase);
        }
    }

    public void LoadPlayerInfo()
    {
        if (IsLoadPlayerInfo)
            return;
        var filePath = Application.dataPath + "/Resources/Data/PlayerInfomation";
        if (File.Exists(filePath) == false)
        {
            return;
        }
        string text = File.ReadAllText(filePath);
        if (text == null)
        {
            print("Json Load Is Failed");
            return;
        }
        playerInfo = new PlayerInfo(text);
        IsLoadPlayerInfo = true;
    }

    public void SavePlayerInfo()
    {
        print("Save");
        var filePath = Application.dataPath + "/Resources/Data/PlayerInfomation";
        print(filePath);
        string jdata = JsonConvert.SerializeObject(playerInfo);
        //JArray jsonArray = JArray.Parse(jdata);
        var data = JObject.Parse(jdata);//jsonArray[0].ToString());

        File.WriteAllText(filePath, data.ToString());
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
