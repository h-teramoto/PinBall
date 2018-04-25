using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripperController : MonoBehaviour {

    //HingiJointコンポーネントを入れる
    private HingeJoint myHingeJoint;

    //初期の傾き
    private float defaultAngle = 20;
    //弾いた時の傾き
    private float flickAngle = -20;

    // Use this for initialization
    void Start()
    {
        //HingeJointコンポーネント取得
        this.myHingeJoint = GetComponent<HingeJoint>();

        //フリッパーの傾きを設定
        SetAngle(this.defaultAngle);

        StartCoroutine("InputTouch");
    }


    //発展課題：スマートフォンでも動かせるようにマルチタッチに対応
    private IEnumerator InputTouch()
    {
        while (true)
        {
            //タッチがあった場合
            if (Input.touchCount > 0)
            {
                //タッチ箇所分処理する
                foreach (Touch touch in Input.touches)
                {
                    //tagがLeftFripperTagで画面の左半分がタッチされているか
                    //tagがRightFripperTagで画面の右半分がタッチされている場合にFlipperを動作させる
                    if ((Screen.width / 2 > touch.position.x && tag == "LeftFripperTag") || (Screen.width / 2 <= touch.position.x && tag == "RightFripperTag"))
                    {
                        //タッチされた場合
                        if (touch.phase == TouchPhase.Began)
                        {
                            //Flipperを上げる
                            SetAngle(this.flickAngle);
                        }
                        //タッチが離れた場合
                        else if (touch.phase == TouchPhase.Ended)
                        {
                            //Flipperを下げる
                            SetAngle(this.defaultAngle);
                        }
                    }
                }
            }

            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //左矢印キーを押した時左フリッパーを動かす
        if (Input.GetKeyDown(KeyCode.LeftArrow) && tag == "LeftFripperTag")
        {
            SetAngle(this.flickAngle);
        }
        //右矢印キーを押した時右フリッパーを動かす
        if (Input.GetKeyDown(KeyCode.RightArrow) && tag == "RightFripperTag")
        {
            SetAngle(this.flickAngle);
        }

        //矢印キー離された時フリッパーを元に戻す
        if (Input.GetKeyUp(KeyCode.LeftArrow) && tag == "LeftFripperTag")
        {
            SetAngle(this.defaultAngle);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && tag == "RightFripperTag")
        {
            SetAngle(this.defaultAngle);
        }
    }

    //フリッパーの傾きを設定
    public void SetAngle(float angle)
    {
        JointSpring jointSpr = this.myHingeJoint.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoint.spring = jointSpr;
    }
}
