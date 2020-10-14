using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: name polish
public class MyCollider : MonoBehaviour
{
    private MySelection a = null;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CircularMenuSelection")
        {
            a = (MySelection)other.gameObject.GetComponent(typeof(MySelection));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CircularMenuSelection")
        {
            a = null;
        }
    }

    public void select()
    {
        if (a != null)
        {
            a.selected();
            a = null;
        }    
    }
}
