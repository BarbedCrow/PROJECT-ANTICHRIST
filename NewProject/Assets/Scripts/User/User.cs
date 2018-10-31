using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    [SerializeField] AbilityInfo[] abilityInfos;

    public AbilityInfo[] GetAbilityInfos()
    {
        return abilityInfos;
    }

    public void AddScore(int score)
    {
        this.score += score;
    }

    public int GetScore()
    {
        return score;
    }

    #region private

    int score;

    #endregion
}
