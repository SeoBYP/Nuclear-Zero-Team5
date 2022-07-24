using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//선택지에 따라 변경되는 것을 인덱스를 달아서 구분
//  대사 인덱스,대사, 답변1, 답변2, 답변3,
//  1, 미ㅏㄴㅇ런ㅁ아러, -1,-1,-1
//  2, 미ㅏㄴㅇ런ㅁ아러, -1,-1,-1
//  3, 미ㅏㄴㅇ런ㅁ아러, -1,-1,-1
//  4, 미ㅏㄴㅇ런ㅁ아러, -1,-1,-1
//  5, 미ㅏㄴㅇ런ㅁ아러, 6, 7,8

[System.Serializable] // 구조체가 인스펙터 창에 보이게 하기 위한 작업
public struct TalkData
{
    public int Index;
    public string name;
    public string[] contexts;
    public int Anser1;
    public int Anser2;
    public int Anser3;
}


[System.Serializable]
public class Dialogue
{
    public int ID;
    public string EventName;
    public TalkData[] dialgoues;
}