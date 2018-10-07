using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{

    public EventOnDie OnDie = new EventOnDie();

    public PropMovement propMovement;
    public List<PropWeaponUserBase> propWeaponUsers;

    public PropWeaponUserRange GetWeaponUserRange()
    {
        return (PropWeaponUserRange)propWeaponUsers[0];
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
        damagable.Init(transform);

        propMovement.Init(transform);
        foreach(PropWeaponUserBase user in propWeaponUsers)
        {
            user.Init(transform);
        }
    }

    protected virtual void TerminateComponents()
    {
        foreach (PropWeaponUserBase user in propWeaponUsers)
        {
            user.Terminate();
        }

        propMovement.Terminate();
    }

    #endregion

}
