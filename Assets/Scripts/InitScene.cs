using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitScene : MonoBehaviour {
    [SerializeField] private GameManager prefabGameManager;
    
    private void Awake() {
        if (GameManager.Instance == null) {
            Instantiate(prefabGameManager);
        }
    }
}
