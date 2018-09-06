using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDamageDiller : MonoBehaviour
{
    public float damage;

    public virtual void Init(DamagerBase propDamager)
    {
        partSys = GetComponent<ParticleSystem>();
        this.propDamager = propDamager;
    }

    public void Terminate()
    {
        propDamager.Terminate();
    }

    #region private
    protected ParticleSystem partSys;
    DamagerBase propDamager;

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Damagable")
        {
            Debug.Log(other.ToString());
            var propDamagable = other.gameObject.GetComponent<DamagableBase>();
            if (propDamagable != null)
            {
                DoDamage(propDamagable);
            }
        }
    }

    void DoDamage(DamagableBase propDamagable)
    {
        var damageInfo = new DamageInfo();
        damageInfo.damagable = propDamagable;
        damageInfo.damager = propDamager;
        damageInfo.damage = damage;
        propDamager.DoDamage(damageInfo);
    }

    #endregion
}
