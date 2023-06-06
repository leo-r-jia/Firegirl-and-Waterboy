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
    [SerializeField] Color dissolveColor;
    public GameObject gameOverMenu;

    public UnityEvent OnDeath;

    //If colliding with obstacles, then Die() is invoked
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

    //Die Coroutine
    private IEnumerator DieCoroutine()
    {
        AudioManager.Instance.PlaySFX("Player Death");

        //Triggers dissolve animation
        DissolveManager dissolveManager = GetComponent<DissolveManager>();
        dissolveManager.Dissolve(3.5f, dissolveColor);

        InputSystem.DisableDevice(Keyboard.current);
        DisableGun();

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

    private void DisableGun()
    {
        GameObject child = transform.GetChild(1).gameObject;
        child.SetActive(false);
    }
}
