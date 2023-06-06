using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;

public class PortalGunTests
{
    //public GameObject bulletPrefab;

    // Player press shoot key, bullet should exist.

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("Scenes/Jack/LevelOne");

        yield return null;

    }

    [UnityTest]
    public IEnumerator BulletInstantiatedWhenWaterboyShoots()
    {
        GameObject player = GameObject.Find("Waterboy");

        // Get portal gun object
        GameObject portalGunObject = player.transform.GetChild(1).gameObject;
        PortalGun portalGun = portalGunObject.GetComponent<PortalGun>();

        Debug.Log(portalGunObject.name);

        // Shoot bullet key
        portalGun.shootKeyPressed = true;

        // Wait for bullet to instantiate
        yield return new WaitForSeconds(0.1f); 

        // Assert
        Assert.IsNotNull(GameObject.FindGameObjectWithTag("BlueShot"));
    }

    [UnityTest]
    public IEnumerator BulletInstantiatedWhenFiregirlShoots()
    {
        GameObject player = GameObject.Find("Firegirl");

        // Get portal gun object
        GameObject portalGunObject = player.transform.GetChild(1).gameObject;
        PortalGun portalGun = portalGunObject.GetComponent<PortalGun>();

        // Shoot bullet key
        portalGun.shootKeyPressed = true;

        // Wait for bullet to instantiate
        yield return new WaitForSeconds(0.1f);

        // Assert
        Assert.IsNotNull(GameObject.FindGameObjectWithTag("OrangeShot"));
    }

    // Portal gun does not shoot if not equipped to Firegirl or Waterboy.
    [UnityTest]
    public IEnumerator BulletDoesNotInstatiateWhenParentHasIncorrectTag()
    {
        GameObject player = new GameObject("Player");
        GameObject portalGunObject = new GameObject("PortalGun");
        PortalGun portalGun = portalGunObject.AddComponent<PortalGun>();
        portalGun.shootingPoint = new GameObject("ShootingPoint").transform;
        portalGun.bulletPrefab = new GameObject("BulletPrefab");

        // Set the tag of the player to a tag that is not "Player1" or "Player2"
        player.tag = "Untagged";

        // Attach the portal gun to the player
        portalGun.transform.parent = player.transform;

        // Shoot bullet key
        portalGun.shootKeyPressed = true;

        // Wait for bullet to instantiate
        yield return new WaitForSeconds(0.1f);

        Assert.IsNull(GameObject.FindGameObjectWithTag("OrangeShot"));

    }

}
