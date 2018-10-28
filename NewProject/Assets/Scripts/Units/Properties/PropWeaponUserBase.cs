using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropWeaponUserBase : PropBase
{
    
    public List<WeaponBase> weapons;
    public List<string> ignoredTags;

    public override void Setup(params MonoBehaviour[] args)
    {
        base.Setup(args);

        propDamager = gameObject.AddComponent<PropDamager>();
        foreach (WeaponBase weapon in weapons)
        {
            weapon.Setup(propDamager);
        }
    }

    public override void Init(Transform owner)
    {
        base.Init(owner);

        foreach (WeaponBase weapon in weapons)
        {
            weapon.Init();
        }

        currentWeapon = weapons[0];
        currentSlot = 0;
        currentWeapon.Enable();
    }

    public override void Terminate()
    {
        foreach (WeaponBase weapon in weapons)
        {
            weapon.Terminate();
        }

        base.Terminate();
    }

    public List<WeaponBase> GetWeapons()
    {
        return weapons;
    }

    public PropDamager GetPropDamager()
    {
        return propDamager;
    }

    #region private

    protected WeaponBase currentWeapon;
    protected int currentSlot;

    protected PropDamager propDamager;

    protected virtual void RequestStartAttackInternal()
    {
        currentWeapon.RequestStartAttack();
    }

    protected virtual void RequestStopAttackInternal()
    {
        currentWeapon.RequestStopAttack();
    }

    protected virtual void SwapWeapon(int slot)
    {
    }

    #endregion
}


