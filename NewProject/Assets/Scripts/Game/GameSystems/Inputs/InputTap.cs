using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTap : InputBase
{

    #region private

    protected override IEnumerator CheckInputs()
    {
        for (; ; )
        {
            foreach (string key in keys)
            {
                if (Input.GetButtonDown(key))
                {
                    OnUse.Invoke();
                }
            }
            yield return base.CheckInputs();
        }
    }

    #endregion
}
