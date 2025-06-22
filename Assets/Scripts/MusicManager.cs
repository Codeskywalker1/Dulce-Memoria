using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance = null;

    // Este método se llama cuando el objeto se inicializa
    void Awake()
    {
        // Si ya existe una instancia de MusicManager, destruir este objeto
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // Establecer la instancia a este objeto
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        // Obtener el componente AudioSource y comenzar la reproducción si no está reproduciendo
        AudioSource audioSource = GetComponent<AudioSource>();
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
