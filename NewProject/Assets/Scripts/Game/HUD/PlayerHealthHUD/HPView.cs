using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HPView : MonoBehaviour
{
    public void Init(Vector3 attachPoint, HPViewData hpViewData, Transform folder)
    {
        this.hpViewData = hpViewData;
        var objRed = new GameObject("RedBar");
        var imageRed = objRed.AddComponent<Image>();
        imageRed.color = redColor;
        imageRed.transform.SetParent(folder);
        imageRed.rectTransform.position = attachPoint;
        imageRed.rectTransform.sizeDelta = sizeOfBar;

        var objGreen = new GameObject("GreenBar");
        imageGreen = objGreen.AddComponent<Image>();
        imageGreen.color = greenColor;
        imageGreen.transform.SetParent(folder);
        imageGreen.rectTransform.position = attachPoint;
        imageGreen.rectTransform.sizeDelta = sizeOfBar;
        imageGreen.rectTransform.pivot = alignBar;

        imageGreen.transform.localScale = new Vector3(0.5f,1,1); //временная строчка для красоты, пока нет функций для изменения полоски
    }

    #region private

    Color redColor = new Color(255, 0, 0);
    Color greenColor = new Color(0, 255, 0);
    Vector3 sizeOfBar = new Vector2(100, 20);
    Vector2 alignBar = new Vector2(1, 0.5f);

    HPViewData hpViewData;
    Image imageGreen;


    void Terminate()
    {

    }

    #endregion

}
