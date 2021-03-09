using System.Collections;
using UnityEngine;

public class HoloDetails : MonoBehaviour
{
    public Animator detailsAnim;

    public void OnModelClicked()
    {
        if (detailsAnim.gameObject.activeSelf == false)
        {
            detailsAnim.gameObject.SetActive(true);
            detailsAnim.SetTrigger("OpenDetails");
        }
        else
        {
            StartCoroutine(CloseDetails());
        }   
    }

    private IEnumerator CloseDetails()
    {
        detailsAnim.SetTrigger("CloseDetails");
        while (!detailsAnim.GetCurrentAnimatorStateInfo(0).IsName("HoloDetailsIdle"))
            yield return null;
        detailsAnim.gameObject.SetActive(false);
    }
}
