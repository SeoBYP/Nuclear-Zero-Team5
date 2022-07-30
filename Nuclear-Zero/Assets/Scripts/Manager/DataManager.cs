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
        StageIndex = data["PlayerInfo"]["PlayerStages"][index]["StageIndex"].ToObject<int>();
        StageName = data["PlayerInfo"]["PlayerStages"][index]["StageName"].ToObject<string>();
        ResultStar = data["PlayerInfo"]["PlayerStages"][index]["ResultStar"].ToObject<int>();
        Cleared = data["PlayerInfo"]["PlayerStages"][index]["Cleared"].ToObject<bool>();
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
        ChapterIndex = data["PlayerInfo"]["PlayerChapters"][index]["ChapterIndex"].ToObject<int>();
        ChapterName = data["PlayerInfo"]["PlayerChapters"][index]["ChapterName"].ToObject<string>();
        BunkerItem1 = data["PlayerInfo"]["PlayerChapters"][index]["BunkerItem1"].ToObject<bool>();
        BunkerItem2 = data["PlayerInfo"]["PlayerChapters"][index]["BunkerItem2"].ToObject<bool>();
        BunkerItem3 = data["PlayerInfo"]["PlayerChapters"][index]["BunkerItem3"].ToObject<bool>();
        BunkerItem4 = data["PlayerInfo"]["PlayerChapters"][index]["BunkerItem4"].ToObject<bool>();
        ChapterStory = data["PlayerInfo"]["PlayerChapters"][index]["ChapterStory"].ToObject<bool>();
        BunkerStory = data["PlayerInfo"]["PlayerChapters"][index]["BunkerStory"].ToObject<bool>();
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
    public List<PlayerChapter> PlayerChapters;
    public List<PlayerStages> Stages;
    public PlayerInfo(string text)
    {
        var data = JObject.Parse(text);
        Gold = data["PlayerInfo"]["Gold"].ToObject<int>();
        DialogueObjectName = data["PlayerInfo"]["DialogueObjectName"].ToObject<string>();
        LookPrologue = data["PlayerInfo"]["LookPrologue"].ToObject<bool>();
        PlayerStars = data["PlayerInfo"]["PlayerStars"].ToObject<int>();
        SelectStage = data["PlayerInfo"]["SelectStage"].ToObject<int>();
        ClearStage = data["PlayerInfo"]["ClearStage"].ToObject<int>();
        SelectChapter = data["PlayerInfo"]["SelectChapter"].ToObject<int>();
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
            Stages = new List<PlayerStages>();
            for (int i = 0; i < match2.Count; i++)
            {
                Stages.Add(new PlayerStages(data, i));
            }
        }
    }

    public void SetClearStage(int selectStage,int starcount,int coincount)
    {
        Gold += coincount;
        for (int i = 0; i < Stages.Count; i++)
        {
            if(Stages[i].StageIndex == selectStage)
            {
                Stages[i].SetStageClear(starcount);
                break;
            }
        }

        int clearstage = selectStage + 1;
        if (clearstage >= Stages.Count)
            clearstage = Stages.Count;
        if (clearstage > ClearStage)
            ClearStage = clearstage;
        SetPlayerStars();
    }

    public PlayerStages GetPlayerStages(int stageIndex)
    {
        for(int i = 0; i < Stages.Count; i++)
        {
            if (Stages[i].StageIndex == stageIndex)
            {
                return Stages[i];
            }
        }
        return null;
    }

    public PlayerChapter GetPlayerChapter(int chapterIndex)
    {
        for(int i = 0; i < PlayerChapters.Count; i++)
        {
            if(PlayerChapters[i].ChapterIndex == chapterIndex)
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
        foreach(PlayerStages stage in Stages)
        {
            PlayerStars += stage.ResultStar;
        }
    }

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

    public void LoadPlayerInfo(string JsonString)
    {
        if (IsLoadPlayerInfo)
            return;
        TextAsset asset = ResourcesManager.Instance.Load<TextAsset>("Data/PlayerInfomation");
        if(asset == null)
        {
            Debug.Log("No File in Data");
            return;
        }
        string text = asset.text;
        if (text == null)
        {
            print("Json Load Is Failed");
        }
        playerInfo = new PlayerInfo(text);
        IsLoadPlayerInfo = true;
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
