using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainnyDelete : MonoBehaviour {

    GameObject obj;

    void Start()
    {
       obj = GameObject.Find("Rainny");
    }

    //当たった時の処理
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }
}
