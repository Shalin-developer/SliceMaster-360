using UnityEngine;

public class Explode : MonoBehaviour
{
    public int cubesPerAxis = 4; // Number of cubes per axis
    public float force = 300f; // Force of the explosion
    public float radius = 2f; // Radius of the explosion
    public GameObject bluePrefab; // Reference to blue beat
    public GameObject redPrefab; // Reference to red beat

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
}