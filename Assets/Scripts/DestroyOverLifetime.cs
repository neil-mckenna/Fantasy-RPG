using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverLifetime : MonoBehaviour
{
    public float lifeTime;

    private void Update() 
    {
        Destroy(gameObject, lifeTime);
        
    }

}
