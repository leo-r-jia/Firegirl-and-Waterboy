using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class playerDeath : MonoBehaviour
{

    [SerializeField] public bool fireWeakness = false;
    public GameObject gameOverMenu;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fire") && fireWeakness == true)
        {
            Die();
        }
        else if (collision.gameObject.CompareTag("Water") && fireWeakness == false)
        {
            Die();
        }
    }

    // Opens game over menu
    private void Die()
    {
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
        Cursor.visible = true;
        InputSystem.DisableDevice(Keyboard.current);
    }
}
