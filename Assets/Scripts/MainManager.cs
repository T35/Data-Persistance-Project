using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainManager : MonoBehaviour {
    private GameManager _gm;

    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverMenu;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    private bool testCase = false;


    private void Awake() {
        _gm = GameManager.Instance;
    }

    // Start is called before the first frame update
    void Start() {
        if (_gm.IsLaunchingInCurrentScene()) {
            testCase = true;
            _gm.data.playerName = "TEST Player";
        }

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i) {
            for (int x = 0; x < perLine; ++x) {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        PrepareUI();
    }

    public void PrepareUI() {
        UpdateScoreField();
        UpdateBestScoreField();
    }

    private void Update() {
        if (!m_Started) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * _gm.data.settings.ballStartForce, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void UpdateScoreField() {
        ScoreText.text = $"Score ({_gm.data.playerName}): {m_Points}";
    }

    public void UpdateBestScoreField() {
        if (m_Points > 0) {
            if (_gm.data.bestScoreList.Count < 10) {
                _gm.data.AddBestScore(new Data.BestScoreClass(_gm.data.playerName, m_Points));
            }
            else if (_gm.data.bestScoreList.Count > 0) {
                if (m_Points >= _gm.data.bestScoreList.Last().points) {
                    _gm.data.AddBestScore(new Data.BestScoreClass(_gm.data.playerName, m_Points));
                }
            }
        }

        BestScoreText.text = $"Best Score: {_gm.data.BestScore.playerName}: {_gm.data.BestScore.points}";
    }

    void AddPoint(int point) {
        m_Points += point;
        UpdateScoreField();
        // UpdateBestScoreField();
    }

    public void GameOver() {
        m_GameOver = true;
        GameOverMenu.SetActive(true);
        UpdateBestScoreField();
        
        if (!testCase) {
            _gm.data.SaveData();
        }
    }
}
