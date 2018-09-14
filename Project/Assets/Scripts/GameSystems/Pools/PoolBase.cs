using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBase : MonoBehaviour
{

    public PoolObjectDesc[] descs;
    public int maxCapacity;

    public void Init()
    {
        foreach(PoolObjectDesc desc in descs)
        {
            List<GameObject> objects = new List<GameObject>();
            availableObjects.Add(desc.gameObject.tag, objects);

            for (int i = 0; i < desc.initialCount; i += 1)
            {
                GameObject gO = Instantiate(desc.gameObject, transform.position, transform.rotation);
                gO.SetActive(false);

                objects.Add(gO);
                currentCapacity += 1;
            }
        }
    }

    public void Terminate()
    {

    }

    public GameObject Take(string key)
    {
        GameObject objectToTake = new GameObject();
        List<GameObject> objects;
        if (!availableObjects.TryGetValue(key, out objects))
        {
            objects = new List<GameObject>();
        }

        if (objects.Capacity > 0)
        {
            objectToTake = objects[0];
            objectToTake.SetActive(true);
            objects.Remove(objectToTake);
            takenObjects.Add(objectToTake);
        }
        else
        {
            // create new object
        }

        return objectToTake;
    }

    public void Release(GameObject gO)
    {
        gO.SetActive(false);
        var objects = availableObjects[gO.tag];
        objects.Add(gO);
        takenObjects.Remove(gO);
    } 

    #region private

    Dictionary<string, List<GameObject>> availableObjects = new Dictionary<string, List<GameObject>>();
    List<GameObject> takenObjects = new List<GameObject>();
    int currentCapacity = 0;

    #endregion

}

[System.Serializable]
public class PoolObjectDesc
{
    public GameObject gameObject;
    public int initialCount;
}
