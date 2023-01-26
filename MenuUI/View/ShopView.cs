using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopView : View
{

    [SerializeField]
    private Button closeButton;

    public override void Initialize()
    {
        closeButton.onClick.AddListener(() => ViewManager.Instance.Show<MainView>());

        base.Initialize();

    }

    public override void Show(object args = null)
    {
        base.Show(args);
    }
}
