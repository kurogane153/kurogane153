using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

    public float speed=0.08f;
    //雨プレハブ
    public GameObject rainPrefab;
    private GameObject cursor;

    void Start()
    {
        cursor= GameObject.Find("Cursor");
    }

    void Update () {

        float x = Input.GetAxis("Right");
        float y = Input.GetAxis("RightUp");
        gameObject.transform.position += new Vector3(x * speed, y * -speed);

        if (Input.GetButtonDown("O"))
        {
            GameObject rain = Instantiate(rainPrefab);
            rain.transform.position = GetRandomPosition();
        }
    }
    private Vector3 GetRandomPosition()
    {
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        return new Vector3(x, y, 0);
    }
}
