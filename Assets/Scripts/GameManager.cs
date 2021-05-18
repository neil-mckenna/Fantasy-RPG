using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharStats[] playerStats;    

    private void Awake() 
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
        
    }




}
