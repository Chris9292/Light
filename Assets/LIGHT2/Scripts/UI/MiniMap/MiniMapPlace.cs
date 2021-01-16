using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

[RequireComponent(typeof(Interactable))]
public class MiniMapPlace : MonoBehaviour
{
    private Interactable interactable;

    private void Start()
    {
        interactable = gameObject.GetComponent<Interactable>();
    }

    private void OnDisable()
    {
        if (interactable.ButtonMode == SelectionModes.Toggle)
        {
            interactable.IsToggled = false;
        }
    }
}