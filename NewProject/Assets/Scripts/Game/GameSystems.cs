using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystems : MonoBehaviour
{

    [SerializeField] InputsLibrary inputsLibrary;
    [SerializeField] ProjectilesPool projectilesPool;
    [SerializeField] EnemiesPool enemiesPool;
    [SerializeField] AbilitiesPool abilitiesPool;
    [SerializeField] HUDManager hudManager;
    [SerializeField] AbilitiesLibrary abilitiesLibrary;
    [SerializeField] RewardRulesLibrary rewardRulesLibrary;

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

    public AbilitiesLibrary GetAbilitiesLibrary()
    {
        return abilitiesLibrary;
    }

    public AbilitiesPool GetAbilitiesPool()
    {
        return abilitiesPool;
    }

    public EnemiesPool GetEnemiesPool()
    {
        return enemiesPool;
    }

    #region private

    void InitComponents()
    {
        abilitiesPool.Init();
        projectilesPool.Init();
        enemiesPool.Init(projectilesPool, abilitiesPool);
        inputsLibrary.Init();
        hudManager.Init();
        rewardRulesLibrary.Init();
    }

    void TerminateComponents()
    {
        inputsLibrary.Terminate();
        projectilesPool.Terminate();
        enemiesPool.Terminate();
        abilitiesPool.Terminate();
    }

    #endregion

}
