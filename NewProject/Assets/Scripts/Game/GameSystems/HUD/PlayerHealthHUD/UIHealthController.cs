using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIHealthController : UIBaseController
{
    public override void Init()
    {
        base.Init();
        damagablePlayer = GameObject.FindWithTag(Tags.PLAYER).GetComponent<PropDamagable>();
        UIHealthViewData data = new UIHealthViewData();
        data.Init(damagablePlayer.GetHealth());
        uiView.Init(data);
        damagablePlayer.OnGotDamage.AddListener(InitialUpdate);
    }

    #region private

    PropDamagable damagablePlayer;

    void InitialUpdate(DamageInfo damageInfo)
    {
        UIHealthViewData data = new UIHealthViewData();
        data.Init(damagablePlayer.GetHealth());
        uiView.UpdateUI(data);
    }

    #endregion

}
