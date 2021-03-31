using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using UnityEngine;

public class HoloDetails : MonoBehaviour
{
    public Animator detailsAnim;
    public Interactable holoModel;

    public void OpenDetails()
    {
        if (detailsAnim.gameObject.activeSelf == false)
        {
            holoModel.enabled = false;
            detailsAnim.gameObject.SetActive(true);
            detailsAnim.SetTrigger("OpenDetails");
        }
    }

    public void CloseDetails()
    {
        if (detailsAnim.gameObject.activeSelf == true)
            StartCoroutine(closeDetails());
    }

    private IEnumerator closeDetails()
    {
        detailsAnim.SetTrigger("CloseDetails");
        holoModel.enabled = true;
        while (!detailsAnim.GetCurrentAnimatorStateInfo(0).IsName("HoloDetailsIdle"))
            yield return null;
        detailsAnim.gameObject.SetActive(false);
    }
}
