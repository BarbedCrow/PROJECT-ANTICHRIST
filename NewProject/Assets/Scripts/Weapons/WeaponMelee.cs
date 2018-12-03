using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponMelee : WeaponBase
{
    [SerializeField] float attackTime;

    public bool IsAttacking()
    {
        return isAttacking;
    }

    public override void Init()
    {
        base.Init();
    }

    #region private

    const string STOP_ATTACK = "RequestStopAttackInternal";
    bool isAttacking = false;

    protected override void RequestStartAttackInternal()
    {
        base.RequestStartAttackInternal();

        OnAttackStared.Invoke();

        isAttacking = true;
        gameObject.SetActive(true);
        
        Invoke(STOP_ATTACK, attackTime);
    }

    protected override void RequestStopAttackInternal()
    {
        base.RequestStopAttackInternal();

        OnAttackStopped.Invoke();

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
