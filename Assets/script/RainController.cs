using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour
{
    //雨プレハブ
    public GameObject rainPrefab;
    //プレイヤーの座標参照用
    private GameObject Player;
    //経過時間
    private float time = 0f;
    //雨生成時間間隔
    public float interval;
    //X座標の最小値
    public float xMinPosition = -10f;
    //X座標の最大値
    public float xMaxPosition = 10f;
    //Y座標の最小値
    public float yMinPosition = 5f;
    //Y座標の最大値
    public float yMaxPosition = 10f;

    void Start()
    {
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        //時間計測
        time += Time.deltaTime;
        //経過時間が生成時間になったとき(生成時間より大きくなったとき)
        if (time > interval)
        {
            //雨をインスタンス化する(生成する)
            GameObject rain = Instantiate(rainPrefab);
            //生成した雨の位置をランダムに設定する
            rain.transform.position = GetRandomPosition();
            //経過時間のリセット
            time = 0f;
        }
    }
    //ランダムな位置を生成する関数
    private Vector3 GetRandomPosition()
    {
        //それぞれの座標をランダムに生成する
        float x = Random.Range(xMinPosition + Player.transform.position.x, xMaxPosition + Player.transform.position.x);
        float y = Random.Range(yMinPosition + Player.transform.position.y, yMaxPosition + Player.transform.position.y);

        //Vector3型のPositionを返す
        return new Vector3(x, y, 0);
    }
}
