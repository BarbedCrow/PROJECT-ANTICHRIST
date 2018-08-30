using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesUser : MonoBehaviour
{
    public AbilityDesc abilityDesc;
    public List<InputUid> attackInputUid;
    public Transform spawnPoint;

    public virtual void Init()
    {
        if (abilityDesc.abilityFirst == null && abilityDesc.abilitySecond == null)
        {
            Debug.Log("No abilities in Abilities user");
            return;
        }
        if (abilityDesc.abilityFirst != null)
        {
            if (abilityDesc.abilitySecond == null)
            {
                Debug.Log("No second ability in Abilities user");
                abilityDesc.abilityFirst.Init(spawnPoint);
                return;
            }
            else
            {
                Debug.Log("All ability in Abilities user");
                abilityDesc.abilityFirst.Init(spawnPoint);
                abilityDesc.abilitySecond.Init(spawnPoint);
                return;
            }
        }
        else
        {
            Debug.Log("No first ability in Abilities user");
            abilityDesc.abilitySecond.Init(spawnPoint);
            return;
        }
    }

    public virtual void Terminate()
    {
        abilityDesc.abilityFirst.Terminate();
        abilityDesc.abilitySecond.Terminate();
    }

    public virtual void Enable()
    {
        if (isEnabled) return; // assert?
        isEnabled = true;
    }

    public virtual void Disable()
    {
        if (!isEnabled) return;
        isEnabled = false;
    }

    public bool IsEnabled()
    {
        return isEnabled;
    }

    public void CacheInputsLibrary(InputsLibrary inputsLibrary)
    {
        this.inputsLibrary = inputsLibrary;

        var input1 = (InputTap)inputsLibrary.GetInput(attackInputUid[0]);
        var input2 = (InputTap)inputsLibrary.GetInput(attackInputUid[1]);

        input1.OnUse.AddListener(() => RequestAttack(ID_FIRST_ABILITY));
        input2.OnUse.AddListener(() => RequestAttack(ID_SECOND_ABILITY));
    }

    #region private

    const int ID_FIRST_ABILITY = 1;
    const int ID_SECOND_ABILITY = 2;

    protected InputsLibrary inputsLibrary;
    bool isEnabled = false;

    protected virtual void RequestAttack(int id)
    {   
        if (id == ID_FIRST_ABILITY)
        {
            if(abilityDesc.abilityFirst != null)
                abilityDesc.abilityFirst.Attack();
        }
        else
        {
            if (abilityDesc.abilitySecond != null)
                abilityDesc.abilitySecond.Attack();
        }
        
    }

    #endregion
}

[System.Serializable]
public class AbilityDesc
{
    
    public AbilityBase abilityFirst;
    public AbilityBase abilitySecond;

}
