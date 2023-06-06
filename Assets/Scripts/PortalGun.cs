using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalGun : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    private bool shootKeyPressed;
    private int currentAmmo = 1;
    private int maxAmmo = 1;
    private bool isReloading = false;
    public float reloadTime = 0.5f;
    [SerializeField] private LayerMask groundLayer;

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
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
        if (!IsGunInWall() && shootKeyPressed && transform.parent.tag == "Player1")
        {
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
            currentAmmo--;

        }
    }

    void PlayerTwoShot()
    {
        if (!IsGunInWall() && shootKeyPressed && transform.parent.tag == "Player2")
        {
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
            currentAmmo--;
        }
    }


    // Reloads the portal gun
    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    //Shoot method
    public void Shoot(InputAction.CallbackContext context)
    {
        shootKeyPressed = context.performed;
    }
}
