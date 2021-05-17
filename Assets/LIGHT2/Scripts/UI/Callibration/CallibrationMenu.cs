using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(TapToPlace))]
public class CallibrationMenu : MonoBehaviour
{
    bool IsMenuActive { get { return menuCanvas.gameObject.activeSelf; } }
    public TMP_InputField input;
    public GameObject authObject;
    public GameObject menuObject;
    public string password = "lightlight";
    public Canvas menuCanvas;
    TapToPlace TTP;
    public GameObject ExhibitionOnject;

    private void Start()
    {
        TTP = GetComponent<TapToPlace>();
        TTP.OnPlacingStopped.AddListener(() => TTP.enabled = false);
    }

    public void OnClicked()
    {
        if (!IsMenuActive)
        {
            menuCanvas.gameObject.SetActive(true);
            input.text = "";
            authObject.SetActive(true);
            menuObject.SetActive(false);
        }
    }

    public void Auth()
    {
        if (input.text == password)
        {
            authObject.SetActive(false);
            menuObject.SetActive(true);
            EnableExhibition(true);
        }
    }   
    
    public void Close()
    {
        menuCanvas.gameObject.SetActive(false);
        EnableExhibition(false);
    }

    public void Move()
    {
        TTP.enabled = true;
        TTP.StartPlacement();
    }

    void EnableExhibition(bool value)
    {
        if (value == true)
            ExhibitionOnject.layer = LayerMask.NameToLayer("Default");
        else
            ExhibitionOnject.layer = LayerMask.NameToLayer("Wall");
    }
}
