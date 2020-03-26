using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    public float speed;
    private float MoveSpeed;
    private float JumpMoveSpeed;
    private float MoveForceMultiplier;
    private float MoveKey;
    private float GravityRate;

    private bool isMagJamp = false;
    private bool isJumping = false;
    private bool isJumpingCheck = true;
    private float jumpTimeCounter;
    private float _jumpPower;
    private float JumpPowerAttenuation;

    private float jumpspeed = 1000f;
    Rigidbody2D rb2d;
    bool active = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        jumpTimeCounter = 0.6f;
        _jumpPower = 15.4f;
        JumpMoveSpeed = 5.2f;
        MoveSpeed = 5.4f;
        MoveForceMultiplier = 2.9f;
        MoveKey = 3f;
        GravityRate = 0.45f;
        JumpPowerAttenuation = 0.82f;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed, 0, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        active = false;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        active = true;
    }

    void FixedUpdate()
    {
        if (active==false)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !active)
            {
                rb2d.AddForce(new Vector2(rb2d.velocity.x, MoveForceMultiplier * (MoveKey * MoveSpeed - rb2d.velocity.y))); 
                isJumpingCheck = false;
                isJumping = true;
                active = true;
            }
        } else {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumping = false;
            }
            if (!isJumping)
            {
                if (rb2d.velocity.y <= 10)
                {
                    rb2d.AddForce(new Vector2(0, MoveForceMultiplier * (MoveKey * JumpMoveSpeed - rb2d.velocity.y)));
                } else if (rb2d.velocity.y <= 0) {
                    rb2d.AddForce(new Vector2(rb2d.velocity.x * GravityRate, MoveForceMultiplier * (MoveKey * JumpMoveSpeed - rb2d.velocity.y))); 
                } else {
                    if (0 <= _jumpPower)
                    {
                        _jumpPower -= JumpPowerAttenuation * 2;
                        rb2d.AddForce(new Vector2(rb2d.velocity.x, MoveForceMultiplier * (MoveKey * JumpMoveSpeed - rb2d.velocity.y)));
                    } else {
                        rb2d.AddForce(new Vector2(rb2d.velocity.x * GravityRate, MoveForceMultiplier * (MoveKey * JumpMoveSpeed - rb2d.velocity.y)));
                    }
                }
            }
        }
        if (isJumping)
        {
            jumpTimeCounter -= 0.01f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _jumpPower -= JumpPowerAttenuation; 
                rb2d.AddForce(new Vector2(1 * _jumpPower, MoveForceMultiplier * (MoveKey * JumpMoveSpeed - rb2d.velocity.y)));
            } else if (Input.GetKeyUp(KeyCode.Space)) {
                _jumpPower -= JumpPowerAttenuation;
            }
            if (jumpTimeCounter < 0)
            {
                isJumping = false;
            }
            if (rb2d.velocity.y <= -1)
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumpingCheck = true;
            active = false;
        }
    }
}