using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUserBase : MonoBehaviour
{

    public List<string> ignoredTags;
    public InputUid attackInputUid;

	public virtual void Init()
    {

    }

    public virtual void Terminate()
    {
        if (attackInput != null)
        {
            attackInput.OnUse.RemoveListener(RequestAttack);
        }
    }

    public virtual void CacheInputsLibrary(InputsLibrary inputsLibrary)
    {
        this.inputsLibrary = inputsLibrary;
        attackInput = (InputHold)inputsLibrary.GetInput(attackInputUid);
    }

    public virtual void Enable()
    {
        if (isEnabled) return; // assert?
        isEnabled = true;

        attackInput?.OnPressed.AddListener(RequestAttack);
        attackInput?.OnReleased.AddListener(RequestStopAttack);
    }

    public virtual void Disable()
    {
        if (!isEnabled) return;
        isEnabled = false;

        attackInput?.OnPressed.RemoveListener(RequestAttack);
        attackInput?.OnReleased.RemoveListener(RequestStopAttack);
    }

    public bool IsEnabled()
    {
        return isEnabled;
    }

    public virtual void RequestAttack()
    {

    }

    public virtual void RequestStopAttack()
    {

    }

    #region private

    InputHold attackInput;
    protected InputsLibrary inputsLibrary;
    bool isEnabled = false;
    
    #endregion

}
