using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystems : MonoBehaviour
{

    public InputsLibrary inputsLibrary;
    public ProjectilesPool projectilesPool;
    public EnemiesPool enemiesPool;

    public void Init()
    {
        InitComponents();
    }

    public void Terminate()
    {
        TerminateComponents();
        Destroy(gameObject);
    }

    public InputsLibrary GetInputsLibrary()
    {
        return inputsLibrary;
    }

    public PoolBase GetProjectilesPool()
    {
        return projectilesPool;
    }

    public EnemiesPool GetEnemiesPool()
    {
        return enemiesPool;
    }

    #region private

    void InitComponents()
    {
        enemiesPool.Init();
        projectilesPool.Init();
        inputsLibrary.Init();
    }

    void TerminateComponents()
    {
        inputsLibrary.Terminate();
        projectilesPool.Terminate();
        enemiesPool.Terminate();
    }

    #endregion

}
