using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    private Controls controls;
    private InputAction menu;

    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public bool activeSettings = false;

    public UnityEvent pauseMenuToggled;

    private bool isPaused;
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
        controls = new Controls();
    }

    public void ActiveSettings()
    {
        activeSettings = !activeSettings;
    }
 
    private void OnEnable()
    {
        menu = controls.Menu.PauseMenu;
        menu.Enable();

        menu.performed += Pause;
    }

    private void OnDisable()
    {
        menu.Disable();
    }

    // Checks if player pressed pause button
    void Pause(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            ActivateMenu();
        }

        // If player presses escape key in settings menu, open pause menu.
        else if(!isPaused && activeSettings == true)
        {
            isPaused = true;
            activeSettings = false;
            settingsMenu.SetActive(false);
            pauseMenu.SetActive(true);
            
        }
        else
        {
            DeactivateMenu();
        }
        
    }

    // Opens pause menu
    void ActivateMenu()
    {
        Time.timeScale = 0f; // Freezes time in-game
        pauseMenu.SetActive(true);
        Cursor.visible = true;

        pauseMenuToggled.Invoke();
    }

    // Closes pause menu
    public void DeactivateMenu()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        isPaused = false;

        pauseMenuToggled.Invoke();
    }

    // Returns user to start point and restarts the level
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        InputSystem.EnableDevice(Keyboard.current);
        Cursor.visible = false;
        
    }
}
