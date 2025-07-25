using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private string currentSceneName;

    // 💥 공통 UIManager (씬마다 별도 존재)
    private UIManager uiManager;
    public UIManager UIManager => uiManager;

    private void Awake()
    {
        Instance = this;

        currentSceneName = SceneManager.GetActiveScene().name;

        // 공통 UIManager 참조 (씬에 존재하는 경우만)
        uiManager = FindObjectOfType<UIManager>();

        //  씬별 초기화 분기
        if (currentSceneName == "MiniGame1Scene") InitMiniGame1();
        else if (currentSceneName == "MiniGame2Scene") InitMiniGame2();
    }

    private void Start()
    {
        //  MiniGame1: 점수 UI 초기화
        if (currentSceneName == "MiniGame1Scene")
        {
            uiManager?.UpdateScore(0);
        }
    }

    private void Update()
    {
        //  MiniGame1: 타이머 기반 점수 증가
        if (currentSceneName == "MiniGame1Scene" && !isGameOver)
        {
            scoreTimer += Time.deltaTime;

            if (scoreTimer >= scoreInterval)
            {
                scoreTimer = 0f;
                currentScore++;
                uiManager?.UpdateScore(currentScore);
            }
        }

        //  MiniGame2: 스페이스바로 시작
        if (currentSceneName == "MiniGame2Scene" && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    // -------------------------------
    //  [MiniGame1 기능 영역] 
    // -------------------------------

    private int currentScore = 0;
    private float scoreTimer = 0f;
    public float scoreInterval = 1f;
    private bool isGameOver = false;

    private void InitMiniGame1()
    {
        currentScore = 0;
        scoreTimer = 0f;
        isGameOver = false;
    }

    public void AddScore(int score)
    {
        if (currentSceneName != "MiniGame1Scene") return;

        currentScore += score;
        uiManager?.UpdateScore(currentScore);
        Debug.Log("Score: " + currentScore);
    }

    // -------------------------------
    //  [MiniGame2 기능 영역] 
    // -------------------------------

    public PlayerController player { get; private set; }
    private ResourceController _playerResourceController;
    [SerializeField] private int currentWaveIndex = 0;
    private EnemyManager enemyManager;

    private void InitMiniGame2()
    {
        player = FindObjectOfType<PlayerController>();
        player?.Init(this);

        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager?.Init(this);

        currentWaveIndex = 0;
    }

    public void StartGame()
    {
        if (currentSceneName != "MiniGame2Scene") return;

        StartNextWave();
    }

    private void StartNextWave()
    {
        currentWaveIndex += 1;
        enemyManager?.StartWave(1 + currentWaveIndex / 5);
    }

    public void EndOfWave()
    {
        if (currentSceneName != "MiniGame2Scene") return;

        StartNextWave();
    }

    // -------------------------------
    // 💥 [공통 GameOver 처리] 💥
    // -------------------------------

    public void GameOver()
    {
        Debug.Log("Game Over");

        if (currentSceneName == "MiniGame1Scene")
        {
            isGameOver = true;
            uiManager?.ShowRestartPanel();
            PlayerPrefs.SetInt("LastMiniGameScore", currentScore);
            PlayerPrefs.Save();
        }
        else if (currentSceneName == "MiniGame2Scene")
        {
            enemyManager?.StopWave();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}