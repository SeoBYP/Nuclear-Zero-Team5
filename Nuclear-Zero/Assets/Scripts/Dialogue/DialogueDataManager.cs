using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDataManager : Managers<DialogueDataManager>
{
    //[SerializeField] string csv_FileName = "Data/Chapter1";

    

    //public static bool isFinish = false;

    //public override void Init()
    //{
    //    DialogueParser theParser = Utils.GetOrAddComponent<DialogueParser>(gameObject);
    //    Dialogue[] dialogues = Parse(csv_FileName);

    //    for (int i = 0; i < dialogues.Length; i++)
    //    {
    //        //dialogueDic.Add(i + 1, dialogues[i]);
    //    }
    //    isFinish = true;
    //}

    //public Dialogue[] Parse(string _CSVFileName)
    //{
    //    List<Dialogue> dialogueList = new List<Dialogue>();
    //    TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);

    //    string[] data = csvData.text.Split(new char[] { '\n' });
    //    for (int i = 1; i < data.Length;)
    //    {
    //        string[] row = data[i].Split(new char[] { ',' });

    //        Dialogue dialogue = new Dialogue();
    //        dialogue.name = row[1];
    //        List<string> contextList = new List<string>();
    //        do
    //        {
    //            string temp = row[2];
    //            var contenxt = temp.Replace('&', ',');
    //            contextList.Add(contenxt);
    //            if (++i < data.Length)
    //                row = data[i].Split(new char[] { ',' });
    //            else
    //                break;
    //        } while (row[0].ToString() == "");
    //        dialogue.contexts = contextList.ToArray();

    //        dialogueList.Add(dialogue);

    //    }
    //    return dialogueList.ToArray();
    //}

    //public string[] Contexts(int index)
    //{
    //    string[] text = null;
    //    if (dialogueDic.ContainsKey(index))
    //    {
    //        for(int i = 0; i < dialogueDic[index].contexts.Length; i++)
    //        {
    //            text = dialogueDic[index].contexts;
    //        }
    //    }
    //    return text;
    //}
}
