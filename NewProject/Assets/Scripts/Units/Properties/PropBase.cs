using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropBase : MonoBehaviour
{

    public virtual void Setup(params MonoBehaviour[] args)
    {

    }

    public virtual void Init(Transform owner)
    {
        this.owner = owner;
    }

    public virtual void Terminate()
    {
        //Destroy(gameObject);
    }

    public virtual void Enable()
    {
        gameObject.SetActive(true);
    }

    public virtual void Disable()
    {
        gameObject.SetActive(false);
    }

    #region private

    protected Transform owner;

    #endregion

}
