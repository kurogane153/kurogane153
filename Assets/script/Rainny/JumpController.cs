using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour {

    public bool fix=false;

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rain")
        {
            fix = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        fix = false;   
    }
}
