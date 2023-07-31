// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// // using UnityEngine.Events;

// public class JumpController : MonoBehaviour
// {
//     Rigidbody2D rb;
//     public Animator _anim;

//     public float jumpForce = 12.0f;

//     bool isGrounded;

//     public float groundCheckRadius;


//     public Transform groundCheck;
//     public LayerMask groundLayer;

//     // [Header("Events")]
//     // [Space]

// 	// public UnityEvent OnLandEvent;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         _anim = GetComponent<Animator>();

//     }

//     // Update is called once per frame
//     void Update()
//     {
//         isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
//         // Ability to jump
//         if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
//         {
//             OnLandEvent.Invoke();
//             _anim.SetBool("isJumping", true);
//             rb.velocity = new Vector2(rb.velocity.x, jumpForce);
//         }

//     }
//     public void OnLanding()
//     {
//         _anim.SetBool("isJumping", false);
//     }
// }
