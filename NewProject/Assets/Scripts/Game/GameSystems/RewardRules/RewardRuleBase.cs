using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardRuleBase : MonoBehaviour
{
    public virtual void Init()
    {
        user = GameObject.FindGameObjectWithTag(Tags.USER).GetComponent<User>();
        difficulty = GameObject.FindGameObjectWithTag(Tags.DIFFICULTY).GetComponent<Difficulty>();
    }

    #region

    protected User user;
    protected Difficulty difficulty;
    #endregion
}
