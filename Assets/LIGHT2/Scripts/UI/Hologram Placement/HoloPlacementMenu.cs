using UnityEngine;
using TMPro;

public class HoloPlacementMenu : MonoBehaviour
{
    public PhotoCaptureUtility photoCaptureUtility;
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
    }

    // Places the current Holo directly in front of the user (at the point of HoloMenu)
    public void PlaceHolo()
    {
        if (hologramType == HologramType.sampling)
        {
            samplingHolo = Instantiate(samplingHolo_GO).GetComponent<SamplingHolo>();
            samplingHolo.SetHoloData(holoName.text, color.options[color.value].text, numberOfRocks.options[numberOfRocks.value].text, photo);
            samplingHolo.transform.position = transform.position;
        }
        else
        {
            sitescreeningHolo = Instantiate(sitescreeningHolo_GO).GetComponent<SitescreeningHolo>();
            sitescreeningHolo.SetHoloData(holoName.text, description.text, photo);
            sitescreeningHolo.transform.position = transform.position;
        }
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
}
