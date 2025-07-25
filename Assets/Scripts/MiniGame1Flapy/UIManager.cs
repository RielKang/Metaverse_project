using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TextMeshProUGUI scoreText;
    public GameObject restartPanel;
    public GameObject restartButton;
    public GameObject returnButton;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // 혹시 중복 생성 방지
        }

        restartPanel?.SetActive(false);
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; //  씬 로드 이벤트 등록
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; //  이벤트 제거 (메모리 누수 방지)
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        Canvas gameCanvas = GameObject.Find("Canvas")?.GetComponent<Canvas>();
        if (gameCanvas != null)
        {
            gameCanvas.gameObject.SetActive(true); //  캔버스 켜주기
        }
    }

    void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("scoreText is null");
            return;
        }

        if (restartPanel == null)
        {
            Debug.LogError("restartPanel is null");
            return;
        }

        if (restartButton == null)
            Debug.LogWarning("restartButton is not assigned");
        if (returnButton == null)
            Debug.LogWarning("returnButton is not assigned");

        restartPanel.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void ShowRestartPanel()
    {
        
        Debug.Log("ShowRestartPanel() 호출됨");
        if (restartPanel != null)
        {
            restartPanel.SetActive(true);
            Debug.Log("RestartPanel 활성화 완료");
        }
        else
        {
            Debug.LogWarning("restartPanel이 연결되지 않았습니다!");
        }


    }

   
    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainScene");
    }
}