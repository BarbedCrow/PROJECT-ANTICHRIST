using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamagerBase))]
public class AbilityFireStream : AbilityBase
{
    public override void Init(Transform transform)
    {
        base.Init(transform);
        propDamager = GetComponent<DamagerBase>();
        propDamager.Init();
    }
   
    public override void Attack()
    {
        base.Attack();
        if (particlesPrefab)
        {
            var fireFX = (GameObject)Instantiate(particlesPrefab, spawnPoint.position, spawnPoint.rotation);
            dmgDlr = fireFX.GetComponent<AbilityDamageDiller>();
            dmgDlr.Init(propDamager);
            fireFX.transform.parent = spawnPoint.transform;
            Terminate(fireFX);

        }
    }

    public void Terminate(GameObject explosion)
    {
        var partSys = explosion.GetComponentInChildren<ParticleSystem>();
        var time = partSys.main.duration + partSys.main.startLifetimeMultiplier;
        Destroy(explosion, time);
        base.Terminate();
    }


    #region private

    protected AbilityDamageDiller dmgDlr;
    protected DamagerBase propDamager;

    #endregion
}
