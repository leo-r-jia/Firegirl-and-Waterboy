using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerDeath : MonoBehaviour
{

    [SerializeField] public bool fireWeakness = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
