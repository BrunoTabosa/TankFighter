using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonLocal<T> : MonoBehaviour
{
    public static T Instance;

    public virtual void Awake()
    {
        if (Instance != null)
        {
            //Destroy(this.gameObject);
        }
       
        DontDestroyOnLoad(this.gameObject);
    }

    public virtual void SetInstance(T instance)
    {
        Instance = instance;
    }

   
}
