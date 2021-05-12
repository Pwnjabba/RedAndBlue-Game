using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed, gravity;

    float sortingOrderCache;

    public float lastKeyPressTime = 1f;
    
    float horizontalMove, verticalMove;
    bool jumping, dashing, canDash;
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

    public float mana, manaRegenSpeed;
    public ProgressBar manaBar;

    public GameObject spawnPoint;

    [SerializeField] PhysicsMaterial2D stickyMat, defaultMat;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        gravity = rb.gravityScale;

        if (manaBar != null)
        {
            mana = manaBar.maximum;
            manaBar.current = (int)mana;
        }


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
        if (manaBar != null)
        RegenerateMana();


        if (isActive)
        {
            Jump();
        }


        if (Input.GetKeyDown(KeyCode.G))
        {
            float timeSinceLastClick = 1;
            timeSinceLastClick = Time.time - lastKeyPressTime;
            lastKeyPressTime = Time.time;

            if (timeSinceLastClick <= .25f && CharacterManager.instance.noCollide && CharacterManager.instance.activeCharacter.isGrounded)
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

        if (transform.position.y <= -100)
        {
            transform.position = spawnPoint.transform.position;
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
            dashTime = startDashTime;
            dashCooldown = startDashCooldown;
            canDash = false;
            dashing = true;
        }
    }

    void PlayerMove()
    {
        dashCooldown -= Time.deltaTime;
        if (!dashing)
        {
            velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            if (isGrounded && dashCooldown <= 0)
            {
                canDash = true;
            }
        }

        else
        {
            if (sprite.flipX)
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

    public void StartDash(bool right, float dashForce)
    {
        if (!dashing)
        StartCoroutine(Dash(right, dashForce));
    }

    public IEnumerator Dash(bool right, float dashForce)
    {
        dashing = true;
        dashTime = startDashTime;
        Debug.Log("dashing");
        do
        {
            dashTime -= Time.deltaTime;
            if (right)
            {
                rb.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.left * dashForce, ForceMode2D.Impulse);
            }
        } while (dashing);
        dashTime = startDashTime;
        dashing = false;
        yield return new WaitForEndOfFrame();
    }

    void RegenerateMana()
    {
        manaBar.current = Mathf.Clamp(manaBar.current, 0, manaBar.maximum);
        if (manaBar.current < manaBar.maximum)
            mana += manaRegenSpeed * Time.deltaTime;

        manaBar.current = (int)mana;
    }

    public void Die()
    {
        if (isActive)
        {
            GameManager.instance.ReloadGame();
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

    private void OnDrawGizmos()
    {
        
    }

}
