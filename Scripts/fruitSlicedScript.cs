using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitSlicedScript : MonoBehaviour
{
    public float explosionForce = 1000f;  // Adjust the force of the explosion
    public float explosionRadius = 5f;   // Adjust the radius of the explosion
    public float upwardsModifier = 1f;   // Adjust how much force is applied upwards
    public Vector3 explosionPosition;    // The position of the explosion

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
        explosionPosition = transform.position;

        // Apply the explosion force to child objects
        ApplyExplosionForceToChildren();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ApplyExplosionForceToChildren()
    {
        // Loop through all child objects of the parent
        foreach (Transform child in transform)
        {
            // Get the Rigidbody attached to the child object (if it exists)
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Apply the explosion force to the child object's Rigidbody
                rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, ForceMode.Impulse);
            }
        }
    }
}
