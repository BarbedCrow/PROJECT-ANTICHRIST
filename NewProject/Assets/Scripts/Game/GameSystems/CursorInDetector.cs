using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CursorInDetector : MonoBehaviour
{
    [HideInInspector] public EventOnCursorTrigger OnTriggerIn = new EventOnCursorTrigger();
    [HideInInspector] public EventOnCursorTrigger OnTriggerOut = new EventOnCursorTrigger();
    [SerializeField] Camera gameCamera;

    #region private

    private void Update()
    {
        var mousePos = gameCamera.ScreenToWorldPoint(Input.mousePosition);
        var newPosition = new Vector3(mousePos.x, 0.5f, mousePos.z);
        transform.SetPositionAndRotation(newPosition, new Quaternion());
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerIn.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerOut.Invoke(other);
    }

    #endregion
}
