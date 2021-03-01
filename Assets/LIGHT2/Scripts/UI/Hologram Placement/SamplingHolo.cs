using UnityEngine;
using TMPro;

public class SamplingHolo : MonoBehaviour
{
    public TMP_Text holoName_TMP;
    public TMP_Text color_TMP;
    public TMP_Text numberOfRocks_TMP;
    public Renderer photo_rend;

    public void SetHoloData(string holoName, string color, string numberOfRocks, Texture2D photo)
    {
        holoName_TMP.text = holoName;
        color_TMP.text = "Color: " + color;
        numberOfRocks_TMP.text = "Number of Rocks: " + numberOfRocks;
        photo_rend.material.SetTexture("_MainTex", photo);
    }
}
