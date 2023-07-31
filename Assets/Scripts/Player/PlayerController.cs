using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Using for text
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public float speed = 800.0f;
    public float jumpForce = 12.0f;
    private Rigidbody2D _body;
    private BoxCollider2D _box;
    private bool isGrounded = false;
    private bool isJump = false;
    private float horizontalInput;
    public float groundCheckRadius;


    public Transform groundCheck;
    public LayerMask groundLayer;


    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;


    private Animator _anim;
    // public Text scoreText;
    private int score;
    // private bool isCrouched;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _box = GetComponent<BoxCollider2D>();
        _anim = GetComponent<Animator>();
        score = 0;
        // scoreText.text = "Score: " + score;

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Side to Side Movement Arrow Keys
        float moveX = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        Vector2 movement = new Vector2(moveX, _body.velocity.y);
        _body.velocity = movement;

        // Pass Speed to animator to trigger conditions
        _anim.SetFloat("speed", Mathf.Abs(moveX));
        if (!Mathf.Approximately(moveX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(moveX), 1, 1);
        }

    }
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if(isGrounded && isJump){
            OnLandEvent.Invoke();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        // Ability to jump
        if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            _anim.SetBool("isJumping", true);
            _body.velocity = new Vector2(_body.velocity.x, jumpForce);
            isJump = true;
        }


        // if(isCrouched){
        //     speed = speed / 2;
        // } else {
        //     speed = speed * 2;
        // }
        if (isGrounded && Input.GetKeyDown(KeyCode.DownArrow))
        {
            _anim.SetBool("crouch", true);
            // isCrouched = true;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            _anim.SetBool("crouch", false);
            // isCrouched = false;
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            score += 10;
            // scoreText.text = "Score: " + score.ToString() ;
        }
    }

    public void OnLanding()
    {
        _anim.SetBool("isJumping", false);
        isJump = false;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded;
    }
}
