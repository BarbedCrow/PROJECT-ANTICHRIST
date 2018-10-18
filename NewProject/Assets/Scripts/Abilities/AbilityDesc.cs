using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDesc : MonoBehaviour
{

    [SerializeField]
    AbilityUid uid;

    [SerializeField]
    AbilityLogicBase[] logics;

    public AbilityLogicBase GetAbilityLogicByLvl(int lvl)
    {
        return logics[lvl];
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
