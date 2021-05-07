using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePhoto : MonoBehaviour
{
	// Start is called before the first frame update
	
	public float timeOfEffect = 1.5f;
	private float timeElapsed = 0.0f;

	private int scale = 0;
	private bool scaleUp = true;

	private Vector3 finalScale;
	private Vector3 finalPos;
	private Transform t;
	private Vector3 startPos;
	private Vector3 curScale;
	private Vector3 startingScale;
	private Vector3 pivot;
	private Vector3 pointC;
	private Vector3 newScale;
	private float RS;

	private Outline o;

	public void OnEnable()
	{
		t = GetComponent<Transform>();
		o = GetComponent<Outline>();
		finalScale = new Vector3(70, 39, 20);
		curScale = t.localScale;
		startPos = t.localPosition;
		startingScale = curScale;
	}


	public void Scale()
	{
		if (scaleUp)
		{
			scale = 1;
			scaleUp = false;
			o.OutlineMode = Outline.Mode.OutlineAll;
		}
		else
		{
			scale = -1;
			scaleUp = true;
			o.OutlineMode = Outline.Mode.OutlineHidden;
		}
	}

	public void Update()
	{
		if (scale != 0)
		{
			if (timeElapsed < timeOfEffect)
			{
				var lerp = Vector3.Lerp(startingScale, finalScale, timeElapsed / timeOfEffect);
				var z = Mathf.Lerp(0.0f, 12.0f, timeElapsed / timeOfEffect);
				newScale = (scale == 1) ? lerp : (finalScale - lerp + startingScale);
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
				scale = 0;
				timeElapsed = 0.0f;
			}
		}
	}
}
