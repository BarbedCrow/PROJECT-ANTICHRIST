using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUserMelee : WeaponUserBase
{

    public WeaponMelee weapon;

    public override void Init()
    {
        base.Init();
        if (weapon == null)
        {
            Debug.Log("No Weapon in melee user");
            return;
        }
            weapon.Init();
    }

    public override void Terminate()
    {
        weapon.Terminate();

        base.Terminate();
    }

    #region private

    protected override void RequestAttack()
    {
        base.RequestAttack();

        weapon.Attack();
    }

    #endregion

}
