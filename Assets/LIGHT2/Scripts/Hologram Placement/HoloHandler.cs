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

    TapToPlace TTP;
    Interactable interactable;
    FocusSelect focusSelect;
    private void Start()
    {
        TTP = GetComponent<TapToPlace>();
        interactable = GetComponent<Interactable>();
        focusSelect = GetComponent<FocusSelect>();

        TTP.OnPlacingStopped.AddListener(() => TTP.enabled = false);

        InteractableOnFocusReceiver focusRec = interactable.AddReceiver<InteractableOnFocusReceiver>();
        focusRec.OnFocusOn.AddListener(focusSelect.StartFocusInteraction);
        focusRec.OnFocusOff.AddListener(focusSelect.StopFocusInteraction);

        focusSelect.OnHoldFocus.AddListener(OpenDetails);
    }
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

    IEnumerator closeDetails()
    {
        detailsAnim.SetTrigger("CloseDetails");
        while (!detailsAnim.GetCurrentAnimatorStateInfo(0).IsName("HoloDetailsIdle"))
            yield return null;
        detailsAnim.gameObject.SetActive(false);
    }
}