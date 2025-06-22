using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class BotonReiniciar : MonoBehaviour
{
    public Button reiniciarButton;
    public AudioClip buttonClickClip; // El clip de sonido que se reproducirá

    private AudioSource audioSource;

    void Start()
    {
        // Obtén el AudioSource del mismo GameObject
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        if (reiniciarButton != null)
        {
            reiniciarButton.onClick.AddListener(() => StartCoroutine(Inicio()));
            reiniciarButton.onClick.AddListener(PlaySound); // Añadir la función de sonido
        }
        else
        {
            Debug.LogError("Botón reiniciar no está asignado en el Inspector.");
        }
    }

    private IEnumerator Inicio()
    {
        Debug.Log("Botón 'Reiniciar' presionado, reproduciendo sonido.");
        yield return new WaitForSeconds(buttonClickClip.length); // Esperar a que el sonido termine
        SceneManager.LoadScene("MenuInicial");
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
