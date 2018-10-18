using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInfo : MonoBehaviour
{

    [SerializeField]
    AbilityUid uid;

    [SerializeField]
    LvlAbilities lvl;

    [SerializeField]
    AbilitySlot slot;

    public AbilityUid GetUid()
    {
        return uid;
    }

    public LvlAbilities GetLvl()
    {
        return lvl;
    }

    public AbilitySlot GetSlot()
    {
        return slot;
    }
}
