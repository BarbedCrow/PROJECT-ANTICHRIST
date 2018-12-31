using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider))]

public class SpriteAbilityAttackBase : MonoBehaviour
{
    [SerializeField] float lifetime;

    public void Init(AbilitiesPool pool)
    {
        this.pool = pool;

        var tryGetAnim = GetComponent<Animator>();
        if (tryGetAnim)
            animator = tryGetAnim;

        gameObject.SetActive(false);
    }

    public void Enable(Transform initTransfrom, float damage, float speed, PropDamager damager, List<string> ignoredTags)
    {
        this.ignoredTags = ignoredTags;
        var newRot = Quaternion.Euler(initTransfrom.rotation.eulerAngles.x, initTransfrom.rotation.eulerAngles.y + 90, initTransfrom.rotation.eulerAngles.z);

        transform.SetPositionAndRotation(initTransfrom.position, newRot);

        gameObject.SetActive(true);

        this.damage = damage;
        this.speed = speed;
        this.damager = damager;

        Invoke("Disable", lifetime);
        StartCoroutine(COROUTINE_MOVE);
    }

    public void Terminate()
    {
        Destroy(gameObject);
    }

    public void Disable()
    {
        speed = 0;
        damage = 0;

        StopCoroutine(COROUTINE_MOVE);
        pool.Release(gameObject);
    }

    #region private

    AbilitiesPool pool;
    protected PropDamager damager;
    protected float damage;
    float speed;
    Timer timer;
    List<string> ignoredTags;
    Vector3 velocity;
    Animator animator;

    const string COROUTINE_MOVE = "Move";

    IEnumerator Move()
    {
        for (; ;)
        {
            transform.position = transform.position + transform.up * speed * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!ignoredTags.Contains(other.tag))
        {
            var damagable = other.gameObject.GetComponent<PropDamagable>();
            if (damagable != null)
            {
                var damageInfo = new DamageInfo();
                damageInfo.damagable = damagable;
                damageInfo.damager = damager;
                damageInfo.damage = damage;
                damager.DoDamage(damageInfo);
            }
            
            Disable();
        }
    }

    #endregion

}
