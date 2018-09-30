using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : UnitBase
{

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

        currentWeaponUser = propWeaponUsers[0];
        currentWeaponUser.Enable();
        propMovement.Enable();
        
    }

    #region private

    InputsLibrary inputsLibrary;
    PoolBase projectilesPool;
    PropWeaponUserBase currentWeaponUser;

    protected override void SetupComponents()
    {
        base.SetupComponents();

        foreach(PropWeaponUserBase user in propWeaponUsers)
        {
            user.Setup(inputsLibrary, projectilesPool);
        }
    }

    #endregion

}

[System.Serializable]
public class PlayerSpawnInfo
{
    public Player player;
    public Transform spawnTransform;
}
