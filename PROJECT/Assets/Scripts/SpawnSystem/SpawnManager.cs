using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public void SpawnUnits(AreaSpawnController areaSpawnController)
    {
        foreach (EnemyDesc enemyDesc in areaSpawnController.enemyDescs)
        {
            for (int i = 0; i < enemyDesc.count; i++)
            {
                Spawn(enemyDesc.enemyType, areaSpawnController.spawnPoints[i]);
            }
        }
    }

    #region private

    void Spawn(EnemyBase enemy, Transform transform)
    {
        var spawnObj = Instantiate(enemy, transform);
        spawnObj.Init();
    }

    void Terminate()
    {
        Destroy(gameObject);
    }

    #endregion


}
