using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public void Init(PoolBase pool)
    {
        this.pool = pool;
    }

    public void Terminate()
    {

    }

    public EnemyBase SpawnUnit(EnemyBase enemy, Transform transform)
    {
        var spawnObj = pool.Take(enemy.tag);
        spawnObj.transform.SetPositionAndRotation(transform.position, transform.rotation);

        EnemyBase spawnObjComponents = spawnObj.GetComponent<EnemyBase>() as EnemyBase;
        spawnObjComponents.Init(pool);

        return spawnObjComponents;
    }

    #region private

    PoolBase pool;

    #endregion


}
