using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoolVariable : ScriptableObject
{
    public bool Value;

    public void SetBool()
    {
        if (Value)
        {
            Value = false;
        }
        else
        {
            Value = true;
        }
    }
}
