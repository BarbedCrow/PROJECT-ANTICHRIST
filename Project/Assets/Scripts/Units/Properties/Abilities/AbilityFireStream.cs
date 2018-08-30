using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class AbilityFireStream : AbilityBase
{
    public GameObject explosionParticlesPrefab;

    public override void Attack()
    {
        if (explosionParticlesPrefab)
        {
            var explosion = (GameObject)Instantiate(explosionParticlesPrefab, spawnPoint.position, spawnPoint.rotation);
            Terminate(explosion);

        }
    }

    public void Terminate(GameObject explosion)
    {
        Destroy(explosion, explosion.GetComponentInChildren<ParticleSystem>().main.startLifetimeMultiplier);
    }

}
