using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimateNotification : MonoBehaviour
{
    public Image BackGroundImage;
    public  TextMeshProUGUI text;
    public float fadeInTime = 2f;
    public float letterwait = 0.2f;
    public float fadeOutTime = 1.5f;


    private bool destroy = false;
    /*
     * public GameObject prefab;
    private Image img;
    private TextMeshProUGUI txt;
    private GameObject refere;

    void Start()
    {
        refere = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        img = refere.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>();
        txt = refere.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        img.color = Color.red;
        txt.text = "se gamaw";
    }
     * */
 //   public void CreateNotification(string Notifi)
	//{
 //       NotiHandler = Instantiate(NotiPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
 //       NotiHandler.transform.parent = GameObject.Find("NotificationMenu").transform;
 //       BackGroundImage = NotiHandler.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>();
 //       text = NotiHandler.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
 //       text.text = Notifi;
 //       NotiHandler.GetComponent<AnimateNotification>().AnimateEnable();
 //   }

 //   public void DestroyNotification()
	//{
 //       NotiHandler.GetComponent<AnimateNotification>().AnimateDisable();
 //       Destroy(NotiHandler);
	//}

    public void AnimateEnable(string NotiText)
	{
        BackGroundImage.fillAmount = 0.0f;
        BackGroundImage.enabled = false;
        text.enabled = false;
        BackGroundImage.enabled = true;
        text.enabled = true;
        BackGroundImage.color = new Color(BackGroundImage.color.r, BackGroundImage.color.g, BackGroundImage.color.b, 1.0f);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1.0f);
        text.text = NotiText;
        StartCoroutine(slideInImage());
	}

    public IEnumerator AnimateDisable()
	{
        BackGroundImage.CrossFadeAlpha(0, fadeOutTime, false);
        text.CrossFadeAlpha(0, fadeOutTime, false);
        yield return new WaitForSeconds(fadeOutTime);
        destroy = true;
	}


	public void Update()
	{
		if(destroy)
		{
            Destroy(this.gameObject);
		}
	}
	private IEnumerator slideInImage()
	{
        BackGroundImage.fillAmount = 0.0f;
        string textToBeDisplayed = text.text;
        text.text = "";
        while (BackGroundImage.fillAmount < 1.0f)
		{
            BackGroundImage.fillAmount += 0.01f;
            yield return new WaitForSeconds(fadeInTime/100.0f);
        }

        foreach (char letter in textToBeDisplayed.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(letterwait);
        }
    }

    
}
