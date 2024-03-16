using UnityEngine;

public class BounceRotate : MonoBehaviour
{
    public float rotationSpeed = 250f;
    public float bounceHeight = 0.1f;
    public float bounceSpeed = 1f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        RotateObject();
        BounceObject();
    }

    void RotateObject()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }

    void BounceObject()
    {
        float newY = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight + startPosition.y;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
