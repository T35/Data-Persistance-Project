using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Data : MonoBehaviour {
    public string playerName;

    public SettingsClass settings;

    public BestScoreClass BestScore {
        get {
            if (bestScoreList.Count > 0)
                return bestScoreList.First();

            return new BestScoreClass("", 0);
        }
    }

    public List<BestScoreClass> bestScoreList = new List<BestScoreClass>();

    public void AddBestScore(BestScoreClass bestScore) {
        for (int i = 0; i < bestScoreList.Count; i++) {
            if (bestScoreList[i].playerName == bestScore.playerName) {
                if (bestScore.points > bestScoreList[i].points) {
                    bestScoreList[i] = bestScore;
                    bestScoreList.Sort(SortPlayerBestScore);
                }

                return;
            }
        }

        bestScoreList.Add(bestScore);
        bestScoreList.Sort(SortPlayerBestScore);
    }

    private static int SortPlayerBestScore(BestScoreClass bestScoreA, BestScoreClass bestScoreB) {
        if (bestScoreA.points == bestScoreB.points) {
            // return bestScoreA.createdAt - bestScoreB.createdAt < 0 ? -1 : 1;
            return bestScoreA.createdAt - bestScoreB.createdAt;
        }
        return bestScoreB.points - bestScoreA.points;
    }

    public void SetDefault() {
        playerName = "Player";
        SetDefaultLoadingData();
    }
    
    public void SetDefaultLoadingData() {
        settings = SettingsClass.GetDefault();
    }
    
    [Serializable]
    public class SettingsClass {
        public float ballAccelerate;
        public float ballMaxVelocity;
        public float ballStartForce;

        public float paddleSpeed;

        public static SettingsClass GetDefault() {
            return new SettingsClass() {
                ballAccelerate = 0.01f,
                ballMaxVelocity = 3,
                ballStartForce = 2,
                
                paddleSpeed = 2
            };
        }
    }

    [Serializable]
    public class BestScoreClass {
        public string playerName;
        public int points;
        public int createdAt;

        public BestScoreClass(string playerName, int points) {
            this.playerName = playerName;
            this.points = points;
            createdAt = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public void SetDefault() {
            playerName = "Player";
            points = 0;
            createdAt = 0;
        }
    }

    [Serializable]
    class SaveDataClass {
        public List<BestScoreClass> bestScoreList;
        public SettingsClass settings;
    }

    public void LoadData() {
        string path = Application.persistentDataPath + "/data_saved.json";
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            SaveDataClass data = JsonUtility.FromJson<SaveDataClass>(json);

            bestScoreList = data.bestScoreList;
            settings = data.settings;
        }
        else {
            SetDefaultLoadingData();
        }
    }

    public void SaveData() {
        SaveDataClass data = new SaveDataClass();
        
        data.bestScoreList = bestScoreList;
        data.settings = settings;

        string json = JsonUtility.ToJson(data);
        string path = Application.persistentDataPath + "/data_saved.json";
        File.WriteAllText(path, json);
    }
}
