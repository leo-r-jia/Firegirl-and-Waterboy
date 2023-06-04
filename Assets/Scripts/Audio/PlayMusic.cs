using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    [SerializeField] string musicName = "";

    //Play a music track on scene start
    void Start()
    {
        if (!string.IsNullOrEmpty(musicName))
            AudioManager.Instance.PlayMusic(musicName);
    }
}
