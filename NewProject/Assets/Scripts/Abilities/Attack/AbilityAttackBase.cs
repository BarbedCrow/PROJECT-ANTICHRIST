using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityAttackBase : AbilityLogicBase
{
    public override void Setup(params MonoBehaviour[] args)
    {
        base.Setup(args);

        foreach(var arg in args)
        {
            if (damager == null && arg is PropDamager)
            {
                damager = (PropDamager)arg;
            }
        }
    }

    public override void StartUse()
    {

    }

    public override void StopUse()
    {

    }

    #region private
    
    protected PropDamager damager;
    protected float currentDamage;

    #endregion

}
