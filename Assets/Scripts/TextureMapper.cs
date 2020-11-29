using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureMapper : MonoBehaviour
{
    [SerializeField] Renderer[] renderers;
    [SerializeField] Texture2D tex;
    public void GetAllRenderers()
    {
        renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    public void SetTexture(Texture2D tex)
    {
        if (Application.isPlaying)
        {
            foreach (Renderer r in renderers)
            {
                r.material.mainTexture = tex;
            }
        }
        else
        {
            foreach (Renderer r in renderers)
            {
                r.sharedMaterial.mainTexture = tex;
            }
        }
    }

    public void ClearTexture()
    {
        SetTexture(null);
    }

    public void PreviewTex()
    {
        SetTexture(tex);
    }



}
