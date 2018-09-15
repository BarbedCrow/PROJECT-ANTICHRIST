using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaSpawnController : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnAllEnemiesDeath = new UnityEvent();

    public List<EnemyDesc> enemyDescs;
    public List<Transform> spawnPoints;
    public int maxSpawnCurrency;

    public void Init(GameArea gameArea, SpawnManager spawnManager)
    {
        currentSpawnCurrency = maxSpawnCurrency;
        this.gameArea = gameArea;
        this.spawnManager = spawnManager;
        gameArea.PlayerEntered.AddListener(TrySpawnEnemies);
    }

    public void Terminate()
    {
        gameArea.PlayerEntered.RemoveListener(TrySpawnEnemies);
    }

    #region private

    int countOfEnemies = 0;
    int currentSpawnCurrency;
    GameArea gameArea;
    SpawnManager spawnManager;

    void SpawnEnemies(List<EnemyBase> SetOfEnemies, List<Transform> SpawnPoints)
    {
        for (int idx = 0; idx < SetOfEnemies.Count; idx++)
        {
            var prefab = spawnManager.SpawnUnit(SetOfEnemies[idx], SpawnPoints[idx % SpawnPoints.Count].transform);
            prefab.OnDeath.AddListener(() => EnemyDeath(prefab));
            countOfEnemies++;
        }
    }

    void TrySpawnEnemies()
    {
        List<EnemyBase> bestSet = GetBestSetOfEnemy();

        if (bestSet.Count != 0)
        {
            SpawnEnemies(bestSet, spawnPoints);
        }
    }
    
    List<EnemyBase> GetBestSetOfEnemy()
    {
        List<EnemyBase> enemies = new List<EnemyBase>();

        foreach (EnemyDesc desc in enemyDescs)
        {
            var countOfEnemy = desc.count;
            for (int idx = 0; idx < countOfEnemy; idx ++)
            {
                if (desc.enemyType.GetReturnCost() > currentSpawnCurrency)
                    return enemies;
                enemies.Add(desc.enemyType);
                currentSpawnCurrency -= desc.cost;
                desc.count--;
            }
        }

        return enemies;
    }

    void EnemyDeath(EnemyBase enemy)
    {
        currentSpawnCurrency += enemy.GetReturnCost();
        countOfEnemies--;
        TrySpawnEnemies();
        if (countOfEnemies == 0)
        {
            OnAllEnemiesDeath.Invoke();
            return;
        }
    }

    #endregion
}

[System.Serializable]
public class EnemyDesc
{
    public EnemyBase enemyType;
    public int count;
    public int cost;
}