using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloPlacementMenu : MonoBehaviour
{
    public Texture2D photo;
    public string holoName;
    public string description;
    public string color;
    public string numberOfRocks;

    GameObject samplingHolo;
    public GameObject SamplingHolo;
    public enum HologramType { sampling, sitescreening };
    public HologramType hologramType = HologramType.sampling;

    private void Awake()
    {
        samplingHolo = Resources.Load("Sampling Hologram") as GameObject;
        SamplingHolo = Instantiate(samplingHolo);
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
