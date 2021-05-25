using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleChar : MonoBehaviour
{
    public bool isPlayer;
    public string[] movesAvailable;

    public string charName;
    public int currentHp, maxHP, currentMP, maximumMP, strength, defense, wpnPower, armorPower;
    public bool hadDied;

    public SpriteRenderer theSprite;
    public Sprite deadSprite;
    public Sprite aliveSprite;

    public bool shouldFade = false;
    public float fadeSpeed = 1f;

    private void Start() 
    {
        if(theSprite == null)
        {
            theSprite = this.gameObject.GetComponent<SpriteRenderer>();

        }
        
    }

    private void Update() 
    {
        if(shouldFade)
        {
            theSprite.color = new Color(
                Mathf.MoveTowards(theSprite.color.r, 1f, (fadeSpeed * Time.deltaTime) * 0.2f),
                Mathf.MoveTowards(theSprite.color.g, 0f, (fadeSpeed * Time.deltaTime) * 0.2f),
                Mathf.MoveTowards(theSprite.color.b, 0f, (fadeSpeed * Time.deltaTime) * 0.2f), 
                1f);

            if(theSprite.color.g == 0)
            {
                gameObject.SetActive(false);
            }

        }
        
    }

    public void EnemyFade()
    {
        shouldFade = true;
    }



 




}
