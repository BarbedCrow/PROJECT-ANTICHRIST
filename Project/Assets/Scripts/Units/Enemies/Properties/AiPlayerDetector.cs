using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Scanner))]
public class AiPlayerDetector : MonoBehaviour
{

    [HideInInspector]
    public EventOnSeen OnSeen = new EventOnSeen();

    [HideInInspector]
    public UnityEvent OnMiss = new UnityEvent();

    public List<ScannerPreset> scannerPresets;

    public void Init()
    {
        scanner = GetComponent<Scanner>();
        scanner.SetPreset(GetPresetByType(ScannerPresetType.BEFORE_DETECTION));

        scanner.OnSeen.AddListener(HandleOnSeen);
        scanner.OnMiss.AddListener(HandleOnMiss);
        scanner.Init();
    }

    public void Terminate()
    {
        scanner.OnSeen.RemoveListener(HandleOnSeen);
        scanner.OnMiss.RemoveListener(HandleOnMiss);
        scanner.Terminate();
    }

    #region private

    Scanner scanner;

    void HandleOnSeen(Transform playerTransform)
    {
        scanner.SetPreset(GetPresetByType(ScannerPresetType.AFTER_DETECTION));
        OnSeen.Invoke(playerTransform);
    }

    void HandleOnMiss()
    {
        scanner.SetPreset(GetPresetByType(ScannerPresetType.BEFORE_DETECTION));
        OnMiss.Invoke();
    }

    ScannerPreset GetPresetByType(ScannerPresetType type)
    {
        foreach(ScannerPreset preset in scannerPresets)
        {
            if (preset.type == type)
            {
                return preset;
            }
        }

        Debug.Log("Scanner preset wasn't found");
        return null;
    }

    #endregion

}

[System.Serializable]
public class ScannerPreset
{
    public ScannerPresetType type;
    public float maxHorAngle;
    public float maxDistance;
}

public enum ScannerPresetType
{
    BEFORE_DETECTION,
    AFTER_DETECTION
}
