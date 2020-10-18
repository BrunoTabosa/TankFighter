using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTankViewController : ViewController
{
    public void SelectBalancedTank()
    {
        SelectTank(DataManager.Instance.BalancedTankPrefab.name);
    }
    public void SelectFastTank()
    {
        SelectTank(DataManager.Instance.FastTankPrefab.name);
    }
    public void SelectHeavyTank()
    {
        SelectTank(DataManager.Instance.HeavyTankPrefab.name);
    }

    void SelectTank(string prefabName)
    {
        GameStateManager.Instance.SpawnPlayer(prefabName);
        Hide();
    }
}
