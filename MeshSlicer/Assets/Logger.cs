using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Logger : MonoBehaviour
{
    // Start is called before the first frame update
    float timeLastFrame;
    void Start()
    {
        //Initialize our timeLastFrame variable
        timeLastFrame = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    List<float> frameTimes = new List<float>();
    float realDeltaTime;
    void Update()
    {
        realDeltaTime = Time.realtimeSinceStartup - timeLastFrame;
        timeLastFrame = Time.realtimeSinceStartup;

        if (Input.GetKey(KeyCode.P))
        {
            frameTimes.Add(realDeltaTime);
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            List<string> tooutput = new List<string>();
            for (int i =0; i < frameTimes.Count; i++)
            {
                tooutput.Add(i + "," + frameTimes[i]);
            }
            File.WriteAllLines(@"D:\dump.csv", tooutput);
            Debug.Log("WRITTEN LOG");
            frameTimes.Clear();
        }
    }
}
