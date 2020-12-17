using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private bool clickedOnce = false;
    public float doubleClickTime = 1f;
    public GameObject mainMenu = null;

    private void Awake()
    {
        if (mainMenu == null)
        {
            throw new UnityException("GameObject MainMenu cannot be null");
        }    
    }

    public void OpenMainMenu()
    {
        if (clickedOnce == true)
        {
            mainMenu.SetActive(true);
        }
        else
        {
            StartCoroutine(ClickedOnce());
        }
    }

    IEnumerator ClickedOnce()
    {
        clickedOnce = true;
        yield return new WaitForSeconds(doubleClickTime);
        clickedOnce = false;
    }
}
