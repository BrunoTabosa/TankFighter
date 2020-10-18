using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonLocal<UIManager>
{
    public SelectTankViewController SelectTankView;

    public override void Awake()
    {
        SetInstance(this);
        base.Awake();
    }

    public void ShowSelectTank() 
    {
        SelectTankView.Show();
    }
}
