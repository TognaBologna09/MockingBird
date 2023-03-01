using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ListVariable : ScriptableObject
{
    [Header("Floats")]
    public List<float> listFloat;
    [Space(8)]
    [Header("Ints")]
    public List<int> listInt;
    [Space(8)]
    public int maxListCount;
    
    public void AddToListInt(int varToAdd)
    {
        if (listInt.Count > maxListCount)
        {
            listInt.RemoveAt(maxListCount);
            listInt.Insert(0, varToAdd);
        }
        else
        {
            listInt.Add(varToAdd);
        }
    }

    public void ListIntReset()
    {
        listInt.Clear();
    }

    public void AddToListFloat(float varToAdd)
    {
        if (listFloat.Count > maxListCount)
        {
            listFloat.RemoveAt(maxListCount);
            listFloat.Insert(0, varToAdd);
        }
        else
        {
            listFloat.Add(varToAdd);
        }
    }

    public void ListFloatReset()
    {
        listFloat.Clear();
    }
}
