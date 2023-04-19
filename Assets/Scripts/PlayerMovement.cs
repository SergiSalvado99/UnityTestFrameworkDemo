using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to execute the movement and the jump of the player (2D)

public class PlayerMovement : MonoBehaviour
{
    public float horizontalForce; //Force to use in Movement function to apply a X velocity, predefined in the Scene
    public float verticalForce; //Force to use in Jump function to apply a Y velocity, predefined in the Scene
    
    private bool isCurrentlyColliding; //Used to check the collision with the rectangle used as floor; tag of rectangle in scene is "Floor"
    
    private Rigidbody2D player; 

    private void Start () {

        player = GetComponent<Rigidbody2D>(); 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor")) 
        {
            isCurrentlyColliding = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isCurrentlyColliding = false;
        }
    }

    void FixedUpdate()
    {
        Movement(); //Better physics for Movement using FixedUpdate
    }
    
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isCurrentlyColliding) //Condition of Jump key (Space) and Collision is here to perform the JumpTest in an easy way 
        {
            Jump(); //Better Jump performance using frame update
        }
    }

    private void Movement()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horizontalAxis) < 0.1) //Condition to decrease the player velocity once the Movement keys are not pressed
        {
            player.velocity = new Vector2(horizontalAxis * horizontalForce, player.velocity.y); //As the player's x velocity is multiplied by a value less than 0.1, x velocity will decrease until it reaches 0.
        }
        else
        {
            if (0 > horizontalAxis) //Movement to left
            {
                player.AddForce(new Vector2(-horizontalForce, player.velocity.y)); 
                transform.localScale = new Vector3(-1, 1, 1); //Flip of the sprite in horizontal to fit with the left movement.
            }
            else if ( 0 < horizontalAxis) //Movement to right
            {
                player.AddForce(new Vector2(horizontalForce, player.velocity.y)); 
                transform.localScale = new Vector3(1, 1, 1); //Flip of the sprite in horizontal to original state
            }
        }
    }

    public float Jump()
    {
        float g = 9.8f;
        float maximumY = ((verticalForce / 5) * (verticalForce / 5))/(2*g); //Let's assume this formula provides us the maximum y position that the player will reach when making the jump.
        
        player.AddForce(new Vector2(horizontalForce, verticalForce));
        
        return maximumY;
    }

}
