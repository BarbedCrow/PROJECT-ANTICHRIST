using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyRewardRule : RewardRuleBase
{
    public override void Init()
    {
        base.Init();

        var player = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<Player>();
        player.GetWeaponUserMelee().GetPropDamager().onKillEnemy.AddListener(AddScore);
        player.GetWeaponUserRange().GetPropDamager().onKillEnemy.AddListener(AddScore);
        player.GetAbilityUser().GetPropDamager().onKillEnemy.AddListener(AddScore);
    }

    #region private

    void AddScore(DamageInfo info)
    {
        user.AddScore(100);
        Debug.Log(user.GetScore().ToString());
    }

    #endregion
}
