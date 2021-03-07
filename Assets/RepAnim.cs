using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepAnim : MonoBehaviour
{
    Animator anim;
  
    void Start()
    {
        anim = GetComponent<Animator>();
    }

   
    public void NextAnim()
    {
       anim.SetTrigger("ConfirmStep");
    }

    public void PreviousAnim()
    {

        anim.SetTrigger("PreviousStep");
    }

   // public void FocusAnim()
    //{
    //    anim.SetTrigger("MakeRot");

   // }
}
