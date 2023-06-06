using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isOrange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player1" && isOrange == true)
        {
            AudioManager.Instance.PlaySFX("Collected P1");
            GameObject child = GameObject.Find("Player 1").transform.GetChild(1).gameObject;
            child.SetActive(true);
            Destroy(this.gameObject);
        }
        else if (collision.tag == "Player2" && isOrange == false)
        {
            AudioManager.Instance.PlaySFX("Collected P2");
            GameObject child = GameObject.Find("Player 2").transform.GetChild(1).gameObject;
            child.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}