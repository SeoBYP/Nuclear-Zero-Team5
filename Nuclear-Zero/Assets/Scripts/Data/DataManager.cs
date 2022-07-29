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
public class Stages
{
    public int StageIndex;
    public string StageName;
    public int ResultStar;
    public bool Cleared;

    public Stages(JObject data, int index)
    {
        StageIndex = data["PlayerInfo"]["Stages"][index]["StageIndex"].ToObject<int>();
        StageName = data["PlayerInfo"]["Stages"][index]["StageName"].ToObject<string>();
        ResultStar = data["PlayerInfo"]["Stages"][index]["ResultStar"].ToObject<int>();
        Cleared = data["PlayerInfo"]["Stages"][index]["Cleared"].ToObject<bool>();
    }
}
public class PlayerChapter
{
    public int ChapterIndex;
    public string ChapterName;
    public bool ChapterStory;
    public bool BunkerStory;
    public PlayerChapter(JObject data,int index)
    {
        ChapterIndex = data["PlayerInfo"]["PlayerChapters"][index]["ChapterIndex"].ToObject<int>();
        ChapterName = data["PlayerInfo"]["PlayerChapters"][index]["ChapterName"].ToObject<string>();
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
    public List<PlayerChapter> PlayerChapters;
    public List<Stages> Stages;
    public PlayerInfo(string text)
    {
        var data = JObject.Parse(text);
        Gold = data["PlayerInfo"]["Gold"].ToObject<int>();
        DialogueObjectName = data["PlayerInfo"]["DialogueObjectName"].ToObject<string>();
        LookPrologue = data["PlayerInfo"]["LookPrologue"].ToObject<bool>();
        PlayerStars = data["PlayerInfo"]["PlayerStars"].ToObject<int>();
        SelectStage = data["PlayerInfo"]["SelectStage"].ToObject<int>();
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
        if (match.Count != 0)
        {
            Stages = new List<Stages>();
            for (int i = 0; i < match.Count; i++)
            {
                Stages.Add(new Stages(data, i));
            }
        }
    }
}
#endregion
public class DataManager : Managers<DataManager>
{
    public static Dictionary<TableType, DataContents> TableDic = new Dictionary<TableType, DataContents>();
    public PlayerInfo playerInfo;

    public override void Init()
    {
        //Load(TableType.Chapter1);
    }

    private void Load(TableType tableType)
    {
        string path = Application.persistentDataPath + "/Data/" + tableType.ToString();
        if (!TableDic.ContainsKey(tableType))
        {
            //if (File.Exists(path) == false)
            //{
            //    Debug.Log($"{path} Don't Have {tableType} Data");
            //    return;
            //}
            DataContents lowBase = new DataContents();
            lowBase.LoadData("Data/" + tableType.ToString());

            TableDic.Add(tableType, lowBase);
        }
    }

    public void LoadPlayerInfo(string JsonString)
    {
        //string path = $"{Application.dataPath}/Data/{JsonString}";
        //print(path);
        //if (File.Exists(path) == false)
        //{
        //    Debug.Log("No File in Data");
        //    return;
        //}
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
