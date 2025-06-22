// Este script controla la lógica del jugador en un juego de memoria en Unity.
// Gestiona el manejo de las cartas, la puntuación y la detección de pares de cartas iguales.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Array de cartas disponibles para el jugador.
    public Card[] cards;

    // Texto que muestra la puntuación del jugador.
    public TextMeshProUGUI scoreText;

    // Puntuación actual del jugador.
    private int score = 0;

    // Referencia a la primera carta seleccionada por el jugador.
    private Card firstCard;

    // Referencia a la segunda carta seleccionada por el jugador.
    private Card secondCard;

    // Número de pares de cartas encontrados por el jugador.
    private int matchesFound = 0;

    // Referencia al controlador del juego.
    private GameController gameController;

    // Nueva referencia al AudioManager
    private AudioManager audioManager; 

    // Propiedad para acceder a la puntuación del jugador desde otros scripts.
    public int Score
    {
        get { return score; }
    }

    void Start()
    {
        // Encuentra el controlador del juego en la escena.
        gameController = FindObjectOfType<GameController>();
        audioManager = FindObjectOfType<AudioManager>(); // Obtener la referencia al AudioManager
    }

    // Método para iniciar el juego, barajando y posicionando las cartas.
    public void StartGame()
    {
        ShuffleAndPositionCards();
    }

    // Método para barajar y posicionar las cartas en la escena.
    void ShuffleAndPositionCards()
    {
        List<int> positions = new List<int>();
        for (int i = 0; i < cards.Length; i++)
        {
            positions.Add(i);
        }

        positions = ShuffleList(positions);

        for (int i = 0; i < cards.Length; i++)
        {
            int position = positions[i];
            cards[i].transform.SetSiblingIndex(position);
        }
    }

    // Método para barajar una lista genérica.
    List<T> ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }

    // Método llamado cuando se hace clic en una carta.
    public void CardClicked(Card card)
    {
        if (firstCard == null)
        {
            firstCard = card;
        }
        else if (secondCard == null)
        {
            secondCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    // Corrutina para comprobar si las dos cartas seleccionadas forman un par.
    IEnumerator CheckMatch()
    {
        // Deshabilita la interacción con todas las cartas.
        foreach (var c in cards)
        {
            var button = c.GetComponent<Button>();
            if (button != null)
            {
                button.interactable = false;
            }
        }

        yield return new WaitForSeconds(0.80f);

        if (firstCard.front.GetComponent<Image>().sprite == secondCard.front.GetComponent<Image>().sprite)
        {
            score += 100;
            scoreText.text = "Pts: " + score;
            matchesFound++;
            firstCard.SetOutlineOpaque(); // Hacer el outline opaco para el primer par encontrado
            secondCard.SetOutlineOpaque(); // Hacer el outline opaco para el segundo par encontrado
            audioManager.PlayMatchSound(); // Reproducir el sonido cuando se encuentra un par
        }
        else
        {
            firstCard.Flip();
            secondCard.Flip();
        }

        firstCard = null;
        secondCard = null;

        // Habilita la interacción con todas las cartas.
        foreach (var c in cards)
        {
            var button = c.GetComponent<Button>();
            if (button != null)
            {
                button.interactable = true;
            }
        }

        // Verifica si el jugador ha encontrado todos los pares.
        if (matchesFound == 8)
        {
            gameController.EndGame(); // Llama directamente a EndGame() del GameController.
        }
    }

    // Método para reiniciar el juego.
    public void ResetGame()
    {
        score = 000;
        matchesFound = 0;
        firstCard = null;
        secondCard = null;
        scoreText.text = "Pts: " + score;

        foreach (var card in cards)
        {
            card.ResetCard();
            var button = card.GetComponent<Button>();
            if (button != null)
            {
                button.interactable = true; // Asegura que todas las cartas sean clicables después de reiniciar
            }
        }
    }
}
