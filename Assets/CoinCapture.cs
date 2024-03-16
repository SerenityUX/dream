using Normal.Realtime; // Normcore namespace
using UnityEngine;

public class CoinCapture : MonoBehaviour
{
    private RealtimeView _realtimeView; // Used to access the Realtime component
    public float proximityDistance = 1f; // Distance within which the object will be destroyed

    void Start()
    {
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

        // Destroy the GameObject this script is attached to
        Realtime.Destroy(gameObject); // Use Realtime.Destroy to properly handle networked object destruction
    }
}
