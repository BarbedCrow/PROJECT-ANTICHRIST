using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnDie : UnityEvent<DamageInfo>
{
    
}

public class DamagableBase : MonoBehaviour
{
    public TextMesh text;

    public EventOnDie OnDie = new EventOnDie();
    
    public float health;

    public void Init()
    {
        currentHp = health;

        if (text)
        {
            text.text = currentHp.ToString();
        }
    }	

    public void Terminate()
    {

    }

    public void GetDamage(DamageInfo info)
    {
        Debug.Log("Do damage " + info.damage + " to " + transform.gameObject);
        currentHp -= info.damage;

        if (text)
        {
            text.text = currentHp.ToString();
        }

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
