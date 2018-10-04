using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HUDBaseController : MonoBehaviour
{
    public virtual void Init(Transform attachPoint)
    {

    }

    public virtual void Init(PropDamagable damagablePlayer, Vector3 attachPoint, Transform folder)
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

    void Terminate()
    {

    }

    #endregion

}
