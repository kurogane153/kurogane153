using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeDelete : MonoBehaviour {

    //当たった時の処理
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }
    }
}
