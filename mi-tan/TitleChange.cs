using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleChange : MonoBehaviour {

    [SerializeField]
    Text _text;

    public void changeSprite()
    {
        _text.text = "";
    }
}
