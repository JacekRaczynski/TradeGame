using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerLevel1 : MonoBehaviour
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
    [Range(0.0f, 10.0f)]
    public float slider;
    public float obr;


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
        float dlug = 0.01f;
        Debug.DrawRay(transform.position, new Vector3(0,-1,-1)* obr);
        Debug.DrawRay(transform.position, new Vector3(0,-1,1)* obr);
        Debug.DrawRay(transform.position+ new Vector3(0,0,0.3f), new Vector3(0, -1, -1) * obr);
        Debug.DrawRay(transform.position+ new Vector3(0,0,-0.3f), new Vector3(0, -1, 1) * obr);



        // isGround = 
        // Physics.Raycast(this.transform.position, Vector3.down, 2f, groundLayer.value);

        animator.SetBool("isGround", isGround);
        animator.SetBool("isWalking", isWalking);
        if (IsGrounded())
        {
            isGround = true;
            canDoubleJump = false;
            animator.SetBool("isGround", true);
        }
        else isGround = false;
        if (GameManager.instance.currentGameState == GameManager.GameState.GS_GAME)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
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
                            transform.Translate(0.0f, 0.0f, MoveSpeed * joystick.GetInputAxis().x / 120, Space.World);
                            isWalking = true;
                            isLock = true;
                        }
                        else if (joystick.GetInputAxis().x <= -.025f)
                        {
                            isMovingRight = false;
                            if (isFacingRight)
                                flip();
                            transform.Translate(0.0f, 0.0f, MoveSpeed * joystick.GetInputAxis().x / 120, Space.World);
                            isWalking = true;
                            isLock = true;
                        }
                        else{
                            isWalking = false;
                            isLock = false;
                        }
                            break;
                        }
                case 2:
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
        else if (other.CompareTag("Kill"))
        {
            GameManager.instance.subTractLives();
        }  else if (other.CompareTag("DeadLine"))
        {
            GameManager.instance.GameOver();
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
        if (!isGround && canDoubleJump)
            {
                DoubleJump();
            }
            if ( isGround && !canDoubleJump)
            {
                rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
          
            isGround = false;
            StartCoroutine(ExampleCoroutine());
            canDoubleJump = true;
        }

    }
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        canDoubleJump = true;
    }
        private bool IsGrounded()
    {
        float dlug = 0.01f;
        return !(
            
      //Physics.Raycast(transform.position, Vector3.down * dlug, groundLayer.value) ||
            Physics.Raycast(transform.position, new Vector3(0, -1, -1) * dlug,obr, groundLayer.value) ||
            Physics.Raycast(transform.position, new Vector3(0, -1, 1) * dlug,obr, groundLayer.value) ||
            Physics.Raycast(transform.position + new Vector3(0, 0, 0.3f), new Vector3(0, -1, -1) * dlug, obr, groundLayer.value) ||
            Physics.Raycast(transform.position + new Vector3(0, 0, -0.3f), new Vector3(0, -1, 1) * dlug, obr, groundLayer.value) 
      //    Physics.Raycast(transform.position + new Vector3(0, 0, 0.3f), Vector3.down * dlug, groundLayer.value) ||
     //     Physics.Raycast(transform.position + new Vector3(0, 0, -0.3f), Vector3.down * dlug, groundLayer.value)

            );
          }
 
    private void DoubleJump()
    {
        if(canDoubleJump)
        {
            rigidbody.AddForce(Vector3.up * jumpForce/2, ForceMode.Impulse);
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
        leftClicked = clicked;
    }
    public void setRightClicked(bool clicked)
    {
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