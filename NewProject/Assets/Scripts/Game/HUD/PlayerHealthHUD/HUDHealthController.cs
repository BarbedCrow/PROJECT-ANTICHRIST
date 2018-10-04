using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HUDHealthController : HUDBaseController
{
    public override void Init(PropDamagable damagablePlayer, Vector3 attachPoint, Transform folder)
    {
        var controllerElement = gameObject.AddComponent<HPControllerElement>();
        controllerElement.Init(damagablePlayer, attachPoint, folder);
    }

    #region private

    void Terminate()
    {

    }

    #endregion

}
