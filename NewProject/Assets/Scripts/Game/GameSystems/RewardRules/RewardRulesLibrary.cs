using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardRulesLibrary : MonoBehaviour
{
    [SerializeField]
    RewardRuleBase[] rules;

    public void Init()
    {
        foreach(var rule in rules)
        {
            rule.Init();
        }
    }
}

