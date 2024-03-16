using Normal.Realtime; // Normcore namespace
using UnityEngine;

public class CoinCapture : MonoBehaviour
<<<<<<< HEAD
{

    private ScoreDisplay scoreDisplay; // Reference to the ScoreDisplay component
    private RealtimeView _realtimeView; // Used to access the Realtime component
    public float proximityDistance = 1f; // Distance within which the object will be destroyed

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
=======
{
    private RealtimeView _realtimeView;
    public float proximityDistance = 1f;
    public AudioClip coinClip; // Assign this in the Unity Editor
    private AudioSource audioSource;
    private static float lastCoinTime = -2.0f; // Static to keep track across all instances
    public float pitchIncreaseDuration = 2.0f; // Time in seconds to reset pitch
    public float highPitch = 1.5f; // High pitch multiplier

    void Start()
    {
        _realtimeView = GetComponent<RealtimeView>();
        audioSource = gameObject.AddComponent<AudioSource>(); // Adding AudioSource dynamically
        audioSource.clip = coinClip;

        if (_realtimeView == null)
        {
            Debug.LogError("RealtimeView component not found. Ensure it's attached to the object.");
        }
>>>>>>> 4d4aa5c (ese)
    }

    void Update()
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCamera != null)
        {
            float distance = Vector3.Distance(mainCamera.transform.position, transform.position);
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
            PlayCoinSound();
            DestroyObject();
        }

        else
        {
            _realtimeView.RequestOwnership();
            _realtimeView.ownerIDSelfDidChange += OwnershipChanged;
        }
    }

    private void OwnershipChanged(RealtimeView realtimeView, int previousOwnerID)
    {
        if (_realtimeView.isOwnedLocallyInHierarchy)
        {
            PlayCoinSound();
            DestroyObject();
        }
        _realtimeView.ownerIDSelfDidChange -= OwnershipChanged;
    }

    private void DestroyObject()
<<<<<<< HEAD
    {
        scoreDisplay.IncreaseScore(1); // Increase the score using ScoreDisplay component
=======
    {
        Realtime.Destroy(gameObject);
    }
>>>>>>> 4d4aa5c (ese)

    private void PlayCoinSound()
    {
        if (Time.time - lastCoinTime <= pitchIncreaseDuration)
        {
            audioSource.pitch = highPitch; // Play sound at a higher pitch
        }
        else
        {
            audioSource.pitch = 1.0f; // Normal pitch
        }
        audioSource.Play();
        lastCoinTime = Time.time; // Update the last coin capture time
    }
}
