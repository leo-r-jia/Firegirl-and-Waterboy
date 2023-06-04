using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSliding : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private AudioSource slideSoundEffect;

    private void Start()
    {
        slideSoundEffect = AudioManager.Instance.GetSound("Box Slide").source;
    }

    //Commented as cannot find a good sound effect
    /*void Update()
    {
        if (rb.velocity.x != 0)
        {
            slideSoundEffect.enabled = true;
        }
        else
        {
            slideSoundEffect.enabled = false;
        }
    }*/
}
