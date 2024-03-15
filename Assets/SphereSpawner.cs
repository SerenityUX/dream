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
    public GameObject cameraPrefab; // Reference to the camera prefab
    private List<SphereData> spheres = new List<SphereData>();
    private GameObject cameraInstance;

    void Start()
    {
        // Instantiate the camera prefab at the start
        cameraInstance = Instantiate(cameraPrefab, new Vector3(0, 0, -10), Quaternion.identity);

        StartCoroutine(SpawnSphereEverySecond());
    }

    IEnumerator SpawnSphereEverySecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second
            SpawnSphere();
        }
    }

    void SpawnSphere()
    {
        // Use the camera instance's position and rotation to spawn spheres
        GameObject sphere = Instantiate(spherePrefab, cameraInstance.transform.position, cameraInstance.transform.rotation);
        SphereData data = new SphereData
        {
            position = sphere.transform.position,
            creationTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
        };
        spheres.Add(data);

        // Log the JSON representation of the sphere's data
        Debug.Log(JsonUtility.ToJson(data));
    }

    void SaveSpheresToJson()
    {
        string json = JsonUtility.ToJson(new { spheres = spheres }, true);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/spheres.json", json);
    }

    void OnApplicationQuit()
    {
        SaveSpheresToJson(); // Save when application quits. Adjust as needed.
    }
}
