using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // variables

     public float moveSpeed = 5f;

    public Rigidbody2D theRB;
    public Animator myAnim;

    public static PlayerController playerInstance;

   
    // methods

    private void Start() 
    {
        if(playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);

        theRB = GetComponent<Rigidbody2D>();
    }


    private void Update() 
    {
        float delta = Time.deltaTime;

        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");

        theRB.velocity = new Vector2(xAxis, yAxis) * moveSpeed * delta;

        
        myAnim.SetFloat("moveX", xAxis);
        myAnim.SetFloat("moveY", yAxis);

        if(xAxis == 1 || xAxis == -1 || yAxis == 1 || yAxis == -1)
        {
            Debug.Log("X " + xAxis + " Y " + yAxis);
            myAnim.SetFloat("lastMoveX", xAxis);
            myAnim.SetFloat("lastMoveY", yAxis);
        }
        

        
    }



}
