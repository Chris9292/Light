using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePhoto : MonoBehaviour
{
	//The script implements a scale around pivot function to scale the photo around the lower left corner of the object
	//Clicks on the photo call the scale function via the editor
	
	//variables controlling the effect of the scale
	public float timeOfEffect = 1.5f;
	private float timeElapsed = 0.0f;


	private int scale = 0;		//0 when idle -1 when scaling down 1 when scaling up
	private bool scaleUp = true;	//true when it's about to scale up false when it's about to scale down
	private bool scaling = false;	//bool disabling the scaling function while the effect is happening

	public Vector3 finalScale;	//final scale of the photo; exposed in the editor
	private Vector3 finalPos;	//the final position of the object after the calculations
	private Transform t;
	private Vector3 startPos;
	private Vector3 curScale;
	private Vector3 startingScale;

	private Vector3 pivot;	//the pivot point around which the scale will happen
	private Vector3 pointC; //relative pos betwn the object and the pivot
	private Vector3 newScale;
	private float RS;	//relative scale

	private Outline o;

	public void OnEnable()
	{
		//Get the transform to make changes to the photo object
		t = GetComponent<Transform>();
		//Get the outline script to enable it disable it
		o = GetComponent<Outline>();
		//previous finalScale value; now it is set in the editor
		//finalScale = new Vector3(70, 39, 20);
		//hold the starting values
		curScale = t.localScale;
		startPos = t.localPosition;
		startingScale = curScale;
	}


	public void Scale()
	{
		if (scaleUp)
		{
			if (!scaling)
			{
				scale = 1;
				scaleUp = false;
				o.OutlineMode = Outline.Mode.OutlineAll;
			}
		}
		else
		{
			if (!scaling)
			{
				scale = -1;
				scaleUp = true;
				o.OutlineMode = Outline.Mode.OutlineHidden;
			}
		}
	}

	public void Update()
	{
		if (scale != 0)
		{
			if (timeElapsed < timeOfEffect)
			{
				scaling = true;	//disable the ability to change the scale state
				//lerping between the starting scale and the final scale,position
				var lerp = Vector3.Lerp(startingScale, finalScale, timeElapsed / timeOfEffect);
				var z = Mathf.Lerp(0.0f, 13.0f, timeElapsed / timeOfEffect);
				//if scale == -1 then scale down from the final scale to the starting one
				newScale = (scale == 1) ? lerp : (finalScale - lerp + startingScale);
				//calculations to perform the scale around pivot
				pivot = new Vector3(startPos.x - (curScale.x / 2), startPos.y - (curScale.y / 2), -20.0f);
				pointC = startPos - pivot;
				RS = newScale.x / curScale.x;
				finalPos = pivot + (pointC * RS);
				t.localScale = newScale;
				t.localPosition = new Vector3(finalPos.x, finalPos.y, (scale == 1) ? -z : -10.0f + z);
				startPos = finalPos;
				curScale = newScale;
				timeElapsed += Time.deltaTime;
			}
			else
			{
				t.localScale = (scale == 1) ? finalScale : startingScale;		//fix the final scale
				scale = 0;	//idle state
				timeElapsed = 0.0f;
				scaling = false;
			}
		}
	}
}
