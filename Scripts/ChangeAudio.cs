using UnityEngine;

public class ChangeAudio : MonoBehaviour
{
    // References to the game objects with AudioSource components
    public GameObject gameObject1;
    public GameObject gameObject2;

    // New audio clips to assign
    public AudioClip newClip1;
    public AudioClip newClip2;

    void Start()
    {
        // Check if game objects and audio clips are set
        if (gameObject1 != null && gameObject2 != null && newClip1 != null && newClip2 != null)
        {
            // Get the AudioSource components
            AudioSource audioSource1 = gameObject1.GetComponent<AudioSource>();
            AudioSource audioSource2 = gameObject2.GetComponent<AudioSource>();

            // Assign the new audio clips to the AudioSource components
            if (audioSource1 != null)
            {
                audioSource1.clip = newClip1;
                audioSource1.Play();
            }

            if (audioSource2 != null)
            {
                audioSource2.clip = newClip2;
                audioSource2.Play();
            }
        }
        else
        {
            Debug.LogError("Game objects or audio clips are not assigned.");
        }
    }
}
