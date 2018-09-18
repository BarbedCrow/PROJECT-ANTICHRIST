using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaSpawnController : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnAllEnemiesDeath = new UnityEvent();

    public List<EnemyDesc> enemyDescs;
    public List<SpawnPoint> spawnPoints;
    public int maxSpawnCurrency;

    public void Init(GameArea gameArea, SpawnManager spawnManager)
    {
        currentSpawnCurrency = maxSpawnCurrency;
        this.gameArea = gameArea;
        this.spawnManager = spawnManager;
        gameArea.PlayerEntered.AddListener(TrySpawnEnemies);

        foreach(SpawnPoint point in spawnPoints)
        {
            point.Init();
            point.OnEnabled.AddListener(TrySpawnEnemies);
        }

        foreach(EnemyDesc enemies in enemyDescs)
        {
            countOfEnemies += enemies.count;
        }
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

    void SpawnEnemy(EnemyBase enemy, Transform SpawnPoint)
    {
        var prefab = spawnManager.SpawnUnit(enemy, SpawnPoint);
        prefab.OnDeath.AddListener(() => EnemyDeath(prefab));        
    }

    void TrySpawnEnemies()
    {
        List<SpawnPoint> points = TryTakeSpawnPoints();
        List<EnemyBase> enemies = GetBestSetOfEnemy(points.Count);

        for (int idx = 0; idx < enemies.Count; idx++)
        {
            SpawnEnemy(enemies[idx], points[idx].transform);
            points[idx].Disable();
        }
    }
    
    List<SpawnPoint> TryTakeSpawnPoints()
    {
        List<SpawnPoint> points = new List<SpawnPoint>();
        foreach (SpawnPoint point in spawnPoints)
        {
            if (point.GetIsEnabled())
                points.Add(point);
        }
        return points;
    }

    List<EnemyBase> GetBestSetOfEnemy(int countOfPoint)
    {
        List<EnemyBase> enemies = new List<EnemyBase>();

        foreach (EnemyDesc desc in enemyDescs)
        {
            if (enemies.Count == countOfPoint)
                return enemies;

            var countOfEnemy = desc.count;
            for (int idx = 0; idx < countOfEnemy; idx ++)
            {
                if (desc.cost > currentSpawnCurrency)
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