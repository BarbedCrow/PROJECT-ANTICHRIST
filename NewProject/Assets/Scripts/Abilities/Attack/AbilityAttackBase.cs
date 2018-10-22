using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityAttackBase : AbilityLogicBase
{
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    [SerializeField] protected SpriteAbilityAttack sprite;
    [SerializeField] List<string> ignoredTags;

    public override void Init()
    {
        base.Init();
        if (sprite != null)
            sprite.Init(ignoredTags);
    }

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

    public void SetupPivot(Transform pivot)
    {
        this.pivot = pivot;
    }

    public override void StartUse()
    {
        base.StartUse();
    }

    public override void StopUse()
    {
        base.StopUse();
    }

    #region private

    protected PropDamager damager;
    protected Transform pivot;

    #endregion

}
