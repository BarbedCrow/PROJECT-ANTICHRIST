using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMovementPlayer : PropMovement
{

    //[SerializeField] CharacterController charControler;
    [SerializeField] float speed;

    public override void Init(Transform owner)
    {
        base.Init(owner);

        currentSpeed = speed;
        gameCamera = GameObject.FindGameObjectWithTag(GAME_CAMERA).GetComponent<Camera>();

        body = owner.GetComponent<Rigidbody>();
    }

    public override void Enable()
    {
        base.Enable();

        StartCoroutine(MOVEMENT_UPDATE_COROUTINE);
    }

    public override void Disable()
    {
        StopCoroutine(MOVEMENT_UPDATE_COROUTINE);

        base.Disable();
    }

    #region private

    const string GAME_CAMERA = "GameCamera";
    const string HORIZONTAL_AXIS = "Horizontal";
    const string VERTICAL_AXIS = "Vertical";
    const string MOVEMENT_UPDATE_COROUTINE = "MovementUpdate";

    Rigidbody body;
    Camera gameCamera;
    float currentSpeed;

    IEnumerator MovementUpdate()
    {
        for(; ; )
        {
            CheckInputs();
            UpdateRotation();

            body.velocity = Vector3.zero;

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
        Vector3 mousePos = gameCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.y = owner.position.y;
        float deltaY = mousePos.z - owner.position.z;
        float deltaX = mousePos.x - owner.position.x;
        float angleInDegrees = Mathf.Atan2(deltaY, deltaX) * 180 / Mathf.PI;
        owner.eulerAngles = new Vector3(0, -angleInDegrees, 0);
    }

    void Move(Vector3 velocity)
    {
        //charControler.Move(velocity * currentSpeed);
        owner.position = owner.position + velocity * currentSpeed;
        OnMove.Invoke(velocity);
    }


    #endregion

}
