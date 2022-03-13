using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMain : MonoBehaviour {
    private GameManager _gm;
    
    private void Awake() {
        _gm = GameManager.Instance;
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
