using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager<T> : MonoBehaviour
{
    public static T Instance;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
       
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetInstance(T instance)
    {
        Instance = instance;
    }

   
}
