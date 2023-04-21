using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private Controls controls;
    private InputAction menu;

    public GameObject pauseMenu;
    public GameObject background;
    public GameObject settingsMenu;

    [SerializeField] private bool isPaused;
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
        controls = new Controls();
    }

    // Update is called once per frame
    void Update()
    {

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

    void Pause(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            ActivateMenu();
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
    }

    // Closes pause menu
    public void DeactivateMenu()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        //background.SetActive(false);
        Cursor.visible = false;
        isPaused = false;
    }

    // Returns user to main menu
    public void quit()
    {
        Application.Quit();
    }

    // Returns user to start point and restarts the level
    public void restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        pauseMenu.SetActive(false);
        Cursor.visible = false;
    }
    
    // Opens settings menu
    public void settings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
}
