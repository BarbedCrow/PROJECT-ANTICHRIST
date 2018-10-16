using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesLibrary : MonoBehaviour
{

    [SerializeField]
    AbilityDesc[] descs;

    public AbilityDesc GetDescByUid(string uid)
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
