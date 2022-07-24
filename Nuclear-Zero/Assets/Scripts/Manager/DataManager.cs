using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Define;

public class DataManager : Managers<DataManager>
{
    public static Dictionary<TableType, DataContents> TableDic = new Dictionary<TableType, DataContents>();
    public static Dictionary<TextType, Dialogue[]> DialogueDic = new Dictionary<TextType, Dialogue[]>();

    public static bool isFinish = false;

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

    public void LoadText(TextType textType)
    {
        string path = "Data/" + textType.ToString();
        DialogueDic.Add(textType, Parse(path));
    }

    public Dialogue[] Parse(string _CSVFileName)
    {
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);
        List<Dialogue> dialogues = new List<Dialogue>();

        string[] rows = csvData.text.Split('\n');
        //string[] subject = rows[0].Split(',');
        do
        {
            for (int i = 1; i < rows.Length; i++)
            {
                Dialogue dialogue = new Dialogue();
                string[] cols = rows[i].Split(',');

                int tableindex = 0;
                int.TryParse(cols[0], out tableindex);
                dialogue.ID = tableindex;
                dialogue.EventName = cols[1];

                int index = 0;
                int.TryParse(cols[2], out index);
                int anser1 = 0;
                int anser2 = 0;
                int anser3 = 0;
                int.TryParse(cols[5], out anser1);
                int.TryParse(cols[6], out anser2);
                int.TryParse(cols[7], out anser3);
                TalkData datas = new TalkData(index, cols[3], cols[4], anser1, anser2, anser3);
                dialogue.SetTalkData(datas);

                dialogues.Add(dialogue);
            }
        } while (rows[0] == "");
 
        return dialogues.ToArray();//dialogueList.ToArray();
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
