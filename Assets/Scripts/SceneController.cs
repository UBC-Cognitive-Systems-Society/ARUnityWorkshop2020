using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] int levelCount = 1;
    int loadedLevelBuildIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        if (Application.isEditor)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene loadedLevel = SceneManager.GetSceneAt(i);
                if (loadedLevel.name.Contains("Environ"))
                {
                    SceneManager.SetActiveScene(loadedLevel);
                    loadedLevelBuildIndex = loadedLevel.buildIndex;
                    return;
                }
            }
        }
#endif
        StartCoroutine(LoadEnvironment(1));
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 1; i <= levelCount; i++)
        {
            if(Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                StartCoroutine(LoadEnvironment(i));
                return;
            }
        }
    }

    IEnumerator LoadEnvironment(int levelBuildIndex)
    {
        if (SceneManager.GetActiveScene().buildIndex == levelBuildIndex) yield break;
        enabled = false;
        if(loadedLevelBuildIndex > 0)
        {
            yield return SceneManager.UnloadSceneAsync(loadedLevelBuildIndex);
        }
        yield return SceneManager.LoadSceneAsync(levelBuildIndex, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(levelBuildIndex));
        loadedLevelBuildIndex = levelBuildIndex;
        enabled = true;
    }
}
