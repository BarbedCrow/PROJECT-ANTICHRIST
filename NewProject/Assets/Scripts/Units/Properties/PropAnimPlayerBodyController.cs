using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropAnimPlayerBodyController : PropAnimController
{

    public override void Init(Transform owner)
    {
        base.Init(owner);

        userRange = owner.GetComponent<UnitBase>().GetWeaponUserRange();
        weapon = (WeaponRange)userRange.GetWeapon();
        weapon.OnAttackStared.AddListener(StartShoot);
        weapon.OnAttackStopped.AddListener(StopShoot);

        userMelee = owner.GetComponent<UnitBase>().GetWeaponUserMelee();
        userMelee.OnStartAttack.AddListener(StartMeleeAttack);
    }

    public override void Terminate()
    {
        weapon?.OnAttackStared.RemoveListener(StartShoot);
        weapon?.OnAttackStopped.RemoveListener(StopShoot);

        userMelee?.OnStartAttack.RemoveListener(StartMeleeAttack);

        base.Terminate();
    }

    #region private

    const string IS_SHOOTING = "isShooting";
    const string IS_MELEE = "isMelee";

    const float DELAY_BEFORE_STOP_MELEE_SEC = 0.5f;

    PropWeaponUserRange userRange;
    PropWeaponUserMelee userMelee;
    WeaponRange weapon;

    void StartShoot()
    {
        animator.SetBool(IS_SHOOTING, true);
    }

    void StartMeleeAttack()
    {
        animator.SetBool(IS_MELEE, true);
        Invoke("StopMeleeAttack", DELAY_BEFORE_STOP_MELEE_SEC);
    }

    void StopMeleeAttack()
    {
        animator.SetBool(IS_MELEE, false);
    }

    void StopShoot()
    {
        animator.SetBool(IS_SHOOTING, false);
    }

    #endregion

}
