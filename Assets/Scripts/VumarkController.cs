using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;

public class VumarkController : MonoBehaviour
{
    TextureMapper texMapper;
    [SerializeField] VuMarkBehaviour vumark;
    [SerializeField] string url;

    [SerializeField] GameObject loader;

    private InstanceId currentId = null;

    public void Awake()
    {
        texMapper = GetComponent<TextureMapper>();
        vumark.RegisterVuMarkTargetAssignedCallback(GetVumarkID);
    }

    public void GetVumarkID()
    {
        if (vumark.VuMarkTarget.InstanceId != currentId)
        {
            currentId = vumark.VuMarkTarget.InstanceId;
            if (currentId.DataType == InstanceIdType.STRING)
            {
                Debug.Log(currentId.StringValue);
                ProcessVumarkID(currentId.StringValue);
            }
        }
    }

    private void ProcessVumarkID(string vumarkID)
    {
        
        if (TextureManager.Instance.vumarkTextures.TryGetValue(vumarkID, out Texture2D tex))
        {
            texMapper.SetTexture(tex);
        }
        else
        {
            loader.SetActive(true);
            StartCoroutine(GetTexture(url + vumarkID + "/image", vumarkID));
        }
    }

    IEnumerator GetTexture(string url, string vumarkID)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        loader.SetActive(false);
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            texMapper.SetTexture(myTexture);
            TextureManager.Instance.SaveTexture(myTexture, vumarkID);
        }
    }

}
