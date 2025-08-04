using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spawnFruits : MonoBehaviour
{
    public GameObject[] fruits;
    public Transform[] spawnPoints;
    public float spawnInterval=1f;
    public float throwForce=30f;
    public bool gameOver;
    public GameObject gameOverScreen;
    public bool destroyObjects;
    public PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawning());
    }

    // Update is called once per frame
    void Update()
    {
        spawnInterval -= Time.deltaTime *0.005f;
    }

    IEnumerator spawning()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(spawnInterval);
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject fruit = Instantiate(fruits[Random.Range(0, fruits.Length)], spawnPoint.position, spawnPoint.rotation);
            spawnPoint.GetComponent<AudioSource>().Play();
            Rigidbody rb = fruit.GetComponent<Rigidbody>();
            rb.AddForce(fruit.transform.forward * throwForce);
            if (destroyObjects)
                Destroy(fruit, 5f);
        }
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverScreen.SetActive(true);
        pauseMenu.gameOver = true;
        StartCoroutine(ReloadScene());
    }
    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Jungle");
    }
}
