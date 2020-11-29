using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class TextureManager : MonoBehaviour
{

    private static TextureManager _instance = null;

    public Dictionary<string, Texture2D> vumarkTextures = null;

    int filesToCount = 0;
    int filesAdded = 0;

    public Texture2D CurrentTextureToApply { get; set; } = null;

    public UnityEvent TexturesLoadedCallback;

    public bool SceneLoaded { get; private set; } = false;

    public static TextureManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<TextureManager>();
            }
            return _instance;
        }
    }


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        TexturesLoadedCallback = new UnityEvent();

        vumarkTextures = new Dictionary<string, Texture2D>();

        var fileNames = Directory.GetFiles(Application.persistentDataPath, " *.png");
        filesToCount = fileNames.Length;
        StartCoroutine(CheckIfAllFilesAreLoaded());
        foreach (var fileName in fileNames)
        {
            StartCoroutine(LoadFile(fileName));
        }
        Debug.Log("Loading " + filesToCount + " files");
    }

    private IEnumerator LoadFile(string url)
    {
        Debug.Log(url);
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("www error!!");
            Debug.Log(www.error);
        }
        else
        {
            string path = Path.GetFileNameWithoutExtension(url);
            vumarkTextures[path] = ((DownloadHandlerTexture)www.downloadHandler).texture;
            filesAdded++;
        }
    }

    private IEnumerator CheckIfAllFilesAreLoaded()
    {
        while (filesAdded != filesToCount)
        {
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        TexturesLoadedCallback.Invoke();
        SceneLoaded = true;
        
    }

    public void SaveTexture(Texture2D tex, string vumarkID)
    {
        vumarkTextures[vumarkID] = tex;
        string path = Path.Combine(Application.persistentDataPath, vumarkID + ".png");
        StartCoroutine(Save(tex, path));
    }

    private IEnumerator Save(Texture2D tex, string url)
    {
        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes(url, bytes);
        yield return null;
    }
}
