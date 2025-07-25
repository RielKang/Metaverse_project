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
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); ← 씬 간 유지하고 싶다면 추가
        }
        else
        {
            Destroy(gameObject); // 혹시 중복 생성 방지
        }

        restartPanel?.SetActive(false);
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