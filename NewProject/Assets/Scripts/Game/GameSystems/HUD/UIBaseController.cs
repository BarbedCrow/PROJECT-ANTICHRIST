using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIBaseController : MonoBehaviour
{
    public UIBaseView uiView;

    public virtual void Init()
    {

    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    #region private

    protected void Terminate()
    {

    }

    #endregion

}
