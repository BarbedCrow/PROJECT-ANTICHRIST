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
        uiView.Init(damagablePlayer.GetHealth());
        UpdateUI(new DamageInfo());
        
        damagablePlayer.OnGotDamage.AddListener(UpdateUI);
    }

    #region private

    UIHealthViewData data = new UIHealthViewData();
    PropDamagable damagablePlayer;

    void UpdateUI(DamageInfo damageInfo)
    {
        data.healthPoints = damagablePlayer.GetHealth();
        uiView.UpdateUI(data);
    }

    #endregion

}
