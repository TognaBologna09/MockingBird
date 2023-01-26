using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ViewManager : MonoBehaviour
{
    public static ViewManager Instance { get; private set; }

    [SerializeField]
    private bool autoInitialization;

    [SerializeField]
    private View[] views;       // list of all the views

    [SerializeField]
    private View defaultView;   // fist view on startup

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        foreach (View view in views)
        {
            view.Initialize();

            view.Hide(); 
        }

        if(defaultView != null) defaultView.Show();

    }

    public void Show<TView>(object args = null) where TView : View
    {
        // polling the views for the Type of View we want,
        // showing the result hiding the rest
        foreach(View view in views)
        {
            if (view is TView)
            {
                view.Show();
            }
            else
            {
                view.Hide();
            }
        }
    } 
}
