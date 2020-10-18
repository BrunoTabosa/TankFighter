using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonLocal<DataManager>
{
    public TankController BalancedTankPrefab;
    public TankController FastTankPrefab;
    public TankController HeavyTankPrefab;

    public RoomConfiguration RoomConfiguration;

    private void Awake()
    {
        SetInstance(this);
        base.Awake();
    }
}
