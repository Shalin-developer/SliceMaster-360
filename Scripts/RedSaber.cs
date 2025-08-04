using System.Collections;
using UnityEngine;
using TMPro;

public class RedSaber : MonoBehaviour
{
    public int cubesPerAxis = 4; // Number of cubes per axis
    public float force = 300f; // Force of the explosion
    public float radius = 2f; // Radius of the explosion
    public GameObject bluePrefab; // Reference to blue beat
    public GameObject redPrefab; // Reference to red beat
    public TextMeshProUGUI txt; // Reference to the TextMeshProUGUI component

    private Coroutine textAnimationCoroutine; // Reference to the text animation coroutine
    private float remainingDisplayTime = 0f; // Time remaining for the text to stay visible
    private bool isTextVisible = false; // Flag to track if the text is currently visible
    BeatSpawner beatSpawner;

    private void Start()
    {
        beatSpawner = FindAnyObjectByType<BeatSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the blue beat
        if (other.CompareTag("blueC"))
        {
            Debug.Log("Blue beat collided! Exploding the blue beat...");

            // Instantiate the prefab at the position of the colliding object
            GameObject specialObjectInstance = Instantiate(bluePrefab, other.transform.position, Quaternion.identity);

            // Explode the instantiated object at the collision position
            ExplodeObject(specialObjectInstance, other.transform.position);

            // Destroy the colliding object
            Destroy(other.gameObject);

            // Update the text and start/reset the animation
            txt.text = "MISS";
            ResetTextAnimation();
        }

        // Check if the colliding object is the red beat
        if (other.CompareTag("redC"))
        {
            Debug.Log("Red beat collided! Exploding the red beat...");

            // Instantiate the prefab at the position of the colliding object
            GameObject specialObjectInstance = Instantiate(redPrefab, other.transform.position, Quaternion.identity);

            // Explode the instantiated object at the collision position
            ExplodeObject(specialObjectInstance, other.transform.position);

            // Destroy the colliding object
            Destroy(other.gameObject);

            // Update the text and start/reset the animation
            txt.text = "HIT";
            ResetTextAnimation();
            ScoreScript.score += 10;
            beatSpawner.IncreaseProgress(10f);
        }
    }

    void ExplodeObject(GameObject objectToExplode, Vector3 explosionPosition)
    {
        // Store the original object's scale and material
        Vector3 originalScale = objectToExplode.transform.localScale;
        Material originalMaterial = objectToExplode.GetComponent<Renderer>().material;

        // Destroy the original object
        Destroy(objectToExplode);

        // Create smaller cubes at the explosion position
        for (int x = 0; x < cubesPerAxis; x++)
        {
            for (int y = 0; y < cubesPerAxis; y++)
            {
                for (int z = 0; z < cubesPerAxis; z++)
                {
                    CreateCube(new Vector3(x, y, z), explosionPosition, originalScale, originalMaterial);
                }
            }
        }
    }

    void CreateCube(Vector3 coordinates, Vector3 explosionPosition, Vector3 originalScale, Material originalMaterial)
    {
        // Create a new cube
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Renderer rd = cube.GetComponent<Renderer>();
        rd.material = new Material(originalMaterial); // Use the same material as the original object
        cube.transform.localScale = originalScale / cubesPerAxis; // Scale the cube

        // Calculate the position of the cube relative to the explosion position
        Vector3 firstCube = explosionPosition - originalScale / 2 + cube.transform.localScale / 2;
        cube.transform.position = firstCube + Vector3.Scale(coordinates, cube.transform.localScale);

        // Add a Rigidbody and apply explosion force
        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.AddExplosionForce(force, explosionPosition, radius);

        // Optional: Destroy the smaller cubes after some time
        Destroy(cube, 0.5f);
    }

    // Reset the text animation
    void ResetTextAnimation()
    {
        // If the text is already visible, simply extend the display time
        if (isTextVisible)
        {
            remainingDisplayTime = 0.5f; // Reset the display time
            return;
        }

        // If the text animation is already running, stop it
        if (textAnimationCoroutine != null)
        {
            StopCoroutine(textAnimationCoroutine);
        }

        // Start the text animation coroutine
        textAnimationCoroutine = StartCoroutine(AnimateText());
    }

    // Coroutine to animate the text
    IEnumerator AnimateText()
    {
        float duration = 0.5f; // Duration for each phase of the animation
        float targetFontSize = 1.8f; // Target font size

        // Increase font size from 0 to 1.8 over 0.5 seconds
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            txt.fontSize = Mathf.Lerp(0f, targetFontSize, t);
            yield return null;
        }

        // Mark the text as visible
        isTextVisible = true;

        // Reset the remaining display time
        remainingDisplayTime = 0.5f;

        // Wait for the remaining display time, resetting it if a new collision occurs
        while (remainingDisplayTime > 0f)
        {
            remainingDisplayTime -= Time.deltaTime;
            yield return null;
        }

        // Decrease font size from 1.8 to 0 over 0.5 seconds
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            txt.fontSize = Mathf.Lerp(targetFontSize, 0f, t);
            yield return null;
        }

        // Reset the text to an empty string
        txt.text = "";

        // Mark the text as no longer visible
        isTextVisible = false;
    }
}