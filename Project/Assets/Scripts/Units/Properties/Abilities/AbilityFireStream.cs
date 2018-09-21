using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamagerBase))]
public class AbilityFireStream : AbilityAttack
{
    
    public void StopSfx(GameObject explosion)
    {
        var partSys = explosion.GetComponentInChildren<ParticleSystem>();
        var time = partSys.main.duration + partSys.main.startLifetimeMultiplier;
        Destroy(explosion, time);
    }


    #region private

    protected AbilityDamageDealer dmgDlr;

    protected override void Activate()
    {
        base.Activate();
        if (particlesPrefab)
        {
            var fireFX = (GameObject)Instantiate(particlesPrefab, transform.position, transform.rotation);
            dmgDlr = fireFX.GetComponent<AbilityDamageDealer>();
            dmgDlr.Init(propDamager, damage, ignoredTags);
            fireFX.transform.SetParent(transform);

            StopSfx(fireFX);
        }
    }

    #endregion
}
