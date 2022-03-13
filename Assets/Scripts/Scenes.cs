using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour {
    private GameManager _gm;
    
    public int launchingSceneIndex = -1;
    public int previousSceneIndex = -1;

    private void Awake() {
        _gm = GameManager.Instance;
    }
    
    private void PrepareLoadNewScene() {
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadPreviousScene() {
        int prevSceneIndex = previousSceneIndex;
        PrepareLoadNewScene();
        SceneManager.LoadScene(prevSceneIndex);
    }

    public void LoadGamePlay() {
        PrepareLoadNewScene();
        SceneManager.LoadScene(1);
    }

    public void LoadStartMenu() {
        PrepareLoadNewScene();
        SceneManager.LoadScene(0);
    }

    public void LoadHighScore() {
        PrepareLoadNewScene();
        SceneManager.LoadScene(2);
    }

    public void LoadSettings() {
        PrepareLoadNewScene();
        SceneManager.LoadScene(3);
    }
}
