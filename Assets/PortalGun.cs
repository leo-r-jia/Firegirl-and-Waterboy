using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalGun : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bulletPrefab;

    void Start()
    {
        //StartCoroutine(waiter());
    }

    // Update is called once per frame
    void Update()
    {
        PlayerOneShot();
        PlayerTwoShot();
    }

    void PlayerOneShot()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && transform.parent.tag == "Player1")
        {
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
            //yield return new WaitForSeconds(2);

        }
    }

    void PlayerTwoShot()
    {
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame && transform.parent.tag == "Player2")
        {
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
            //yield return new WaitForSeconds(2);
        }
    }
}

/*
 * void Start()
    {
        StartCoroutine(reload());
    }

    // Update is called once per frame
    void Update()
    {
        PlayerOneShot();
        PlayerTwoShot();
    }

    void PlayerOneShot()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && transform.parent.tag == "Player1" && blueReloaded == true)
        {
            reload();

        }
    }

    void PlayerTwoShot()
    {
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame && transform.parent.tag == "Player2" && orangeReloaded == true)
        {
            //reload(orangeReloaded);
        }
    }

    IEnumerator reload()
    {
        blueReloaded = false;
        Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
        yield WaitForSeconds(2);
        blueReloaded = true;
    }
}

*/