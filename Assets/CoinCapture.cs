using Normal.Realtime; // Normcore namespace
using UnityEngine;
public class CoinCapture : MonoBehaviour
{
    
    private GameStateSync _gameStateSync; // Reference to the ScoreDisplay component

    

    private ScoreDisplay scoreDisplay; // Reference to the ScoreDisplay component
    private RealtimeView _realtimeView; // Used to access the Realtime component
    public float proximityDistance = 1f; // Distance within which the object will be destroyed

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

    void Start()
    {



        // Find the GameObject with the "scoreText" tag and get the ScoreDisplay component from it
        GameObject scoreTextGameObject = GameObject.FindGameObjectWithTag("scoreText");
        if (scoreTextGameObject != null)
        {
            scoreDisplay = scoreTextGameObject.GetComponent<ScoreDisplay>();
            if (scoreDisplay == null)
            {
                Debug.LogError("ScoreDisplay component not found on the object with 'scoreText' tag.");
            }
        }
        else
        {
            Debug.LogError("'scoreText' tagged object not found. Ensure it's tagged correctly in the scene.");
        }
        // Get the RealtimeView component attached to the object
        _realtimeView = GetComponent<RealtimeView>();
        if (_realtimeView == null)
        {
            Debug.LogError("RealtimeView component not found. Ensure it's attached to the object.");
        }
    }
    void Update()
    {
        // Find the GameObject tagged as MainCamera (usually the player)
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCamera != null)
        {
            float distance = Vector3.Distance(mainCamera.transform.position, transform.position);
            // Check if the MainCamera is within the specified proximity
            if (distance <= proximityDistance)
            {
                AttemptDestruction();
            }
        }
    }
    public void AttemptDestruction()
    {
        if (_realtimeView.isOwnedLocallyInHierarchy)
        {
            // The client already owns the object, proceed with destruction
            DestroyObject();
        }
        else
        {
            // Request ownership before destruction
            _realtimeView.RequestOwnership();
            // Listen for ownership changes
            _realtimeView.ownerIDSelfDidChange += OwnershipChanged;
        }
    }
    private void OwnershipChanged(RealtimeView realtimeView, int previousOwnerID)
    {
        // Check again if this client now owns the object
        if (_realtimeView.isOwnedLocallyInHierarchy)
        {
            // Proceed with destruction as the client now has ownership
            DestroyObject();
        }
        // Unsubscribe from the event to avoid potential memory leaks or unwanted behavior
        _realtimeView.ownerIDSelfDidChange -= OwnershipChanged;
    }
    private void DestroyObject()
    {
        _gameStateSync.AddPointsToPlayer(1);
        //Debug.Log(_gameStateSync.GetPlayerPoints());
        //scoreDisplay.IncreaseScore(1); // Increase the score using ScoreDisplay component
        // Destroy the GameObject this script is attached to
        Realtime.Destroy(gameObject); // Use Realtime.Destroy to properly handle networked object destruction
    }
}




