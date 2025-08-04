using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject quittingUI; // UI that appears when holding A
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject menuScreen;

    [Header("Input Action")]
    [SerializeField] private InputActionReference aButtonAction;

    private bool isPaused = false;
    private bool isHoldingButton = false;
    private float holdTime = 0f;
    public float holdThreshold = 5f;
    public bool gameOver;

    void Start()
    {
        // Ensure the game starts unpaused when the scene loads
        isPaused = false;
        Time.timeScale = 1f; // Unpause the game
        pauseMenu.SetActive(false);
        quittingUI.SetActive(false);

        if (aButtonAction != null)
        {
            aButtonAction.action.Enable();
            aButtonAction.action.started += OnAButtonDown;
            aButtonAction.action.canceled += OnAButtonUp;
        }
    }

    private void OnDestroy()
    {
        if (aButtonAction != null)
        {
            aButtonAction.action.started -= OnAButtonDown;
            aButtonAction.action.canceled -= OnAButtonUp;
        }
    }

    private void Update()
    {
        if (isPaused && isHoldingButton)
        {
            holdTime += Time.unscaledDeltaTime; // Continue counting even when paused

            if (holdTime >= holdThreshold)
            {
                SceneManager.LoadScene("Menu");
                startScreen.SetActive(false);
                menuScreen.SetActive(true);
            }
        }
    }

    private void OnAButtonDown(InputAction.CallbackContext context)
    {
        if (isPaused)
        {
            isHoldingButton = true;
            holdTime = 0f;
            quittingUI.SetActive(true); // Show quitting UI immediately
        }
    }

    private void OnAButtonUp(InputAction.CallbackContext context)
    {
        if (isPaused)
        {
            if (holdTime < holdThreshold)
            {
                // If released before 5 seconds, just unpause
                quittingUI.SetActive(false);
                TogglePauseMenu();
            }
            else
            {
                // If held for 5+ seconds, just reset without unpausing
                isHoldingButton = false;
                holdTime = 0f;
            }
        }
        else
        {
            TogglePauseMenu(); // Toggle pause if not already paused
        }
    }

    private void TogglePauseMenu()
    {
        if (!gameOver)
        {
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f;
            if (isPaused)
            {
                AudioChange.play = false;
            }
            else
            {
                AudioChange.play = true;
            }
            if (!isPaused)
            {
                holdTime = 0f;
                isHoldingButton = false; 
            }
        }
    }

}
