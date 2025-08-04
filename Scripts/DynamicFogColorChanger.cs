using UnityEngine;

public class DynamicFogColorChanger : MonoBehaviour
{
    public float changeDuration = 5f; // Duration for each color transition
    public float minBrightness = 0.7f; // Minimum brightness for lighter colors
    public float maxBrightness = 1f; // Maximum brightness for lighter colors

    private Color currentColor; // Current fog color
    private Color targetColor; // Target fog color
    private float elapsedTime = 0f; // Time elapsed since the last color change

    void Start()
    {
        // Initialize with the current fog color
        currentColor = RenderSettings.fogColor;
        RenderSettings.fog = true; // Ensure fog is enabled

        // Generate the first target color
        targetColor = GetRandomLightColor();
    }

    void Update()
    {
        // Increment the elapsed time
        elapsedTime += Time.deltaTime;

        // Calculate the interpolation factor (0 to 1)
        float t = Mathf.Clamp01(elapsedTime / changeDuration);

        // Interpolate between the current and target fog colors
        RenderSettings.fogColor = Color.Lerp(currentColor, targetColor, t);

        // If the transition is complete, generate a new target color
        if (t >= 1f)
        {
            elapsedTime = 0f; // Reset the timer
            currentColor = RenderSettings.fogColor; // Set the current color to the achieved color
            targetColor = GetRandomLightColor(); // Generate a new random target color
        }
    }

    // Generates a random light color
    Color GetRandomLightColor()
    {
        // Randomize RGB values within the brightness range
        float r = Random.Range(minBrightness, maxBrightness);
        float g = Random.Range(minBrightness, maxBrightness);
        float b = Random.Range(minBrightness, maxBrightness);

        return new Color(r, g, b);
    }
}