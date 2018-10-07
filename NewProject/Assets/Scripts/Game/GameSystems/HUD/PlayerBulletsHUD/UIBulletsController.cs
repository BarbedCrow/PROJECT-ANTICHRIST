using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIBulletsController : UIBaseController
{
    public override void Init()
    {
        base.Init();
        var weaponUserRange = GameObject.FindWithTag(Tags.PLAYER).GetComponent<Player>().GetWeaponUserRange();
        weapon = (WeaponRange)weaponUserRange.GetWeapon();
        data.currentBulletsInClip = weapon.GetCurrClipBullets();
        data.currentAllBullets = weapon.GetCurrBullets();
        uiView.Init(data);
        
        weapon.OnAttackStopped.AddListener(UpdateUI);
        weapon.OnReloadStarted.AddListener(UpdateUI);
    }

    #region private

    UIBulletsViewData data = new UIBulletsViewData();
    WeaponRange weapon;

    void UpdateUI()
    {
        data.currentBulletsInClip = weapon.GetCurrClipBullets();
        data.currentAllBullets = weapon.GetCurrBullets();
        uiView.UpdateUI(data);
    }

    #endregion

}
