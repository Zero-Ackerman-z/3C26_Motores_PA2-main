using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ScoreData", menuName = "ScriptableObjects/ScoreData", order = 1)]
public class ScoreData : ScriptableObject
{
    public static ScoreData instance;

    public int totalScore;
    public List<int> TopScores = new List<int>(10); // Lista de los 10 mejores puntajes

    private void OnEnable()
    {
        if (instance == null)
            instance = this;
    }
    public bool IsHighScore(int score)
    {
        return score > TopScores[TopScores.Count - 1];
    }

    public void AddScore(int scoreToAdd)
    {
        // Agregar el puntaje al puntaje total
        totalScore += scoreToAdd;

        // Agregar el puntaje a la lista de los 10 mejores puntajes si es mayor que el puntaje más bajo actual
        if (scoreToAdd > TopScores[TopScores.Count - 1])
        {
            TopScores.Add(scoreToAdd);
            // Ordenar la lista de los 10 mejores puntajes de mayor a menor
            TopScores.Sort((a, b) => b.CompareTo(a));
            // Limitar la lista a los 10 mejores puntajes
            if (TopScores.Count > 10)
            {
                TopScores.RemoveAt(TopScores.Count - 1);
            }
        }
    }

    public void ResetScores()
    {
        // Reiniciar el puntaje total
        totalScore = 0;
    }
}
