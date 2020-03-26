using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJump : MonoBehaviour {

    //移動速度
    public float speed;

    //ジャンプキー入力
    private int jumpKey = 0;
    private float x;

    //ラインキャストで地面にいるかどうかの判定に必要なやーつ
    private bool isGrounded = true;
    Vector2 groundedStart;
    Vector2 groundedEnd;

    private bool isLeftGrounded = true;
    Vector2 leftgroundedStart;
    Vector2 leftgroundedEnd;

    private bool isRightGrounded = true;
    Vector2 rightgroundedStart;
    Vector2 rightgroundedEnd;

    public LayerMask groundLayer;

    //自身のRigidbody
    Rigidbody2D rb2d;

    //ジャンプに必要なもの
    private float JumpTimeCounter = 0.5f;
    private bool isJumpingCheck = true;
    private bool isJumping = false;
    private float JumpPower = 15f;
    private float gravityRate = 1.5f;
    private float jumpPowerAttenuation = 0.5f;

    //調査中
    //GameObject JumpManager;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //調査中
        //JumpManager = GameObject.Find("JumpManager");
    }

    void Update()
    {
        //地面判定取得
        //真下
        groundedStart = this.transform.position - this.transform.up * 1.2f;
        groundedEnd = this.transform.position + this.transform.up * 0.1f;
        isGrounded = Physics2D.Linecast(groundedStart, groundedEnd, groundLayer);
        //左下
        leftgroundedStart = this.transform.position - this.transform.right * 0.5f - this.transform.up * 1.2f ;
        leftgroundedEnd = this.transform.position + this.transform.up * 0.5f;
        isLeftGrounded = Physics2D.Linecast(leftgroundedStart, leftgroundedEnd, groundLayer);
        //右下
        rightgroundedStart = this.transform.position - this.transform.right * -0.5f - this.transform.up * 1.2f;
        rightgroundedEnd = this.transform.position + this.transform.up * 0.5f;
        isRightGrounded = Physics2D.Linecast(rightgroundedStart, rightgroundedEnd, groundLayer);

        //移動関連
        x = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed, 0, 0);
        }

        // ジャンプキー取得
        if (Input.GetButtonDown("X"))
        {
            jumpKey = 1;
        }
        else if (Input.GetButton("X"))
        {
            jumpKey = 2;
        }
        else if (Input.GetButtonUp("X"))
        {
            jumpKey = 0;
        }

        //デバッグ用
        Debug.DrawLine(groundedStart,groundedEnd, Color.red);
        Debug.DrawLine(leftgroundedStart, leftgroundedEnd, Color.red);
        Debug.DrawLine(rightgroundedStart, rightgroundedEnd, Color.red);

    }

    void FixedUpdate()
    {
        gameObject.transform.position += new Vector3(x * speed, 0);
        //地面にいるとき
        if (isGrounded || isLeftGrounded || isRightGrounded)
        {
            //飛べるかどうかのフラグがtrueかつジャンプキーが押されたら
            //各種フラグ,数値を代入
            if (isJumpingCheck && jumpKey != 0)
            {
                JumpTimeCounter = 0.7f;
                isJumpingCheck = false;
                isJumping = true;
                JumpPower = 35f;
            }
        }
        //地面にいない（空中にいる判定）
        else
        {
            //ジャンプキーが離されたらジャンプ中のフラグをfalseにする
            if (jumpKey == 0)
            {
                isJumping = false;
            }
            //if (rb2d.velocity.y <= -3)
            //{
            //    rb2d.AddForce(new Vector2(rb2d.velocity.x, 0));
            //}
            //else 

            //veloctityが規定値より下回ったら重力を使って落とす
            if (rb2d.velocity.y <= 0)
            {
                rb2d.AddForce(new Vector2(rb2d.velocity.x, Physics.gravity.y * gravityRate * 2.5f));
            }
            //veloctityが規定値よりも上回っていたら
            else
            {
                //ジャンプパワーがあるなら二倍の軽減率で減少させ飛ばす
                if (0 <= JumpPower)
                {
                    JumpPower -= jumpPowerAttenuation * 3;
                    rb2d.AddForce(new Vector2(rb2d.velocity.x, JumpPower));
                }
                //ないなら重力を使って落とす
                else
                {
                    rb2d.AddForce(new Vector2(rb2d.velocity.x, Physics.gravity.y * gravityRate * 2.5f));
                }
            }
        }
        
        // ジャンプ中
        if (isJumping)
        {
            JumpTimeCounter -= Time.deltaTime;
            //キー入力がある場合ジャンプパワーを軽減率で減少させAddForceで飛ばす
            if (jumpKey == 2)
            {
                JumpPower -= jumpPowerAttenuation;
                rb2d.AddForce(new Vector2(rb2d.velocity.x, JumpPower));
            }
            //キー入力がない場合ジャンプパワーを軽減率で減少させる（AddForceで飛ばさない）
            else if (jumpKey == 0)
            {
                JumpPower -= jumpPowerAttenuation*3;
            }
            //飛べる秒数のカウンターが０になったらジャンプを解除する
            if (JumpTimeCounter < 0)
            {
                isJumping = false;
            }
            //同様にvelocity.yが規定値よりも下回ったらジャンプを解除する
            if (rb2d.velocity.y < -1)
            {
                isJumping = false;
            }
        }
        //ジャンプキーが離されたら飛べるかどうかのフラグをtrueにする
        if (jumpKey == 0)
        {
            isJumpingCheck = true;
        }
    }
}
