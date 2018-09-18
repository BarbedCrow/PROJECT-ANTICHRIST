using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionDetector))]
[RequireComponent(typeof(Timer))]
[RequireComponent(typeof(DamagableBase))]
public class ProjectileBase : MonoBehaviour {

    public float lifetime = 3;

    public virtual void Init(DamagerBase propDamager, PoolBase pool, float damage, float speed)
    {
        this.speed = speed; 
        this.pool = pool;
        timer = GetComponent<Timer>();
        timer.Init(lifetime);
        timer.StartWork();
        timer.OnTimersFinished.AddListener(() => pool.Release(gameObject));
        
        this.damage = damage;
        velocity = transform.forward;
        this.propDamager = propDamager;
        collisionDetector = GetComponent<CollisionDetector>();
        SubscribeOnCollisionDetector();
        StartCoroutine(COROUTINE_MOVE);
    }

    public void Terminate()
    {
        UnsubscribeFromCollisionDetector();
        propDamager.Terminate();

        Destroy(gameObject);
    }

    #region private

    const string COROUTINE_MOVE = "Move";

    float speed = 15;
    PoolBase pool;
    Timer timer;
    float damage;
    Vector3 velocity;
    DamagerBase propDamager;
    CollisionDetector collisionDetector;

    void SubscribeOnCollisionDetector()
    {
        collisionDetector.OnCollideWith.AddListener(HandleOnCollideWith);
    }

    void UnsubscribeFromCollisionDetector()
    {
        collisionDetector.OnCollideWith.RemoveListener(HandleOnCollideWith);
    }

    void HandleOnCollideWith(Collision collision)
    {
        var propDamagable = collision.gameObject.GetComponent<DamagableBase>();
        if (propDamagable != null)
        {
            DoDamage(collision, propDamagable);
        }

        pool.Release(gameObject);
    }

    void DoDamage(Collision collision, DamagableBase propDamagable)
    {
        UnsubscribeFromCollisionDetector();
        var damageInfo = new DamageInfo();
        damageInfo.damagable = propDamagable;
        damageInfo.damager = propDamager;
        damageInfo.damage = damage;
        propDamager.DoDamage(damageInfo);
    }

    IEnumerator Move()
    {
        for(;;)
        {
            transform.position = transform.position + velocity * speed * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    #endregion
}
