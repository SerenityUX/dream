using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Normal.Realtime;
using UnityEngine.Networking;

[Serializable]
public class SphereData
{
    public Vector3 position;
    public string creationTime;
}

[Serializable]
public class SpheresCollection
{
    public List<SphereData> spheres = new List<SphereData>();
}

public class SphereSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // Assign your coin prefab in the inspector
    public float coinSpacing = 1f; // Distance between coins

    public GameObject spherePrefab;
    private List<SphereData> spheres = new List<SphereData>();
    private bool isRecording = false; // Flag to control recording

    private GameStateSync _gameStateSync; // Reference to the ScoreDisplay component

    private void Awake()
    {
        _gameStateSync = GetComponent<GameStateSync>();

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
            // Consider adding code here to stop the coroutine if isRecording is false
        }

        // Check if KeyCode.JoystickButton1 (B Button) is pressed
        if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.G))
        {
            GenerateCoins(); // Call the GenerateCoins function
        }
    }

    IEnumerator SpawnSphereEverySecond()
    {
        while (isRecording)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second
            if (Camera.main != null) // Ensure there's a camera to use
            {
                SpawnSphere(Camera.main); // Pass the camera as a parameter
            }
        }
    }

    void SpawnSphere(Camera camera)
    {
        if (isRecording && camera != null)
        {
            Vector3 cameraPosition = camera.transform.position;
            Quaternion cameraRotation = camera.transform.rotation;

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
    void GenerateCoins()
    {
        StartCoroutine(SendSpheresData());


        _gameStateSync.AddPointsToPlayer(1);
        Debug.Log("Generate coins");
        GameObject[] pathPoints = GameObject.FindGameObjectsWithTag("Path");

        // Optionally, sort pathPoints by some criteria (e.g., name or position)
        // System.Array.Sort(pathPoints, (p1, p2) => string.Compare(p1.name, p2.name));

        for (int i = 0; i < pathPoints.Length - 1; i++)
        {
            Vector3 start = pathPoints[i].transform.position;
            Vector3 end = pathPoints[i + 1].transform.position;
            float distance = Vector3.Distance(start, end);
            Vector3 direction = (end - start).normalized;

            // Calculate how many coins can fit between these points
            int coinsCount = Mathf.FloorToInt(distance / coinSpacing);
            for (int j = 0; j < coinsCount; j++)
            {
                // Calculate the position for each coin
                Vector3 coinPosition = start + direction * (j + 1) * coinSpacing;

                // Offset the position 1 unit down in the y-direction
                coinPosition.y -= 0.4f;

                float chance = UnityEngine.Random.Range(0.0f, 1.0f);

                // Only instantiate the coin if the random number is less than 0.6
                if (chance < 0.6f)
                {
                    GameObject coin = Realtime.Instantiate("Carrot", coinPosition, Quaternion.identity);

                    // Find the parent GameObject with the "realTime" tag
                    GameObject realTimeParent = GameObject.FindGameObjectWithTag("realTime");
                    if (realTimeParent != null)
                    {
                        // Set the coin's parent to the found GameObject
                        coin.transform.SetParent(realTimeParent.transform);
                    }
                    else
                    {
                        Debug.LogWarning("No GameObject found with the 'realTime' tag. Coin instantiated without a parent.");
                    }
                }
            }
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

    IEnumerator SendSpheresData()
    {
        SpheresCollection collection = new SpheresCollection { spheres = this.spheres };

        // Randomly generate a name consisting of 4 digits
        string name = UnityEngine.Random.Range(1000, 9999).ToString();

        // Serialize the data to JSON
        string jsonData = JsonUtility.ToJson(new { name = name, path = collection });

        // Create a UnityWebRequest to POST the JSON data
        using (UnityWebRequest www = UnityWebRequest.PostWwwForm("https://x8ki-letl-twmt.n7.xano.io/api:7v6zckRK/paths", jsonData))
        {
            www.SetRequestHeader("Content-Type", "application/json");

            // Send the request and wait for a response
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error sending spheres data: " + www.error);
            }
            else
            {
                Debug.Log("Successfully sent spheres data. Response: " + www.downloadHandler.text);
            }
        }
    }
}
