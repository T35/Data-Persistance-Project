using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
    [SerializeField] private TMP_InputField playerNameField;
    
    public void SetPlayerNameFromField() {
        Data.Instance.playerName = playerNameField.text;
        // Debug.Log(playerNameField.text);
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void GoToHighScore() {
        SceneManager.LoadScene(2);
    }

    public void GoToSettings() {
        SceneManager.LoadScene(3);
    }
}
