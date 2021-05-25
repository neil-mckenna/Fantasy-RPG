using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{
    public Text damageText;
    public float lifeTime = 1f;
    public float moveSpeed = 1f;

    public float placementJitter = 0.5f;

    private void Update() 
    {
        Destroy(gameObject, lifeTime);
        this.transform.position += new Vector3(0f, moveSpeed * Time.deltaTime * 0.5f , 0f);
        
    }

    public void SetDamage(int damageAmount)
    {
        damageText.text = damageAmount.ToString();
        this.transform.position += new Vector3(
            Random.Range(-placementJitter * 2, placementJitter * 2),
            Random.Range(-placementJitter * 2, placementJitter * 2),
            0f);



    }



}
