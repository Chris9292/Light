using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloDetails : MonoBehaviour
{
    public Animator detailsAnim;

    public void OnModelClicked()
    {
        if (detailsAnim.GetCurrentAnimatorStateInfo(0).IsName("HoloDetailsOpen"))
            detailsAnim.SetTrigger("CloseDetails");
        else
            detailsAnim.SetTrigger("OpenDetails");
    }    
}
