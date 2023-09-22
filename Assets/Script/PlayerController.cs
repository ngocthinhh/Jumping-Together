using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbPlayer;

    public PhysicsMaterial2D bounceMat, normalMat;

    // JUMP
    public float jumpForce;
    public bool canJump;
    private bool touchGround;
    Animator animatorPlayer;

    // MOVE
    private float horizontalInput;
    private float verticalInput;
    public float speedMove;
    public bool canMove;

    // MOVE WHILE JUMPING
    public bool moveWhileJumping;
    public string side;

    // MOVE ON SCREEN
    public bool right;
    public bool left;
    public bool pressJump;
    public bool outJump;

    private void Awake()
    {
        rbPlayer = gameObject.GetComponent<Rigidbody2D>();

        animatorPlayer = gameObject.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        // CHECK TOUCH GROUND AND VELOCITY = 0, SO YOU CAN JUMP.
        if (touchGround
            && gameObject.GetComponent<Rigidbody2D>().velocity.x == 0f
            && gameObject.GetComponent<Rigidbody2D>().velocity.y == 0f)
        {
            canJump = true;
        }
        else if (!touchGround
            || gameObject.GetComponent<Rigidbody2D>().velocity.x != 0f
            || gameObject.GetComponent<Rigidbody2D>().velocity.y != 0f)
        {
            canJump = false;
        }
        //

        // CLICK LEFT MOUSE OR KEY SPACE TO JUMP
        if (/*Input.GetMouseButton(0) && canJump || */Input.GetKey(KeyCode.Space) && canJump || pressJump && canJump)
        {
            animatorPlayer.Play("ReadyToJump");
            jumpForce += 0.5f;
        }
        else if (/*Input.GetMouseButtonUp(0) && canJump || */Input.GetKeyUp(KeyCode.Space) && canJump || outJump && canJump)
        {
            animatorPlayer.Play("Jumping");
            rbPlayer.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpForce = 0.0f;

            moveWhileJumping = true;
        }
        else if (gameObject.GetComponent<Rigidbody2D>().velocity.x == 0f
            && gameObject.GetComponent<Rigidbody2D>().velocity.y == 0f)
        {
            animatorPlayer.Play("Idle");
        }
        //

        if (canMove)
        {
            if (horizontalInput > 0)
            {
                side = "Right";
            }
            else if (horizontalInput < 0)
            {
                side = "Left";
            }
            else if (verticalInput > 0)
            {
                side = "Up";
            }
        }
        
        if (moveWhileJumping)
        {
            if (side == "Left")
            {
                transform.Translate(Vector2.left * Time.deltaTime * speedMove);
            }
            else if (side == "Right")
            {
                transform.Translate(Vector2.right * Time.deltaTime * speedMove);
            }
        }    

        // MOVE
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (canMove)
        {
            transform.Translate(Vector2.right * Time.deltaTime * horizontalInput * speedMove);
        }
        //

        if (jumpForce == 0.0f && canJump)
        {
            canMove = true;
            outJump = false;
        }
        else if (jumpForce != 0.0f || !canJump)
        {
            canMove = false;
        }

        if (rbPlayer.velocity.y > 0.0f)
        {
            rbPlayer.sharedMaterial = bounceMat;
        }
        else
        {
            rbPlayer.sharedMaterial = normalMat;
        }

        MoveBySrceen();
    }

    // CHECK YOU TOUCH GROUND
    private void OnCollisionEnter2D(Collision2D collision)
    {
        moveWhileJumping = false;

        if (collision.gameObject.tag == "Ground")
        {
            touchGround = true;
        }
    }

    // CHECK YOU STAY GROUND
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            touchGround = true;
        }
    }

    // CHECK YOU JUMPED OUT OF GROUND
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            touchGround = false;
        }
    }

    public void OnDownRight()
    {
        right = true; 
    }

    public void OnUpRight()
    {
        right = false;
    }

    public void OnDownLeft()
    {
        left = true;
    }

    public void OnUpLeft()
    {
        left = false;
    }

    public void OnUp()
    {
        side = "Up";
    }

    public void OnJump()
    {
        pressJump = true;
    }

    public void OnUpJump()
    {
        pressJump = false;
        outJump = true;
    }

    void MoveBySrceen()
    {
        if (right)
        {
            horizontalInput = 1;
            if (canMove)
            {
                transform.Translate(Vector2.right * Time.deltaTime * horizontalInput * speedMove);
            }
        }
        else if (left)
        {
            horizontalInput = -1;
            if (canMove)
            {
                transform.Translate(Vector2.right * Time.deltaTime * horizontalInput * speedMove);
            }
        }
        else
        {
            horizontalInput = 0;
        }
    }
}
