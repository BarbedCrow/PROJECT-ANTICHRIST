using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPool : PoolBase
{

    public override void Init()
    {
        base.Init();

        foreach (var objs in availableObjects)
        {
            foreach (var obj in objs.Value)
            {
                var enemy = obj.GetComponent<Enemy>();
                enemy.Setup(this);
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
}
