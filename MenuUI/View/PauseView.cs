using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseView : View
{ 

    [SerializeField]
    private Button resumeButton;


    public override void Initialize()
    {
        resumeButton.onClick.AddListener(() => ViewManager.Instance.Show<PlayView>());

        base.Initialize();

    }

    public override void Show(object args = null)
    {
        base.Show(args);
    }
}
