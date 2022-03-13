using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartMenu : MonoBehaviour {
    private GameManager _gm;

    [SerializeField] private TMP_InputField playerNameField;

    private void Awake() {
        _gm = GameManager.Instance;
    }

    private void Start() {
        if (_gm.data.playerName.Length > 0) {
            playerNameField.text = _gm.data.playerName;
        }
    }

    public void SetPlayerNameFromField() {
        _gm.data.playerName = playerNameField.text;
        // Debug.Log(playerNameField.text);
    }

    public void StartGame() {
        if (_gm.data.playerName.Length > 0)
            _gm.scenes.LoadGamePlay();
    }

    public void GoToHighScore() {
        _gm.scenes.LoadHighScore();
    }

    public void GoToSettings() {
        _gm.scenes.LoadSettings();
    }

    public void ExitGame() {
        _gm.ExitGame();
    }
}
