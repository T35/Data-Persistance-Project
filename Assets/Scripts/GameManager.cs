using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    public Data data;
    public Scenes scenes;

    private void Awake() {
        InitSelf();
        InitData();
    }

    private void InitSelf() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            scenes.launchingSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void InitData() {
        data.LoadData();
    }

    public bool IsLaunchingInCurrentScene() {
        return scenes.launchingSceneIndex == SceneManager.GetActiveScene().buildIndex;
    }

    public void ExitGame() {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
