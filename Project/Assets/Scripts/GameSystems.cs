using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PoolBase))]
public class GameSystems : MonoBehaviour
{
    
    public void Init()
    {
        pool = GetComponent<PoolBase>();
        pool.Init();

        spawnManager = GetComponent<SpawnManager>();
        spawnManager.Init(pool);
    }

    public void Terminate()
    {
        spawnManager.Terminate();
        pool.Terminate();

        Destroy(gameObject);
    }

    public PoolBase GetPool()
    {
        return pool;
    }

    public SpawnManager GetSpawnManager()
    {
        return spawnManager;
    }

    #region private

    SpawnManager spawnManager;
    PoolBase pool;

    #endregion
}
