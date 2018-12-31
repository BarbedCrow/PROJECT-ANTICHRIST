using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesPool : PoolBase
{

    public override void Init(params MonoBehaviour[] args)
    {
        base.Init();

        foreach(var objs in availableObjects)
        {
            foreach(var obj in objs.Value)
            {
                var ability = obj.GetComponent<SpriteAbilityAttackBase>();
                ability.Init(this);
            }
        }
    }

    public override void Terminate()
    {
        foreach (var objs in availableObjects)
        {
            foreach (var obj in objs.Value)
            {
                var ability = obj.GetComponent<SpriteAbilityAttackBase>();
                ability.Terminate();
            }
        }

        base.Terminate();
    }

}
