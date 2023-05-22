using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform destination;

    public bool isOrange;
    public float distance = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.rotation = Quaternion.identity;
        if (isOrange == false)
        {
            destination = GameObject.FindGameObjectWithTag("Orange Portal").GetComponent<Transform>();
        }
        else
        {
            destination = GameObject.FindGameObjectWithTag("Blue Portal").GetComponent<Transform>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Vector2.Distance(transform.position, other.transform.position) > distance)
        {
            StartCoroutine(Waiter(other));
        }
    }

    private IEnumerator Waiter(Collider2D other)
    {
        yield return new WaitForSeconds(0.2f);
        other.transform.position = new Vector2(destination.position.x+2, destination.position.y+2);
        other.GetComponent<Rigidbody2D>().AddForce(new Vector2(200f, 2f));

    }
}
