using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainOperation : MonoBehaviour {

    [SerializeField, Range(0,20f)] private float speed = 0.05f;
    public bool fix = false;

    void Update ()
    {
        
    }

    void FixedUpdate()
    {
        RainMove();
    }

    void RainMove()
    {
        if (fix == false)
        {
            this.transform.Translate(0, -speed, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        fix = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        fix = false;
    }
}
