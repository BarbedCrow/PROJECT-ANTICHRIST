using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InputBase : MonoBehaviour {

    [HideInInspector]
    public UnityEvent OnUse = new UnityEvent();

    public InputUid uid;
    public string[] keys;

    public virtual void Init()
    {
        StartCoroutine(CHECK_INPUTS_COROUTINE);
    }

    public virtual void Terminate()
    {
        StopCoroutine(CHECK_INPUTS_COROUTINE);
    }

    #region private

    const string CHECK_INPUTS_COROUTINE = "CheckInputs";

    protected virtual IEnumerator CheckInputs()
    {
        yield return new WaitForSeconds(1/120);
    }

    #endregion
}
