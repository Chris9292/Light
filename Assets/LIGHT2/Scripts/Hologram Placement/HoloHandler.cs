using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TapToPlace))]
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(FocusSelect))]
public class HoloHandler : MonoBehaviour
{
    public Animator detailsAnim;
    public GameObject circularLoading;

    TapToPlace TTP;
    Interactable interactable;
    FocusSelect focusSelect;
    private void Start()
    {
        // Get variables
        TTP = GetComponent<TapToPlace>();
        interactable = GetComponent<Interactable>();
        focusSelect = GetComponent<FocusSelect>();

        // When placing ends disable TapToPlace to avoid user being able to move object by clicking it
        TTP.OnPlacingStopped.AddListener(() => TTP.enabled = false);

        // Link focusSelect interaction to interactable.onFocuseReceiver
        // Enable/Disable loading bar on focus
        InteractableOnFocusReceiver focusRec = interactable.AddReceiver<InteractableOnFocusReceiver>();
        
        focusRec.OnFocusOn.AddListener(focusSelect.StartFocusInteraction);
        focusRec.OnFocusOn.AddListener( () => {
            if (!detailsAnim.gameObject.activeSelf)
                circularLoading.SetActive(true);
        });
        
        focusRec.OnFocusOff.AddListener(focusSelect.StopFocusInteraction);
        focusRec.OnFocusOff.AddListener(() => circularLoading.SetActive(false));

        // Open details and disable loading bar when focus interaction is complete
        focusSelect.OnHoldFocus.AddListener(OpenDetails);
        focusSelect.OnHoldFocus.AddListener(() => circularLoading.SetActive(false));
    }

    void OnDisable()
    {
        detailsAnim.gameObject.SetActive(false);
    }

    #region Basic Functionalities

    /// <summary>
    /// Basic functionalities to link to buttons
    /// </summary>
    
    public void DeleteHolo()
    {
        Destroy(gameObject);
    }

    public void MoveHolo()
    {
        TTP.enabled = true;
        TTP.StartPlacement();
    }

    public void CloseDetails()
    {
        if (detailsAnim.gameObject.activeSelf == true)
            StartCoroutine(closeDetails());
    }

    void OpenDetails()
    {
        if (detailsAnim.gameObject.activeSelf == false)
        {
            detailsAnim.gameObject.SetActive(true);
            detailsAnim.SetTrigger("OpenDetails");
        }
    }

    // Trigger "CloseDetails" animation and wait to finish before setting object to inactive
    IEnumerator closeDetails()
    {
        detailsAnim.SetTrigger("CloseDetails");
        while (!detailsAnim.GetCurrentAnimatorStateInfo(0).IsName("HoloDetailsIdle"))
            yield return null;
        detailsAnim.gameObject.SetActive(false);
    }
    #endregion
}