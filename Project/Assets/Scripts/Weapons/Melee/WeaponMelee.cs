using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamagerBase))]
public class WeaponMelee : WeaponBase
{

    public override void Init()
    {
        base.Init();

        collisionDetector = GetComponent<CollisionDetector>();
        collisionDetector.AddIgnoredTags(ignoredTags);

        propDamager = GetComponent<DamagerBase>();
        propDamager.Init();
    }

    public override void Terminate()
    {
        UnsubscribeFromCollisionDetector();
        propDamager.Terminate();

        base.Terminate();
    }

    public void Attack()
    {
        UnsubscribeFromCollisionDetector(); // temp while animations won't be added
        SubscribeOnCollisionDetector();
    }

    #region private

    CollisionDetector collisionDetector;

    void SubscribeOnCollisionDetector()
    {
        collisionDetector.OnCollideWith.AddListener(HandleOnCollideWith);
    }

    void UnsubscribeFromCollisionDetector()
    {
        collisionDetector.OnCollideWith.RemoveListener(HandleOnCollideWith);
    }

    void HandleOnCollideWith(Collider other)
    {
        var propDamagable = other.gameObject.GetComponent<DamagableBase>();
        if (propDamagable != null)
        {
            DoDamage(other, propDamagable);
        }
    }

    void DoDamage(Collider other, DamagableBase propDamagable)
    {
        UnsubscribeFromCollisionDetector();
        var damageInfo = new DamageInfo();
        damageInfo.damagable = propDamagable;
        damageInfo.damager = propDamager;
        damageInfo.damage = damage;
        propDamager.DoDamage(damageInfo);
    }

    #endregion

}
