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

        foreach(WeaponBase weapon in weapons)
        {
            weapon.Init();
        }

        currentWeapon = weapons[0];
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

    #region private

    protected WeaponBase currentWeapon;
    protected PropDamager propDamager;

    protected virtual void RequestStartAttackInternal()
    {
        currentWeapon.RequestStartAttack();
    }

    protected virtual void RequestStopAttackInternal()
    {
        currentWeapon.RequestStopAttack();
    }

    #endregion

}
