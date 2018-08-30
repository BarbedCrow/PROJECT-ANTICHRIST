using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShotGun : WeaponRange
{
    public float angleShots;
    public int BulletsOnOneShot;

    public override void Init()
    {
        base.Init();

        bulletDmg = damage / BulletsOnOneShot;
    }

    protected override void Shoot()
    {
        pauseShotsTimer.StartWork();
        countOfBullets -= 1;
        Debug.Log(countOfBullets);
        var transformBullet = projectileSpawnInfo.projectileSpawnPoint;

        anglesForShots.Clear();
        var angleInterval = (BulletsOnOneShot % 2 == 1) ? angleShots / (BulletsOnOneShot - 1) : angleShots / BulletsOnOneShot;

        if (BulletsOnOneShot % 2 == 1)
        {
            anglesForShots.Add(transformBullet.rotation);
            for (int i = 0; i < BulletsOnOneShot / 2; i++)
            {
                anglesForShots.Add(transformBullet.rotation * Quaternion.Euler(0, (i + 1) * angleInterval, 0));
                anglesForShots.Add(transformBullet.rotation * Quaternion.Euler(0, (i + 1) * -angleInterval, 0));
            }
        }
        else
        {
            for (int i = 0; i < BulletsOnOneShot / 2; i++)
            {
                anglesForShots.Add(transformBullet.rotation * Quaternion.Euler(0, (i + 0.5f) * angleInterval, 0));
                anglesForShots.Add(transformBullet.rotation * Quaternion.Euler(0, (i + 0.5f) * -angleInterval, 0));
            }
        }

        for (int i = 0; i < BulletsOnOneShot; i++)
        {
            var projectile = Instantiate(projectileSpawnInfo.projectilePrefab, projectileSpawnInfo.projectileSpawnPoint.position, anglesForShots[i]);
            var projectileLogic = projectile.GetComponent<ProjectileBase>();
            projectileLogic.Init(propDamager, bulletDmg);
        }
    }

    #region private

    List<Quaternion> anglesForShots = new List<Quaternion>();
    float bulletDmg;

    #endregion
}
