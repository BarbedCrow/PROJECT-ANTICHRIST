using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AbilitiesUser))]
[RequireComponent(typeof(WeaponUserMelee))]
[RequireComponent(typeof(WeaponUserRange))]
[RequireComponent(typeof(MovementController))]
public class Player : UnitBase
{
    public InputUid swapWeaponInputUid;

    public void Init(InputsLibrary inputsLibrary, PoolBase pool)
    {
        this.inputsLibrary = inputsLibrary;
        this.pool = pool;

        base.Init();

        //propWeaponUserMelee.Enable();
        propWeaponUserRange.Enable();
        propAbilitiesUser.Enable();

        InitInputs();
        SubscribeInputs();
    }

    public override void Terminate()
    {
        UnsubscribeInputs();

        base.Terminate();
    }

    #region private

    const string PLAYER_CAMERA = "PlayerCamera";

    PoolBase pool;
    AbilitiesUser propAbilitiesUser;
    WeaponUserBase propWeaponUserMelee;
    WeaponUserRange propWeaponUserRange;
    MovementController propMovement;
    CameraController propCamera;

    InputsLibrary inputsLibrary;
    InputTap swapWeaponInput;

    protected override void InitComponents()
    {
        base.InitComponents();

        propWeaponUserMelee = GetComponent<WeaponUserMelee>();
        propWeaponUserMelee.CacheInputsLibrary(inputsLibrary);
        propWeaponUserMelee.Init();

        propWeaponUserRange = GetComponent<WeaponUserRange>();
        propWeaponUserRange.CacheInputsLibrary(inputsLibrary);
        propWeaponUserRange.Init(pool);

        propAbilitiesUser = GetComponent<AbilitiesUser>();
        propAbilitiesUser.CacheInputsLibrary(inputsLibrary);
        propAbilitiesUser.Init();

        propMovement = GetComponent<MovementController>();
        propMovement.Init();

        propCamera = GameObject.FindGameObjectWithTag(PLAYER_CAMERA).GetComponent<CameraController>();
        propCamera.Init(transform);
    }

    protected override void TerminateComponents()
    {
        propWeaponUserMelee.Terminate();
        propWeaponUserRange.Terminate();
        propAbilitiesUser.Terminate();
        propMovement.Terminate();
        propCamera.Terminate();

        base.TerminateComponents();
    }

    void InitInputs()
    {
        swapWeaponInput = (InputTap)inputsLibrary.GetInput(swapWeaponInputUid);
    }

    void SubscribeInputs()
    {
        swapWeaponInput?.OnUse.AddListener(SwapWeaponUser);
    }

    void UnsubscribeInputs()
    {
        swapWeaponInput?.OnUse.RemoveListener(SwapWeaponUser);
    }

    void SwapWeaponUser()
    {
        if (propWeaponUserMelee.IsEnabled())
        {
            propWeaponUserMelee.Disable();
            propWeaponUserRange.Enable();
        }else
        {
            propWeaponUserMelee.Enable();
            propWeaponUserRange.Disable();
        }
    }

    #endregion
}
