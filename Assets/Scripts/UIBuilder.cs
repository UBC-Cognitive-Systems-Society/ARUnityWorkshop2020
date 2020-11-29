using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuilder : MonoBehaviour
{
    [SerializeField] ApplyTexture texturePrefab;
    [SerializeField] GameObject AddTexturePrefab;
    [SerializeField] GameObject defaultPrefab;
    [SerializeField] GameObject loader;

    void Start()
    {
        loader.SetActive(true);
        if (TextureManager.Instance.SceneLoaded)
        {
            BuildUI();
        }
    }

    private void OnEnable()
    {
        TextureManager.Instance.TexturesLoadedCallback.AddListener(BuildUI);
    }

    private void OnDisable()
    {
        TextureManager.Instance.TexturesLoadedCallback.RemoveListener(BuildUI);
    }

    // Update is called once per frame
    public void BuildUI()
    {
        Debug.Log("Called");
        loader.SetActive(false);
        Instantiate(defaultPrefab, this.transform);
        foreach(var loadedPair in TextureManager.Instance.vumarkTextures)
        {
            ApplyTexture button = Instantiate(texturePrefab, this.transform);
            button.Init(loadedPair.Value);
        }

        Instantiate(AddTexturePrefab, this.transform);
    }
}
