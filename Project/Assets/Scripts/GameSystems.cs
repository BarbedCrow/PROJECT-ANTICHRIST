using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PoolBase))]
[RequireComponent(typeof(PoolBase))]
public class GameSystems : MonoBehaviour
{
    
    public void Init()
    {
        var pools = GetComponents<PoolBase>();

        foreach (PoolBase pool in pools)
        {
            if (pool.uid == "PoolEnemies")
            {
                poolEnemies = pool;
                poolEnemies.Init();
            }

            if (pool.uid == "PoolProjectiles")
            {
                poolProjectiles = pool;
                poolProjectiles.Init();
            }
        }

        spawnManager = GetComponent<SpawnManager>();
        spawnManager.Init(poolEnemies);
    }

    public void Terminate()
    {
        spawnManager.Terminate();
        poolEnemies.Terminate();
        poolProjectiles.Terminate();

        Destroy(gameObject);
    }

    public PoolBase GetPoolProjectiles()
    {
        return poolProjectiles;
    }

    public PoolBase GetPoolEnemies()
    {
        return poolEnemies;
    }

    public SpawnManager GetSpawnManager()
    {
        return spawnManager;
    }

    #region private

    SpawnManager spawnManager;
    PoolBase poolProjectiles;
    PoolBase poolEnemies;

    #endregion
}
