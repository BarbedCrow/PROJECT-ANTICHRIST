using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HUDBaseController : MonoBehaviour
{
    [SerializeField] UIBaseController uiController;

    public virtual void Init()
    {
        uiController.Init();
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

    void Terminate()
    {

    }

    #endregion

}
