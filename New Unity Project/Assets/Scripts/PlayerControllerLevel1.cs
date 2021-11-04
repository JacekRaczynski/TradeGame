using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerLevel1 : PlayerController
{
    private float moveSpeed = 6f;
    private float jumpForce = 6f;
    private float killOffset = 0.3f;
    private Rigidbody rigidbody;

    private float startPositionX;
    private bool isMovingRight = true;
    private bool isFacingRight = true;
    public bool canDoubleJump = false;
    public bool isGround = true;
    public LayerMask groundLayer;
    public Animator animator;
   


    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float JumpForce { get => jumpForce; set => jumpForce = value; }

    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        isGround = true;
   
    }
    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            isGround = true;
            canDoubleJump= false;
        }
        if (GameManager.instance.currentGameState == GameManager.GameState.GS_GAME)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                isMovingRight = true;
                if (!isFacingRight)
                    flip();
                transform.Translate(0.0f, 0.0f, MoveSpeed * Time.deltaTime, Space.World);
             
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                isMovingRight = false;
                if (isFacingRight)
                    flip();
                transform.Translate(0.0f, 0.0f, -MoveSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (!isGround && canDoubleJump) DoubleJump();
              
                if (isGround && !canDoubleJump)
                {
                    rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    canDoubleJump = true;
                        isGround = false;
                    }
           
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            GameManager.instance.addCoins();
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Heart"))
        {
            GameManager.instance.addLives();
            other.gameObject.SetActive(false);
        }
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(this.transform.position, Vector3.down, 0.1f, groundLayer.value);
    }
    private void DoubleJump()
    {
        if(canDoubleJump)
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canDoubleJump = false;
        }
    }
    private void flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;
    }



}

        //     if (other.CompareTag("Heart"))
        //  {
        // if(other.gameObject.transform.position.y + killOffset < this.transfrom.position.y)
        /*
         * {
         * score += 10
         * }
         * subTrackLives();
         * if(lives<=0)
         * this.transform.position = startPosition;
         *
        //}

    }

}
*/