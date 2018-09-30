using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsLibrary : MonoBehaviour
{

    public List<InputBase> inputs;

    public void Init()
    {
        foreach(InputBase input in inputs)
        {
            input.Init();
        }
    }

    public void Terminate()
    {
        foreach (InputBase input in inputs)
        {
            input.Terminate();
        }

        Destroy(gameObject);
    }

    public InputBase GetInput(InputType type)
    {
        return inputs[(int)type];
    }
	
}

public enum InputType
{
    ATTACK,
    MAX_COUNT
}
