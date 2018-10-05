using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIBaseView : MonoBehaviour
{
    public List<Image> viewElements;

    public virtual void Init()
    {
        
    }

    public virtual void Init(UIHealthViewData hpViewData)
    {

    }

    public virtual void UpdateUI(UIBaseViewData viewData)
    {

    }

    #region private

    protected void Terminate()
    {

    }

    #endregion

}
