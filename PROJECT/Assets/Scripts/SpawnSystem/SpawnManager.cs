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

    public void SpawnUnit(EnemyBase enemy, Transform transform)
    {
        var spawnObj = Instantiate(enemy, transform.position, transform.rotation);
        spawnObj.Init();
    }

    #region private

    GameArea gameArea;

    #endregion


}
