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
    public GameObject coinPrefab; // Assign your coin prefab in the inspector
    public float coinSpacing = 0.1f; // Distance between coins

    public GameObject spherePrefab;
    private List<SphereData> spheres = new List<SphereData>();
    private bool isRecording = false; // Flag to control recording

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
                    Instantiate(coinPrefab, coinPosition, Quaternion.identity);
                }

            }
        }
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        PathConnector PathConnector = GetComponent<PathConnector>();

        if (lineRenderer != null)
        {
            Destroy(PathConnector);

            Destroy(lineRenderer);
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
