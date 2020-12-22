using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;


public class MapTexturePointerHandler : MonoBehaviour, IMixedRealityPointerHandler
{
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        Debug.Log(eventData.Pointer.Position);
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        //Debug.Log("I was dragged here");
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {

    }

    private void Update()
    {
        
    }
}
