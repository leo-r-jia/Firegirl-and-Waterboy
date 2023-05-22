using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;
    private float spacialAdjust = 0.5f;
    public Portal bluePortal, orangePortal;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {

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
