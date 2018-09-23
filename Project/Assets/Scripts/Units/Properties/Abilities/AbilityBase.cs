using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBase : MonoBehaviour
{
    public GameObject particlesPrefab;
    public InputUid activateInputUid;

    public virtual void Init(InputsLibrary inputsLibrary, List<string> ignoredTags)
    {
        activateInput = (InputTap)inputsLibrary.GetInput(activateInputUid);
        activateInput.OnUse.AddListener(Activate);
    }

    public virtual void Terminate()
    {
        activateInput.OnUse.RemoveListener(Activate);
    }

    #region private

    InputTap activateInput;

    protected virtual void Activate()
    {
    }

    #endregion
}