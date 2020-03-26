using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {

    [SerializeField]
    //　ポーズした時に表示するUIのプレハブ
    private GameObject pauseUIPrefab;
    //　ポーズUIのインスタンス
    private GameObject pauseUIInstance;

    private float Z_Camera = 0.0f;
    private float Y_Camera = 0.0f;
    private bool flg = false;

    void Update()
    {
        if (Input.GetButtonDown("Option")||Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseUIInstance == null)
            {
                pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
                Time.timeScale = 0f;
                flg = true;
            }
            else
            {
                Destroy(pauseUIInstance);
                Time.timeScale = 1f;
                flg = false;
            }
        }
        if (flg)
        {
            if (transform.position.z > -50)
            {
                Z_Camera = transform.position.z;
                Z_Camera += -1f;
                this.transform.position = new Vector3(transform.position.x, transform.position.y, Z_Camera);
            }
            if (transform.position.y < 10)
            {
                Y_Camera = transform.position.y;
                Y_Camera += 1f;
                this.transform.position = new Vector3(transform.position.x, Y_Camera, Z_Camera);
            }
        }
    }
}
