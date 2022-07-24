using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;

public class DataContents
{
    public Dictionary<int, Dictionary<string, string>> InfoDic = new Dictionary<int, Dictionary<string, string>>();

    public void LoadData(string path)
    {
        TextAsset asset = Resources.Load<TextAsset>(path);
        string[] rows = asset.text.Split('\n');
        rows[0] = rows[0].Replace("\r", "");
        string[] subjects = rows[0].Split(',');

        for(int i = 1; i < rows.Length; i++)
        {
            rows[i] = rows[i].Replace("\r", "");
            string[] cols = rows[i].Split(',');

            int tableindex = 0;
            int.TryParse(cols[0], out tableindex);

            if (!InfoDic.ContainsKey(tableindex))
            {
                InfoDic.Add(tableindex, new Dictionary<string, string>());
            }

            for(int j = 1; j < cols.Length; j++)
            {
                if(InfoDic[tableindex].ContainsKey(subjects[j]) == false)
                {
                    InfoDic[tableindex].Add(subjects[j], cols[j]);
                }
            }
        }
    }

    public void LoadText()
    {

    }

    public int ToInter(int tableIndex,string subject)
    {
        int data = 0;
        if (InfoDic.ContainsKey(tableIndex))
        {
            if (InfoDic[tableIndex].ContainsKey(subject))
            {
                int.TryParse(InfoDic[tableIndex][subject], out data);
            }
        }
        return data;
    }

    public float Tofloat(int tableIndex,string subject)
    {
        float data = 0;
        if (InfoDic.ContainsKey(tableIndex))
        {
            if (InfoDic[tableIndex].ContainsKey(subject))
            {
                float.TryParse(InfoDic[tableIndex][subject], out data);
            }
        }
        return data;
    }

    public string Tostring(int tableIndex,string subject)
    {
        string data = null;
        if (InfoDic.ContainsKey(tableIndex))
        {
            if (InfoDic[tableIndex].ContainsKey(subject))
            {
                data = InfoDic[tableIndex][subject];
            }
        }
        return data;
    }

    public bool Tobool(int tableIndex,string subject)
    {
        string boolIndex = Tostring(tableIndex, subject);
        if(boolIndex == null)
        {
            Debug.Log($"Don't Have {subject} Data");
            return false;
        }
        if (boolIndex == "True" || boolIndex == "true")
            return true;
        else
            return false;
    }
}
