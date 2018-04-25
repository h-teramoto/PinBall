using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 得点管理クラス
/// Ballにアタッチする
/// 課題：得点を表示しよう対応
/// </summary>
public class ScoreManager : MonoBehaviour {

    //ポップアップするText
    [SerializeField]
    private GameObject popupPref;

    //点数を表示するText
    [SerializeField]
    private Text amountScoreText;

    //得点合計
    private int amountScore = 0;

    //スコア列挙
    enum ScoreEnum
    {
        SmallStarTag = 30,//小さい星
        LargeStarTag = 70,//大きい星
        SmallCloudTag = 50,//小さき雲
        LargeCloudTag = 100//大きい雲
    }

    /// <summary>
    /// 初期処理
    /// </summary>
    void Start()
    {
        //合計点数の初期化
        amountScoreText.text = string.Format("{0}", 0);
    }

    /// <summary>
    /// 点数ポップアップ処理
    /// </summary>
    /// <param name="col"></param>
    /// <param name="score"></param>
    private void Popup(Collision col,int score)
    {
        GameObject ins = GameObject.Instantiate(popupPref) as GameObject;
        Text txt = ins.GetComponentInChildren<Text>();
        ins.transform.position = col.transform.position + new Vector3(0,(float)1.2,0);
        txt.text = score.ToString();
    }

    
    /// <summary>
    /// 衝突時に呼ばれる
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter(Collision col)
    {
        if(Enum.IsDefined(typeof(ScoreEnum), col.collider.tag)) {

            int score = (int)Enum.Parse(typeof(ScoreEnum), col.collider.tag);

            //合計点数の加算して表示
            amountScore += score;
            amountScoreText.text = string.Format("{0}", amountScore);
            
            //当たった場所に点数をポップアップ表示
            Popup(col, score);
        }
    }


}
