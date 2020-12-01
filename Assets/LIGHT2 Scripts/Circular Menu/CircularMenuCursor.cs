using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class CircularMenuCursor : MonoBehaviour
{
    private CircularMenuSelection circularMenuSelection = null;

    // Triggers OnHoverEntered event in CircularMenuSelection
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CircularMenuSelection")
        {
            circularMenuSelection = (CircularMenuSelection)other.gameObject.GetComponent(typeof(CircularMenuSelection));
            circularMenuSelection.HoverEntered();
        }
    }

    // Triggers OnHoverExited event in CircularMenuSelection
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CircularMenuSelection")
        {
            circularMenuSelection.HoverExited();
            circularMenuSelection = null;
        }
    }

    // Triggers OnButtonClicked event in CircularMenuSelection
    public void ClickButton()
    {
        if (circularMenuSelection != null)
        {
            circularMenuSelection.ButtonClicked();
            circularMenuSelection = null;
        }
    }

    // Sets the position of the cursor to the parent's position
    // May be used to reset the cursor back to its starting postion
    public void SetToParentCoords()
    {
        transform.position = transform.parent.position;
    }

    // TODO: FIX THESE!
    private void Start()
    {
        Collider collider = (Collider)gameObject.GetComponent(typeof(Collider));
        collider.isTrigger = true;
        Rigidbody rigidbody = (Rigidbody)gameObject.GetComponent(typeof(Rigidbody));
        //rigidbody.useGravity = false;
       // rigidbody.isKinematic = true;
    }
}