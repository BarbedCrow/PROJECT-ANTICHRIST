using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{

    public void Init()
    {

    }

    public void Terminate()
    {

    }

    public EnemyBase SpawnUnit(EnemyBase enemy, Transform transform)
    {
        var spawnObj = Instantiate(enemy, transform.position, transform.rotation);
        spawnObj.Init();
        return spawnObj;
    }

    #region private

    //GameArea gameArea;

    #endregion


}
