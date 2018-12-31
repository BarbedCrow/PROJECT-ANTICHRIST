using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropAbilityUser : PropBase
{
    [SerializeField] protected List<AbilityLogicBase> abilities;
    public List<string> ignoredTags;

    [SerializeField] protected Transform pivot;

    public override void Setup(params MonoBehaviour[] args)
    {
        base.Setup(args);

        foreach (var arg in args)
        {
            if (propDamager == null && arg is PropDamager)
            {
                propDamager = (PropDamager)arg;
            }

        }

        CreateAbilities();

        foreach (AbilityLogicBase ability in abilities)
        {
            if (ability is AbilityAttackBase)
            {
                ability.Setup(propDamager, abilitiesPool);
                var attackAbility = (AbilityAttackBase)ability;
                attackAbility.SetupPivot(pivot);
            }
        }
    }

    public override void Init(Transform owner)
    {
        base.Init(owner);

        foreach (AbilityLogicBase ability in abilities)
        {
            if(ability)
                ability.Init();
        }
    }

    public override void Terminate()
    {
        foreach (AbilityLogicBase ability in abilities)
        {
            if(ability)
                ability.Terminate();
        }

        base.Terminate();
    }

    public virtual void StartUse(int idx)
    {
        if (idx < abilities.Count && abilities[idx] != null)
            StartUseInternal(idx);
    }

    public virtual void StopUse(int idx)
    {
        if (idx < abilities.Count && abilities[idx] != null)
            StopUseInternal(idx);
    }

    public List<AbilityLogicBase> GetAbilities()
    {
        return abilities;
    }

    public PropDamager GetPropDamager()
    {
        return propDamager;
    }

    #region private

    protected PropDamager propDamager;
    protected AbilitiesPool abilitiesPool;

    protected virtual void CreateAbilities() {}

    protected void StartUseInternal(int idx)
    {
        abilities[idx].TryStartUse();
    }

    protected void StopUseInternal(int idx)
    {
        abilities[idx].StopUse();
    }

    #endregion
}

public enum AbilitySlot
{
    SLOT_1,
    SLOT_2,
    MAX_COUNT
}

