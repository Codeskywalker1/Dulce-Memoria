// Este script controla el flujo del juego en un juego de memoria en Unity.
// Gestiona el tiempo de juego, el inicio y fin del juego, y determina al ganador.

using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Referencias a los controladores de jugador para los dos jugadores.
    public PlayerController player1Controller;
    public PlayerController player2Controller;

    // Duración total del juego en segundos.
    public float gameDuration = 60f;

    // Texto que muestra el tiempo restante de juego.
    public TextMeshProUGUI textoTiempo;

    // Tiempo restante de juego.
    private float timeLeft;

    void Start()
    {
        InitializeGame();
    }

    // Inicializa el juego, reiniciando y comenzando el juego.
    void InitializeGame()
    {
        ResetGame();
        StartGame();
    }

    // Inicia el juego, estableciendo el tiempo restante y comenzando el juego para ambos jugadores.
    void StartGame()
    {
        timeLeft = gameDuration;
        player1Controller.StartGame();
        player2Controller.StartGame();
        ActualizarTextoTiempo();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            EndGame();
        }
        ActualizarTextoTiempo();
    }

    // Actualiza el texto que muestra el tiempo restante de juego.
    void ActualizarTextoTiempo()
    {
        int minutos = Mathf.FloorToInt(timeLeft / 60);
        int segundos = Mathf.FloorToInt(timeLeft % 60);
        textoTiempo.text = minutos.ToString("00") + ":" + segundos.ToString("00");
    }

    // Verifica si el juego ha terminado basado en las condiciones de victoria.
    public void CheckGameEnd()
    {
        if (player1Controller.Score == player2Controller.Score && player1Controller.Score + player2Controller.Score == player1Controller.cards.Length * 50)
        {
            EndGame();
        }
        else if (player1Controller.Score + player2Controller.Score == player1Controller.cards.Length * 100)
        {
            EndGame();
        }
    }

    // Finaliza el juego, determina al ganador y carga la escena de resultados.
    public void EndGame()
    {
        string winner;
        int winningScore;
        if (player1Controller.Score > player2Controller.Score)
        {
            winner = "Jugador 1";
            winningScore = player1Controller.Score;
        }
        else if (player2Controller.Score > player1Controller.Score)
        {
            winner = "Jugador 2!";
            winningScore = player2Controller.Score;
        }
        else
        {
            winner = "Empate!";
            winningScore = player1Controller.Score; // Usamos el puntaje del jugador 1 para mostrar el puntaje con el que empataron
        }

        PlayerPrefs.SetString("Winner", winner);
        PlayerPrefs.SetInt("WinningScore", winningScore);
        PlayerPrefs.Save();

        Debug.Log("Soy El GameController, el ganador es: " + winner + "\n" +
                                  "Puntaje del ganador: " + winningScore);

        StartCoroutine(LoadResultsScene());
    }

    // Corrutina para cargar la escena de resultados de forma asincrónica.
    IEnumerator LoadResultsScene()
    {
        SceneManager.LoadScene("Resultados");
        yield return null;
    }

    // Reinicia el juego, reiniciando ambos controladores de jugador.
    void ResetGame()
    {
        player1Controller.ResetGame();
        player2Controller.ResetGame();
    }
}
