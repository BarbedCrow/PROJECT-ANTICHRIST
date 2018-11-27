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
        foreach(PropWeaponUserBase user in propWeaponUsers)
        {
            user.Setup(inputsLibrary, projectilesPool);
        }

        base.SetupComponents();
    }

    protected override void InitComponents()
    {
        base.InitComponents();

        var propWeaponUserRangePlayer = (PropWeaponUserRangePlayer)propWeaponUsers[0];
        var propWeaponUserMeleePlayer = (PropWeaponUserMeleePlayer)propWeaponUsers[1];
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
        propWeaponUsers[1].Disable();
    }

    protected void RangeAttackStop()
    {
        propAbilityUser.Enable();
        propWeaponUsers[1].Enable();
    }

    protected void MeleeAttackStart()
    {
        propAbilityUser.Disable();
        propWeaponUsers[0].Disable();
    }

    protected void MeleeAttackStop()
    {
        propAbilityUser.Enable();
        propWeaponUsers[0].Enable();
    }

    protected void AbilityUseStart()
    {
        propWeaponUsers[0].Disable();
        propWeaponUsers[1].Disable();
    }

    protected void AbilityUseStop()
    {
        propWeaponUsers[0].Enable();
        propWeaponUsers[1].Enable();
    }

    #endregion

}

[System.Serializable]
public class PlayerSpawnInfo
{
    public Player player;
    public Transform spawnTransform;
}
