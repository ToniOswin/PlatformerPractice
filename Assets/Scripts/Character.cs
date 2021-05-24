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
    [SerializeField]
    AudioClip jumpSound;
    [SerializeField]
    AudioSource _audioSource;

    [SerializeField]
    GameManager gameMan;
    PsyHook psyScript;


    Rigidbody2D Rb;

    bool isOnGround;
    [SerializeField]
    Transform checkGroundPos;
    [SerializeField]
    float checkGroundRadius;
    public LayerMask WhatISground;
    void Start()
    {
        Rb = gameObject.GetComponent<Rigidbody2D>();
        psyScript = gameObject.GetComponent<PsyHook>();
    }

    void Update()
    {
        isOnGround = Physics2D.OverlapCircle(checkGroundPos.position, checkGroundRadius, WhatISground);
        float horizontalInput = Input.GetAxis("Horizontal");

        //flip de sprite
        if (horizontalInput < 0) { SpRenderer.flipX = true; } else { SpRenderer.flipX = false; }
        if (isOnGround) { playerAnimator.SetBool("IsOnAir", false); } else { playerAnimator.SetBool("IsOnAir", true); }
        if(horizontalInput != 0f && isOnGround) { playerAnimator.SetBool("IsMoving", true); } else { playerAnimator.SetBool("IsMoving", false); }

        //move
        if(psyScript.isAtached == false)
        {
            transform.position += new Vector3(horizontalInput, 0, 0) * speed * Time.deltaTime;
        }
        Jump();

        if(transform.position.y <= -1)
        {
            gameMan.Die();
        }
    }

    void Jump()
    {
        if(isOnGround && Input.GetKeyDown(KeyCode.Space))
        {
            _audioSource.clip = jumpSound;
            _audioSource.Play();
            Rb.AddForce(new Vector2(0, jumpVelocity), ForceMode2D.Impulse);
        }
    }
}
