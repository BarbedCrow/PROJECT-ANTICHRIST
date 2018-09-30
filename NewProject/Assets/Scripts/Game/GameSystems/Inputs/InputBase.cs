using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputBase : MonoBehaviour
{

    [HideInInspector]
    public UnityEvent OnUse = new UnityEvent();

    public string[] keys;

    public virtual void Init()
    {
        StartCoroutine(CHECK_INPUTS_COROUTINE);
    }

    public virtual void Terminate()
    {
        StopCoroutine(CHECK_INPUTS_COROUTINE);
        Destroy(gameObject);
    }

    #region private

    const string CHECK_INPUTS_COROUTINE = "CheckInputs";

    protected virtual IEnumerator CheckInputs()
    {
        yield return new WaitForSeconds(1 / 120);
    }

    #endregion

}
