using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropAbilityUserPlayer : PropAbilityUser
{

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

    public override void Init(Transform owner)
    {
        base.Init(owner);

        //TO DO: Get Inputs for ability slots
    }

    #region private

    InputsLibrary inputsLibrary;

    #endregion

}
