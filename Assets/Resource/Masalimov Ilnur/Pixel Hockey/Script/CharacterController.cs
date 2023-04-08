using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float speed;  // character speed
    public Rigidbody2D rb;  // body
    public Vector2 moveInput;  // direction vector
    public Vector2 moveVelocity;  // velocity of the direction vector
    private Vector2 targetVelocity; // target speed
    private Animator anin;  // animator character
    private bool facingRight = true; // the character looks to the right



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // finding our body
        anin = GetComponent<Animator>();  // we get an animator
    }


    void Update()
    {
        {
            moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));  // find out the direction vector
            moveVelocity = moveInput.normalized * speed; // multiply the speed by the vector
        }

        if (moveInput.x == 0)  // if the player's position on X is 0
        {
            anin.SetBool("isRuning", false);  // then the running animation is turned off
        }

        else  // in any other case
        {
            anin.SetBool("isRuning", true);  // running animation is enabled
        }

        if (!facingRight && moveInput.x > 0) // if we are not looking to the right and the direction vector on X is greater than 0
        {
            Flip();  // that turn
        }

        else if (facingRight && moveInput.x < 0)  // if we look to the right and the direction vector on X is less than 0
        {
            Flip();  // that turn
        }

        if (Input.GetKeyDown(KeyCode.Q))  // animation of the throw
        {
            anin.SetTrigger("Shot");
        }

        if (Input.GetKeyDown(KeyCode.E))  // animation of the pass
        {
            anin.SetTrigger("Pass");
        }

        if (Input.GetKeyDown(KeyCode.R))  // impact animation
        {
            anin.SetTrigger("Hit");
        }

        if (Input.GetKeyDown(KeyCode.T))  // animation of taking damage
        {
            anin.SetTrigger("Damage");
        }

        if (Input.GetKeyDown(KeyCode.Y))  // fall animation
        {
            anin.SetTrigger("Fall");
        }

        if (Input.GetKeyDown(KeyCode.U))  // animation joy
        {
            anin.SetTrigger("Joi");
        }

        if (Input.GetKey(KeyCode.W))  // animation up
        {
            anin.SetTrigger("Up");
        }

    }

    void FixedUpdate()
    {
        targetVelocity = Vector2.Lerp(targetVelocity, moveVelocity, 0.01f);  // interpolate the speed
        rb.MovePosition(rb.position + targetVelocity * Time.fixedDeltaTime);  // moving our body
    }


    //  player rotation
    private void Flip()  
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

}
