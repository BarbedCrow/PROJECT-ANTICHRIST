using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{

    [SerializeField] float lifetime = 0.2f;

    public void Init(ProjectilesPool pool)
    {
        this.pool = pool;

        timer = gameObject.AddComponent<Timer>();
        timer.Init(lifetime);
    }

    public void Terminate()
    {
        Destroy(gameObject);
    }

    public void Enable(Transform initTransfrom, float damage, float speed, PropDamager damager)
    {
        this.damage = damage;
        this.speed = speed;
        this.damager = damager;

        timer.OnTimersFinished.AddListener(Disable);
        timer.StartWork();
        transform.SetPositionAndRotation(initTransfrom.position, initTransfrom.rotation);
        StartCoroutine(COROUTINE_MOVE);
    }

    public void Disable()
    {
        speed = 0;
        damage = 0;

        timer.OnTimersFinished.RemoveListener(Disable);
        timer.StopWork();

        StopCoroutine(COROUTINE_MOVE);
        pool.Release(gameObject);
    }

    #region private

    const string COROUTINE_MOVE = "Move";

    ProjectilesPool pool;
    Timer timer;

    float damage;
    float speed;
    PropDamager damager;

    IEnumerator Move()
    {
        for (; ; )
        {
            transform.position = transform.position + transform.forward * speed * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var damagable = other.gameObject.GetComponent<PropDamagable>();
        if(damagable != null)
        {
            var damageInfo = new DamageInfo();
            damageInfo.damagable = damagable;
            damageInfo.damager = damager;
            damageInfo.damage = damage;
            damager.DoDamage(damageInfo);
        }

        Disable();
    }

    #endregion

}
