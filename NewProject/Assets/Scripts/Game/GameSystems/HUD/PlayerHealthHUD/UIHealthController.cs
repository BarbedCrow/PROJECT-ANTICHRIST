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
        data.currentHealthPoints = damagablePlayer.GetCurrentHealth();
        data.maxHealthPoints = damagablePlayer.GetMaxHealth();
        uiView.Init(data);
        
        damagablePlayer.OnGotDamage.AddListener(UpdateUI);
    }

    #region private

    UIHealthViewData data = new UIHealthViewData();
    PropDamagable damagablePlayer;

    void UpdateUI(DamageInfo damageInfo)
    {
        data.currentHealthPoints = damagablePlayer.GetCurrentHealth();
        data.maxHealthPoints = damagablePlayer.GetMaxHealth();
        uiView.UpdateUI(data);
    }

    #endregion

}
