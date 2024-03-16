using UnityEngine;
using System.Collections;

public class ImageScaler : MonoBehaviour
{
    public Vector3 targetScale = new Vector3(0.054245f, 0.054245f, 0.054245f);
    public float scaleDuration = 1.0f; // Duration for scaling up
    public float fadeDuration = 1.0f; // Duration for fading in
    private Material objectMaterial;

    void Awake()
    {
        // Attempt to get the Renderer component and its material
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            objectMaterial = renderer.material;
        }

        // Initialize scale to 0
        transform.localScale = Vector3.zero;

        // Start the scaling, shaking, and fading sequence
        StartCoroutine(ScaleAndShakeSequence());
        StartCoroutine(FadeIn());
    }

    IEnumerator ScaleAndShakeSequence()
    {
        // Scale up from 0 to target scale smoothly
        float currentTime = 0.0f;
        Vector3 originalScale = Vector3.zero;
        while (currentTime < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, currentTime / scaleDuration);
            currentTime += Time.deltaTime;
            yield return null;
        }

        // Wait for 3 seconds before starting the soft shake
        yield return new WaitForSeconds(1f);
        StartCoroutine(SoftShake());

        // After an additional 6 seconds, scale back to 0
        yield return new WaitForSeconds(2f);
        StartCoroutine(ScaleDown());
    }

    IEnumerator SoftShake()
    {
        float duration = 0.5f * 0.7f; // Making the shake 60% faster by reducing the duration to 40% of the original
        float magnitude = 0.05f; // Reduced magnitude for a softer shake

        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = originalPosition.x + Random.Range(-1f, 1f) * magnitude;
            float y = originalPosition.y + Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);
            elapsed += Time.deltaTime;

            yield return null;
        }

        // Return to the original position
        transform.localPosition = originalPosition;
    }

    IEnumerator ScaleDown()
    {
        float currentTime = 0.0f;
        Vector3 startScale = transform.localScale;
        while (currentTime < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, currentTime / scaleDuration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = Vector3.zero; // Ensure it ends at scale 0
    }

    IEnumerator FadeIn()
    {
        if (objectMaterial == null) yield break; // Exit if no material found

        float currentTime = 0.0f;
        Color colorStart = new Color(1, 1, 1, 0); // Starting color (transparent)
        Color colorEnd = new Color(1, 1, 1, 1); // End color (opaque)

        while (currentTime < fadeDuration)
        {
            objectMaterial.color = Color.Lerp(colorStart, colorEnd, currentTime / fadeDuration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        objectMaterial.color = colorEnd; // Ensure it ends fully opaque
    }
}
