using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip matchSound; // Asigna aquí el clip de audio para cuando se encuentra un par

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayMatchSound()
    {
        audioSource.PlayOneShot(matchSound);
    }
}
