using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonLocal<DataManager>
{
    public PlayerController PlayerControllerPrefab;
    public TankController BalancedTankPrefab;
    public TankController FastTankPrefab;
    public TankController HeavyTankPrefab;

    public RoomConfiguration RoomConfiguration;

    private void Awake()
    {
        SetInstance(this);
        base.Awake();
    }

    public string GetRandomTank()
    {
        switch(Random.Range(0, 3))
        {
            case 0:
                return BalancedTankPrefab.name;
            case 1:
                return FastTankPrefab.name;
            case 2:
                return HeavyTankPrefab.name;
            default:
                return BalancedTankPrefab.name;
        }
    }
}
