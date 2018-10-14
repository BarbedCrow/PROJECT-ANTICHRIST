using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMelee : WeaponBase
{

    [SerializeField] float attackTime = 0.33f;

    public bool IsAttacking()
    {
        return isAttacking;
    }

    #region private

    const string STOP_ATTACK = "RequestStopAttackInternal";

    bool isAttacking = false;

    protected override void RequestStartAttackInternal()
    {
        base.RequestStartAttackInternal();

        isAttacking = true;
        gameObject.SetActive(true);
        Invoke(STOP_ATTACK, attackTime);
    }

    protected override void RequestStopAttackInternal()
    {
        base.RequestStopAttackInternal();

        isAttacking = false;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var damagable = other.gameObject.GetComponent<PropDamagable>();
        if (damagable != null)
        {
            var damageInfo = new DamageInfo();
            damageInfo.damagable = damagable;
            damageInfo.damager = damager;
            damageInfo.damage = damage;
            damager.DoDamage(damageInfo);

            RequestStopAttackInternal();
        }
    }

    #endregion

}
