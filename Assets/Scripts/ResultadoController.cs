using UnityEngine;
using TMPro;

public class ResultadoController : MonoBehaviour
{
    public TextMeshProUGUI resultadoText;
    public TextMeshProUGUI resultado2Text;

    void Start()
    {
        string winner = PlayerPrefs.GetString("Winner", "No Winner");
        int winningScore = PlayerPrefs.GetInt("WinningScore", 0);

        resultadoText.text = winner;
        resultado2Text.text = "Obtuviste: " + winningScore + "pts";
    }

}
