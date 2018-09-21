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
        
        folderForText = GameObject.FindWithTag("HPText");

        var anglesForHPText = Quaternion.Euler(50, -90, 0);
        var gO = new GameObject("hpText");
        hpText = gO.AddComponent<TextMesh>();
        hpText.transform.SetParent(folderForText.transform);
        hpText.anchor = TextAnchor.UpperCenter;
        hpText.transform.SetPositionAndRotation(transform.position, anglesForHPText);
        hpText.text = currentHp.ToString();
    }	

    public void Terminate()
    {

    }

    public void GetDamage(DamageInfo info)
    {
        Debug.Log("Do damage " + info.damage + " to " + transform.gameObject);
        currentHp -= info.damage;

        if (hpText)
        {
            hpText.text = currentHp.ToString();
        }

        if (currentHp <= 0)
        {
            Die(info);
        }

    }

    void Update()
    {
        var newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z) + hpOffset;
        hpText.transform.position = newPos;
    }

    public TextMesh GetHPText()
    {
        return hpText;
    }

    #region private

    GameObject folderForText;
    Vector3 hpOffset = new Vector3(-2, 2, 0);
    TextMesh hpText;
    float currentHp;

    void Die(DamageInfo info)
    {
        Destroy(hpText.gameObject);
        OnDie.Invoke(info);
    }

    #endregion
}
