using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCall : MonoBehaviour {

    [SerializeField]
    TitleChange _titleChange;

	// Use this for initialization
	void Start () {

        _titleChange.GetComponent<TitleChange>();
	}

    void ChangeTitle()
    {
        _titleChange.changeSprite();
    }
}
