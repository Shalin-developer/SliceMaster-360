using UnityEngine;

public class RotatingTunnels : MonoBehaviour
{
    [SerializeField] private GameObject[] structures; // Array of objects to rotate
    [SerializeField] private float[] rotationSpeeds; // Corresponding rotation speeds

    void Update()
    {
        // Ensure both arrays have the same length to avoid errors
        if (structures.Length != rotationSpeeds.Length)
        {
            Debug.LogError("Mismatch: The number of structures and rotation speeds must be equal!");
            return;
        }

        // Rotate each object with its corresponding speed
        for (int i = 0; i < structures.Length; i++)
        {
            if (structures[i] != null) // Ensure the object is not null
            {
                structures[i].transform.Rotate(Vector3.forward * rotationSpeeds[i] * Time.deltaTime, Space.Self);
            }
        }
    }
}
