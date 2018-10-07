using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropAnimMovementController : PropAnimController
{

    public override void Init(Transform owner)
    {
        base.Init(owner);
        
        propMovement = owner.GetComponentInChildren<PropMovement>();
        propMovement.OnMove.AddListener(HandleOnMove);
    }

    public override void Terminate()
    {
        propMovement?.OnMove.RemoveListener(HandleOnMove);

        base.Terminate();
    }

    #region private

    const string IS_MOVING = "isMoving";

    PropMovement propMovement;

    void HandleOnMove(Vector3 velocity)
    {
        var isMoving = velocity.x != 0 || velocity.z != 0;
        animator.SetBool(IS_MOVING, isMoving);
    }

    #endregion

}
