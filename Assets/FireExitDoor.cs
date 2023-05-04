using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExitDoor : MonoBehaviour
{
    [SerializeField] public bool isAtFireDoor = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player 1")
            isAtFireDoor = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player 1")
            isAtFireDoor = false;
    }

    public bool getIsAtDoor()
    {
        return isAtFireDoor;
    }
}
