using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackRain : MonoBehaviour {

    //雨プレハブ
    public GameObject rainPrefab;
    //経過時間
    private float time = 0f;
    //雨生成時間間隔
    public float interval;
    //X座標の最小値
    public float xMinPosition = 0f;
    //X座標の最大値
    public float xMaxPosition = 0f;
    //Y座標の最小値
    public float yMinPosition = 0f;
    //Y座標の最大値
    public float yMaxPosition = 0f;

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
        float x = Random.Range(xMinPosition, xMaxPosition);
        float y = Random.Range(yMinPosition, yMaxPosition);

        //Vector3型のPositionを返す
        return new Vector3(x, y, 0);
    }
}
