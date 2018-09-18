using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShotGun : WeaponRange
{
    public float angleShots;
    public int BulletsOnOneShot;

    public override void Init(PoolBase pool)
    {
        base.Init(pool);

        bulletDmg = damage / BulletsOnOneShot;
    }

    protected override void Shoot()
    {
        pauseShotsTimer.StartWork();
        countOfBullets -= 1;
        Debug.Log("Bullets: " + countOfBullets);
        var transformBullet = projectileSpawnInfo.projectileSpawnPoint;

        bool bulletsIsOdd = BulletsOnOneShot % 2 == 1;

        anglesForShots.Clear();
        var angleInterval = (bulletsIsOdd) ? angleShots / (BulletsOnOneShot - 1) : angleShots / BulletsOnOneShot;
        var multiplier = (bulletsIsOdd) ? ALL_SECTOR : HALF_SECTOR;

        if (bulletsIsOdd)
            anglesForShots.Add(transformBullet.rotation);

        for (int i = 0; i < BulletsOnOneShot / 2; i++)
        {
            anglesForShots.Add(transformBullet.rotation * Quaternion.Euler(0, (i + multiplier) * angleInterval, 0));
            anglesForShots.Add(transformBullet.rotation * Quaternion.Euler(0, (i + multiplier) * -angleInterval, 0));
        }

        for (int i = 0; i < BulletsOnOneShot; i++)
        {
            var projectile = pool.Take(Tags.BULLET);
            projectile.transform.SetPositionAndRotation(projectileSpawnInfo.projectileSpawnPoint.position, anglesForShots[i]);

            var projectileLogic = projectile.GetComponent<ProjectileBase>();
            projectileLogic.Init(propDamager, pool, bulletDmg, projectileSpeed);
        }
    }

    #region private

    List<Quaternion> anglesForShots = new List<Quaternion>();
    float bulletDmg;

    const float ALL_SECTOR = 1;
    const float HALF_SECTOR = 0.5f;

    #endregion
}
