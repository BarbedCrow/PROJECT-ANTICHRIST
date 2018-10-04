using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIBaseView : MonoBehaviour
{
    [SerializeField] List<Image> viewElements;

    public virtual void Init()
    {

    }

    #region private

    void Terminate()
    {

    }

    #endregion

}
