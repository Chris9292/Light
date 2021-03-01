using UnityEngine;

public class HoloPlacementMenu : MonoBehaviour
{
    public Texture2D photo;
    public string holoName;
    public string description;
    public string color;
    public string numberOfRocks;

    GameObject samplingHolo_GO;
    SamplingHolo samplingHolo;
    public enum HologramType { sampling, sitescreening };
    public HologramType hologramType = HologramType.sampling;

    private void Awake()
    {
        samplingHolo_GO = Resources.Load("Sampling Hologram") as GameObject;
        samplingHolo = Instantiate(samplingHolo_GO).GetComponent<SamplingHolo>();
        samplingHolo.SetHoloData(holoName, color, numberOfRocks, photo);
    }

    public void Place()
    {
        if (hologramType == HologramType.sampling)
        {
            //SamplingHolo
        }
    }

    public void NextHolo()
    {
        if (hologramType == HologramType.sampling)
            hologramType = HologramType.sitescreening;
        else
            hologramType = HologramType.sampling;
    }
}
