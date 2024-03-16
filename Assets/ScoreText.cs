using UnityEngine;
using TMPro; // Namespace for TextMeshPro

public class ScoreDisplay : MonoBehaviour
{
    private GameStateSync _gameStateSync; // Reference to the ScoreDisplay component

    public TextMeshPro textMesh; // Reference to TextMeshPro component

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

    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = _gameStateSync.GetPlayerPoints()
.ToString();
    }

    // Method to update the TextMeshPro text with the current score
    public void UpdateScoreDisplay()
    {
        if (textMesh != null) // Check if the TextMeshPro component is assigned
        {
            textMesh.text = _gameStateSync.GetPlayerPoints()
.ToString(); // Update the display text
        }
    }

    //// Example method to increase score and update display
    //public void IncreaseScore(int points)
    //{
    //    Score += points;
    //    UpdateScoreDisplay();
    //}
    //// Example method to increase score and update display
    //public void SetScore(int points)
    //{
    //    Score = points;
    //    UpdateScoreDisplay();
    //}
}
