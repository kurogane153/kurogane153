using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    //カメラオブジェクト
    public GameObject mainCamera;
    //プレイヤーオブジェクト
    private GameObject Player;
    //z軸を調整。正の数ならプレイヤーの前に、負の数ならプレイヤーの後ろに配置する
    public int zAdjust = 0;
    //X座調整
    public float X_camera = 0.0f;
    public float Y_camera = 0.0f;
    internal static object main;

    void Start()
    {
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        if (Player.transform.position.y > -2.8f)
        {
            //カメラはプレイヤーと同じ位置にする
            mainCamera.transform.position = new Vector3(0, Player.transform.position.y + Y_camera, Player.transform.position.z + zAdjust);
            //mainCamera.transform.Rotate(0.0f, 0.0f, 0.0f);
        }
    }

    internal Vector3 WorldToViewportPoint(Vector3 position)
    {
        throw new NotImplementedException();
    }
}
