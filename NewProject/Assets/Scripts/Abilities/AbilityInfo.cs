using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInfo : MonoBehaviour
{

    [SerializeField]
    AbilityUid uid;

    [SerializeField]
    int lvl;

    [SerializeField]
    AbilitySlot slot;

    public AbilityUid GetUid()
    {
        return uid;
    }

    public int GetLvl()
    {
        return lvl;
    }

    public AbilitySlot GetSlot()
    {
        return slot;
    }
}
