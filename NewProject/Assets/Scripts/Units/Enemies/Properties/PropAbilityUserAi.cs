using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PropAbilityUserAI : PropAbilityUser
{
    [HideInInspector] public UnityEvent OnAIAbilityUseStart;
    [HideInInspector] public UnityEvent OnAIAbilityUseStop;
    
    [SerializeField] float maxRangeForAttack;
    [SerializeField] float coolDownTime;
    [SerializeField] float timeForUse;

    public void HandleOnSeen(Transform playerPosition)
    {
        canAbilityUse = true;
        this.playerPosition = playerPosition;
    }

    public void HandleOnMiss()
    {
        canAbilityUse = false;
        playerPosition = null;
    }

    #region private
    
    Transform playerPosition;
    bool canAbilityUse = false;
    AbilitiesLibrary abilitiesLibrary;

    void TryAttack()
    {
        var rangeBetween = Vector3.Distance(gameObject.transform.position, playerPosition.position);
        if (rangeBetween > maxRangeForAttack)
            return;
        
        canAbilityUse = false;
        OnAIAbilityUseStart.Invoke();
        StartUse(0);
        Invoke("InvokeStopUse", timeForUse);
        Invoke("SetCanAbilityUser", coolDownTime);
    }

    void InvokeStopUse()
    {
        OnAIAbilityUseStop.Invoke();
    }

    void SetCanAbilityUser()
    {
        canAbilityUse = true;
    }

    void Update()
    {
        if (canAbilityUse)
            TryAttack();
    }

    #endregion
}

