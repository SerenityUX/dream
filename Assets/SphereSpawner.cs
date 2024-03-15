using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SphereData
{
    public Vector3 position;
    public string creationTime;
}

public class SphereSpawner : MonoBehaviour
{
    public GameObject spherePrefab;
    private List<SphereData> spheres = new List<SphereData>();
    private bool isRecording = false; // Flag to control recording

    void Start()
    {
        // Optionally, start recording automatically on start
        // isRecording = true;
        // StartCoroutine(SpawnSphereEverySecond());
    }

    void Update()
    {
        // Toggle recording on user input
        if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Space))
        {
            isRecording = !isRecording; // Toggle the recording state

            if (isRecording)
            {
                // If starting to record, begin spawning spheres every second
                StartCoroutine(SpawnSphereEverySecond());
            }
            // If stopping, you might want to stop the coroutine, but stopping a specific coroutine requires storing its reference
            // Alternatively, modify the SpawnSphereEverySecond to check isRecording state
        }
    }

    IEnumerator SpawnSphereEverySecond()
    {
        while (isRecording)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second
            SpawnSphere();
            // You could also check isRecording inside here for additional control
        }
    }

    void SpawnSphere()
    {
        if (isRecording && Camera.main != null)
        {
            Vector3 cameraPosition = Camera.main.transform.position;
            Quaternion cameraRotation = Camera.main.transform.rotation;

            GameObject sphere = Instantiate(spherePrefab, cameraPosition, cameraRotation);
            SphereData data = new SphereData
            {
                position = sphere.transform.position,
                creationTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
            };
            spheres.Add(data);

            Debug.Log(JsonUtility.ToJson(data));
        }
    }

    void SaveSpheresToJson()
    {
        string json = JsonUtility.ToJson(new { spheres = spheres }, true);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/spheres.json", json);
    }

    void OnApplicationQuit()
    {
        SaveSpheresToJson();
    }
}
