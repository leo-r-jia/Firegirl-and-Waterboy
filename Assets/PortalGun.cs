using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalGun : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    private int currentAmmo = 1;
    private int maxAmmo = 1;
    private bool isReloading = false;
    public float reloadTime = 0.5f;
    [SerializeField] private LayerMask groundLayer;

    // Update is called once per frame
    void Update()
    {
        if(isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        PlayerOneShot();
        PlayerTwoShot();
    }

    //Returns true if the gun is inside a vertical wall.
    private bool IsGunInWall()
    {
        return Physics2D.OverlapCircle(shootingPoint.position, 0.2f, groundLayer);
    }

    void PlayerOneShot()
    {
        if (!IsGunInWall() && Keyboard.current.spaceKey.wasPressedThisFrame && transform.parent.tag == "Player1")
        {
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
            currentAmmo--;

        }
    }

    void PlayerTwoShot()
    {
        if (!IsGunInWall() && Keyboard.current.leftShiftKey.wasPressedThisFrame && transform.parent.tag == "Player2")
        {
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
            currentAmmo--;
        }
    }


    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        
        currentAmmo = maxAmmo;
        isReloading = false;
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
}

*/