using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaSpawnController : MonoBehaviour
{

    [HideInInspector]
    public UnityEvent OnAllEnemiesDeath = new UnityEvent();
    public List<WaveDesc> WaveDesks;

    public void Init(EnemiesPool enemiesPool, GameArea gameArea)
    {
        currentWave = WaveDesks[0];
        this.enemiesPool = enemiesPool;
        this.gameArea = gameArea;
        SetupNextWave();
        gameArea.OnPlayerEntered.AddListener(TrySpawnEnemies);
    }

    public void Terminate()
    {
        gameArea.OnPlayerEntered.RemoveListener(TrySpawnEnemies);

        foreach (var point in currentWave.spawnPoints)
        {
            point.OnEnabled.RemoveListener(TrySpawnEnemies);
            point.Terminate();
        }
    }

    #region private

    int countOfEnemies = 0;
    int currentSpawnCurrency;
    GameArea gameArea;
    EnemiesPool enemiesPool;
    WaveDesc currentWave;
    
    void SetupNextWave()
    {
        currentSpawnCurrency = currentWave.maxSpawnCurrency;

        foreach (var point in currentWave.spawnPoints)
        {
            point.Init();
            point.OnEnabled.AddListener(TrySpawnEnemies);
        }

        foreach (var enemyDesc in currentWave.enemyDescs)
            countOfEnemies += enemyDesc.count;
    }

    void TrySpawnEnemies()
    {
        List<SpawnPoint> points = TryTakeSpawnPoints();
        List<Enemy> enemies = GetBestSetOfEnemy(points.Count);

        for (int idx = 0; idx < enemies.Count; idx++)
        {
            SpawnEnemy(enemies[idx], points[idx].transform);
            points[idx].Disable();
        }
    }

    List<SpawnPoint> TryTakeSpawnPoints()
    {
        List<SpawnPoint> points = new List<SpawnPoint>();
        foreach (SpawnPoint point in currentWave.spawnPoints)
        {
            if (point.IsEnabled())
                points.Add(point);
        }
        return points;
    }

    List<Enemy> GetBestSetOfEnemy(int countOfPoint)
    {
        List<Enemy> enemies = new List<Enemy>();

        foreach (EnemyDesc desc in currentWave.enemyDescs)
        {
            var countOfEnemy = desc.count;
            for (int idx = 0; idx < countOfEnemy; idx++)
            {
                if (enemies.Count == countOfPoint)
                    return enemies;
                if (desc.cost > currentSpawnCurrency)
                    return enemies;
                enemies.Add(desc.enemyType);
                currentSpawnCurrency -= desc.cost;
                desc.count--;
            }
        }

        return enemies;
    }

    void SpawnEnemy(Enemy enemy, Transform spawnPoint)
    {
        var spawnedEnemy = enemiesPool.Take(enemy.tag).GetComponent<Enemy>();
        spawnedEnemy.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        spawnedEnemy.Enable();
        spawnedEnemy.OnDie.AddListener((_) => EnemyDeath(spawnedEnemy));
    }

    void EnemyDeath(Enemy enemy)
    {
        enemy.OnDie.RemoveListener((_) => EnemyDeath(enemy));
        currentSpawnCurrency += enemy.GetReturnCost();
        countOfEnemies--;
        TrySpawnEnemies();
        if (countOfEnemies <= 0)
        {
            WaveDesks.RemoveAt(0);
            if (WaveDesks.Count != 0)
            {
                currentWave = WaveDesks[0];
                SetupNextWave();
                TrySpawnEnemies();
            }
            else
            {
                OnAllEnemiesDeath.Invoke();
            }
            //gameArea.Terminate();
        }
    }

    #endregion

}

[System.Serializable]
public class WaveDesc
{
    public List<EnemyDesc> enemyDescs;
    public List<SpawnPoint> spawnPoints;
    public int maxSpawnCurrency;
}

[System.Serializable]
public class EnemyDesc
{
    public Enemy enemyType;
    public int count;
    public int cost;
}
