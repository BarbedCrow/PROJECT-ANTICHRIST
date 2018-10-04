using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIHealthController : UIBaseController
{
    public void Init()
    {
        damagablePlayer = GameObject.FindWithTag(Tags.PLAYER).GetComponent<PropDamagable>();
        hpView.Init(new UIHealthViewData(damagablePlayer.GetHealth()));
    }

    #region private

    PropDamagable damagablePlayer;
    UIHealthView hpView;

    void Terminate()
    {

    }

    #endregion

}
