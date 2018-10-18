using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    [HideInInspector]
    public EventOnDie OnDie = new EventOnDie();

    [SerializeField] protected PropAnimMovementController propAnimMovementController;
    [SerializeField] protected PropMovement propMovement;
    [SerializeField] protected List<PropWeaponUserBase> propWeaponUsers;
    [SerializeField] protected PropAbilityUser propAbilityUser;

    public PropWeaponUserRange GetWeaponUserRange()
    {
        return (PropWeaponUserRange)propWeaponUsers[0];
    }

    public PropWeaponUserMelee GetWeaponUserMelee()
    {
        return (PropWeaponUserMelee)propWeaponUsers[1];
    }

    public PropAbilityUser GetAbilityUser()
    {
        return propAbilityUser;
    }

    public virtual void Setup(params MonoBehaviour[] args)
    {

    }

    public virtual void Init()
    {
        SetupComponents();
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

    }

    public virtual void Disable()
    {

    }

    public PropDamagable GetDamagable()
    {
        return damagable;
    }

    #region private

    protected PropDamagable damagable;

    protected virtual void SetupComponents()
    {

    }

    protected virtual void InitComponents()
    {
        damagable = GetComponent<PropDamagable>();
        damagable.OnDie.AddListener(Die);
        damagable.Init(transform);

        propMovement.Init(transform);
        foreach(PropWeaponUserBase user in propWeaponUsers)
        {
            user.Init(transform);
        }

        propAbilityUser.Init(transform);

        propAnimMovementController?.Init(transform);
    }

    protected virtual void TerminateComponents()
    {
        damagable.OnDie.RemoveListener(Die);
        propAnimMovementController?.Terminate();
        propAbilityUser.Terminate();
        foreach (PropWeaponUserBase user in propWeaponUsers)
        {
            user.Terminate();
        }

        propMovement.Terminate();
    }

    protected virtual void Die(DamageInfo info)
    {
        OnDie.Invoke(info);
    }

    #endregion

}
