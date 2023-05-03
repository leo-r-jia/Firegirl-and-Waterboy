using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    //Score manager that manages the scene score
    [SerializeField] public ScoreManager scoreManager;

    //Collect sound effect
    [SerializeField] private AudioSource collectionSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When colliding with a Coin object, destroy the object and update score
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            scoreManager.UpdateScore();
            collectionSoundEffect.Play();
        }
    }
}