using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : UnitBase
{

    [SerializeField] PropAnimPlayerBodyController propAnimBodyController;

    public override void Setup(params MonoBehaviour[] args)
    {
        base.Setup(args);

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
        }
    }

    public override void Enable()
    {
        base.Enable();

        
        foreach(var user in propWeaponUsers)
        {
            user.Enable();
        }

        propMovement.Enable();
        
    }

    #region private

    InputsLibrary inputsLibrary;
    PoolBase projectilesPool;

    protected override void SetupComponents()
    {
        base.SetupComponents();

        foreach(PropWeaponUserBase user in propWeaponUsers)
        {
            user.Setup(inputsLibrary, projectilesPool);
        }
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
