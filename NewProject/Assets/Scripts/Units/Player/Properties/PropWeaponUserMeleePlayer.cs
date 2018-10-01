using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropWeaponUserMeleePlayer : PropWeaponUserMelee {

    [SerializeField] InputType attackInputType;

    public override void Setup(params MonoBehaviour[] args)
    {
        base.Setup(args);

        foreach (MonoBehaviour arg in args)
        {
            if (inputsLibrary == null && arg is InputsLibrary)
            {
                inputsLibrary = (InputsLibrary)arg;
            }
        }
    }

    #region private

    InputsLibrary inputsLibrary;

    #endregion

}
