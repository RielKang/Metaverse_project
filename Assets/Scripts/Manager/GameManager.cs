using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // General
    private string currentSceneName;
    [SerializeField] private UIManager uiManager;
    public UIManager UIManager => uiManager;

    // MiniGame1
    private int currentScore = 0;
    private float scoreTimer = 0f;
    public float scoreInterval = 1f;
    private bool isGameOver = false;
    public bool IsGameOver => isGameOver;
    // MiniGame2
    public PlayerController player { get; private set; }
    private ResourceController _playerResourceController;
    [SerializeField] private int currentWaveIndex = 0;
    private EnemyManager enemyManager;
    public static bool isFirstLoading = true;

    private void Awake()
    {
        Instance = this;
        currentSceneName = SceneManager.GetActiveScene().name;

        uiManager = FindObjectOfType<UIManager>();

        if (currentSceneName == "MiniGame1Scene")
        {
            InitMiniGame1();
        }
        else if (currentSceneName == "MiniGame2Scene")
        {
            player = FindObjectOfType<PlayerController>();
            player?.Init(this);

            enemyManager = GetComponentInChildren<EnemyManager>();
            enemyManager?.Init(this);
        }
    }

    private void Start()
    {
        // UIManager 수동 연결 (우선순위 높게 위에 둠)
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();
            Debug.Log("UIManager 수동 연결 완료");
        }


        if (currentSceneName == "MiniGame1Scene")
        {
            uiManager?.UpdateScore(0);
        }
        else if (currentSceneName == "MiniGame2Scene")
        {
            if (!isFirstLoading)
                StartGame();
            else
                isFirstLoading = false;
        }
    }

    private void Update()
    {
        if (currentSceneName == "MiniGame1Scene" && !isGameOver)
        {
            //scoreTimer += Time.deltaTime;

            //if (scoreTimer >= scoreInterval)
            //{
            //    scoreTimer = 0f;
            //    currentScore++;
            //    uiManager?.UpdateScore(currentScore);
            //}

        }

        if (currentSceneName == "MiniGame2Scene" && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }


    // MiniGame1 기능

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


    // MiniGame2 기능
    public void StartGame()
    {
        //uiManager?.ShowRestartPanel();
        StartNextWave();
    }

    private void StartNextWave()
    {
        currentWaveIndex += 1;
        enemyManager?.StartWave(1 + currentWaveIndex / 5);
    }

    public void EndOfWave()
    {
        StartNextWave();
    }


    // 공통 GameOver 처리
    public void GameOver()
    {
        Debug.Log("Game Over");
        if (UIManager.Instance != null)
        {
            Debug.Log("UIManager.Instance 연결됨: " + UIManager.Instance.name);
            UIManager.Instance.ShowRestartPanel();
        }
        else
        {
            Debug.LogError("UIManager.Instance == null !!");
        }


        if (currentSceneName == "MiniGame1Scene")
        {
            Debug.Log("게임오버: MiniGame1Scene 진입");

            isGameOver = true;
            PlayerPrefs.SetInt("LastMiniGameScore", currentScore);
            PlayerPrefs.Save();
            
            UIManager.Instance?.ShowRestartPanel();

                        
        }
        else if (currentSceneName == "MiniGame2Scene")
        {
            enemyManager?.StopWave();
            UIManager.Instance.ShowRestartPanel();  // 있으면 표시 가능
        }
    }

    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentSceneName);
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainScene");
    }
}

