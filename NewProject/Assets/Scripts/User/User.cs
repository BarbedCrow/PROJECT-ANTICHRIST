using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    [SerializeField]
    AbilityInfo[] abilityInfos;

    public AbilityInfo[] GetAbilityInfos()
    {
        return abilityInfos;
    }

}
