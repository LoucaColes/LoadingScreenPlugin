using LSP.LS.Data;
using LSP.LS.Elements;
using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager instance = null;

    [SerializeField, Foldout("Components")] private Image backingImage = null;
    [SerializeField, Foldout("Components")] private LoadingBar loadingBar = null;
    [SerializeField, Foldout("Components")] private Spinner spinner = null;
    [SerializeField, Foldout("Components")] private LoadingMessage loadingMessage = null;
    [SerializeField, Foldout("Components")] private GameObject animatedCharacter = null;

    [SerializeField, Foldout("Settings")] private bool useLoadingBar = true;
    [SerializeField, Foldout("Settings")] private bool useSpinner = true;
    [SerializeField, Foldout("Settings")] private bool useLoadingMessage = true;
    [SerializeField, Foldout("Settings")] private bool useAnimatedCharacter = true;
    [SerializeField, Foldout("Settings")] private LoadingMessagesData loadingMessages = null;

    private AsyncOperation currentLoadOp = null;


    private void Awake()
    {
        CreateInstance();
    }

    // Start is called before the first frame update
    private void Start()
    {
        HookIntoLoadSystem();
        HideLoadingScreen();
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            UnhookFromLoadSystem();
        }
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

    private void HookIntoLoadSystem()
    {
        LoadManager.instance.OnLoadScene += ShowLoadingScreen;
    }

    private void UnhookFromLoadSystem()
    {
        LoadManager.instance.OnLoadScene -= ShowLoadingScreen;
    }

    public void ShowLoadingScreen(AsyncOperation loadOp)
    {
        currentLoadOp = loadOp;
        currentLoadOp.completed += OnLoadComplete;

        ResetLoadingScreen();

        StartCoroutine(UpdateLoadingScreen(loadOp));
    }

    private void OnLoadComplete(AsyncOperation loadOp)
    {
        currentLoadOp.completed -= OnLoadComplete;

        HideLoadingScreen();
    }

    private IEnumerator UpdateLoadingScreen(AsyncOperation loadOp)
    {
        while(loadOp.progress <= 1f)
        {
            if (useLoadingBar)
            {
                loadingBar.UpdateLoadingBar(loadOp.progress);
            }

            yield return null;
        }
    }

    private void HideLoadingScreen()
    {
        backingImage.gameObject.SetActive(false);

        if (useSpinner)
        {
            spinner.StopSpinner(); 
        }
    }

    private void ResetLoadingScreen()
    {
        backingImage.gameObject.SetActive(true);

        HideUnusedElements();
        ResetElements();
    }

    private void ResetElements()
    {
        if (useLoadingBar)
        {
            loadingBar.ResetLoadingBarProgress();
        }

        if (useSpinner)
        {
            spinner.StartSpinner();
        }

        if (useLoadingMessage)
        {
            loadingMessage.SetLoadingMessage(loadingMessages.GetRandomMessage());
        }
    }

    private void HideUnusedElements()
    {
        loadingBar.gameObject.SetActive(useLoadingBar);
        spinner.gameObject.SetActive(useSpinner);
        loadingMessage.gameObject.SetActive(useLoadingMessage);
        animatedCharacter.SetActive(useAnimatedCharacter);
    }

    #region Debug
    [Button("Quick Toggle Backing", EButtonEnableMode.Editor)]
    private void QuickToggleBacking()
    {
        bool active = backingImage.gameObject.activeSelf;
        backingImage.gameObject.SetActive(!active);
    }

    [Button("Test Loading Screen", EButtonEnableMode.Playmode)]
    private void TestLoadingScreen()
    {
        ResetLoadingScreen();
        StartCoroutine(TestScreen());
    }

    private IEnumerator TestScreen()
    {
        float timer = 0;

        while (timer < 5f)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            loadingBar.UpdateLoadingBar(timer / 5f);
        }

        HideLoadingScreen();
    }
    #endregion
}
