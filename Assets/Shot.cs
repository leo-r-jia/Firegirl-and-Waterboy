using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;
    private float spacialAdjust = 0f;
    public bool isRightShot;
    public Portal bluePortal, orangePortal;

    private GameObject player;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

        // Player is aiming left or right
        direction = transform.right;
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "VerticalWall")
        {
            if (tag == "BlueShot")
            {
                GameObject bluePortals = GameObject.FindGameObjectWithTag("Blue Portal"); // Finds and destroys blue portals
                Destroy(bluePortals);
                var portalPosition = new Vector3(transform.position.x + spacialAdjust, transform.position.y, transform.position.z); // creates new portal
                Portal portal = Instantiate(bluePortal, portalPosition, transform.rotation) as Portal;

                // Checks if portal is left or right.
                if (portal.transform.position.x > 0)
                    portal.isRight = true;
                else if (portal.transform.position.x < 0)
                    portal.isLeft = true;
            }
            else if (tag == "OrangeShot")
            {
                GameObject orangePortals = GameObject.FindGameObjectWithTag("Orange Portal"); // Finds and destroys other portals
                Destroy(orangePortals);
                var portalPosition = new Vector3(transform.position.x + spacialAdjust, transform.position.y, transform.position.z); // creates new portal
                Portal portal = Instantiate(orangePortal, portalPosition, transform.rotation) as Portal;

                // Checks if portal is left or right.
                if (portal.transform.position.x > 0)
                    portal.isRight = true;
                else if (portal.transform.position.x < 0)
                    portal.isLeft = true;
            }
        }
        Destroy(gameObject);
    }
}
