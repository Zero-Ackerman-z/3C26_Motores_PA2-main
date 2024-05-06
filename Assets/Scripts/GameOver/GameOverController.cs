using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class GameOverController : MonoBehaviour
{
    public Button btnPlay;
    public TextMeshProUGUI scoreText;
    public GameObject topScoresPanel;
    public GameObject topScoreEntryPanel;
    // Start is called before the first frame update
    void Start()
    {
        btnPlay.onClick.AddListener(() => ReturnMenu());
        int totalScore = ScoreData.instance.totalScore;
        scoreText.text = "Total Score: " + totalScore.ToString();

        List<int> topScores = ScoreData.instance.TopScores;
        for (int i = 0; i < Mathf.Min(topScores.Count, 10); i++) // Mostrar solo los 10 primeros puntajes
        {
            GameObject entry = new GameObject("TopScoreEntry_" + i);
            entry.transform.SetParent(topScoresPanel.transform); // Establecer el panel como padre
            RectTransform rectTransform = entry.AddComponent<RectTransform>();
            TextMeshProUGUI entryText = entry.AddComponent<TextMeshProUGUI>();
            entryText.text = (i + 1).ToString() + ". " + topScores[i].ToString();
        }
    }


    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
        ScoreData.instance.ResetScores();

    }

}
