using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(PhotoCaptureUtility))]
public class HoloPlacementMenu : MonoBehaviour
{
    // Parent of all holograms
    Transform holograms;
    
    // Used when taking photo
    public Transform photoMenu;
    // Used when not taking photo
    public Transform placementOptions;

    // Used to take photos
    PhotoCaptureUtility photoCaptureUtility;
    
    // The UIs for sampling and sitescreening holos
    public Transform samplingData;
    public Transform sitescreeningData;

    Texture2D photo;
    public TMP_InputField holoName;
    public TMP_InputField description;
    public TMP_Dropdown color;
    public TMP_Dropdown numberOfRocks;

    GameObject samplingHolo_GO;
    SamplingHolo samplingHolo;

    GameObject sitescreeningHolo_GO;
    SitescreeningHolo sitescreeningHolo;

    public enum HologramType { sampling, sitescreening };
    public HologramType hologramType = HologramType.sampling;

    private void Awake()
    {
        samplingHolo_GO = Resources.Load("Holo Placement/Sampling Hologram") as GameObject;
        sitescreeningHolo_GO = Resources.Load("Holo Placement/Sitescreening Hologram") as GameObject;
        photoCaptureUtility = gameObject.GetComponent<PhotoCaptureUtility>();
    }

    private void Start()
    {
        holograms = GameObject.FindGameObjectWithTag("MainMenuManager").GetComponent<MainMenuManager>().holograms;
        photoCaptureUtility.OnPhotoModeEnded += () => SetPhotoFrameActive(false);
    }

    // Places the current Holo directly in front of the user (at the point of HoloMenu)
    public void PlaceHolo()
    {
        Transform placedHolo;
        if (hologramType == HologramType.sampling)
        {
            samplingHolo = Instantiate(samplingHolo_GO).GetComponent<SamplingHolo>();
            samplingHolo.SetHoloData(holoName.text, color.options[color.value].text, numberOfRocks.options[numberOfRocks.value].text, photo);
            placedHolo = samplingHolo.transform;
        }
        else
        {
            sitescreeningHolo = Instantiate(sitescreeningHolo_GO).GetComponent<SitescreeningHolo>();
            sitescreeningHolo.SetHoloData(holoName.text, description.text, photo);
            placedHolo = sitescreeningHolo.transform;
        }
        placedHolo.position = transform.position;
        placedHolo.parent = holograms;
    }

    public void NextHolo()
    {
        if (hologramType == HologramType.sampling)
            hologramType = HologramType.sitescreening;
        else
            hologramType = HologramType.sampling;

        RefreshMenuData();
    }

    // Take a photo and save it to photo as Texture2D
    public void TakePhoto()
    {
        StartCoroutine(TakePhotoCor());
    }
    IEnumerator TakePhotoCor()
    {
        SetPhotoFrameActive(true);
        yield return new WaitForSeconds(2f);
        photo = photoCaptureUtility.TakePhoto();
    }

    // Refreshes the menu data to reflect the menu of the currently selected hologram. Must be called after calling NextHolo()
    private void RefreshMenuData()
    {
        if (hologramType == HologramType.sampling)
        {
            samplingData.gameObject.SetActive(true);
            sitescreeningData.gameObject.SetActive(false);
        }
        else
        {
            samplingData.gameObject.SetActive(false);
            sitescreeningData.gameObject.SetActive(true);
        }
    }

    // Enable/Disable the photo frame
    void SetPhotoFrameActive(bool state)
    {
        placementOptions.gameObject.SetActive(!state);
        photoMenu.gameObject.SetActive(state);
    }
}