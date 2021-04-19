using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Responder : MonoBehaviour
{
    public TMP_Text tm = null;
    public Communication comm;

    public void ResponseToUDPPacket(string incomingIP, string incomingPort, byte[] data)
    {

        if (tm != null)
            tm.text = System.Text.Encoding.UTF8.GetString(data);
        

#if !UNITY_EDITOR

        //ECHO 
        //Communication comm = Communication.Instance;
        comm.SendUDPMessage(incomingIP, comm.externalPort, data);

#endif
    }
}