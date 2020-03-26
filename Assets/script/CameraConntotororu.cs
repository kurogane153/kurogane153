using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConntotororu : MonoBehaviour
{

    //カメラオブジェクト
    public GameObject mainCamera;
    //プレイヤーオブジェクト
    private GameObject Player;
    //z軸を調整。正の数ならプレイヤーの前に、負の数ならプレイヤーの後ろに配置する
    private float zAdjust = -12.0f;
    private float yAdjust = 5.0f;

    //X座調整
    public float X_camera = 0.0f;
    public float Y_camera = 0.0f;

    // カメラの移動につかうやつまとめ(Nodake)
    GameObject[] rains;
    public bool fix; // 雨に触れたかどうか
    private bool fix2 = false;
    private bool flg = false;
    private bool flg2 = false;
    public bool CameraMoveSwitch = false;
    float PlayerY_Save = 0.0f; // ジャンプする前のプレイヤーの位置

    void Start()
    {
        Player = GameObject.Find("Player");
        mainCamera = GameObject.Find("Main Camera");
    }

    void Update()
    {

        fix = false;
        PlayerOnGrand(); // プレイヤーが地面に立っているかどうか
        PlayerOnRain(); // プレイヤーが雨に当たっているかどうか
    }

    void FixedUpdate()
    {
        CameraMoveOnRain(); // カメラがプレイヤーに乗ったときのうごき
        if (!fix2)
        {
            mainCamera.transform.position = new Vector3(Player.transform.position.x,
               Y_camera, zAdjust);
        }
    }

    // プレイヤーが地面に触れているかどうか
    void PlayerOnGrand()
    {
        if (Player.transform.position.y <= -3.60f)
        {
            //fix = true;
            fix2 = false;
            if (Player.transform.position.y <= mainCamera.transform.position.y && !flg2)
            {
                Y_camera = Player.transform.position.y + yAdjust;
            }
            if (Player.transform.position.y >= mainCamera.transform.position.y)
            {
                Y_camera = Player.transform.position.y + yAdjust;
            }
        }

    }

    // プレイヤーが雨に当たっているかどうか
    void PlayerOnRain()
    {
        rains = GameObject.FindGameObjectsWithTag("Rain");
        for (int i = 0; i < rains.Length; ++i)
        {
            if (rains[i].gameObject.GetComponent<RainOperation>().fix == true)
            {
                fix = true;
            }
        }
    }
    // カメラが雨粒に乗ったとき、移動する
    void CameraMoveOnRain()
    {
        CameraMoveSwitch = false;
        if (fix == true)
        {
            CameraMoveSwitch = true; // カメラを移動する
            fix2 = true;
        }
        // カメラを動かすスイッチがオンのとき
        if (CameraMoveSwitch == true)
        {
            if (Player.transform.position.y >= mainCamera.transform.position.y)
            {
                Y_camera = mainCamera.transform.position.y;
                Y_camera += 0.10f;
                mainCamera.transform.position = new Vector3(Player.transform.position.x, Y_camera, zAdjust);
            }
            if (Player.transform.position.y <= mainCamera.transform.position.y)
            {
                Y_camera = mainCamera.transform.position.y;
                Y_camera -= 0.10f;
                mainCamera.transform.position = new Vector3(Player.transform.position.x, Y_camera, zAdjust);
            }
        }
        else
        {
            fix2 = false;
        }
    }

}
