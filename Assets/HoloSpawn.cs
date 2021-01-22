using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloSpawn : MonoBehaviour
{
    GameObject Display;
    Transform DisplayTrans;
    private void OnEnable()
    {
        Display = GameObject.Find("HologramPlate") ;
        DisplayTrans = Display.transform;
        for (int i = 0; i <= DisplayTrans.childCount - 1; i++)
        {   
            if (DisplayTrans.GetChild(i).gameObject.activeInHierarchy)
            {
                

            }
        }
    }
    
}
