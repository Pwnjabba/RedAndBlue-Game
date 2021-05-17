using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed, speedCache, gravity;

    float sortingOrderCache;

    public float lastKeyPressTime = 1f;

    public Transform lookUp, lookDown;

    public float maxSpeed;
    float horizontalMove, verticalMove;
    public bool sliding, canSlide;
    float slideTime;
    public float startSlideTime, slideSpeed;

    public bool jumping, dashing, canDash;
    public float dashTime, startDashTime, dashForce, dashCooldown, startDashCooldown;
    public bool isGrounded, nonPlayerGrounded;
    public float checkRadius, jumpForce;
    public LayerMask groundLayer, nonPlayerGroundLayer;
    Vector2 velocity;

    public Rigidbody2D rb;
    public BoxCollider2D col;
    public SpriteRenderer sprite;

    [SerializeField] Transform groundCheck, groundCheckL, groundCheckR;

    public bool isActive, canTeleportOtherCharacter;

    public PlayerController otherCharacter;

    //public float mana, manaRegenSpeed;
    //public ProgressBar manaBar;

    public GameObject spawnPoint;

    [SerializeField] PhysicsMaterial2D stickyMat, defaultMat;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        gravity = rb.gravityScale;
        speedCache = speed;
        //if (manaBar != null)
        //{
        //    mana = manaBar.maximum;
        //    manaBar.current = (int)mana;
        //}


        sortingOrderCache = sprite.sortingOrder;

        if (gameObject.CompareTag("Blue"))
        {
            otherCharacter = GameObject.FindGameObjectWithTag("Red").GetComponent<PlayerController>();
        }
        else
        {
            otherCharacter = GameObject.FindGameObjectWithTag("Blue").GetComponent<PlayerController>();
        }

        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        HandleSprite();

        //if (manaBar != null)
        //RegenerateMana();

        if (isActive)
        {
            Jump();
        }

        if (transform.position.y <= -100)
        {
            transform.position = CheckpointManager.instance.currentCheckPoint.transform.position;
        }

        if (isGrounded && dashCooldown <= 0)
        {
            canDash = true;
        }

        if (rb.velocity.magnitude >= maxSpeed)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }






    }

    private void FixedUpdate()
    {
        isGrounded =
       Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer)
    || Physics2D.Raycast(groundCheckL.position, Vector2.down, checkRadius, groundLayer)
    || Physics2D.Raycast(groundCheckR.position, Vector2.down, checkRadius, groundLayer);

        nonPlayerGrounded = 
       Physics2D.OverlapCircle(groundCheck.position, checkRadius, nonPlayerGroundLayer)
    || Physics2D.Raycast(groundCheckL.position, Vector2.down, checkRadius, nonPlayerGroundLayer)
    || Physics2D.Raycast(groundCheckR.position, Vector2.down, checkRadius, nonPlayerGroundLayer);


        if (isActive)
        {
            rb.sharedMaterial = defaultMat;
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
        if (Input.GetKeyDown(KeyCode.C) && canDash && gameObject.tag == "Red" && isActive)
        {
            GetComponent<RedAudio>().PlayDashSound();
            dashTime = startDashTime;
            dashCooldown = startDashCooldown;
            canDash = false;
            dashing = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && gameObject.tag == "Blue" && isActive && canDash)
        {
            if (isGrounded)
            {
                canDash = false;
                if (transform.localScale.y == 1)
                transform.position = new Vector2(transform.position.x, transform.position.y -.4f);
                dashCooldown = startDashCooldown;
                slideTime = startSlideTime;
                sliding = true;
            }
            //ice slide
        }
    }

    void PlayerMove()
    {
        dashCooldown -= Time.deltaTime;
        if (!dashing)
        {
            if (sliding)
            {
                speed = slideSpeed;
                slideTime -= Time.deltaTime;

                if (slideTime <= 0)
                {
                    speed = speedCache;
                    sliding = false;
                    if (!GetComponent<HeadCheck>().noHeadRoom)
                    transform.position = new Vector2(transform.position.x, transform.position.y + .5f);
                }
            }

            velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            if (transform.localScale.x < 0)
            {
                velocity = Vector2.left * dashForce;
            }
            else
            {
                velocity = Vector2.right * dashForce;
            }
        }

        if (dashing)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            dashTime -= Time.deltaTime;

            if (dashTime <= 0)
            {
                dashing = false;
            }
        }
            rb.velocity = velocity;
    }

    void Jump()
    {
        if (sliding)
        {
            return;
        }
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Z))
        {
            rb.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetKeyUp(KeyCode.Z) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
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
        Vector3 characterScale = transform.localScale;
        if (isActive)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                characterScale.x = 1;
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                characterScale.x = -1;
            }
            if (sliding)
            {
                characterScale.y = .5f;
            }
            else if (gameObject.tag == "Blue")
            {
                if (!GetComponent<HeadCheck>().noHeadRoom)
                characterScale.y = 1f;
            }

            //crouch and stretch
            //if (gameObject.tag == "Blue")
            //{
            //    if (Input.GetKeyDown(KeyCode.DownArrow) && isGrounded && !sliding)
            //    {
            //        transform.position = new Vector2(transform.position.x, transform.position.y - .5f);
            //    }
            //    if (Input.GetKeyUp(KeyCode.DownArrow) && isGrounded && !sliding)
            //    {
            //        transform.position = new Vector2(transform.position.x, transform.position.y + .5f);
            //    }
            //    if (Input.GetKey(KeyCode.DownArrow))
            //    {
            //        characterScale.y = .5f;

            //    }
                //else if (Input.GetKey(KeyCode.UpArrow) && !GetComponent<HeadCheck>().noHeadRoom)
                //{
                //    characterScale.y = 2f;
                //}
                //if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
                //{
                //    transform.position = new Vector2(transform.position.x, transform.position.y + 1f);
                //}

                //if (Input.GetKeyUp(KeyCode.UpArrow) && isGrounded)
                //{
                //    transform.position = new Vector2(transform.position.x, transform.position.y - 1f);
            //    //}
            //}


            transform.localScale = characterScale;
        }
        //if (isActive)
        //{
        //    if (Input.GetKeyDown(KeyCode.LeftArrow))
        //    {
        //        sprite.flipX = true;
        //    }
        //    else if (Input.GetKeyDown(KeyCode.RightArrow))
        //    {
        //        sprite.flipX = false;
        //    }
        //}

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
            otherCharacter.transform.position = new Vector2(transform.position.x + 3.5f, transform.position.y + 1f);
        }

    }

    //void RegenerateMana()
    //{
    //    manaBar.current = Mathf.Clamp(manaBar.current, 0, manaBar.maximum);
    //    if (manaBar.current < manaBar.maximum)
    //        mana += manaRegenSpeed * Time.deltaTime;

    //    manaBar.current = (int)mana;
    //}

    public void Die()
    {
        if (isActive)
        {
            ImmortalAudio.instance.PlayDeathSound();
            GameManager.instance.ReloadGame();
        }
    }

    public void Launch(float force)
    {
        if (gameObject.tag == "Red")
        {
            canDash = true;
        }
        rb.velocity = Vector2.up * force;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.layer == nonPlayerGroundLayer)
        //    nonPlayerGrounded = true;

        if (collision.transform.GetComponent<BaseEnemy>())
        {
            if (sliding)
            {
                collision.transform.GetComponent<BaseEnemy>().TakeDamage(10);
            }
        }
    }
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == nonPlayerGroundLayer)
    //        nonPlayerGrounded = true;

    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == nonPlayerGroundLayer)
    //        nonPlayerGrounded = false;
    //}


}
