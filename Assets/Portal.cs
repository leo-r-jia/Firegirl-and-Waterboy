using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform destination;

    public bool isOrange;
    public float distance = 0.1f;
    public bool isLeft = false;
    public bool isRight = false;
    private bool otherPortal;
    public GameObject player;
    public float portalThrust = 500f;
    private float localScaleX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (isRight)
        {
            transform.Rotate(0, 0, 180);
        }
        else if (isLeft)
        {
            transform.Rotate(0, 0, -180);
        }
    }

    void Update()
    {
        if (isOrange == false)
        {
            destination = GameObject.FindGameObjectWithTag("Orange Portal").GetComponent<Transform>();
            otherPortal = GameObject.FindGameObjectWithTag("Orange Portal").GetComponent<Portal>().isLeft;
  
        }
        else
        {
            destination = GameObject.FindGameObjectWithTag("Blue Portal").GetComponent<Transform>();
            otherPortal = GameObject.FindGameObjectWithTag("Blue Portal").GetComponent<Portal>().isLeft;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag != "VerticalWall" && other.tag != "HorizontalWall" && other.tag != "Orange Portal" && other.tag != "Blue Portal") { 
            if (Vector2.Distance(transform.position, other.transform.position) > distance)
            {
                StartCoroutine(Waiter(other));
            }
        }
    }

    private IEnumerator Waiter(Collider2D other)
    {
        yield return new WaitForSeconds(0.2f);

        if (otherPortal)
        {
            other.transform.position = new Vector2(destination.position.x + 2, destination.position.y);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(portalThrust, 2f));
        }
        else
        {
            other.transform.position = new Vector2(destination.position.x - 2, destination.position.y);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(-portalThrust, 2f));
        }
    }
}
