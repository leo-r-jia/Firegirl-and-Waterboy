using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;
    private float spacialAdjust = 0.5f;
    private float directionalAdjust = 1f;
    private float horizontalAdjust = 1f;
    public Portal bluePortal, orangePortal;

    private GameObject player;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        // Get player object
        player = GameObject.FindGameObjectWithTag("Player1");

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

        // If player is aiming up
        if (Input.GetKey("w"))
        {
            //isUpShot = true;
            // Prevent shot from colliding with floor
            transform.position = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
            direction = new Vector2(0, directionalAdjust * transform.localScale.y);
            if (player.transform.localScale.x < 0)
            {
                transform.Rotate(Vector3.forward * 180);
                horizontalAdjust = -horizontalAdjust;
            }
        }
        // If player is aiming down
        else if (Input.GetKey("s"))
        {
            //isDownShot = true;
            direction = new Vector2(0, -(directionalAdjust * transform.localScale.y));
            if (player.transform.localScale.x < 0)
            {
                transform.Rotate(Vector3.forward * 180);
                horizontalAdjust = -horizontalAdjust;
            }
        }

        // Player is aiming left or right
        else direction = transform.right;
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D target)
    {

        if (target.tag == "HorizontalWall" || target.tag == "VerticalWall")
        {
            GameObject bluePortals = GameObject.FindGameObjectWithTag("Blue Portal"); // Finds and destroys other portals
            Destroy(bluePortals);
            var portalPosition = new Vector3(transform.position.x + spacialAdjust, transform.position.y, transform.position.z); // creates new portal
            Portal portal = Instantiate(bluePortal, portalPosition, transform.rotation) as Portal;
            //portal.transform.localScale = transform.localScale;
            //bluePortal portal = Instantiate();
        }
        Destroy(gameObject);
    }
}
