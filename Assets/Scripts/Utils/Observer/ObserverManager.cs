﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverManager : SingletonLocal<ObserverManager>
{
    public Action OnEnemyKilled;

    public override void Awake()
    {
        SetInstance(this);
        base.Awake();
    }
}
