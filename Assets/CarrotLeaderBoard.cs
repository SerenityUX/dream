using UnityEngine;
using UnityEngine.UI; // Add this line
using TMPro;
using System.Collections.Generic;
using System;


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
    private void Start()
    {
        // Check and cache TextMeshProUGUI components
        for (int i = 0; i < gamePropertiesArray.Length; i++)
        {
            if (gamePropertiesArray[i].name != null)
                gamePropertiesArray[i].name.GetComponent<TextMeshPro>(); // Cache this if needed

            if (gamePropertiesArray[i].score != null)
                gamePropertiesArray[i].score.GetComponent<TextMeshPro>(); // Cache this if needed
        }
    }

    private void Update()
    {
        try
        {
            List<Tuple<string, int>> sortedPlayerStates = _gameStateSync.GetPlayerNamesAndPoints();

            for (int i = 0; i < gamePropertiesArray.Length; i++)
            {
                var nameText = gamePropertiesArray[i]?.name?.GetComponent<TextMeshPro>();
                var scoreText = gamePropertiesArray[i]?.score?.GetComponent<TextMeshPro>();

                if (i < sortedPlayerStates.Count)
                {
                    if (nameText != null)
                        nameText.text = sortedPlayerStates[i].Item1;
                    else
                        nameText.text = "";

                    if (scoreText != null)
                        scoreText.text = sortedPlayerStates[i].Item2.ToString();
                    else
                        scoreText.text = "";

                    if (gamePropertiesArray[i]?.carrotIcon != null)
                        gamePropertiesArray[i].carrotIcon.SetActive(true);
                }
                else
                {
                    if (nameText != null)
                        nameText.text = "";

                    if (scoreText != null)
                        scoreText.text = "";

                    if (gamePropertiesArray[i]?.carrotIcon != null)
                        gamePropertiesArray[i].carrotIcon.SetActive(false);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log($"Error in Update method: {e.Message}");
        }
    }
}
