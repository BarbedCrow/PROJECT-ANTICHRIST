using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HPControllerElement : MonoBehaviour
{
    public void Init(PropDamagable damagablePlayer, Vector3 attachPoint, Transform folder)
    {
        this.damagablePlayer = damagablePlayer;
        hpViewData = new HPViewData(damagablePlayer.GetHealth());
        hpView = gameObject.AddComponent<HPView>();
        hpView.Init(attachPoint, hpViewData, folder);
    }

    #region private

    PropDamagable damagablePlayer;
    HPView hpView;
    HPViewData hpViewData;

    void Terminate()
    {

    }

    #endregion

}

[System.Serializable]
public class HPViewData
{
    public float healthPoints;

    public HPViewData(float HP)
    {
        healthPoints = HP;
    }


}
