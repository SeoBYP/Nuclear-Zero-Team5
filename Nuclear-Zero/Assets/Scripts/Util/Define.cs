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

    public enum Map
    {
        Map1_1,
        Map1_2,
        Map1_3,
        Map1_4,
        Map1_5,
    }

    public enum TableType
    {
        CharacterStat,
    }

    public enum Character
    {
        Worrior,
        Mage,
        Gunner,
        Archur,
    }

    public enum UIEvents
    {
        Click,
        Drag,
    }
}
