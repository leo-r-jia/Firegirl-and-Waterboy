using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] public bool fireWeakness = false;
    public GameObject gameOverMenu;

    public UnityEvent OnDeath;

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

    // Opens game over menu and invokes the death event
    private void Die()
    {
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
        Cursor.visible = true;
        InputSystem.DisableDevice(Keyboard.current);
        OnDeath.Invoke();
    }
}
