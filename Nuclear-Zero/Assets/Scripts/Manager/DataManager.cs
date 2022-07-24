using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Define;

public class DataManager : Managers<DataManager>
{
    public static Dictionary<TableType, DataContents> TableDic = new Dictionary<TableType, DataContents>();
    public static Dictionary<TextType, Dialogue> DialogueDic = new Dictionary<TextType, Dialogue>();

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
        if (!DialogueDic.ContainsKey(textType))
        {
            Dialogue dialogues = Parse(path);

            //for (int i = 0; i < dialogues.Length; i++)
            //{
            //    DialogueDic.Add(textType, dialogues[i]);
            //}
            isFinish = true;
        }
    }

    public Dialogue Parse(string _CSVFileName)
    {
        print(_CSVFileName);
        List<Dialogue> dialogueList = new List<Dialogue>();
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);

        string[] data = csvData.text.Split(new char[] { '\n' });
        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' });

            Dialogue dialogue = new Dialogue();
            dialogue.EventName = row[1];
            List<TalkData> contextList = new List<TalkData>();
            //do
            //{
            //    print(dialogue.EventName);
            //    //string temp = row[2];
            //    //var contenxt = temp.Replace('&', ',');
            //    //contextList.Add(contenxt);
            //    //if (++i < data.Length)
            //    //    row = data[i].Split(new char[] { ',' });
            //    //else
            //    //    break;
            //} while (row[0].ToString() == "");
            //dialogue.contexts = contextList.ToArray();

            dialogueList.Add(dialogue);

        }
        return null;//dialogueList.ToArray();
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
