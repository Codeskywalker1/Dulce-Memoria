using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class BotonMenuInicial : MonoBehaviour
{
    public Button comenzarButton;
    public AudioClip buttonClickClip; // El clip de sonido que se reproducirá

    private AudioSource audioSource;

    void Start()
    {
        // Obtén el AudioSource del mismo GameObject
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        if (comenzarButton != null)
        {
            comenzarButton.onClick.AddListener(() => StartCoroutine(Instrucciones()));
            comenzarButton.onClick.AddListener(PlaySound); // Añadir la función de sonido
        }
        else
        {
            Debug.LogError("Botón comenzar no está asignado en el Inspector.");
        }
    }

    private IEnumerator Instrucciones()
    {
        Debug.Log("Botón 'Comenzar' presionado, reproduciendo sonido.");
        yield return new WaitForSeconds(buttonClickClip.length); // Esperar a que el sonido termine
        SceneManager.LoadScene("Instrucciones");
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
