using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Data : MonoBehaviour {
    public static Data Instance;

    public string playerName;

    public BestScoreClass BestScore {
        get {
            if (bestScoreList.Count > 0)
                return bestScoreList.First();

            return new BestScoreClass() { playerName = "", points = 0 };
        }
    }

    [HideInInspector] public List<BestScoreClass> bestScoreList = new List<BestScoreClass>();

    public void AddBestScore(BestScoreClass bestScore) {
        for (int i = 0; i < bestScoreList.Count; i++) {
            if (bestScoreList[i].playerName == bestScore.playerName) {
                if (bestScore.points > bestScoreList[i].points) {
                    bestScoreList[i] = bestScore;
                }

                return;
            }
        }

        bestScoreList.Add(bestScore);

        bestScoreList.Sort(SortPlayerBestScore);
    }

    private static int SortPlayerBestScore(BestScoreClass bestScoreA, BestScoreClass bestScoreB) {
        return bestScoreB.points - bestScoreA.points;
    }

    public void SetDefault() {
        playerName = "Player";
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

    [Serializable]
    public class BestScoreClass {
        public string playerName;
        public int points;

        public void SetDefault() {
            playerName = "Player";
            points = 0;
        }
    }

    [Serializable]
    class SaveDataClass {
        public List<BestScoreClass> bestScoreList;
    }

    public void LoadData() {
        string path = Application.persistentDataPath + "/data_saved.json";
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            SaveDataClass data = JsonUtility.FromJson<SaveDataClass>(json);

            bestScoreList = data.bestScoreList;
        }
        else {
            //Что-то по-умолчанию, если нужно.
        }
    }

    public void SaveData() {
        SaveDataClass data = new SaveDataClass();
        data.bestScoreList = bestScoreList;

        string json = JsonUtility.ToJson(data);

        string path = Application.persistentDataPath + "/data_saved.json";
        File.WriteAllText(path, json);
    }
}
