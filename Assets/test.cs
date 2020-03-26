using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    //ラインキャストで地面にいるかどうかの判定に必要なやーつ
    public bool ReinGrounded = false;
    Vector2 groundedStart;
    Vector2 groundedEnd;

    public LayerMask groundLayer;

	void Update ()
    {
        //地面判定取得
        groundedStart = this.transform.position - this.transform.up * 0.1f;
        groundedEnd = this.transform.position + this.transform.up * 1f;
        ReinGrounded = Physics2D.Linecast(groundedStart, groundedEnd, groundLayer);

        Debug.DrawLine(groundedStart, groundedEnd, Color.red);
    }
}
