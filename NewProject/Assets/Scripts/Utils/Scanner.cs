using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scanner : MonoBehaviour
{
    
    public float maxHorAngle;
    public float maxDistance;

    [HideInInspector]
    public EventOnSeen OnSeen = new EventOnSeen();

    [HideInInspector]
    public UnityEvent OnMiss = new UnityEvent();

    public void Init(Transform owner, string tagToLook)
    {
        this.owner = owner;
        this.tagToLook = tagToLook;
    }

    public void Terminate()
    {
        Disable();
    }

    public void SetPreset(ScannerPreset preset)
    {
        //Debug.Log("Set preset " + preset.type);
        maxHorAngle = preset.maxHorAngle;
        maxDistance = preset.maxDistance;
    }

    public void Enable()
    {
        if(transformToLook == null)
        {
            transformToLook = GameObject.FindGameObjectWithTag(tagToLook).transform;
        }

        StartCoroutine(SCAN_COROUTINE);
    }

    public void Disable()
    {
        StopCoroutine(SCAN_COROUTINE);
    }

    #region private

    const int UPDATE_FREQUENCY = 1 / 2;
    const string SCAN_COROUTINE = "Scan";

    Transform owner;

    string tagToLook;
    Transform transformToLook;
    bool isVisible = false;

    IEnumerator Scan()
    {
        for (; ; )
        {
            ScanInternal();
            yield return new WaitForSeconds(UPDATE_FREQUENCY);
        }
    }

    void ScanInternal()
    {
        var dist = Vector3.Distance(owner.position, transformToLook.position);
        if ((dist > maxDistance) && isVisible)
        {
            isVisible = false;
            OnMiss.Invoke();
            return;
        }

        var targetDir = (transformToLook.position - (owner.position + owner.forward)).normalized;
        var dot = Vector3.Dot(targetDir, owner.forward);
        var angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        if ((angle > maxHorAngle / 2) && isVisible)
        {
            isVisible = false;
            OnMiss.Invoke();
            return;
        }

        if (isVisible)
        {
            return;
        }

        isVisible = true;
        OnSeen.Invoke(transformToLook);
    }

    #endregion

}
