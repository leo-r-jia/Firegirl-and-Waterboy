using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Threading;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] public bool fireWeakness = false;
    private Material material;
    private float fade = 1f;
    private bool animationComplete = false;
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
        DeathAnimation();
        if (animationComplete)
        {
            Time.timeScale = 0f;
            gameOverMenu.SetActive(true);
            Cursor.visible = true;
            InputSystem.DisableDevice(Keyboard.current);
            OnDeath.Invoke();
            material.SetFloat("_Fade", 1f);
        }
    }

    //Death animation
    private void DeathAnimation()
    {
        material = GetComponent<SpriteRenderer>().material;
        
        while (fade > 0)
        {
            fade -= 0.05f;
            Thread.Sleep(50);
            material.SetFloat("_Fade", fade);
        }
        animationComplete = true;
    }
}
