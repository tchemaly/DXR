using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found on the GameObject.");
            return;
        }
        audioSource.Play();
    }
}
