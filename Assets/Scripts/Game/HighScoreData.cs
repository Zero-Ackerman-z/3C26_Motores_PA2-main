using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HighScoreData", menuName = "ScriptableObjects/HighScoreData", order = 1)]
public class HighScoreData : ScriptableObject
{
    public List<int> highScores = new List<int>(10); // Lista de los 10 puntajes más altos

    // Método para agregar un puntaje a la lista si es lo suficientemente alto
    public void AddScore(int score)
    {
        // Agregar el puntaje si la lista está vacía o si es más alto que el puntaje más bajo en la lista
        if (highScores.Count == 0 || score > highScores[highScores.Count - 1])
        {
            highScores.Add(score);
            // Ordenar la lista en orden descendente
            highScores.Sort((a, b) => b.CompareTo(a));
            // Mantener solo los 10 puntajes más altos
            if (highScores.Count > 10)
            {
                highScores.RemoveAt(highScores.Count - 1);
            }
        }
    }

    // Método para verificar si un puntaje está entre los 10 puntajes más altos
    public bool IsInTop10(int score)
    {
        return highScores.Contains(score);
    }
}
