using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDesc : MonoBehaviour
{

    [SerializeField]
    AbilityUid uid;

    [SerializeField]
    AbilityLogicBase[] logics;

    public AbilityLogicBase GetAbilityLogicByLvl(LvlAbilities lvl)
    {
        return logics[(int)lvl];
    }

    public AbilityUid GetUid()
    {
        return uid;
    }
}

public enum AbilityUid
{
    FIRE,
    ICE,
    MAX_COUNT
}

public enum LvlAbilities
{
    FIRST,
    SECOND,
    THIRD,
    MAX_COUNT
}
