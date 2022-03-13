using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHighScore : MonoBehaviour {
    private GameManager _gm;

    [SerializeField] private RectTransform scoreListBox;
    [SerializeField] private TMP_Text scoreRecordPrefab;

    [SerializeField] private Vector2 scoreRecordOffset = new Vector2(0, -40);
    [SerializeField] private float scoreRecordYDeltaSize = -60;

    [SerializeField] private GameObject playGameButton;

    private int _recordCount = 0;

    private float _yDelta = 0;
    // private bool _testCase = false;

    private void Awake() {
        _gm = GameManager.Instance;
    }

    private void Start() {
        if (_gm.data.playerName.Length < 1) {
            playGameButton.SetActive(false);
        }

        LoadScoreList();
    }

    public void LoadScoreList() {
        Dictionary<string, int> tmp = new Dictionary<string, int>() {
            // {"Player 1", 24},
            // {"Player 2", 21},
            // {"Player 3", 14}
        };

        AddString($"Всего рекордов: {_gm.data.bestScoreList.Count}");
        
        foreach (Data.BestScoreClass bestScoreElement in _gm.data.bestScoreList) {
            // Debug.Log(Data.Instance.bestScoreList.Count);
            
            // tmp.Add(bestScoreElement.playerName, bestScoreElement.points);
            AddRecord(bestScoreElement);
        }

        // foreach (KeyValuePair<string, int> pair in tmp) {
            // AddRecord(pair.Key, pair.Value);
        // }
    }

    public void AddString(string str) {
        TMP_Text recordText = Instantiate(scoreRecordPrefab, scoreListBox, false);
        recordText.rectTransform.position += new Vector3(0, _yDelta, 0);
        _yDelta += scoreRecordYDeltaSize;
        scoreListBox.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Abs(_yDelta) - 10);

        recordText.text = str;
    }

    public void AddRecord(string playerName, int points) {
        string str = $"{++_recordCount}) {playerName}: {points}";
        AddString(str);
    }

    public void AddRecord(Data.BestScoreClass bestScore) {
        string str = $"{++_recordCount}) {bestScore.playerName}: {bestScore.points} \r\n({UnixTimeStampToDateTime(bestScore.createdAt)})";
        AddString(str);
    }

    public static DateTime UnixTimeStampToDateTime(int unixTimeStamp) {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dateTime;
    }

    public void GoToStartMenu() {
        _gm.scenes.LoadStartMenu();
    }

    public void GoToSettings() {
        _gm.scenes.LoadSettings();
    }

    public void GoToPlayGame() {
        _gm.scenes.LoadGamePlay();
    }
    
    public void ExitGame() {
        _gm.ExitGame();
    }
}
