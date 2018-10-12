using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIBulletsController : UIBaseController
{
    public override void Init()
    {
        base.Init();
        var weaponUserRange = (PropWeaponUserRangePlayer)GameObject.FindWithTag(Tags.PLAYER).GetComponent<Player>().GetWeaponUserRange();
        weapon = (WeaponRange)weaponUserRange.GetWeapon();
        data.currentBulletsInClip = weapon.GetCurrClipBullets();
        data.currentAllBullets = weapon.GetCurrBullets();
        uiView.Init(data);
        
        weaponUserRange.OnSwapWeapon.AddListener(() => SwapWeapon(weaponUserRange));
        SubscribeOnWeapons(weaponUserRange);
    }

    #region private

    UIBulletsViewData data = new UIBulletsViewData();
    WeaponRange weapon;

    void SubscribeOnWeapons(PropWeaponUserRangePlayer userRangePlayer)
    {
        foreach (WeaponDesc desc in userRangePlayer.weaponDescs)
        {
            var weaponDesc = (WeaponRange)desc.weapon;
            weaponDesc.OnShoot.AddListener(UpdateUI);
            weaponDesc.OnReloadStopped.AddListener(UpdateUI);
        }
    }

    void SwapWeapon(PropWeaponUserRangePlayer userRangePlayer)
    {
        weapon = (WeaponRange)userRangePlayer.GetWeapon();
        UpdateUI();
    }

    void UpdateUI()
    {
        data.currentBulletsInClip = weapon.GetCurrClipBullets();
        data.currentAllBullets = weapon.GetCurrBullets();
        uiView.UpdateUI(data);
    }

    #endregion

}
