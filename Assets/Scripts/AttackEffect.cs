using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{

    public float effectLength;
    public int soundEffect;

    private void Start() 
    {
        AudioManager.instance.PlaySFX(soundEffect);
        
    }

    private void Update() 
    {
        Destroy(gameObject, effectLength);
    }





}
