using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialImageChanger : MonoBehaviour {

    [SerializeField]
    private Texture[] texture;

    private int textureNum = 0;

    private RawImage rawImage;

	private void Start () {

        rawImage = GetComponent<RawImage>();
        rawImage.texture = texture[textureNum];
    }

    /// <summary>
    /// 一つ次のテクスチャを表示
    /// </summary>
    public void NextTexture()
    {
        //Debug.Log(texture.Length +"："+ (textureNum + 1));
        if (texture.Length <= textureNum + 1) { return; }
        textureNum++;
        rawImage.texture = texture[textureNum];
    }

    /// <summary>
    /// 一つ前のテクスチャを表示
    /// </summary>
    public void BackTexture()
    {
        //Debug.Log(0 +"："+ (textureNum - 1));
        if (0 > textureNum - 1) { return; }
        textureNum--;
        rawImage.texture = texture[textureNum];
    }

}
