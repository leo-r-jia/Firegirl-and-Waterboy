using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform destination;
    private Portal otherPortal;
    private bool otherPortalExists = false;
    public bool isLeft = false;
    public bool isRight = false;

    public float distance = 0.1f;
    public float portalThrust = 500f;
    public float teleportCooldown = 0.5f;

    void Start()
    {
        // Code may be implemented if a portal sprite is added.
        /*if (isRight)
        {
            transform.Rotate(0, 0, 180);
        }
        else if (isLeft)
        {
            transform.Rotate(0, 0, -180);
        }*/
    }

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

        if (other.tag != "VerticalWall" && other.tag != "HorizontalWall" && other.tag != "Orange Portal" && other.tag != "Blue Portal" && otherPortalExists) { 
            if (Vector2.Distance(transform.position, other.transform.position) > distance)
            {
                StartCoroutine(Teleport(other));
            }
        }
    }

    // Teleports object to the opposite portal.
    private IEnumerator Teleport(Collider2D other)
    {
        TeleportingCooldown.Instance.teleporting = true;
        
        if (otherPortal.isLeft)
        {
            other.transform.position = new Vector2(destination.position.x + 2, destination.position.y);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(portalThrust, 2f));
        }
        else
        {
            other.transform.position = new Vector2(destination.position.x - 2, destination.position.y);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(-portalThrust, 2f));
        }

        yield return new WaitForSeconds(teleportCooldown);
        TeleportingCooldown.Instance.teleporting = false;
      

    }

}
