using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;


public class VRButton : MonoBehaviour
{
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;
    [SerializeField] private Animator animator; // Optional: For button press animation

    [Header("Input Action")]
    [SerializeField] private InputActionReference aButtonAction; // Reference to A button input action

    public int function;
    public GameObject startScreen, selectScreen, loading, musicScreen, diffScreen;
    public Material skybox1, skybox2, skybox3, skyeasy, skymed, skyhard;
    public AudioSource hover, select;

    void Start()
    {
        // Get the XR Simple Interactable component
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        animator = GetComponent<Animator>(); // Optional: For animation

        // Subscribe to the select event (still useful for hover/selection feedback)
        interactable.selectEntered.AddListener(OnButtonPressed);
        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);

        // Enable and subscribe to the A button input action
        if (aButtonAction != null)
        {
            aButtonAction.action.Enable();
            aButtonAction.action.performed += OnAButtonPressed; // Trigger when A is pressed
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        if (aButtonAction != null)
        {
            aButtonAction.action.performed -= OnAButtonPressed;
        }
    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        // This can still handle selection feedback if needed
        Debug.Log("Interactable Selected!");
    }

    private void OnAButtonPressed(InputAction.CallbackContext context)
    {
        // Check if the button is being hovered or selected when A is pressed
        if (interactable.isHovered || interactable.isSelected)
        {
            Debug.Log("A Button Pressed!");
            if (animator != null)
            {
                animator.SetTrigger("Press");
            }
            switch (function)
            {
                case 0:
                    {//Start Screen
                        startScreen.SetActive(false);
                        selectScreen.SetActive(true);
                        break;
                    }
                case 1:
                    {//Jungle Mode
                        selectScreen.SetActive(false);
                        loading.SetActive(true);
                        SceneManager.LoadScene("Jungle");
                        break;
                    }
                case 2:
                    {//Song Beat
                        selectScreen.SetActive(false);
                        musicScreen.SetActive(true);
                        break;
                    }
                case 3:
                    {//Castle Mode
                        selectScreen.SetActive(false);
                        loading.SetActive(true);
                        SceneManager.LoadScene("Castle");
                        break;
                    }
                case 4:
                    {//song1
                        musicScreen.SetActive(false) ;
                        diffScreen.SetActive(true) ;
                        PlayerPrefs.SetString("Song","music1" );
                        PlayerPrefs.Save();
                        break;
                    }
                case 5:
                    {//song2
                        musicScreen.SetActive(false);
                        diffScreen.SetActive(true);
                        PlayerPrefs.SetString("Song", "music2");
                        PlayerPrefs.Save();
                        break;
                    }
                case 6:
                    {//song3
                        musicScreen.SetActive(false);
                        diffScreen.SetActive(true);
                        PlayerPrefs.SetString("Song", "music3");
                        PlayerPrefs.Save();
                        break;
                    }
                case 7:
                    {//song4
                        musicScreen.SetActive(false);
                        diffScreen.SetActive(true);
                        PlayerPrefs.SetString("Song", "music4");
                        PlayerPrefs.Save();
                        break;
                    }
                case 8:
                    {//easy
                        diffScreen.SetActive(false);
                        
                        PlayerPrefs.SetString("Diff", "easy");
                        PlayerPrefs.Save();
                        SceneManager.LoadScene("mode2");
                        break;
                    }
                case 9:
                    {//medium
                        PlayerPrefs.SetString("Diff", "med");
                        PlayerPrefs.Save();
                        SceneManager.LoadScene("mode2");
                        break;
                    }
                case 10:
                    {//hard
                        PlayerPrefs.SetString("Diff", "hard");
                        PlayerPrefs.Save();
                        SceneManager.LoadScene("mode2");
                        break;
                    }
            }
            select.Play();
        }
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f); // Slightly enlarge
        switch (function)
        {
            case 1:
                RenderSettings.skybox = skybox1;
                break;
            case 2:
                RenderSettings.skybox = skybox2;
                break;
            case 3:
                RenderSettings.skybox = skybox3;
                break;
            case 8:
                RenderSettings.skybox = skyeasy;
                break;
            case 9:
                RenderSettings.skybox = skymed;
                break;
            case 10:
                RenderSettings.skybox = skyhard;
                break;
        }
        hover.Play();
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        transform.localScale = new Vector3(1f, 1f, 1f); // Reset scale
    }

}