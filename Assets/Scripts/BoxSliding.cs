using UnityEngine;

public class BoxSliding : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool doSoundEffect;
    private AudioSource slideSoundEffect;

    //Get the sliding sound effect on start
    private void Start()
    {
        slideSoundEffect = AudioManager.Instance.GetSound("Box Slide").source;
    }

    //Play the sound while the box is moving horizontally
    void Update()
    {
        if (doSoundEffect && rb.velocity.x != 0)
        {
            slideSoundEffect.enabled = true;
        }
        else
        {
            slideSoundEffect.enabled = false;
        }
    }
}
