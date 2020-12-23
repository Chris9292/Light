using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myTest2 : MonoBehaviour, IMixedRealityPointerHandler
{
    private GameObject MainMenu = null;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "MainMenu")
            {
                MainMenu = child.gameObject;
                break;
            }
        }
        if (MainMenu == null)
        {
            throw new UnityException("No object with MainMenu tag found");
        }
    }

    public void OnPointerClicked(MixedRealityPointerEventData evenData)
    {
        var result = evenData.Pointer.Result;

        if (!MainMenu.activeSelf)
        {
            Vector3 spawnPosition = result.Details.Point;
            MainMenu.transform.position = spawnPosition;
            MainMenu.SetActive(true);
        }
        else
        {
            MainMenu.SetActive(false);
        }
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
    }

    private void CreateSphere(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        //spawnPosition.z += 1;
        Texture2D photo = Resources.Load<Texture2D>("chart");

        // Create a GameObject to which the texture can be applied
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Renderer quadRenderer = sphere.GetComponent<Renderer>() as Renderer;
        quadRenderer.material = new Material(Shader.Find("Mixed Reality Toolkit/Standard"));
        quadRenderer.material.SetTexture("_MainTex", photo);

        sphere.transform.localPosition = spawnPosition;
        sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    //  Places cubes everywhere!
    /*
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        Debug.Log("HI!");
        var result = eventData.Pointer.Result;
        var spawnPosition = result.Details.Point;
        var spawnRotation = Quaternion.LookRotation(result.Details.Normal);
        CreateSphere(spawnPosition, spawnRotation);
    }
    */
}
