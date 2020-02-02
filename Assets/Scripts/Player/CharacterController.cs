using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent(typeof(ResourceThrower))]
public class CharacterController : MonoBehaviour
{

    public int playerNum = 0;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    public float speed = 10;
    public float acceleration = 0.2f;
    public float deceleration = 0.5f;
    /// <summary>
    /// The initial upward force impulse of a jump
    /// </summary>
    public float jumpImpulse = 9;
    /// <summary>
    /// How much force is applied if the player continues to hold the jump button
    /// </summary>
    public float jumpHoldVelocity = 2;
    /// <summary>
    /// How quickly the upward acceleration from holding jump stops doing anything
    /// </summary>
    public float jumpHoldVelocityDecay = 2;

    public GameObject jumpParticlesPrefab;
    public GameObject landParticlesPrefab;

    public AudioClip audioJump;

    public LayerMask groundLayers;

    [SerializeField]
	private float interactRadius = 1f;

    private Rewired.Player player;

    private float horizontalInput;
    private float verticalInput;
    private float aimHorizontalInput;
    private float aimVerticalInput;
    private bool jumpInput = false;
    private bool throwInput = false;
    private bool interactInput = false;
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
    private Vector2 aimDirection = new Vector2(1, 0);

    private ResourceThrower resourceThrower;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //get the player!
        player = ReInput.players.GetPlayer(playerNum);
        if (player == null) Debug.LogError("Player " + playerNum + " is null");
        resourceThrower = GetComponent<ResourceThrower>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //inputs
        if (player != null)
        {
            horizontalInput = player.GetAxis("Horizontal");
            verticalInput = player.GetAxis("Vertical");
            aimHorizontalInput = player.GetAxis("AimHorizontal");
            aimVerticalInput = player.GetAxis("AimVertical");
            if (aimHorizontalInput != 0 && aimVerticalInput != 0)
            {
                aimDirection = new Vector2(aimHorizontalInput, aimVerticalInput).normalized;
            }
            throwInput = player.GetButton("Throw");
            interactInput = player.GetButton("Interact");
            jumpInput = player.GetButton("Jump");
            if (player.GetButtonDown("Jump") && isGrounded)
            {
                //apply an impulse when the player presses jump
                rb.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
                jumpHold = true;
                isGrounded = false;
                currentJumpHoldVelocity = jumpHoldVelocity;
                audioSource.PlayOneShot(audioJump);
                //jump particles
                if(jumpParticlesPrefab != null) Instantiate(jumpParticlesPrefab, transform.position, Quaternion.identity);
            }
            if (!jumpInput) jumpHold = false;

            //swap resources
            if(player.GetButtonDown("SwapRight"))
            {
                resourceThrower.SwapResourceRight();
            }
            if (player.GetButtonDown("SwapLeft"))
            {
                resourceThrower.SwapResourceLeft();
            }
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
        RaycastHit2D boxCast = Physics2D.BoxCast(transform.position, new Vector2(boxCollider.size.x, boxCollider.size.y/2), 0, Vector2.down, boxCollider.size.y / 2 + 0.1f, groundLayers);
        if (boxCast.collider != null)
        {
            if(!isGrounded)
            {
                if (landParticlesPrefab != null) Instantiate(landParticlesPrefab, transform.position, Quaternion.identity);
            }
            isGrounded = true;
        } 
        else
        {
            isGrounded = false;
        }


		// Interact with breakable objects
		if (interactInput)
		{
			Collider2D breakableCollider = Physics2D.OverlapCircle(transform.position, interactRadius, 1 << LayerMask.NameToLayer("Breakable"));
			if (breakableCollider != null)
			{
				Breakable breakable = breakableCollider.GetComponentInParent<Breakable>();
				if (breakable != null)
					breakable.Activate();
			}
		}
    }

    private void FixedUpdate()
    {
        //horizontal speed
        rb.velocity = new Vector2(currentSpeed * speed, rb.velocity.y);

        if(jumpHold)
        {
            rb.AddForce(Vector2.up * currentJumpHoldVelocity);
        }


        // Get the velocity
        Vector2 horizontalMove = rb.velocity;
        // Don't use the vertical velocity
        horizontalMove.y = 0;
        // Calculate the approximate distance that will be traversed
        float distance = horizontalMove.magnitude * Time.fixedDeltaTime;
        // Normalize horizontalMove since it should be used to indicate direction
        horizontalMove.Normalize();
        RaycastHit hit;

        // Check if the body's current velocity will result in a collision
        RaycastHit2D boxCast = Physics2D.BoxCast(transform.position, new Vector2(boxCollider.size.x, boxCollider.size.y * 0.9f), 0, horizontalMove, distance, groundLayers);
        if(boxCast.collider != null)
        {
            // If so, stop the movement
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    /// <summary>
    /// Is the player standing on the ground?
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        return isGrounded;
    }

    /// <summary>
    /// Is the player holding the throw resource button?
    /// </summary>
    /// <returns></returns>
    public bool IsThrowingResource()
    {
        return throwInput;
    }

    /// <summary>
    /// Is the player holding the interact button?
    /// </summary>
    /// <returns></returns>
    public bool IsInteracting()
    {
        return interactInput;
    }

    /// <summary>
    /// Get the normalized aim direction
    /// </summary>
    /// <returns></returns>
    public Vector2 GetAimDirection()
    {
        return aimDirection;
    }
}
