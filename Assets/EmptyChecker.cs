using System.Collections;
using UnityEngine;

public class CarrotChecker : MonoBehaviour
{
    public GameObject objectToActivate; // Public variable to assign in the Inspector
    private bool carrotsHaveSpawned = false;
    private bool isChecking = true;

    private void Start()
    {
        // Start the coroutine to check for carrots
        StartCoroutine(CheckForCarrots());
    }

    private IEnumerator CheckForCarrots()
    {
        // Loop as long as isChecking is true
        while (isChecking)
        {
            // Wait for 1 second before each check
            yield return new WaitForSeconds(1f);

            // Check if any game objects with the tag "Carrot" exist
            GameObject[] carrots = GameObject.FindGameObjectsWithTag("Carrot");

            // If carrots exist, set carrotsHaveSpawned to true
            if (carrots.Length > 0)
            {
                carrotsHaveSpawned = true;
            }

            // If carrots have spawned and no carrots are currently found
            if (carrotsHaveSpawned && carrots.Length == 0)
            {
                // All carrots have been cleared
                OnAllCarrotsCleared();
                // Optional: Stop checking if you only need to detect this once
                isChecking = false;
            }
        }
    }



    private void OnAllCarrotsCleared()
    {
        // Disable all game objects with the tag "HUD"
        GameObject[] hudObjects = GameObject.FindGameObjectsWithTag("HUD");
        foreach (GameObject hudObject in hudObjects)
        {
            hudObject.SetActive(false);
        }
        Debug.Log("All HUD elements have been disabled.");

        // Activate the assigned GameObject
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
            Debug.Log($"{objectToActivate.name} has been activated.");
        }
        else
        {
            Debug.LogWarning("Object to activate not assigned.");
        }
    }
}
