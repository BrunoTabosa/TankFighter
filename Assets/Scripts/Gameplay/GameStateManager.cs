using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : SingletonManager<GameStateManager>
{
    

    public void Awake()
    {
        SetInstance(this);
        base.Awake();
    }

    public void Play()
    {
        print("play click");
    }
}
