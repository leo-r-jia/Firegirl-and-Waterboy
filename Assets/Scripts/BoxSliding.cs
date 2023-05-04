using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSliding : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioSource slideSoundEffect;

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x != 0)
        {
            slideSoundEffect.enabled = true;
        }
        else
        {
            slideSoundEffect.enabled = false;
        }
    }
}
