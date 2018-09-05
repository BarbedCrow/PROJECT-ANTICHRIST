using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
[RequireComponent(typeof(Timer))]
public class WeaponRange : WeaponBase
{
    public float timeBetweenShots;
    public float timeForReload;

    [HideInInspector]
    public UnityEvent OnShootEmpty = new UnityEvent();

    public int maxBullets;
    public ProjectileSpawnInfo projectileSpawnInfo;

    public virtual void RequestAttack()
    {
        isShooting = true;

        if (isReloading)
        {
            return;
        }

        if (pauseShotsTimer.IsRunning())
        {
            return;
        }

        if (countOfBullets <= NO_BULLETS)
        {
            OnShootEmpty.Invoke();
            return;
        }

        Shoot();
    }

    public virtual void RequestStopAttack()
    {
        isShooting = false;
    }

    public override void Init()
    {
        base.Init();

        countOfBullets = maxBullets;

        var timers = GetComponents<Timer>();
        pauseShotsTimer = timers[0];
        reloadTimer = timers[1];

        pauseShotsTimer.Init(timeBetweenShots);
        reloadTimer.Init(timeForReload);

        Disable();
    }

    public override void Terminate()
    {
        reloadTimer.OnTimersFinished.RemoveListener(HandleOnReloaded);
        base.Terminate();
    }

    public void Reload()
    {
        isReloading = true;
        Debug.Log("I am reloading");
        reloadTimer.StartWork();
        reloadTimer.OnTimersFinished.AddListener(HandleOnReloaded);
    }

    public virtual void Enable()
    {
        gameObject.SetActive(true);
    }

    public virtual void Disable()
    {
        isReloading = false;
        reloadTimer.OnTimersFinished.RemoveListener(HandleOnReloaded);
        reloadTimer.StopWork();
        pauseShotsTimer.StopWork();
        isShooting = false;
        gameObject.SetActive(false);
    }

    #region private

    protected const int NO_BULLETS = 0;

    protected bool isShooting = false;
    protected bool isReloading = false;

    protected int countOfBullets;
    Timer reloadTimer;
    protected Timer pauseShotsTimer;

    protected virtual void Shoot()
    {
        pauseShotsTimer.StartWork();
        countOfBullets -= 1;
        Debug.Log(countOfBullets);

        var projectile = Instantiate(projectileSpawnInfo.projectilePrefab, projectileSpawnInfo.projectileSpawnPoint.position, projectileSpawnInfo.projectileSpawnPoint.rotation);
        var projectileLogic = projectile.GetComponent<ProjectileBase>();
        projectileLogic.Init(propDamager, damage);
    }

    protected virtual void HandleOnReloaded()
    {
        isReloading = false;
        reloadTimer.OnTimersFinished.RemoveListener(HandleOnReloaded);
        Debug.Log("I reloaded");
        countOfBullets = maxBullets;
    }

    #endregion
}


[System.Serializable]
public class ProjectileSpawnInfo
{
    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;
}
