using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonLocal<UIManager>
{
    public ViewController SelectTankView;
    public ViewController GameOverView;

    public override void Awake()
    {
        SetInstance(this);
        base.Awake();
    }

    public void ShowSelectTank() 
    {
        GameOverView.Hide();
        SelectTankView.Show();
    }
    public void ShowGameOver()
    {
        GameOverView.Show();
    }
}
