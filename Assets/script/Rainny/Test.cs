using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public float speed;
    private float maxjumpspeed = 1000f;
    private float jumpspeed = 1000f;
    Rigidbody2D rb2d;
    bool active = false;
    // 加速度計算用の位置情報
    private Vector3 oldPosition;
    // 現在の加速度
    private Vector3 velocity;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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
        if (Input.GetKeyDown(KeyCode.Space) && !active)
        {
            active = true;
            //rb2d.AddForce(Vector2.up * jumpspeed);
        }
        if (Input.GetKey(KeyCode.Space) && active)
        {

            Vector3 moveDirection = Vector3.zero;
            moveDirection += CalcJumping(moveDirection);
            rb2d.AddForce(Vector2.up * moveDirection * Time.deltaTime);
            // 移動処理後の加速度を計算
            velocity = (transform.position - oldPosition) / Time.deltaTime;
        }
    }

    protected Vector3 CalcJumping(Vector3 moveDirection)
    {
        // 着地時にジャンプボタンが押された場合は加速度を与える
        if (Input.GetKey(KeyCode.Space) && active)
        {
            velocity.y = jumpspeed;
        }

        // 空中でジャンプボタンが押されていない場合は加速度を0に戻す
        if (!Input.GetKey(KeyCode.Space) && !active)
        {
            if (0 <= velocity.y)
            {
                velocity.y = 0;
            }
            active = true;
        }

        moveDirection.y = velocity.y - rb2d.gravityScale * Time.deltaTime;
        moveDirection.y = Mathf.Max(moveDirection.y, -maxjumpspeed);

        return moveDirection;
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
