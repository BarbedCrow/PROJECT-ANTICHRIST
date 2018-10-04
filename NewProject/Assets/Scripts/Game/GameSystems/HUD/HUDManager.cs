using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HUDManager : MonoBehaviour
{
    [SerializeField] List<HUDBaseController> HUDControllers = new List<HUDBaseController>();

    public void Init()
    {
        foreach (HUDBaseController HUDController in HUDControllers)
        {
            HUDController.Init();
        }
    }

    #region private

    private void Terminate()
    {

    }

    #endregion

}
