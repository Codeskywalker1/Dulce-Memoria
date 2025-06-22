// Este script define el comportamiento de una carta en un juego de memoria en Unity.
// La carta puede estar en uno de dos estados: volteada o no volteada.
// Cuando se hace clic en la carta, cambia entre estos dos estados y notifica a un controlador de jugador.
// La carta también tiene la capacidad de ocultarse y reiniciarse a su estado original.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Card : MonoBehaviour
{
    // El objeto que se muestra en el frente de la carta.
    public GameObject front;
    // Referencia al controlador de jugador que maneja la lógica del juego.
    public PlayerController playerController;
    // Indica si la carta está volteada o no.
    private bool isFlipped = false;
    // Referencia al contorno de la carta.
    private Outline buttonOutline;

    // Color del contorno de la carta (dorado semi-transparente).
    private Color outlineColor = new Color32(233, 43, 127, 128);
    private float flipDuration = 0.35f;
    private float flipAngle = 180f;

    void Start()
    {
        // Verifica si el controlador de jugador está asignado.
        if (playerController == null)
        {
            Debug.LogError($"PlayerController not assigned to {gameObject.name}. Please assign it in the Inspector.");
            return;
        }

        // Configura el listener para el evento de clic en el botón de la carta.
        GetComponent<Button>().onClick.AddListener(OnClick);

        // Obtiene la referencia al componente de contorno de la carta.
        buttonOutline = GetComponent<Outline>();

        // Hace que el contorno de la carta sea transparente.
        SetOutlineTransparent();
    }

    // Hace que el contorno de la carta sea transparente.
    private void SetOutlineTransparent()
    {
        if (buttonOutline != null)
        {
            buttonOutline.enabled = true;
            buttonOutline.effectColor = new Color32(0, 0, 0, 0);
        }
    }

    // Hace que el contorno de la carta sea opaco y del color especificado.
    public void SetOutlineOpaque()
    {
        if (buttonOutline != null)
        {
            buttonOutline.enabled = true;
            buttonOutline.effectColor = outlineColor;
        }
    }

    // Método que se llama cuando se hace clic en la carta.
    public void OnClick()
    {
        if (!isFlipped)
        {
            Flip();
            if (playerController != null)
            {
                playerController.CardClicked(this);
            }
            else
            {
                Debug.LogError("playerController is null. CardClicked not called.");
            }
        }
    }

    public void Flip()
{
    isFlipped = !isFlipped;

    if (isFlipped)
    {
        StartCoroutine(FlipAnimationToFront());
    }
    else
    {
        StartCoroutine(FlipAnimationToBack());
    }
}

private IEnumerator FlipAnimationToFront()
{
    float t = 0f;
    float angle = 0f;
    while (t < 0.5f)
    {
        t += Time.deltaTime / flipDuration;
        angle = Mathf.Lerp(0f, flipAngle, t * 2);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        yield return null;
    }

    front.SetActive(true);

    while (t < 1f)
    {
        t += Time.deltaTime / flipDuration;
        angle = Mathf.Lerp(flipAngle, flipAngle * 2, (t - 0.5f) * 2);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        yield return null;
    }
}

private IEnumerator FlipAnimationToBack()
{
    float t = 0f;
    float angle = flipAngle * 2;
    while (t < 0.5f)
    {
        t += Time.deltaTime / flipDuration;
        angle = Mathf.Lerp(flipAngle * 2, flipAngle, t * 2);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        yield return null;
    }

    front.SetActive(false);

    while (t < 1f)
    {
        t += Time.deltaTime / flipDuration;
        angle = Mathf.Lerp(flipAngle, 0f, (t - 0.5f) * 2);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        yield return null;
    }
}

    public void ResetCard()
    {
        isFlipped = false;
        front.SetActive(false);
        gameObject.SetActive(true);
        SetOutlineTransparent();
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
