using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{ 
    public float speed;
    public Portal playerPortal;
    private GameObject player;
    private Vector2 direction;
    private Rigidbody2D rb;
    private float spacialAdjust = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = transform.right;
        rb.velocity = direction * speed;
       
    }

    // Affects the bullet velocity.
    void Update()
    {
        rb.velocity = direction * speed;
    }

    // Bullets collides with an object
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "VerticalWall")
        {
            if (tag == "BlueShot")
            {
                // Deletes existing blue portals
                GameObject bluePortals = GameObject.FindGameObjectWithTag("Blue Portal"); // Finds and destroys blue portals
                Destroy(bluePortals);

                // Creates portal at bullet position.
                var portalPosition = new Vector3(transform.position.x + spacialAdjust, transform.position.y, transform.position.z); // creates new portal
                Portal portal = Instantiate(playerPortal, portalPosition, transform.rotation) as Portal;

                // Checks if portal is left or right.
                if(direction.x > 0)
                    portal.isRight = true;
                else if (direction.x < 0)
                    portal.isLeft = true;
            }
            else if (tag == "OrangeShot")
            {
                // Deletes existing orange portals
                GameObject orangePortals = GameObject.FindGameObjectWithTag("Orange Portal"); // Finds and destroys other portals
                Destroy(orangePortals);

                // Creates portal at bullet position.
                var portalPosition = new Vector3(transform.position.x + spacialAdjust, transform.position.y, transform.position.z); // creates new portal
                Portal portal = Instantiate(playerPortal, portalPosition, transform.rotation) as Portal;

                // Checks if portal is left or right.
                if (direction.x > 0)
                    portal.isRight = true;
                else if (direction.x < 0)
                    portal.isLeft = true;
            }
            Destroy(gameObject);
        }
        if (target.tag == "Box")
        {
            Destroy(gameObject);
        }
    }
}
