using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUserMelee : WeaponUserBase
{

    public WeaponSpawnDesc weaponDesc;

    public override void Init()
    {
        base.Init();

        if (weaponDesc.prefab == null)
        {
            return;
        }

        var weaponObj = Instantiate(weaponDesc.prefab, weaponDesc.transform.position, weaponDesc.transform.rotation);
        
        weapon = weaponObj.GetComponent<WeaponMelee>();
        weapon.AddIgnoredTags(ignoredTags);
        weapon.Init();
        weapon.transform.SetParent(weaponDesc.transform);
    }

    public override void Terminate()
    {
        weapon.Terminate();

        base.Terminate();
    }

    public override void RequestAttack()
    {
        base.RequestAttack();

        weapon.Attack();
    }

    #region private

    WeaponMelee weapon;

    #endregion

}
