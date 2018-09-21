using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesUser : MonoBehaviour
{
    public List<AbilityDesc> abilityDescs;
    public List<string> ignoredTags;

    public virtual void Init()
    {
        foreach (AbilityDesc desc in abilityDescs)
        {
            var ability = Instantiate(desc.ability, desc.spawnPoint.position, desc.spawnPoint.rotation);
            ability.transform.SetParent(transform);
            var abilityLogic = ability.GetComponent<AbilityBase>();
            abilityLogic.Init(inputsLibrary, ignoredTags);
            abilities.Add(abilityLogic);
        }
    }

    public virtual void Terminate()
    {
        foreach(AbilityBase ability in abilities)
        {
            ability.Terminate();
        }
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
    }

    #region private

    protected List<AbilityBase> abilities = new List<AbilityBase>();
    protected InputsLibrary inputsLibrary;
    bool isEnabled = false;

    #endregion
}

[System.Serializable]
public class AbilityDesc
{
    public AbilityBase ability;
    public Transform spawnPoint;
}
