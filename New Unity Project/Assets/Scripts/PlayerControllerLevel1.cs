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
    public bool isWalking = true;
    public LayerMask groundLayer;
    public Animator animator;
    [SerializeField]
    public JoystickClickTracker joystick;
    [SerializeField]
    private bool isLock;
    private bool leftClicked;
    private bool rightClicked;

   


    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float JumpForce { get => jumpForce; set => jumpForce = value; }
    public bool IsLock { get => isLock; }
  
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
        animator.SetBool("isGround", isGround);
        animator.SetBool("isWalking", isWalking);
        if (IsGrounded())
        {
            isGround = true;
            canDoubleJump= false;
            animator.SetBool("isGround", true);
        }
        if (GameManager.instance.currentGameState == GameManager.GameState.GS_GAME)
        {
                switch (PlayerPrefs.GetInt("Control", 0))
                {
                    case 0:
                        {
                        if (!rightClicked && !leftClicked)
                        {
                        
                            isWalking = false;
                            isLock = false;
                        }

                        else
                        {
                            if (Input.GetKey(KeyCode.RightArrow) || rightClicked)
                            {
                                isMovingRight = true;
                                if (!isFacingRight)
                                    flip();
                                transform.Translate(0.0f, 0.0f, MoveSpeed * Time.deltaTime, Space.World);
                                isWalking = true;
                                isLock = true;
                            }
                            if (Input.GetKey(KeyCode.LeftArrow) || leftClicked)
                            {
                                isMovingRight = false;
                                if (isFacingRight)
                                    flip();
                                transform.Translate(0.0f, 0.0f, -MoveSpeed * Time.deltaTime, Space.World);
                                isWalking = true;
                                isLock = true;

                            }
                        }
                            break;
                        }
                    case 1:
                        {
                        if (joystick.GetInputAxis().x >= .025f)
                        {
                            isMovingRight = true;
                            if (!isFacingRight)
                                flip();
                            transform.Translate(0.0f, 0.0f, MoveSpeed * joystick.GetInputAxis().x / 25, Space.World);
                            isWalking = true;
                            isLock = true;
                        }
                        else if (joystick.GetInputAxis().x <= -.025f)
                        {
                            isMovingRight = false;
                            if (isFacingRight)
                                flip();
                            transform.Translate(0.0f, 0.0f, MoveSpeed * joystick.GetInputAxis().x / 25, Space.World);
                            isWalking = true;
                            isLock = true;
                        }
                        else{
                            isWalking = false;
                            isLock = false;
                        }
                            break;
                        }

                }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            GameManager.instance.addBronzeCoin();
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("CoinSilver"))
        {
            GameManager.instance.addSilverCoin();
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("CoinGold"))
        {
            GameManager.instance.addGoldCoin();
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Heart"))
        {
            GameManager.instance.addLives();
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Key"))
        {
            GameManager.instance.addKeys();
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Exit"))
        {
        if(GameManager.instance.keysCompleted)
            {

                GameManager.instance.LevelCompleted();
            }
        }

    }
    public void Jump()
    {
        {
            if (!isGround && canDoubleJump)
            {
                DoubleJump();
            }
            if (isGround && !canDoubleJump)
            {
                rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                canDoubleJump = true;
                isGround = IsGrounded();
            }

        }
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(this.transform.position, Vector3.down, 0.2f, groundLayer.value);
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
        Quaternion theScale = transform.localRotation;

        theScale = Quaternion.AngleAxis(180, Vector3.up) * theScale;
        //     theScale.y *= -1;
        transform.localRotation = theScale;
    }

    public void setLeftClicked(bool clicked)
    {
        Debug.Log("Left " + clicked);
        leftClicked = clicked;
    }
    public void setRightClicked(bool clicked)
    {
        Debug.Log("right " + clicked);
        rightClicked = clicked;
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