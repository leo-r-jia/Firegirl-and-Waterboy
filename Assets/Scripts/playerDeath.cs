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
    public GameObject gameOverMenu;

    [SerializeField] private AudioSource deathSoundEffect;

    public UnityEvent OnDeath;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fire") && fireWeakness == true || collision.gameObject.CompareTag("Acid"))
        {
            Die();
        }
        else if (collision.gameObject.CompareTag("Water") && fireWeakness == false)
        {
            Die();
        }
    }

    private IEnumerator DieCoroutine()
    {
        deathSoundEffect.Play();
        DissolveManager dissolveManager = GetComponent<DissolveManager>();
        dissolveManager.Dissolve(3.5f);
        InputSystem.DisableDevice(Keyboard.current);

        yield return new WaitForSeconds(0.4f); // Wait for 0.4 second

        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
        Cursor.visible = true;
        OnDeath.Invoke();
    }

    // Opens game over menu and invokes the death event
    private void Die()
    {
        StartCoroutine(DieCoroutine());
    }
}
