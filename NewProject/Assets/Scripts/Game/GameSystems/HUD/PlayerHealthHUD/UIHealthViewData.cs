using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIHealthViewData : UIBaseViewData
{
    public void Init(float HP)
    {
        healthPoints = HP;
    }

    public float GetHP()
    {
        return healthPoints;
    }

    #region private

    float healthPoints;

    #endregion

}
