using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public static LoadManager instance = null;

    public delegate void LoadScene(AsyncOperation loadOp);
    public LoadScene OnLoadScene;

    private void Awake()
    {
        CreateInstance();
    }

    private void CreateInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadSceneAsync(int index)
    {
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(index);
        OnLoadScene?.Invoke(loadOp);
    }
}
