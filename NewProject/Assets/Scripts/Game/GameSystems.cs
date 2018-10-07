using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystems : MonoBehaviour
{

    [SerializeField] InputsLibrary inputsLibrary;
    [SerializeField] ProjectilesPool projectilesPool;
    [SerializeField] EnemiesPool enemiesPool;
    [SerializeField] HUDManager hudManager;

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
        hudManager.Init();
    }

    void TerminateComponents()
    {
        inputsLibrary.Terminate();
        projectilesPool.Terminate();
        enemiesPool.Terminate();
    }

    #endregion

}
