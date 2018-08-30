using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Vector3 cameraOffset;

	public void Init(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }

    public void Terminate()
    {

    }

    #region private

    Transform playerTransform;

    void Update()
    {
        var newPos = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z) + cameraOffset;
        transform.position = newPos;
    }

    #endregion
}
