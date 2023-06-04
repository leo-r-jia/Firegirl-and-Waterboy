using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    //Score manager that manages the scene score
    [SerializeField] private ScoreManager scoreManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When colliding with a Coin object, destroy the object and update score
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            scoreManager.UpdateScore();

            if (gameObject.name == "Player 1")
                AudioManager.Instance.PlaySFX("Collected P1");
            else
                AudioManager.Instance.PlaySFX("Collected P2");
        }
    }
}