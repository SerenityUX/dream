using System;
using UnityEngine;

// Define a class to hold the properties for each game object
[Serializable]
public class GameProperties
{
    public GameObject name;
    public GameObject carrotIcon;
    public GameObject score;
}

public class CarrotLeaderBoard : MonoBehaviour
{


    // Public array to hold 5 GameProperties objects
    public GameProperties[] gamePropertiesArray = new GameProperties[4];

    private GameStateSync _gameStateSync; // Reference to the ScoreDisplay component

    private void Awake()
    {

        // Find the game object with the tag "sync"
        GameObject syncGameObject = GameObject.FindWithTag("sync");

        // Ensure the game object was found
        if (syncGameObject != null)
        {
            // Get the GameStateSync component from the found game object
            _gameStateSync = syncGameObject.GetComponent<GameStateSync>();
        }
        else
        {
            Debug.LogError("GameObject with tag 'sync' not found.");
        }


    }

    private void Update()
    {
        Debug.Log("states:");
        Debug.Log(_gameStateSync.GetAllPlayerStates());
    }


}
