using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float jumpVelocity;
    [SerializeField]
    Animator playerAnimator;
    [SerializeField]
    SpriteRenderer SpRenderer;


    Rigidbody2D Rb;

    bool isOnGround;
    [SerializeField]
    Transform checkGroundPos;
    [SerializeField]
    float checkGroundRadius;
    public LayerMask WhatISground;
    float jumpTimeCounter;
    [SerializeField]
    float jumpTime;
    bool isJumping;
    void Start()
    {
        Rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isOnGround = Physics2D.OverlapCircle(checkGroundPos.position, checkGroundRadius, WhatISground);
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector2 direction = new Vector2(horizontalInput,Rb.velocity.y).normalized;

        //flip de sprite
        if(horizontalInput < 0) { SpRenderer.flipX = true; } else { SpRenderer.flipX = false; }
        if (isOnGround) { playerAnimator.SetBool("IsOnAir", false); } else { playerAnimator.SetBool("IsOnAir", true); }
        if(direction.x != 0f && isOnGround) { playerAnimator.SetBool("IsMoving", true); } else { playerAnimator.SetBool("IsMoving", false); }

        //move
        if (direction.magnitude >= 0.1f)
        {
            gameObject.transform.Translate(direction * speed * Time.deltaTime);
        }
        //jump
        jump();
    }

    void jump()
    {
        if (isOnGround && Input.GetKeyDown(KeyCode.Space))
        {
            Rb.AddForce(Vector2.up * jumpVelocity * Time.deltaTime);
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                Rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
                jumpTimeCounter -= Time.deltaTime;
            }
            else { isJumping = false; }
        }
        if (Input.GetKeyUp(KeyCode.Space)) { isJumping = false; }
    }
}
