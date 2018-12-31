using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityFireFirst : AbilityAttackBase
{
    public override void StopUse()
    {
        base.StopUse();
    }

    #region private

    protected override void StartUse()
    {
        base.StartUse();

        var fireball = pool.Take(Tags.FIREBALL);
        var fireballLogic = fireball.GetComponent<SpriteAbilityAttackBase>();
        fireballLogic.Enable(pivot, damage, speed, damager, ignoredTags);
    }

    #endregion

}
