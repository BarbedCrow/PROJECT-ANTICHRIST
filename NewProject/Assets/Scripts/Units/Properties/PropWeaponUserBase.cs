using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropWeaponUserBase : PropBase
{
    
    public List<WeaponDesc> weaponDescs;
    public List<string> ignoredTags;

    public override void Setup(params MonoBehaviour[] args)
    {
        base.Setup(args);

        propDamager = gameObject.AddComponent<PropDamager>();
        foreach (WeaponDesc weaponDesc in weaponDescs)
        {
            weaponDesc.weapon.Setup(propDamager);
        }
    }

    public override void Init(Transform owner)
    {
        base.Init(owner);

        foreach (WeaponDesc weaponDesc in weaponDescs)
        {
            weaponDesc.weapon.Init();
        }

        currentWeapon = weaponDescs[0].weapon;
        currentSlot = weaponDescs[0].slot;
        currentWeapon.Enable();
    }

    public override void Terminate()
    {
        foreach (WeaponDesc weaponDesc in weaponDescs)
        {
            weaponDesc.weapon.Terminate();
        }

        base.Terminate();
    }

    #region private

    protected WeaponBase currentWeapon;
    protected SlotType currentSlot;

    protected PropDamager propDamager;

    protected virtual void RequestStartAttackInternal()
    {
        currentWeapon.RequestStartAttack();
    }

    protected virtual void RequestStopAttackInternal()
    {
        currentWeapon.RequestStopAttack();
    }

    protected virtual void SwapWeapon(SlotType slot)
    {
    }

    #endregion
}

[System.Serializable]
public class WeaponDesc
{
    public SlotType slot;
    public WeaponBase weapon;
}

[System.Serializable]
public enum SlotType
{
    SLOT_1,
    SLOT_2,
    SLOT_3
}


