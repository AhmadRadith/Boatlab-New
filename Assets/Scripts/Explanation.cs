using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class Explanation : MonoBehaviour
{
    private Dictionary<string, Dictionary<string, string>> data;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            string path = Application.dataPath + "/Explanation/Explanation.json";

            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);
                data = JsonUtility.FromJson<Dictionary<string, Dictionary<string, string>>>(jsonString);
                foreach (var key in data.Keys)
                {
                    Debug.Log("Key: " + key);
                }
                /*
                // Accessing values inside data[0]
                string shortExplanation = data["digestive-system"]["shortexplanation"];
                string testValue = data["digestive-system"]["test"];

                Debug.Log("Short Explanation: " + shortExplanation);
                Debug.Log("Test Value: " + testValue);*/
            }
            else
            {
                Debug.LogError("File not found at path: " + path);
            }
        }
    }
}
