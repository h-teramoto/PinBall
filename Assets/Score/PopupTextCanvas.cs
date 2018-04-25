using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// PopupTextCanvasの状態管理クラス
/// PopupTextCanvasにアタッチする
/// 課題：得点を表示しよう対応
/// </summary>
public class PopupTextCanvas : MonoBehaviour {

    //PopupしたTextが消えるスピード
    [SerializeField]
    private float fadeOutSpeed = 1f;

    //ポップアップするテキスト
    private Text _popupText;
    private Text popupText {
        set { this._popupText = value; }
        get
        {
            if (_popupText == null)
            {
                _popupText = this.GetComponentInChildren<Text>();
            }
            return _popupText;
        }
    }

    /// <summary>
    /// 初期処理
    /// </summary>
	void Start () {
        StartCoroutine("ExtinguishGradually");
    }

    /// <summary>
    /// PopupするTextを徐々に消していき、最後にDestroyする
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator ExtinguishGradually()
    {
        while (true)
        {
            popupText.color = new Color(popupText.color.r, popupText.color.g, popupText.color.b, popupText.color.a - fadeOutSpeed * Time.deltaTime);
            if (popupText.color.a <= 0f)
            {
                Destroy(gameObject);
                yield break;
            }

            yield return null;
        }
    }

}
