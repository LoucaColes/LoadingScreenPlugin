using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int firstLevelIndex = 1;

    public void LoadFirstLevel()
    {
        LoadManager.instance.LoadSceneAsync(firstLevelIndex);
    }
}
