using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonLocal<UIManager>
{
    public ViewController SelectTankView;
    public GameOverViewController GameOverViewController;
    public PlayerHUDViewController PlayerHUDViewController;

    private ViewController currentViewController;

    public override void Awake()
    {
        SetInstance(this);
        base.Awake();
    }

    public void ShowSelectTank() 
    {
        GameOverViewController.Hide();
        SelectTankView.Show();
    }
    public void ShowGameOver()
    {
        PlayerHUDViewController.Hide();
        GameOverViewController.Show();
    }

    public void ShowPlayerHUD()
    {
        PlayerHUDViewController.Show();
    }
    public void UpdateScore(int score)
    {
        PlayerHUDViewController.SetScore(score);
        GameOverViewController.SetScore(score);
    }

    public void UpdateAmmo(int currentAmmo)
    {
        PlayerHUDViewController.SetAmmo(currentAmmo);
    }
}
