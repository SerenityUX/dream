using UnityEngine;
using TMPro; // Namespace for TextMeshPro

public class ScoreDisplay : MonoBehaviour
{
    public int Score = 0; // Public variable to hold the score
    public TextMeshPro textMesh; // Reference to TextMeshPro component

    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        // Optionally, update the text in real-time or based on certain conditions
        // For example, this could be updated when the player earns points
    }

    // Method to update the TextMeshPro text with the current score
    public void UpdateScoreDisplay()
    {
        if (textMesh != null) // Check if the TextMeshPro component is assigned
        {
            textMesh.text = Score.ToString(); // Update the display text
        }
    }

    // Example method to increase score and update display
    public void IncreaseScore(int points)
    {
        Score += points;
        UpdateScoreDisplay();
    }
    // Example method to increase score and update display
    public void SetScore(int points)
    {
        Score = points;
        UpdateScoreDisplay();
    }
}
