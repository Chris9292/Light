using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramDisplay : MonoBehaviour
{
        GameObject Display;
    //Transform DisplayTransform = Display.transform;
    public int CurrentHolo = 0;

//change to onEnable
    private void OnEnable(){
        //Display = GameObject.FindGameObjectWithTag("HologramPlate");
        //Display = transform.GetChild(0).gameObject;
        Display = GameObject.Find("HologramPlate");

        //First Hologram in Hierarchy Enabled ; All the rest disabled
        for (int i=0; i<=Display.transform.childCount-1; i++){
        Display.transform.GetChild(i).gameObject.SetActive(false);
        }
        Display.transform.GetChild(CurrentHolo).gameObject.SetActive(true);
    }

    

    public void DisplayNext(){
        Display.transform.GetChild(CurrentHolo).gameObject.SetActive(false);

        CurrentHolo += 1;
        if (CurrentHolo >= Display.transform.childCount)
        {
        CurrentHolo = 0;
            }
         Display.transform.GetChild(CurrentHolo).gameObject.SetActive(true);
    
    }


    

    public void DisplayPrevious(){
        Display.transform.GetChild(CurrentHolo).gameObject.SetActive(false);

        CurrentHolo -= 1;
        if (CurrentHolo < 0)
        {
        CurrentHolo = Display.transform.childCount - 1;
            }
         Display.transform.GetChild(CurrentHolo).gameObject.SetActive(true);
    }


}
