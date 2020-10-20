using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHUDViewController : ViewController
{
    [SerializeField]
    TMP_Text TextScore;

    int score;

    private void Start()
    {
        SetScore(0);
    }

    public void SetScore(int newScore)
    {
        score = newScore;
        TextScore.text = score.ToString();
    }

    public void AddScore(int points)
    {
        score += points;
        TextScore.text = score.ToString();
    }
}
