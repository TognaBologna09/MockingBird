using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainView : View
{
    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button shopButton;

    [SerializeField]
    private Button settingsButton;

    public override void Initialize()
    {
        playButton.onClick.AddListener(() => ViewManager.Instance.Show<PlayView>());

        shopButton.onClick.AddListener(() => ViewManager.Instance.Show<ShopView>());

        settingsButton.onClick.AddListener(() => ViewManager.Instance.Show<SettingsView>());

        

        base.Initialize();

    }

    public override void Show(object args = null)
    {
        base.Show(args);
    }

}
