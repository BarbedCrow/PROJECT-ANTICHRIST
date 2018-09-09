using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnSeen : UnityEvent<Transform>{}

public class Scanner : MonoBehaviour
{
    public string tagToLook;
    
    public float maxHorAngle;
    //public float vertAngle;
    public float maxDistance;

    [HideInInspector]
    public EventOnSeen OnSeen = new EventOnSeen();

    [HideInInspector]
    public UnityEvent OnMiss = new UnityEvent();

    public void Init()
    {
        transformToLook = GameObject.FindGameObjectWithTag(tagToLook).transform;
        StartWork();
    }

    public void Terminate()
    {
        StopWork();
    }

    public void StartWork()
    {
        StartCoroutine(SCAN_COROUTINE);
    }

    public void StopWork()
    {
        StopCoroutine(SCAN_COROUTINE);
    }

    #region private
    
    const int UPDATE_FREQUENCY = 1 / 2;
    const string SCAN_COROUTINE = "Scan";

    Transform transformToLook;
    bool isVisible = false;

    IEnumerator Scan()
    {
        for(; ; )
        {
            ScanInternal();
            yield return new WaitForSeconds(UPDATE_FREQUENCY);
        }
    }

    void ScanInternal()
    {
        var dist = Vector3.Distance(transform.position, transformToLook.position);
        if(dist > maxDistance)
        {
            isVisible = false;
            OnMiss.Invoke();
            return;
        }

        var targetDir = (transformToLook.position - transform.position).normalized;
        var dot = Vector3.Dot(targetDir, transform.forward);
        var angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        if (angle > maxHorAngle / 2)
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
