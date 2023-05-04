using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterExitDoor : MonoBehaviour
{
    [SerializeField] public bool isAtWaterDoor = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player2")
            isAtWaterDoor = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player2")
            isAtWaterDoor = false;
    }

    public bool getIsAtDoor()
    {
        return isAtWaterDoor;
    }
}
