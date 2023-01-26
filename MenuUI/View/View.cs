using UnityEngine;

public abstract class View : MonoBehaviour
{
    // abstract view to be inherited by _____
    // this abstract type class provides implementable and override-able methods 
    public bool IsInitialized { get; private set; }

    // virtuality allows methods to be overridden
    public virtual void Initialize()
    {
        IsInitialized = true;
    }

    public virtual void Show(object args = null)
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
