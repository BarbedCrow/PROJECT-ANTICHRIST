using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBase : MonoBehaviour
{
    public List<ObjectDesc> objectsDesc;
    public int maxCapacity;
    
    public void Init()
    {
        foreach (ObjectDesc objDesc in objectsDesc)
        {
            for(int i = 0; i < objDesc.initialCount; i++)
            {
                currentCapacity++;
                if (currentCapacity < maxCapacity)
                    availableObjects.Add(objDesc.id, Instantiate(objDesc.prefab, transform.position, transform.rotation));
            }
        }
    }

    public void Terminate()
    {
        Destroy(gameObject);
    }

    public void Take(string id)
    {
        if (availableObjects.ContainsKey(id))
        {
            foreach (KeyValuePair<string, GameObject> kvp in availableObjects)
            {
                if (kvp.Key == id)
                {
                    takenObjects.Add(kvp.Key, kvp.Value);
                    availableObjects.Remove(id);
                    return;
                }
            }
        }
        else
        {
            if (currentCapacity < maxCapacity)
            {
                currentCapacity++;
                foreach (ObjectDesc objDesc in objectsDesc)
                {
                    if(objDesc.id == id)
                    {
                        takenObjects.Add(objDesc.id, Instantiate(objDesc.prefab, transform.position, transform.rotation));
                        return;
                    }
                }
            }
        }
    }

    public void Release(GameObject gameObject)
    {

    }

    #region private
    
    int currentCapacity = 0;
    Dictionary<string, GameObject> availableObjects = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> takenObjects = new Dictionary<string, GameObject>();
    
    #endregion
}

[System.Serializable]
public class ObjectDesc
{
    public GameObject prefab;
    public int initialCount;
    public string id;
}

