using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsLibrary : MonoBehaviour {
    
    public List<InputBase> inputDescs;

	public void Init()
    {
        foreach(InputBase inputDesc in inputDescs)
        {
            var input = Instantiate(inputDesc);
            input.Init();
            inputs.Add(input);
        }
    }

    public void Terminate()
    {
        foreach (InputBase input in inputs)
        {
            input.Terminate();
        }
    }

    public InputBase GetInput(InputUid uid)
    {
        foreach(InputBase input in inputs)
        {
            if (input.uid == uid) return input;
        }

        return null;
    }

    #region private

    List<InputBase> inputs = new List<InputBase>();

    #endregion
}
