using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnDie : UnityEvent<DamageInfo>
{
}

public class DamagableBase : MonoBehaviour
{

    public EventOnDie OnDie = new EventOnDie();

    public float health;

    public void Init()
    {
        currentHp = health;
    }	

    public void Terminate()
    {

    }

    public void GetDamage(DamageInfo info)
    {
        Debug.Log("Do damage" + info.damage + "to " + transform.gameObject);
        currentHp -= info.damage;
        if (currentHp <= 0)
        {
            Die(info);
        }
    }

    #region private

    float currentHp;

    void Die(DamageInfo info)
    {
        OnDie.Invoke(info);
    }

    #endregion
}
