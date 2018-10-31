using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : UnitBase
{
    [SerializeField] PropAnimPlayerBodyController propAnimBodyController;

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

    public override void Enable()
    {
        base.Enable();

        
        foreach(var user in propWeaponUsers)
        {
            user.Enable();
        }

        propAbilityUser.Enable();

        propMovement.Enable();
        
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

        propAnimBodyController.Init(transform);
    }

    protected override void TerminateComponents()
    {
        propAnimBodyController.Terminate();

        base.TerminateComponents();
    }

    #endregion

}

[System.Serializable]
public class PlayerSpawnInfo
{
    public Player player;
    public Transform spawnTransform;
}
