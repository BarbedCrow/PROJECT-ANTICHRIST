using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    [HideInInspector]
    public EventOnDie OnDie = new EventOnDie();

    [SerializeField] protected PropAnimMovementController propAnimMovementController;
    [SerializeField] protected PropMovement propMovement;
    [SerializeField] protected PropWeaponUserMelee propWeaponUserMelee;
    [SerializeField] protected PropWeaponUserRange propWeaponUserRange;
    [SerializeField] protected PropAbilityUser propAbilityUser;
    [SerializeField] protected PropAnimPlayerBodyController propAnimBodyController;

    public PropWeaponUserRange GetWeaponUserRange()
    {
        return propWeaponUserRange;
    }

    public PropWeaponUserMelee GetWeaponUserMelee()
    {
        return propWeaponUserMelee;
    }

    public PropAbilityUser GetAbilityUser()
    {
        return propAbilityUser;
    }

    public virtual void Setup(params MonoBehaviour[] args)
    {
        SetupComponents();
    }

    public virtual void Init()
    {
        InitComponents();
    }

    public virtual void Terminate()
    {
        TerminateComponents();
        if(gameObject)
            Destroy(gameObject);
    }

    public virtual void Enable()
    {
        propWeaponUserMelee.Enable();
        propWeaponUserRange.Enable();
        propAbilityUser.Enable();

        propMovement.Enable();
    }

    public virtual void Disable()
    {
        propWeaponUserMelee.Disable();
        propWeaponUserRange.Disable();
        propAbilityUser.Disable();

        propMovement.Disable();
    }

    public PropDamagable GetDamagable()
    {
        return damagable;
    }

    public PropDamager GetDamager()
    {
        return damager;
    }

    #region private

    protected PropDamagable damagable;
    protected PropDamager damager;

    protected virtual void SetupComponents()
    {
        damager = gameObject.AddComponent<PropDamager>();

        propWeaponUserMelee.Setup(damager);
        propWeaponUserRange.Setup(damager);
        propAbilityUser.Setup(damager);
    }

    protected virtual void InitComponents()
    {
        damagable = GetComponent<PropDamagable>();
        damagable.OnDie.AddListener(Die);
        damagable.Init(transform);

        propMovement.Init(transform);

        propWeaponUserMelee.Init(transform);
        propWeaponUserRange.Init(transform);
        propAbilityUser.Init(transform);

        propAnimMovementController?.Init(transform);
        propAnimBodyController?.Init(transform);
    }

    protected virtual void TerminateComponents()
    {
        damagable.OnDie.RemoveListener(Die);
        propAbilityUser.Terminate();

        propWeaponUserMelee.Terminate();
        propWeaponUserRange.Terminate();
        propMovement.Terminate();

        propAnimMovementController?.Terminate();
        propAnimBodyController?.Terminate();
    }

    protected virtual void Die(DamageInfo info)
    {
        OnDie.Invoke(info);
    }

    #endregion

}
