using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class User : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnScoreChange = new UnityEvent();

    [SerializeField] AbilityInfo[] abilityInfos;

    public AbilityInfo[] GetAbilityInfos()
    {
        return abilityInfos;
    }

    public void AddScore(int score)
    {
        this.score += score;
        OnScoreChange.Invoke();
    }

    public int GetScore()
    {
        return score;
    }

    #region private

    int score;

    #endregion
}
