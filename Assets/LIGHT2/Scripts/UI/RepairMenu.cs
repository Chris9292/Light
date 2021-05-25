using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RepairMenu : MonoBehaviour
{
    Animator repairAnim;
    public GameObject mainMenuVol2;

    void Awake()
    {
        repairAnim = GetComponent<Animator>();
    }

    public void NextAnim()
    {
        repairAnim.SetTrigger("NextAnim");
    }    

    public void PreviousAnim()
    {
        repairAnim.SetTrigger("PreviousAnim");
    }    

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        repairAnim.SetTrigger("Reset");
    }

    // Close this if we open the main menu 
    private void Update()
    {
        if (mainMenuVol2.activeSelf)
        {
            CloseMenu();

            // black magic bug fixing stuff to turn Holograms GO off
            mainMenuVol2.SetActive(false);
            mainMenuVol2.SetActive(true);
        }
    }
}