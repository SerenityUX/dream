using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PathConnector : MonoBehaviour
{
    public string tagName = "Path";
    private LineRenderer lineRenderer;
    private float updateInterval = 1.0f; // Update every second
    private float timeSinceLastUpdate = 0.0f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        InitializeLineRenderer();
    }

    void Update()
    {
        timeSinceLastUpdate += Time.deltaTime;

        if (timeSinceLastUpdate >= updateInterval)
        {
            UpdatePath();
            timeSinceLastUpdate = 0.0f;
        }
    }

    void InitializeLineRenderer()
    {
        // Set the color of the line
        Color lineColor = Color.blue;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;

        // Set the width of the line
        float lineWidth = 0.1f; // Adjust this value to make the line thicker or thinner
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        // Optional: Assign a material to the LineRenderer
        // lineRenderer.material = new Material(Shader.Find("Standard")); // Replace "Standard" with your preferred shader
        // For a simple color without a material, the above code is not necessary
    }

    void UpdatePath()
    {
        GameObject[] pathObjects = GameObject.FindGameObjectsWithTag(tagName);

        if (pathObjects.Length > 1)
        {
            lineRenderer.positionCount = pathObjects.Length;
            for (int i = 0; i < pathObjects.Length; i++)
            {
                lineRenderer.SetPosition(i, pathObjects[i].transform.position);
            }
        }
        else
        {
            lineRenderer.positionCount = 0; // No line if there are less than 2 points
        }
    }
}
