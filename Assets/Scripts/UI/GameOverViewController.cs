using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverViewController : ViewController
{
    [SerializeField]
    TextMeshProUGUI finalScore;
    public void OnPlayAgainClick()
    {
        GameStateManager.Instance.Play();
    }

    public void SetScore(int score)
    {
        finalScore.text = score.ToString();
    }
}
