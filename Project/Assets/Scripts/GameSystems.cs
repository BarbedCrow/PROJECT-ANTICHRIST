using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystems : MonoBehaviour
{
    public void Init()
    {
        testPool = GetComponent<PoolBase>();
        testPool.Init();
        var gO = testPool.Take("Damagable");
        testPool.Release(gO);
    }

    public void Terminate()
    {

    }

    #region private

    PoolBase testPool;

    #endregion
}
