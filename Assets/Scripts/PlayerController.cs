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

    public string areaTransitionName;
    private Vector3 playerBottomLeftLimit;
    private Vector3 playerTopRightLimit; 

   
    // methods

    private void Start() 
    {
        if(playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            if(playerInstance != this)
            {
                Destroy(gameObject);
            }
            
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
            //Debug.Log("X " + xAxis + " Y " + yAxis);
            myAnim.SetFloat("lastMoveX", xAxis);
            myAnim.SetFloat("lastMoveY", yAxis);
        }

        // keep the camera inside in the bounds
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, playerBottomLeftLimit.x, playerTopRightLimit.x),
            Mathf.Clamp(transform.position.y, playerBottomLeftLimit.y, playerTopRightLimit.y),
            transform.position.z
        );   
    }

    public void SetBounds(Vector3 botLeft, Vector3 topRight)
    {
        playerBottomLeftLimit = botLeft + new Vector3(0.5f, 1f, 0f);
        playerTopRightLimit = topRight + new Vector3(-0.5f, -0.5f, 0f);
    }



}
