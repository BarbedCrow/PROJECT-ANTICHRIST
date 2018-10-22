using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Difficulty : MonoBehaviour
{
    [HideInInspector] public EventOnDifficultChanged OnDifficultChanged = new EventOnDifficultChanged();
    [SerializeField] private DifficultyType currDifficulty;

    public DifficultyType GetDifficult()
    {
        return currDifficulty;
    }

    public void Change(DifficultyType difficulty)
    {
        if (difficulty == currDifficulty)
            return;

        currDifficulty = difficulty;
        OnDifficultChanged.Invoke(difficulty);
    }
}

public enum DifficultyType
{
    EASY,
    MEDIUM,
    HARD,
    MOMMY_WILL_NOT_HELP_YOU
}
