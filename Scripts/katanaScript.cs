using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class katanaScript : MonoBehaviour
{
    public GameObject apple, banana, coconut, gApple, tomato, peach, watermelon, orange;
    public GameObject Papple, Pbanana, Pcoconut, PgApple, Ptomato, Ppeach, Pwatermelon, Porange;
    public SliceMeter sliceMeter;
    public GameObject explosion;
    public int health =3;
    public GameObject[] healthIcon;
    public spawnFruits spawner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        switch (collision.gameObject.tag)
        {
            case "apple":
                Slice(collision.gameObject,apple, Papple);
                break;
            case "banana":
                Slice(collision.gameObject, banana, Pbanana);
                break;
            case "coconut":
                Slice(collision.gameObject, coconut , Pcoconut);
                break;
            case "gApple":
                Slice(collision.gameObject, gApple, PgApple);
                break;
            case "tomato":
                Slice(collision.gameObject, tomato, Ptomato);
                break;
            case "peach":
                Slice(collision.gameObject, peach, Ppeach);
                break;
            case "watermelon":
                Slice(collision.gameObject, watermelon,Pwatermelon);
                break;
            case "orange":
                Slice(collision.gameObject, orange,Porange);
                break;
            case "bomb":
                Explode(collision.gameObject);
                break;
        }
    }

    void Slice(GameObject fruit, GameObject slice, GameObject particle)
    {
        sliceMeter.Sliced();
        sliceMeter.IncreaseScore(10);
        Transform point = fruit.transform;
        Destroy(fruit);
        Instantiate(slice, point.position, point.rotation);
        GameObject splash = Instantiate(particle, point.position, point.rotation);
        Destroy(splash, 0.5f);
        Rigidbody[] rbs = slice.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            // Apply a forward force relative to each slice's local Z-axis
            float forceMultiplier = 500f; // Adjust this value as needed
            rb.AddForce(rb.transform.forward * forceMultiplier, ForceMode.Impulse);
        }
        
    }

    void Explode(GameObject bomb)
    {
        Transform point = bomb.transform;
        GameObject smoke = Instantiate(explosion, point.position, point.rotation);
        Destroy(bomb);
        Destroy(smoke,1f);
        health--;
        healthIcon[health].SetActive(false);
        if (health <= 0)
        {
            spawner.GameOver();
        }
    }
}
