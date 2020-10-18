using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonLocal<DataManager>
{
    public TankStats BasicTank;
    public TankStats FastTank;

    private void Awake()
    {
        SetInstance(this);
        base.Awake();
    }
}
