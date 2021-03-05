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

   
    public void ClickAnim()
    {
       anim.SetTrigger("MakeRep");
    }

    public void FocusAnim()
    {
        anim.SetTrigger("MakeRot");

    }
}
