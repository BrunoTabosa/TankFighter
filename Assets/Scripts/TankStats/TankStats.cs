﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tank Stats")]
public class TankStats : ScriptableObject
{
    public float MovementSpeed;
    public int MaximumHealth;
    public int ClipSize;
    public int BulletDamage;
    public float BulletSpeed;
    public float BulletLifeTime;
}
