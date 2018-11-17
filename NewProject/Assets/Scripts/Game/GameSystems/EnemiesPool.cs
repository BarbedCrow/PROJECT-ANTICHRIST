using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPool : PoolBase
{
    public override void Init(params MonoBehaviour[] args)
    {
        base.Init();

        foreach (var arg in args)
        {
            if (projectilesPool == null && arg is ProjectilesPool)
                projectilesPool = (ProjectilesPool)arg;
        }

        foreach (var objs in availableObjects)
        {
            foreach (var obj in objs.Value)
            {
                var enemy = obj.GetComponent<Enemy>();
                enemy.Setup(this, projectilesPool);
                enemy.Init();
            }
        }
    }

    public override void Terminate()
    {
        foreach (var objs in availableObjects)
        {
            foreach (var obj in objs.Value)
            {
                var enemy = obj.GetComponent<Enemy>();
                enemy.Terminate();
            }
        }

        base.Terminate();
    }

    #region private

    ProjectilesPool projectilesPool;

    #endregion
}
