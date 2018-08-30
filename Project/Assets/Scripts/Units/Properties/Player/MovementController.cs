using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public float walkSpeed = 5f;
    public float runSpeed = 6.5f;
    public float rotationSpeed = 5f;

    public void Init()
    {
        playerCamera = GameObject.FindGameObjectWithTag(PLAYER_CAMERA).GetComponent<Camera>();
        //propRigid = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed;
        StartCoroutine(MOVEMENT_UPDATE_COROUTINE); // Do it on start when all props are ready?
    }

    public void Terminate()
    {
        StopCoroutine(MOVEMENT_UPDATE_COROUTINE);
    }

    #region private

    const string HORIZONTAL_AXIS = "Horizontal";
    const string VERTICAL_AXIS = "Vertical";
    const string MOVEMENT_UPDATE_COROUTINE = "MovementUpdate";
    const string PLAYER_CAMERA = "PlayerCamera";

    Camera playerCamera;
    float currentSpeed;

    IEnumerator MovementUpdate()
    {
        for (;;)
        {
            CheckInputs();
            UpdateRotation();
            yield return new WaitForFixedUpdate();
        }
    }

    void CheckInputs()
    {
        var horAxis = Input.GetAxisRaw(HORIZONTAL_AXIS);
        var vertAxis = Input.GetAxisRaw(VERTICAL_AXIS);
        var velocity = new Vector3(-vertAxis, 0, horAxis) * Time.deltaTime;
        Move(velocity);
    }

    void UpdateRotation()
    {
        var ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo);
        var targetDir = hitInfo.point - transform.position;
        var finalTargetDir = new Vector3(targetDir.x, 0, targetDir.z);
        float step = rotationSpeed * Time.deltaTime;
        var newDir = Vector3.RotateTowards(transform.forward, finalTargetDir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void Move(Vector3 velocity)
    {
        transform.position = transform.position + velocity * currentSpeed;
    }

    #endregion
}
