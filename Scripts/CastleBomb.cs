using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBomb : MonoBehaviour
{
    public float timer=2f;
    public GameObject smoke,body;
    GameObject player;
    public bool playerNear;
    CastleLevelManager levelManager;
    bool explodebegin;
    Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindAnyObjectByType<CastleLevelManager>();
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(timer);
        body.SetActive(false);
        smoke.SetActive(true);
        if (playerNear)
        {
            print("Damage");
            levelManager.DecreaseHealth(10);
        }
        else
        {
            levelManager.IncreaseScore(10);
        }
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!explodebegin)
        {
            StartCoroutine(Explode());
            explodebegin = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            playerNear = true;
            player = other.gameObject;
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")){
            playerNear = false;
        }
    }

}
