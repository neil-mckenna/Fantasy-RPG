using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // variables

    

    public Rigidbody2D theRB;
    public float moveSpeed = 5f;


    // methods

    private void Start() 
    {
        theRB = GetComponent<Rigidbody2D>();
    }


    private void Update() 
    {
        float delta = Time.deltaTime;

        float xAxis = Input.GetAxisRaw("Horizontal") * delta;
        float yAxis = Input.GetAxisRaw("Vertical") * delta;

        theRB.velocity = new Vector2(xAxis, yAxis) * moveSpeed;

        



        

        
    }



}
