using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class BeatSpawner : MonoBehaviour
{
    public AudioSource audioSource; // Audio source for the song
    public AudioSource userSource;//Audio that the user hears
    public GameObject[] blockPrefab; // The block prefab to spawn
    public Transform playerMarker; // Reference to the player position (for Z alignment)
    public float blockSpeed = 1f; // Speed at which blocks move toward the player
    public float beatDetectionThreshold = 0.5f; // Threshold for beat detection
    public static bool playM;
    public float progress = 100;
    public Slider progressBar;
    public GameObject gameOverScreen;
    public PauseMenu pauseMenu;
    public static float time=0.3f;

    private float[] samples = new float[1024]; // Audio sample data
    private Vector3[] spawnPositions; // Array of spawn positions

    void Start()
    {
        playM = false;
        if (audioSource == null || blockPrefab == null || playerMarker == null)
        {
            Debug.LogError("Please assign all required references in the inspector!");
            return;
        }

        // Define the four spawn positions along the same Z-axis
        spawnPositions = new Vector3[] {
            new Vector3(transform.position.x-4f, transform.position.y+4f, this.transform.position.z), // Left
            new Vector3(transform.position.x,  transform.position.y+4f, this.transform.position.z),  // Right
            new Vector3(transform.position.x-2f, transform.position.y+6f, this.transform.position.z),  // Top
            new Vector3(transform.position.x-2f, transform.position.y + 2f, this.transform.position.z)  // Bottom
        };

        // Start playing the song
        audioSource.Play();
        StartCoroutine(SpawnBlocksOnBeat());
    }

    IEnumerator SpawnBlocksOnBeat()
    {
        while (audioSource.isPlaying)
        {
            audioSource.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);
            float sum = 0f;
            for (int i = 0; i < samples.Length; i++)
            {
                sum += samples[i];
            }

            if (sum > beatDetectionThreshold) // If beat detected
            {
                SpawnBlock();
            }

            yield return new WaitForSeconds(time); // Check for beats every 0.1 seconds
        }
    }

    void SpawnBlock()
    {
        int randomIndex = Random.Range(0, spawnPositions.Length); // Choose a random spawn position
        int randomBlock = Random.Range(0, 2);
        GameObject newBlock = Instantiate(blockPrefab[randomBlock], spawnPositions[randomIndex], Quaternion.identity);

        // Move block towards the player over time
        BlockMover blockMover = newBlock.AddComponent<BlockMover>();
        blockMover.Initialize(playerMarker.position.z, blockSpeed);
    }

    private void Update()
    {
        //playM = BlockMover.playMusic;
        if(playM == true && !userSource.isPlaying)
        {
            userSource.Play();
            Debug.Log("Music is playing");
        }
    }

    public void DecreaseProgress(float decrease)
    {
        if((progress -= decrease) >= 0)
        {
            progress -= decrease;
        }
        progressBar.value = progress;
        if (progress <= 0)
        {
            StartCoroutine(GameOver());
        }
    }
    public void IncreaseProgress(float increase)
    {
        if((progress+increase) <= 100)
        {
            progress += increase;
        }
        progressBar.value = progress;
    }

    IEnumerator GameOver()
    {
        pauseMenu.gameOver = true;
        gameOverScreen.SetActive(true);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("mode2");
    }
}
