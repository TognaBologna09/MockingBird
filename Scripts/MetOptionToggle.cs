using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetOptionToggle : MonoBehaviour
{
    [SerializeField] private BoolVariable rhythmModeBool;
    [SerializeField] private GameObject metModeToggle;

    void Update()
    {
        if (rhythmModeBool.Value)
        {
            metModeToggle.SetActive(true);
        }
    }
}
