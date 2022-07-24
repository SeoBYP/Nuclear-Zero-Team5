using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum CharacterIndex
    {
        ID,
        NAME,
        JOB,
        LEVEL,
        MAXLEVEL,
        EXP,
        MAXEXP,
        HP,
        ATTACK,
        CRITICAL,
        DEFENCE,
        MAGICDEFFENCE,
        ADDMAXEXP,
        ADDHP,
        ADDATTACT,
        ADDCRITICAL,
        ADDDEFENCE,
        ADDMAGICDEFFENCE,
    }

    public enum Chapter
    {
        Chapter1,
        Chapter2,
        Chapter3,
        Chapter4,
    }

    public enum Stage
    {
        None,
        Stage1_1,
        Stage1_2,
        Stage1_3,
        Stage1_4,
        Stage2_1,
        Stage2_2,
        Stage2_3,
        Stage2_4,
        Stage3_1,
        Stage3_2,
        Stage3_3,
        Stage3_4,
        Stage4_1,
        Stage4_2,
        Stage4_3,
        Stage4_4,
    }

    public enum TableType
    {
        //Chapter1,
    }

    public enum TextType
    {
        Chapter1,
    }

    public enum UIEvents
    {
        Click,
        Drag,
    }
}
