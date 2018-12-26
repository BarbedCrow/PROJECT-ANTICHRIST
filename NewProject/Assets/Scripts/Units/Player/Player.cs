using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : UnitBase
{
    public override void Setup(params MonoBehaviour[] args)
    {
        foreach(MonoBehaviour arg in args)
        {
            if(inputsLibrary == null && arg is InputsLibrary)
            {
                inputsLibrary = (InputsLibrary)arg;
            }

            if (projectilesPool == null && arg is PoolBase)
            {
                projectilesPool = (PoolBase)arg;
            }

            if (abilitiesLibrary == null && arg is AbilitiesLibrary)
            {
                abilitiesLibrary = (AbilitiesLibrary)arg;
            }
        }
        
        base.Setup(inputsLibrary, abilitiesLibrary);
    }

    #region private

    InputsLibrary inputsLibrary;
    AbilitiesLibrary abilitiesLibrary;
    PoolBase projectilesPool;

    protected override void SetupComponents()
    {
        propAbilityUser.Setup(inputsLibrary, abilitiesLibrary);

        propWeaponUserMelee.Setup(inputsLibrary, projectilesPool);
        propWeaponUserRange.Setup(inputsLibrary, projectilesPool);

        base.SetupComponents();
    }

    protected override void InitComponents()
    {
        base.InitComponents();

        var propWeaponUserRangePlayer = (PropWeaponUserRangePlayer)propWeaponUserRange;
        var propWeaponUserMeleePlayer = (PropWeaponUserMeleePlayer)propWeaponUserMelee;
        var propAbilityUserPlayer = (PropAbilityUserPlayer)propAbilityUser;

        propWeaponUserRangePlayer.OnPlayerRangeAttackStart.AddListener(RangeAttackStart);
        propWeaponUserRangePlayer.OnPlayerRangeAttackStop.AddListener(RangeAttackStop);

        propWeaponUserMeleePlayer.OnPlayerMeleeAttackStart.AddListener(MeleeAttackStart);
        propWeaponUserMeleePlayer.OnPlayerMeleeAttackStop.AddListener(MeleeAttackStop);

        propAbilityUserPlayer.OnPlayerAbilityUseStart.AddListener(AbilityUseStart);
        propAbilityUserPlayer.OnPlayerAbilityUseStop.AddListener(AbilityUseStop);
    }

    protected override void TerminateComponents()
    {
        base.TerminateComponents();
    }

    protected void RangeAttackStart()
    {
        propAbilityUser.Disable();
        propWeaponUserMelee.Disable();
    }

    protected void RangeAttackStop()
    {
        propAbilityUser.Enable();
        propWeaponUserMelee.Enable();
    }

    protected void MeleeAttackStart()
    {
        propAbilityUser.Disable();
        propWeaponUserRange.Disable();
    }

    protected void MeleeAttackStop()
    {
        propAbilityUser.Enable();
        propWeaponUserRange.Enable();
    }

    protected void AbilityUseStart()
    {
        propWeaponUserRange.Disable();
        propWeaponUserMelee.Disable();
    }

    protected void AbilityUseStop()
    {
        propWeaponUserRange.Enable();
        propWeaponUserMelee.Enable();
    }

    #endregion

}

[System.Serializable]
public class PlayerSpawnInfo
{
    public Player player;
    public Transform spawnTransform;
}
