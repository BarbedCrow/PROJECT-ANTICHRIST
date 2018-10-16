using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDesc : MonoBehaviour
{

    [SerializeField]
    string uid;

    [SerializeField]
    AbilityLogicBase[] logics;

    public AbilityLogicBase GetAbilityLogicByLvl(int lvl)
    {
        return logics[lvl];
    }

    public string GetUid()
    {
        return uid;
    }

}
