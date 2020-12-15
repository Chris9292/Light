using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CircularMenuSelection : MonoBehaviour
{
    // DebugMode does not apply during runtime (must be set before play)
    public bool DebugMode = false;

    public delegate void ButtonClickedHandler();
    public event ButtonClickedHandler OnButtonClicked;

    public void ButtonClicked()
    {
        if (OnButtonClicked != null)
        {
            OnButtonClicked();
        }
    }

    public delegate void HoverEntereddHandler();
    public event ButtonClickedHandler OnHoverEntered;

    public void HoverEntered()
    {
        if (OnHoverEntered != null)
        {
            OnHoverEntered();
        }
    }

    public delegate void HoverExitedHandler();
    public event ButtonClickedHandler OnHoverExited;

    public void HoverExited()
    {
        if (OnHoverExited != null)
        {
            OnHoverExited();
        }
    }

    //TODO: FIX THESE!
    private void Start()
    {
        gameObject.tag = "CircularMenuSelection";
        Collider collider = gameObject.GetComponent<Collider>();
        collider.isTrigger = false;

        if (DebugMode == true)
        {
            OnButtonClicked += () => { Debug.Log(gameObject.name + ": button clicked"); };
            OnHoverEntered += () => { Debug.Log(gameObject.name + ": hover entered"); };
            OnHoverExited += () => { Debug.Log(gameObject.name + ": hover exited"); };
        }
    }
}