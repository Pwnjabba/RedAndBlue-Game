using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed, followSpeed, acceleration, gravity;

    float sortingOrderCache;

    public float lastKeyPressTime = 1f;
    
    float horizontalMove, verticalMove;
    bool jumping, dashing;
    public bool isGrounded, nonPlayerGrounded;
    public float checkRadius, jumpForce, dashForce, dashTime, startDashTime;
    public LayerMask groundLayer, nonPlayerGroundLayer;
    Vector2 velocity;

    public Rigidbody2D rb;
    public BoxCollider2D col;
    public SpriteRenderer sprite;

    [SerializeField] Transform groundCheck, groundCheckL, groundCheckR;

    public bool isActive, canTeleportOtherCharacter;

    public PlayerController otherCharacter;
    [SerializeField] float maxDistanceFromOtherCharacter;
    public float distanceFromOtherCharacter, horizontalDistanceFromOtherCharacter;

    [SerializeField] PhysicsMaterial2D stickyMat, defaultMat;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        gravity = rb.gravityScale;
        dashTime = startDashTime;

        sortingOrderCache = sprite.sortingOrder;

        if (gameObject.CompareTag("Blue"))
        {
            otherCharacter = GameObject.FindGameObjectWithTag("Red").GetComponent<PlayerController>();
        }
        else
        {
            otherCharacter = GameObject.FindGameObjectWithTag("Blue").GetComponent<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();

        HandleSprite();


        if (isActive)
        {
            Jump();
        }


        if (Input.GetKeyDown(KeyCode.G))
        {
            float timeSinceLastClick = 1;
            timeSinceLastClick = Time.time - lastKeyPressTime;
            lastKeyPressTime = Time.time;

            if (timeSinceLastClick <= .25f && CharacterManager.instance.noCollide)
            {              
                    canTeleportOtherCharacter = true;
            }

        }
        if (canTeleportOtherCharacter)
        {
            lastKeyPressTime = 1f;
            canTeleportOtherCharacter = false;

            if (isActive)
                TeleportOtherCharacterToThis();
        }

        if (Input.GetKeyDown(KeyCode.E) && !dashing)
        {
            dashing = true;
            Dash(1);
        }

        while (dashing == true && dashTime > 0)
        {
            dashTime -= Time.deltaTime;
        }

        if (dashTime <= 0)
        {
            dashTime = startDashTime;
            dashing = false;
        }




    }

    private void FixedUpdate()
    {
        isGrounded = 
       Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer)
    || Physics2D.OverlapCircle(groundCheckL.position, checkRadius, groundLayer)
    || Physics2D.OverlapCircle(groundCheckR.position, checkRadius, groundLayer);

        nonPlayerGrounded = 
       Physics2D.OverlapCircle(groundCheck.position, checkRadius, nonPlayerGroundLayer)
    || Physics2D.OverlapCircle(groundCheckL.position, checkRadius, nonPlayerGroundLayer)
    || Physics2D.OverlapCircle(groundCheckR.position, checkRadius, nonPlayerGroundLayer);

        horizontalDistanceFromOtherCharacter = Mathf.Abs(transform.position.x - otherCharacter.transform.position.x);
        distanceFromOtherCharacter = Vector2.Distance(transform.position, otherCharacter.transform.position);

        if (isActive)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            PlayerMove();
        }

        else
        {
            rb.bodyType = RigidbodyType2D.Kinematic;

            if (!isGrounded)
                rb.velocity = new Vector2(0, -gravity);
            else
                rb.velocity = Vector2.zero;
        }





    }

    void PlayerInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        jumping = Input.GetKey(KeyCode.Space);
    }

    void PlayerMove()
    {
        //if (rb.sharedMaterial != defaultMat)
        //rb.sharedMaterial = defaultMat;

        velocity = NormalMovement();

        if (dashTime == startDashTime)
        rb.velocity = velocity;


        if (dashTime <= 0)
        {
            dashTime = startDashTime;
            dashing = false;
        }

        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            velocity = Vector2.left * dashForce;
        }
    }

    void Jump()
    {
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    //void AutoFollow()
    //{
    //    //if (rb.sharedMaterial != stickyMat)
    //    //rb.sharedMaterial = stickyMat;

    //    if (CharacterManager.instance.followMode && CanFollow() && WithinRange())
    //    {
    //        //if (otherCharacter.transform.position.x >= transform.position.x)
    //        //{
    //        //    rb.AddForce(new Vector2(followSpeed * horizontalDistanceFromOtherCharacter, 0));
    //        //}
    //        //else if (otherCharacter.transform.position.x < transform.position.x)
    //        //{
    //        //    rb.AddForce(new Vector2(-followSpeed * horizontalDistanceFromOtherCharacter, 0));
    //        //}
            
    //        transform.position = Vector2.MoveTowards(transform.position, otherCharacter.transform.position, followSpeed * Time.deltaTime);

    //        //if (isGrounded)
    //        //{
    //        //    float xMove = followSpeed * horizontalDistanceFromOtherCharacter * rb.mass * Time.deltaTime;
    //        //    if (otherCharacter.transform.position.x < transform.position.x)
    //        //    {
    //        //        xMove = -xMove;
    //        //    }
    //        //    velocity = new Vector2(xMove, 0);
    //        //}
    //        //else
    //        //{
    //        //    Debug.Log("Character out of range, double tap G to teleport your ally to you");
    //        //    velocity = new Vector2(rb.velocity.x, -gravity);
    //        //}

    //    }
    //    else
    //    {
    //        if (kinematicGrounded)
    //        {
    //            velocity = (Vector2.zero);
    //        }
    //        else
    //        {
    //            velocity = new Vector2(rb.velocity.x, -gravity);
    //        }
    //    }
    //   // rb.velocity = velocity;

    //}

    Vector2 NormalMovement()
    {
        return new Vector2(horizontalMove * speed, rb.velocity.y);
    }

    public bool CanFollow()
    {
        if (horizontalDistanceFromOtherCharacter >= 1.5f)
        {
            return true;
        }
        return false;
    }

    public bool WithinRange()
    {
        if (horizontalDistanceFromOtherCharacter <= 15)
        {
            return true;
        }
        return false;
    }

    void HandleSprite()
    {
        //sorting layer
        if (isActive)
        {
            sprite.sortingOrder = 100;
        }
        else
        {
            sprite.sortingOrder = (int)sortingOrderCache;
        }

        //flip sprite
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || rb.velocity.x < -.1f)
            {
                sprite.flipX = true;
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || rb.velocity.x > 0.1f)
            {
                sprite.flipX = false;
            }
        }
        else
        {
            if (otherCharacter.transform.position.x >= transform.position.x)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
        }

        //sprite color
        if (CharacterManager.instance.noCollide && !isActive)
        {
            sprite.color = CharacterManager.instance.inactiveColor;
        }
        else
        {
            sprite.color = CharacterManager.instance.defaultColor;
        }

    }

    public void ToggleActiveCharacter()
    {
        isActive = !isActive;
        rb.velocity = new Vector2(0, velocity.y);
    }

    public void TeleportOtherCharacterToThis()
    {
        canTeleportOtherCharacter = true;
        if (canTeleportOtherCharacter)
        {
            canTeleportOtherCharacter = false;
            otherCharacter.transform.position = new Vector2(transform.position.x, transform.position.y + 1.5f);
        }

    }

    public void Dash(int num)
    {
        if (num == 0)
        {
            rb.AddForce(Vector2.left * 100, ForceMode2D.Impulse);
        }
        else if (num == 1)
        {
            while(dashing && dashTime > 0)
            {
                velocity = Vector2.right * dashForce * rb.mass;
            }
        }
    }

    public void Launch(float force)
    {
        rb.velocity = Vector2.up * force;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == nonPlayerGroundLayer)
            nonPlayerGrounded = true;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == nonPlayerGroundLayer)
            nonPlayerGrounded = true;

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == nonPlayerGroundLayer)
            nonPlayerGrounded = false;
    }

}
