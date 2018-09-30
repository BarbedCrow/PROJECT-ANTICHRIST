using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitBase
{

    [SerializeField] AiPlayerDetector playerDetector;
    [SerializeField] int returnCost;

    public override void Setup(params MonoBehaviour[] args)
    {
        base.Setup(args);

        foreach(var arg in args)
        {
            if(pool == null && arg is EnemiesPool)
            {
                pool = (EnemiesPool)arg;
            }
        }
    }

    public override void Enable()
    {
        base.Enable();

        playerDetector.Enable();
    }

    public override void Disable()
    {


        base.Disable();
    }

    public int GetReturnCost()
    {
        return returnCost;
    }

    #region private

    EnemiesPool pool;

    protected override void InitComponents()
    {
        base.InitComponents();

        playerDetector.Init(transform);
    }

    protected override void TerminateComponents()
    {
        playerDetector.Terminate();

        base.TerminateComponents();
    }



    #endregion

}
