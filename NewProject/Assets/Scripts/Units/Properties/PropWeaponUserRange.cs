using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropWeaponUserRange : PropWeaponUserBase
{

    public override void Setup(params MonoBehaviour[] args)
    {
        base.Setup(args);

        PoolBase projectilesPool = null;
        foreach (MonoBehaviour arg in args)
        {
            if (projectilesPool == null && arg is PoolBase)
            {
                projectilesPool = (PoolBase)arg;
            }
        }

        foreach (WeaponDesc weaponDesc in weaponDescs)
        {
            var rangeWeapon = (WeaponRange)weaponDesc.weapon;
            rangeWeapon.Setup(projectilesPool);
        }
    }

    public override void Init(Transform owner)
    {
        base.Init(owner);

        foreach(var weaponDesc in weaponDescs)
        {
            var range = (WeaponRange)weaponDesc.weapon;
            range.OnShootEmpty.AddListener(RequestReload);
        }
    }

    public override void Terminate()
    {
        foreach (var weaponDesc in weaponDescs)
        {
            var range = (WeaponRange)weaponDesc.weapon;
            range.OnShootEmpty.RemoveListener(RequestReload);
        }

        base.Terminate();
    }

    public WeaponBase GetWeapon()
    {
        return currentWeapon;
    }

    #region private

    protected override void SwapWeapon(SlotType slot)
    {
        int idx;
        if (currentSlot != slot)
        {
            if (slot == SlotType.SLOT_1 && weaponDescs.Count > 0)
                idx = 0;
            else if (slot == SlotType.SLOT_2 && weaponDescs.Count > 1)
                idx = 1;
            else if (slot == SlotType.SLOT_3 && weaponDescs.Count > 2)
                idx = 2;
            else return;
        }
        else return;

        Debug.Log(idx);
        currentWeapon.Disable();
        currentWeapon = weaponDescs[idx].weapon;
        currentSlot = weaponDescs[idx].slot;
        currentWeapon.Enable();
    }

    protected void RequestReload()
    {
        var rangeWeapon = (WeaponRange)currentWeapon;
        rangeWeapon.TryReload();
    }

    #endregion

}
