using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHighScore : MonoBehaviour {
    [SerializeField] private RectTransform scoreListBox;
    [SerializeField] private TMP_Text scoreRecordPrefab;

    [SerializeField] private Vector2 scoreRecordOffset = new Vector2(0, -40);
    [SerializeField] private float scoreRecordYDeltaSize = -60;
    
    private float _yDelta = 0;

    public GameObject DataPrefab;
    // private bool _testCase = false;

    private void Start() {
        if (Data.Instance == null) {
            // _testCase = true;

            Instantiate(DataPrefab);
            Data.Instance.SetDefault();
            Data.Instance.LoadData();
        }
        
        LoadScoreList();
    }

    public void LoadScoreList() {
        Dictionary<string, int> tmp = new Dictionary<string, int>() {
            // {"Player 1", 24},
            // {"Player 2", 21},
            // {"Player 3", 14}
        };

        foreach (Data.BestScoreClass bestScoreElement in Data.Instance.bestScoreList) {
            // Debug.Log(Data.Instance.bestScoreList.Count);
            tmp.Add(bestScoreElement.playerName, bestScoreElement.points);
        }

        foreach (KeyValuePair<string, int> pair in tmp) {
            AddRecord(pair.Key, pair.Value);
        }
    }

    public void AddRecord(string playerName, int points) {
        TMP_Text recordText = Instantiate(scoreRecordPrefab, scoreListBox, false);
        recordText.rectTransform.position += new Vector3(0, _yDelta, 0);
        _yDelta += scoreRecordYDeltaSize;

        recordText.text = $"{playerName}: {points}";
    }
}
