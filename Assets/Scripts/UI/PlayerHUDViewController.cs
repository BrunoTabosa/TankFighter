using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHUDViewController : ViewController
{
    [SerializeField]
    TMP_Text TextScore;

    [SerializeField]
    TMP_Text TextAmmo;


    private void Start()
    {
        SetScore(0);
    }

    public void SetScore(int newScore)
    {
        TextScore.text = newScore.ToString();
    }

    public void SetAmmo(int current, int maximum)
    {
        TextAmmo.text = $"{current}/{maximum}";
    }
}
