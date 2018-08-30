using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagerBase : MonoBehaviour
{
    public void Init()
    {

    }

    public void Terminate()
    {

    }

    public void DoDamage(DamageInfo info)
    {
        info.damagable.GetDamage(info);
    }

    #region private

    #endregion
}
