using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationSpawner : MonoBehaviour
{
    public GameObject NotiPrefab;
    private GameObject NotiHandler;
    public void CreateNotification(string Notifi)
    {
        NotiHandler = Instantiate(NotiPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        NotiHandler.transform.parent = GameObject.Find("NotificationMenu").transform;
        NotiHandler.GetComponent<AnimateNotification>().AnimateEnable(Notifi);
    }

    public void DestoyNotification()
    {
        StartCoroutine(NotiHandler.GetComponent<AnimateNotification>().AnimateDisable());
    }
}
