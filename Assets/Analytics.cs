using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Analytics : MonoBehaviour
{
    public Dictionary<string, float> dic;
    private bool count = true;
    private float tp1 = 0.0f,tp2 = 0.0f;
    private string path;
    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "\\Analytics.txt";
        dic = new Dictionary<string, float>();
        Transform t = this.gameObject.transform;
        for (int i = 0; i < 6;i ++)
        {
            dic.Add(t.GetChild(i).name, 0.0f);
		}
    }

    public void Point(GameObject go)
	{
        if (count)
        {
            tp1 = Time.time;
            if (!dic.ContainsKey(go.name))
            {
                dic.Add(go.name, 0.0f);
            }
            count = false;
        }
		else 
        {
            tp2 = Time.time;
			dic[go.name] += tp2 - tp1;
            printResults();
            count = true;
        }
	}

    // Update is called once per frame
    void printResults()
    {
        string s = "",s2="";
        s += "Menu and time spent on them\n\n";
        foreach(var line in dic)
		{
            s2 = line.Key.ToString() + "-->\t" + line.Value.ToString() + " seconds\n";
            s+=s2;
        }
        Debug.Log(s);
        File.WriteAllText(path, s);
    }
}
