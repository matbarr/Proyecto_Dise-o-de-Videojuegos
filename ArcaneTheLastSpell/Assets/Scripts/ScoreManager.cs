using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;

    private int score = 0;

    private void Awake()
    {
        instance = this;
    }

    public void AddPoint()
    {
        score++;
        scoreText.text = "PUNTOS: " + score;
    }
}