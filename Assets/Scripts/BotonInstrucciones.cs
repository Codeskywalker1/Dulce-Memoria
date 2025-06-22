using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class BotonInstrucciones : MonoBehaviour
{
    public Button instruccionesButton;
    public AudioClip buttonClickClip; // El clip de sonido que se reproducirá

    private AudioSource audioSource;

    void Start()
    {
        // Obtén el AudioSource del mismo GameObject
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        if (instruccionesButton != null)
        {
            instruccionesButton.onClick.AddListener(() => StartCoroutine(Juego()));
            instruccionesButton.onClick.AddListener(PlaySound); // Añadir la función de sonido
        }
        else
        {
            Debug.LogError("Botón instrucciones no está asignado en el Inspector.");
        }
    }

    private IEnumerator Juego()
    {
        Debug.Log("Botón 'Instrucciones' presionado, reproduciendo sonido.");
        yield return new WaitForSeconds(buttonClickClip.length); // Esperar a que el sonido termine
        SceneManager.LoadScene("Juego");
    }

    private void PlaySound()
    {
        if (audioSource != null && buttonClickClip != null)
        {
            Debug.Log("Reproduciendo sonido de botón.");
            audioSource.PlayOneShot(buttonClickClip);
        }
        else
        {
            Debug.LogError("AudioSource o ButtonClickClip no está asignado.");
        }
    }
}
