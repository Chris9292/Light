using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//To use this script add it to the playerArrow or any other obj and then
//add it to the recievers list in zoomIn and zoomOut Interactables with the following settings:
//in zoomIn and in the onPress event set the Must Scale property to 1
//in zoomOut and in the onPress event set the Must Scale property to -1
//in both quads in the onRelease event set the property to 0
public class AdjustScale : MonoBehaviour
{
	private Camera minimapCam;
	private float startSize = 0.0f;

	public Transform arrowStartScale;
	public float startArrowHeight;
	public Transform dynamicStartScale;
	public float startDynamicHeight;
	public Transform staticStartScale;
	public float startStaticHeight;


	private float arrowRatioyx;
	private float arrowRatioyz;
	private float dynamicRatioyx;
	private float dynamicRatioyz;
	private float staticRatioyx;
	private float staticRatioyz;


	private bool firstTime = true;
	public void Awake()
	{
		startArrowHeight = arrowStartScale.localScale.y;
		arrowRatioyx = arrowStartScale.localScale.y / arrowStartScale.localScale.x;
		arrowRatioyz = arrowStartScale.localScale.y / arrowStartScale.localScale.z;

		startDynamicHeight = dynamicStartScale.localScale.y;
		dynamicRatioyx = dynamicStartScale.localScale.y / dynamicStartScale.localScale.x;
		dynamicRatioyz = dynamicStartScale.localScale.y / dynamicStartScale.localScale.z;

		startStaticHeight = staticStartScale.localScale.y;
		staticRatioyx = staticStartScale.localScale.y / staticStartScale.localScale.x;
		staticRatioyz = staticStartScale.localScale.y / staticStartScale.localScale.z;
	}

	public void OnEnable()
	{
		minimapCam = GameObject.FindGameObjectWithTag("MiniMapCamera").GetComponent<Camera>();
	}

	void startCamSize()
	{
		startSize = (float)minimapCam.orthographicSize * 2.0f;
		firstTime = false;
	}
	public void Update()
	{
		if (firstTime)
			startCamSize();
		float percentage = (startSize - (float)minimapCam.orthographicSize*2.0f) / startSize;
		GameObject[] dynamicPathObjects = GameObject.FindGameObjectsWithTag("DynamicPathObject");
		foreach(var g in dynamicPathObjects)
		{
			var newDynamicHeight = startDynamicHeight * (1.0f - percentage);
			g.transform.localScale = new Vector3(newDynamicHeight / dynamicRatioyx,
				newDynamicHeight, newDynamicHeight / dynamicRatioyz);
		}
		GameObject[] staticPathObjects = GameObject.FindGameObjectsWithTag("StaticPathObject");
		foreach (var g in staticPathObjects)
		{
			var newStaticHeight = startStaticHeight * (1.0f - percentage);
			g.transform.localScale = new Vector3(newStaticHeight / staticRatioyx,
				newStaticHeight, newStaticHeight / staticRatioyz);
		}
		var newArrowHeight = startArrowHeight * (1.0f - percentage);
		arrowStartScale.gameObject.transform.localScale = new Vector3(newArrowHeight / arrowRatioyx,
			newArrowHeight, newArrowHeight / arrowRatioyz);
	}
}