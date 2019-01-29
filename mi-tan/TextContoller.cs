using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextContoller : MonoBehaviour {

    public string[] sentence;
    [SerializeField]
    Text uiText;

    [SerializeField]
    GameObject _Canvas;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    // 一文字にかける時間
    float intervalForCharDisplay = 0.1f;

    public bool endFlag = false;

    // 現在表示している文章番号
    private int currentSentenceNum = 0;
    // 現在の文字列
    private string currentSentence = string.Empty;
    // 表示にかかる時間
    private float timeUntilDsiplay = 0;
    // 文字列の表示を開始した時間
    private float timeBeganDisplay = 1;
    // 表示中の文字数
    private int lastUpdateCharCount = -1;


    void Start()
    {
        Invoke("SetNextSentence", 1f);
    }

    void Update()
    {
        // 文章の表示完了 / 未完了
        if (IsDisplayComplete() && endFlag == false)
        {
            endFlag = true;

        }else if (endFlag == false)
        {
            // ボタンが押された
            if (Input.anyKeyDown)
            {
                timeUntilDsiplay = 0;
                endFlag = true;
            }
        }

        if(endFlag == true)
        {
            _Canvas.GetComponent<Animator>().SetTrigger("MouseMove");
            Debug.Log(endFlag);
        }

        // 表示される文字数を計算
        int displayCharCount = (int)(Mathf.Clamp01((Time.time - timeBeganDisplay) / timeUntilDsiplay) * currentSentence.Length);
        
        // 表示される文字数が表示している文字数と違う
        if(displayCharCount != lastUpdateCharCount)
        {
            uiText.text = currentSentence.Substring(0, displayCharCount);
            // 表示している文字数の更新
            lastUpdateCharCount = displayCharCount;
        }
    }




    // 次の文字数をセットする
    void SetNextSentence()
    {
            currentSentence = sentence[currentSentenceNum];
            timeUntilDsiplay = currentSentence.Length * intervalForCharDisplay;
            timeBeganDisplay = Time.time;
            currentSentenceNum++;
            lastUpdateCharCount = 0;
    }


    bool IsDisplayComplete()
    {
        return Time.time > timeBeganDisplay + timeUntilDsiplay;
    }


}
