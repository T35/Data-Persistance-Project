using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Data : MonoBehaviour {
    public static Data Instance;

    public void SetDefault() {
        playerName = "Player";
        bestScore = new BestScoreClass();
        bestScore.SetDefault();
    }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else {
            Destroy(gameObject);
        }
    }

    public string playerName;
    public BestScoreClass bestScore;

    [Serializable]
    public class BestScoreClass {
        public string playerName;
        public int points;

        public void SetDefault() {
            playerName = "BestPlayer";
            points = 0;
        }
    }
    
    [Serializable]
    class SaveDataClass {
        public BestScoreClass bestScore;
    }

    public void LoadData() {
        string path = Application.persistentDataPath + "/data_saved.json";
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            SaveDataClass data = JsonUtility.FromJson<SaveDataClass>(json);

            bestScore = data.bestScore;
        }
        else {
            bestScore = new BestScoreClass();
            bestScore.SetDefault();
        }
    }

    public void SaveData() {
        SaveDataClass data = new SaveDataClass();
        data.bestScore = bestScore;

        string json = JsonUtility.ToJson(data);
        
        string path = Application.persistentDataPath + "/data_saved.json";
        File.WriteAllText(path, json);
    }
}
