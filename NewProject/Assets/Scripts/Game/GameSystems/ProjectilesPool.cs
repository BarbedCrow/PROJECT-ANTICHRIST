using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesPool : PoolBase
{

    public override void Init(params MonoBehaviour[] args)
    {
        base.Init();

        foreach(var objs in availableObjects)
        {
            foreach(var obj in objs.Value)
            {
                var projectile = obj.GetComponent<ProjectileBase>();
                projectile.Init(this);
            }
        }
    }

    public override void Terminate()
    {
        foreach (var objs in availableObjects)
        {
            foreach (var obj in objs.Value)
            {
                var projectile = obj.GetComponent<ProjectileBase>();
                projectile.Terminate();
            }
        }

        base.Terminate();
    }

}
