﻿using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TooltipCreator : MonoBehaviour
{
    GameObject resourcesTooltip;
    GameObject tooltip;

    [Tooltip("Check this in order for the tooltip to remain until object loses focus")]
    public bool remainIndefinite;
    [Tooltip("Only relevant if remainIndefinite is set to false")]
    public float activeTime = 5f;

    public int fontSize = 36;

    [Tooltip("Tooltip's local position")]
    public Vector3 tooltipPosition = new Vector3();

    public string tooltipText;

    private void Awake()
    {
        // Load tooltip from resources
        resourcesTooltip = Resources.Load("Tooltip") as GameObject;

        // Create tooltip GO
        tooltip = Instantiate(resourcesTooltip);
        tooltip.SetActive(false);
        tooltip.transform.SetParent(transform);
        tooltip.transform.localPosition = tooltipPosition;
        tooltip.transform.rotation = transform.rotation;

        TMP_Text tmp = tooltip.GetComponent<TMP_Text>();
        tmp.text = tooltipText;
        tmp.fontSize = fontSize;

    }

    // Check if we have a Button or Interactable component and AddListeners
    private void Start()
    {
        if (TryGetComponent(out Button button))
        {
            // Create an EventTrigger Component and add new entrys
            EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();

            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((data) => tooltip.SetActive(true));
            trigger.triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerExit;
            entry.callback.AddListener((data) => tooltip.SetActive(false));
            trigger.triggers.Add(entry);

            // Turn off after activeTime if remainIndefinite is false
            if (!remainIndefinite)
            {
                entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerEnter;
                entry.callback.AddListener((data) => StartCoroutine(DisableTooltipAfterSeconds(activeTime)));
                trigger.triggers.Add(entry);
            }
        }
        else if (TryGetComponent(out Interactable interactable))
        {
            InteractableOnFocusReceiver focusReceiver = interactable.GetReceiver<InteractableOnFocusReceiver>();
            if (focusReceiver == null)
                focusReceiver = interactable.AddReceiver<InteractableOnFocusReceiver>();

            focusReceiver.OnFocusOn.AddListener(() => tooltip.SetActive(true));
            focusReceiver.OnFocusOff.AddListener(() => tooltip.SetActive(false));

            // Turn off after activeTime if remainIndefinite is false
            if (!remainIndefinite)
                focusReceiver.OnFocusOn.AddListener(() => StartCoroutine(DisableTooltipAfterSeconds(activeTime)));
        }
        else
        {
            throw new UnityException("No Button or Interactable component found in " + gameObject);
        }
    }

    IEnumerator DisableTooltipAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        tooltip.SetActive(false);
    }
}
