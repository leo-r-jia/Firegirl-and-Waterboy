using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform destination;
    private Portal otherPortal;
    private bool otherPortalExists = false;
    public bool isLeft = false;
    public bool isRight = false;

    public float distance = 0.1f;
    public float portalThrust = 1000f;
    public float teleportCooldown = 0.5f;

    private bool isPlayer;
    private float teleportSpeed = 0.1f;

    // Gets location of the opposite portal & checks if other portal exists.
    void Update()
    {
        if (tag == "Blue Portal" && GameObject.FindGameObjectWithTag("Orange Portal") != null)
        {
            destination = GameObject.FindGameObjectWithTag("Orange Portal").GetComponent<Transform>();
            otherPortal = GameObject.FindGameObjectWithTag("Orange Portal").GetComponent<Portal>();
            otherPortalExists = true;
        }
        else if (tag == "Orange Portal" && GameObject.FindGameObjectWithTag("Blue Portal") != null)
        {
            destination = GameObject.FindGameObjectWithTag("Blue Portal").GetComponent<Transform>();
            otherPortal = GameObject.FindGameObjectWithTag("Blue Portal").GetComponent<Portal>();
            otherPortalExists = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Stops collider from running if teleporting.
        if (TeleportingCooldown.Instance.teleporting == true)
        {
            return;
        }

        if ((other.tag == "Player1" || other.tag == "Player2" || other.tag == "Box") && otherPortalExists) {

            if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
                isPlayer = true;

            if (Vector2.Distance(transform.position, other.transform.position) > distance)
            {
                StartCoroutine(Teleport(other));
            }
        }
    }

    // Teleports object to the opposite portal.
    private IEnumerator Teleport(Collider2D other)
    {
        //If object is a player, trigger dissolve animation
        if (isPlayer)
        {
            PlayerDissolve(other);
            yield return new WaitForSeconds(teleportSpeed);
        }

        TeleportingCooldown.Instance.teleporting = true;

        if (otherPortal.isLeft)
        {
            other.transform.position = new Vector2(destination.position.x + 1, destination.position.y);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(portalThrust, 2f));
        }
        else
        {
            other.transform.position = new Vector2(destination.position.x - 1, destination.position.y);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(-portalThrust, 2f));
        }

        //Trigger reappear animation
        if (isPlayer)
            PlayerAppear(other);

        yield return new WaitForSeconds(teleportCooldown);
        TeleportingCooldown.Instance.teleporting = false;
    }

    //Calls functions in the PlayTeleport script
    void PlayerDissolve(Collider2D other)
    {
        PlayerTeleport playerTeleport = other.gameObject.GetComponent<PlayerTeleport>();
        playerTeleport.Dissolve();
    }

    void PlayerAppear(Collider2D other)
    {
        PlayerTeleport playerTeleport = other.gameObject.GetComponent<PlayerTeleport>();
        playerTeleport.Appear();
    }
}
