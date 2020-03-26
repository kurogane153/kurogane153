using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOperation : MonoBehaviour {

    public float speed;
    private float MoveForceMultiplier;
    private float MoveKey;
    private float jumpspeed = 1000f;
    private float MoveSpeed;
    private bool isGrounded = true;

    Vector2 groundedStart;
    Vector2 groundedEnd;
    public LayerMask groundLayer;

    Rigidbody2D rb2d;
    bool active = false;

    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        MoveSpeed = 5.4f;
        MoveForceMultiplier = 2.9f;
        MoveKey = 60f;
    }
	
	void Update () {

        groundedStart = this.transform.position - this.transform.up * 1f;
        groundedEnd = this.transform.position + this.transform.up * 0.1f;
        isGrounded = Physics2D.Linecast(groundedStart, groundedEnd, groundLayer);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.Space) && !active)
        {
            active = true;
            rb2d.AddForce(new Vector2(rb2d.velocity.x, MoveForceMultiplier * (MoveKey * MoveSpeed - rb2d.velocity.y)));
        }
        Debug.Log("grounded ->" + isGrounded);
        Debug.DrawLine(
            groundedStart,
            groundedEnd,
            Color.red
            );
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        active = false;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        active = true;
    }
}
