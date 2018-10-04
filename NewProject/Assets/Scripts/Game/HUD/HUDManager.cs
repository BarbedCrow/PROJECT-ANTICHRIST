using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HUDManager : MonoBehaviour
{
    [SerializeField] RectTransform hpPoint;

    public void Init(Player player)
    {
        var hpElement = new GameObject("HpElement");
        hpElement.transform.SetParent(hpPoint);
        var hudHealthController = hpElement.AddComponent<HUDHealthController>();

        hudHealthController.Init(player.GetDamagable(), hpPoint.position, hpElement.transform);
        HUDControllers.Add(hudHealthController);
    }

    #region private

    List <HUDBaseController> HUDControllers = new List<HUDBaseController>();

    private void Terminate()
    {

    }

    #endregion

}
