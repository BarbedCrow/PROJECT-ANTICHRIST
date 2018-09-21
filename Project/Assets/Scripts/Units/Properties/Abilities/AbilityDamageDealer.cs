using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDamageDealer : MonoBehaviour
{

    public virtual void Init(DamagerBase propDamager, float damage, List<string> ignoredTags)
    {
        this.damage = damage;
        this.propDamager = propDamager;
        this.ignoredTags = ignoredTags;
        propDamager.Init();
        partSys = GetComponent<ParticleSystem>();
    }

    public void Terminate()
    {
        propDamager.Terminate();
    }

    #region private

    List<string> ignoredTags;
    float damage;
    ParticleSystem partSys;
    DamagerBase propDamager;

    void OnParticleCollision(GameObject other)
    {
        if (ignoredTags != null && ignoredTags.Contains(other.tag))
        {
            return;
        }

        Debug.Log(other.ToString());
        var propDamagable = other.gameObject.GetComponent<DamagableBase>();
        if (propDamagable != null)
        {
            DoDamage(propDamagable);
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
