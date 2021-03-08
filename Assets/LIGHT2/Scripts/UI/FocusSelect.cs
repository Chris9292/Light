using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Instructions: You need to call the StartFocusInteraction method on an interactable OnFocusOn event and the StopFocusInteraction method on a OnFocusOff event
/// </summary>


public class FocusSelect : MonoBehaviour
{
    public Animator holoAnimator;
    public UnityEvent OnHoldFocus;
    public float focusDuration = 1f;
    private IEnumerator focusInteraction;

    public void StartFocusInteraction()
    {
        holoAnimator.SetTrigger("StartFocusInteraction");
        focusInteraction = FocusInteraction();
        StartCoroutine(focusInteraction);
    }

    public void StopFocusInteraction()
    {
        holoAnimator.SetTrigger("StopFocusInteraction");
        if (focusInteraction != null)
            StopCoroutine(focusInteraction);
    }

    private IEnumerator FocusInteraction()
    {
        yield return new WaitForSeconds(focusDuration);
        TriggerFocusEvents();
        holoAnimator.SetTrigger("StopFocusInteraction");
    }

    private void TriggerFocusEvents()
    {
        if (OnHoldFocus != null)
            OnHoldFocus.Invoke();
    }
}