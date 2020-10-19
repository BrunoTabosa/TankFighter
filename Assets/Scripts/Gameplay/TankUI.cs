using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankUI : MonoBehaviour
{
    [SerializeField]
    Slider health;

    [SerializeField]
    TankController TankController;

    private void Awake()
    {
        TankController.OnTankHealthChanged += OnTankHealthChanged;
    }

    public void OnTankHealthChanged(int current, int maximum)
    {
        health.value = (float)current / (float)maximum;
    }
}
