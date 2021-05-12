using System.Collections;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    // Main Menu object to be activated ~ must be linked in Editor or null Exception is thrown
    public GameObject mainMenu = null;
    
    // bool to emulate a double click
    private bool clickedOnce = false;

    // allowed time between clicks for double click
    public float DoubleClickTime = 1f;

    // Parent of all holograms tagged as "Holograms"
    [HideInInspector]
    public Transform holograms;

    // Checks if a menu is active
    public bool ActiveMenu
    {
        get
        {
            return isMenuActive;
        }
        set
        {
            isMenuActive = value;
            holograms.gameObject.SetActive(!isMenuActive);
        }
    }
    private bool isMenuActive;

    private void Awake()
    {
        if (mainMenu == null)
        {
            throw new UnityException("GameObject MainMenu cannot be null");
        }
        holograms = GameObject.FindGameObjectWithTag("Holograms").transform;
    }
    // Sets the Main Menu to active if double clicked or enables double click if clicked once for 'DoubleClickTime'
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

    // Sets the clickedOnce bool to true for doubleClickTime seconds
    IEnumerator ClickedOnce()
    {
        clickedOnce = true;
        yield return new WaitForSeconds(DoubleClickTime);
        clickedOnce = false;
    }
}
