using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class swordScript : MonoBehaviour
{
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Slice(target);
        }
    }

    void Slice(GameObject target)
    {
        SlicedHull hull = target.Slice(transform.position, transform.up);

        if(hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target);
            SetupSlicedComponent(upperHull);
            GameObject lowerHull = hull.CreateLowerHull(target);
            SetupSlicedComponent(lowerHull);
            Destroy(target);
        }
    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(200f, slicedObject.transform.position, 1);
    }
}
