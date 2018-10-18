using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesLibrary : MonoBehaviour
{

    [SerializeField]
    AbilityDesc[] descs;

    public AbilityDesc GetDescByUid(AbilityUid uid)
    {
        foreach(var desc in descs)
        {
            if (desc.GetUid() == uid)
            {
                return desc;
            }
        }

        return null;
    }
}
