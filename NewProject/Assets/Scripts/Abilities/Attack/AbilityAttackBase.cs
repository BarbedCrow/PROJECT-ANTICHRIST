using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityAttackBase : AbilityLogicBase
{
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    [SerializeField] protected List<string> ignoredTags;

    public override void Setup(params MonoBehaviour[] args)
    {
        base.Setup(args);

        foreach (MonoBehaviour arg in args)
        {
            if (pool == null && arg is AbilitiesPool)
            {
                pool = (AbilitiesPool)arg;
            }

            if (damager == null && arg is PropDamager)
            {
                damager = (PropDamager)arg;
            }
        }
    }

    public void SetupPivot(Transform pivot)
    {
        this.pivot = pivot;
    }

    #region private

    protected PropDamager damager;
    protected Transform pivot;
    protected AbilitiesPool pool;

    #endregion

}
