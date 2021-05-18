using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
public class API_Response
{
    public int id { get; set; }
    public string name { get; set; }
    public double value { get; set; }
    public DateTime date { get; set; }
}
