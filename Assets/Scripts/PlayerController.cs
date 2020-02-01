using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{

    public int playerNum = 0;
    public Rigidbody2D rigidbody;
    public BoxCollider2D boxCollider;
    public float speed = 10;
    public float acceleration = 0.2f;
    public float deceleration = 0.5f;
    /// <summary>
    /// The initial upward force impulse of a jump
    /// </summary>
    public float jumpImpulse = 2;
    /// <summary>
    /// How much force is applied if the player continues to hold the jump button
    /// </summary>
    public float jumpHoldVelocity = 2;
    /// <summary>
    /// How quickly the upward acceleration from holding jump stops doing anything
    /// </summary>
    public float jumpHoldVelocityDecay = 2;

    private Rewired.Player player;

    private float horizontalInput;
    private float verticalInput;
    private bool jumpInput = false;
    /// <summary>
    /// Remains true for the duration of the player holding the jump button, and then is false until the player hits the ground again
    /// </summary>
    private bool jumpHold = false;
    /// <summary>
    /// How much jump hold force is currently being applied
    /// </summary>
    private float currentJumpHoldVelocity = 0;

    private float currentSpeed = 0;
    private bool isGrounded = false;


    // Start is called before the first frame update
    void Start()
    {
        //get the player!
        player = ReInput.players.GetPlayer(playerNum);
        if (player == null) Debug.LogError("Player " + playerNum + " is null");
    }

    // Update is called once per frame
    void Update()
    {
        //inputs
        if (player != null)
        {
            horizontalInput = player.GetAxis("Horizontal");
            verticalInput = player.GetAxis("Vertical");
            jumpInput = player.GetButton("Jump");
            if (player.GetButtonDown("Jump") && isGrounded)
            {
                //apply an impulse when the player presses jump
                rigidbody.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
                jumpHold = true;
                isGrounded = false;
                currentJumpHoldVelocity = jumpHoldVelocity;
            }
            if (!jumpInput) jumpHold = false;
        }

        //deceleration
        if(horizontalInput >= 0 && currentSpeed < 0)
        {
            currentSpeed += deceleration * Time.deltaTime;
            if (currentSpeed > 0) currentSpeed = 0;
        }
        if (horizontalInput <= 0 && currentSpeed > 0)
        {
            currentSpeed -= deceleration * Time.deltaTime;
            if (currentSpeed < 0) currentSpeed = 0;
        }

        //acceleration
        if (horizontalInput > 0)
        {
            if (currentSpeed < 1)
            {
                currentSpeed += acceleration * Time.deltaTime;
                if (currentSpeed >= 1) currentSpeed = 1;
            }
        }
        if (horizontalInput < 0)
        {
            if (currentSpeed > -1)
            {
                currentSpeed -= acceleration * Time.deltaTime;
                if (currentSpeed <= -1) currentSpeed = -1;
            }
        }

        //the force gained by holding jump decreases over time
        currentJumpHoldVelocity = Mathf.Max(0, currentJumpHoldVelocity - jumpHoldVelocityDecay * Time.deltaTime);

        //are we grounded?
        RaycastHit2D boxCast = Physics2D.BoxCast(transform.position, boxCollider.size, 0, Vector2.down, 0.02f, ~LayerMask.NameToLayer("Ground"));
        if (boxCast.collider != null)
        {
            isGrounded = true;
        } 
        else
        {
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        //horizontal speed
        rigidbody.velocity = new Vector2(currentSpeed * speed, rigidbody.velocity.y);

        if(jumpHold)
        {
            rigidbody.AddForce(Vector2.up * currentJumpHoldVelocity);
        }
    }

}
