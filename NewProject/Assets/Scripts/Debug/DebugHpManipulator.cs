using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHpManipulator : MonoBehaviour {

    PropDamagable damagable;
    bool isCtrlPressed = false;
    bool isDPressed = false;
    bool isUsed = false;

	void Update ()
    {
		if (damagable == null)
        {
            damagable = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<Player>().GetDamagable();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCtrlPressed = true;
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCtrlPressed = false;
            isUsed = false;
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            isDPressed = true;
        }
        else if(Input.GetKeyUp(KeyCode.D))
        {
            isDPressed = false;
            isUsed = false;
        }

        if(isDPressed && isCtrlPressed && !isUsed)
        {
            isUsed = true;
            var info = new DamageInfo();
            info.damagable = damagable;
            info.damage = 10;
            info.damageType = DamageType.PHYSICAL;
            damagable.GetDamage(info);
        }
    }

    
}
