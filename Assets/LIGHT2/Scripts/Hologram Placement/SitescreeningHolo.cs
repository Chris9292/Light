using UnityEngine;
using TMPro;

public class SitescreeningHolo : MonoBehaviour
{
    public TMP_Text holoName_TMP;
    public TMP_Text description_TMP;
    public Renderer photo_rend;

    public void SetHoloData(string holoName, string description, Texture2D photo)
    {
        holoName_TMP.text = holoName;
        description_TMP.text = description;
        photo_rend.material.SetTexture("_MainTex", photo);
    }
}
