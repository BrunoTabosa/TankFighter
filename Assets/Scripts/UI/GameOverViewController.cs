using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverViewController : ViewController
{
    public void OnPlayAgainClick()
    {
        GameStateManager.Instance.Play();
    }
}
