using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankStatsManager : SingletonLocal<TankStatsManager>
{
    public TankStats BasicTank;

    public void Awake()
    {
        SetInstance(this);
        base.Awake();
    }


}
