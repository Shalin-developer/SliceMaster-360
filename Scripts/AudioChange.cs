using UnityEngine;

public class AudioChange : MonoBehaviour
{
    // Public variables to reference the GameObjects that contain the AudioSources
    public GameObject audioSourceObject1;
    public GameObject audioSourceObject2;
    public static bool play;

    // Public variables to assign 4 different audio clips
    public AudioClip music1Clip;
    public AudioClip music2Clip;
    public AudioClip music3Clip;
    public AudioClip music4Clip;

    // Key to check the value in PlayerPrefs for song choice
    private string songPrefKey = "Song";

    // Key to check the value in PlayerPrefs for difficulty setting
    private string difficultyPrefKey = "Diff";

    // Private variables to store the AudioSource components
    private AudioSource audioSource1;
    private AudioSource audioSource2;

    // Start is called before the first frame update
    void Start()
    {
        play = false;
        // Find the AudioSource components on the GameObjects
        audioSource1 = audioSourceObject1.GetComponent<AudioSource>();
        audioSource2 = audioSourceObject2.GetComponent<AudioSource>();

        // Get the song preference from PlayerPrefs and change audio clips accordingly
        string songPreference = PlayerPrefs.GetString(songPrefKey, "music1");
        ChangeAudioClip(songPreference);

        // Adjust the time in BeatSpawner based on the difficulty
        SetDifficultyTime();
    }

    void Update()
    {
        if(!play)
        {
            audioSource1.Pause();
            audioSource2.Pause();
        }
        else
        {
            audioSource1.UnPause();
            audioSource2.UnPause();
        }
    }

    // Function to change audio clip based on the songPreference value
    void ChangeAudioClip(string songPreference)
    {
        // Select the clips based on the value of the songPreference
        switch (songPreference)
        {
            case "music1":
                audioSource1.clip = music1Clip;
                audioSource2.clip = music1Clip;
                break;

            case "music2":
                audioSource1.clip = music2Clip;
                audioSource2.clip = music2Clip;
                break;

            case "music3":
                audioSource1.clip = music3Clip;
                audioSource2.clip = music3Clip;
                break;

            case "music4":
                audioSource1.clip = music4Clip;
                audioSource2.clip = music4Clip;
                break;

            default:
                // If no valid song preference is set, play music1 by default
                audioSource1.clip = music1Clip;
                audioSource2.clip = music1Clip;
                break;
        }

        // Play the clips
        audioSource1.Play();
        audioSource2.Play();
    }

    // Method to set the time in BeatSpawner based on the "Diff" value
    void SetDifficultyTime()
    {
        // Get the difficulty preference from PlayerPrefs (default to "med" if not set)
        string difficulty = PlayerPrefs.GetString(difficultyPrefKey, "med");

        // Set the time variable in BeatSpawner based on the difficulty
        switch (difficulty)
        {
            case "easy":
                BeatSpawner.time = 1.5f; // Update BeatSpawner's time
                break;

            case "med":
                BeatSpawner.time = 1.0f; // Update BeatSpawner's time
                break;

            case "hard":
                BeatSpawner.time = 0.5f; // Update BeatSpawner's time
                break;

            default:
                // If the difficulty is invalid, default to "med"
                BeatSpawner.time = 1.0f;
                Debug.LogWarning("Invalid 'Diff' value in PlayerPrefs. Defaulting to 'med' (1.0).");
                break;
        }

        // Log the updated time to verify
        Debug.Log("Time set to: " + BeatSpawner.time);
    }

}
