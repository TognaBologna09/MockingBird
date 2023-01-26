using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode()]
public class FlexibleUI : MonoBehaviour
{
    public FlexibleUIData skinData;

    protected virtual void OnSkinUI()
    {

    }

    public virtual void Awake()
    {
        OnSkinUI();
    }

    // for the purposes of the demo this is OK, however

    // it is best to create a custom editor script that calls the update method so that when the game is running this check does not occur
    public virtual void Update()
    {
        if (Application.isEditor)
        {
            OnSkinUI();
        }
    }
}
