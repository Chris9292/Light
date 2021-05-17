using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Analytics : MonoBehaviour
{
    //The script creates a folder sessions where there are stored analytics text files containing the time spent on each 
    //of the sub-menus
    //It also creates a analytics in total text file containing the times of all the sessions added


    public Dictionary<string, float> dic;   //key: name of submenu  value:time spent on it
    //bool indicating if the clock should start or stop counting
    private bool count = true;
    //variables presenting the timepoints when the user opens and closes a menu
    private float tp1 = 0.0f,tp2 = 0.0f;
    //path of the sessions folder
    private string path;
    void Start()
    {
        path = Application.dataPath + "\\Sessions";
        System.IO.Directory.CreateDirectory(path);
        //loop through the analyticsX.txt files where X is an increasing integer to find the one that it is not created yet
        for(int i = 1; ;i++)
        {
            path = Application.dataPath + "\\Sessions" + "\\Analytics" + i.ToString() + ".txt";
            if (!File.Exists(path))
                break;
		}
        dic = new Dictionary<string, float>();
        Transform t = this.gameObject.transform;
        //add the names of the submenus in the order that the children appear in the editor
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

    void printResults()
    {
        string s = "";
        s += "Menus and time spent on them\n\n";
        foreach(var line in dic)
		{
            s += "Time spent in " + line.Key.ToString() + " in seconds\n" + line.Value.ToString() + "\n";
        }
        Debug.Log(s);
        File.WriteAllText(path, s);
    }

	public void OnDestroy()
	{
        //if the file doesn't exist create one and copy the session that was just created
        if (!File.Exists(Application.dataPath + "\\AnalyticsInTotal.txt"))
		{
            File.Copy(path, Application.dataPath + "\\AnalyticsInTotal.txt");
		}
		else
        {
            //get the file contents in a string list
            StreamReader sr = new StreamReader(Application.dataPath + "\\AnalyticsInTotal.txt");
            List<string> lines = new List<string>();
            string line;
            do
            {
                line = sr.ReadLine();
                lines.Add(line);
            } while (line != null);
            sr.Close();
            string s = "";
            s += "Menus and time spent on them\n\n";
            //i indicates the exact line on the file where the value is stored
            int i = 3;
            foreach (var l in dic)
            {
                s += "Time spent in " + l.Key.ToString() + " in seconds\n" + (l.Value + float.Parse(lines[i])).ToString() + "\n";
                //increase i by 2 to go to the next line containing a value
                i += 2;
            }
            File.WriteAllText(Application.dataPath + "\\AnalyticsInTotal.txt", s);
        }
	}
}
