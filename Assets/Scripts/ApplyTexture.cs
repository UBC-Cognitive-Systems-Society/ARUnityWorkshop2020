using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyTexture : MonoBehaviour
{
    RawImage img;
    Texture2D tex;


    // Start is called before the first frame update
    void Awake()
    {
        img = GetComponent<RawImage>();
    }

    public void Init(Texture2D tex)
    {
        this.tex = tex;
        img.texture = tex;
    }

    public void Submit()
    {
        TextureManager.Instance.CurrentTextureToApply = tex;
        SceneControllerAR.Instance.LoadARFoundationScene();
    }
}
